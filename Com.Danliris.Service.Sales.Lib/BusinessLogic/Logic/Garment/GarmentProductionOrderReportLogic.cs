using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Sales.Lib.ViewModels.Garment;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.Garment
{
    public class GarmentProductionOrderReportLogic : BaseMonitoringLogic<GarmentProductionOrderReportViewModel>
    {
        private SalesDbContext dbContext;
        private IIdentityService identityService;

        public GarmentProductionOrderReportLogic(SalesDbContext dbContext, IIdentityService identityService)
        {
            this.dbContext = dbContext;
            this.identityService = identityService;
        }

        public override IQueryable<GarmentProductionOrderReportViewModel> GetQuery(string filterString)
        {
            Filter filter = JsonConvert.DeserializeObject<Filter>(filterString);

            IQueryable<CostCalculationGarment> Query = dbContext.CostCalculationGarments;

            if (filter.year > 0 && filter.month > 0)
            {
                DateTimeOffset min = new DateTimeOffset(filter.year, filter.month, 1, 0, 0, 0, TimeSpan.FromHours(identityService.TimezoneOffset));
                DateTimeOffset max = new DateTimeOffset(filter.month < 12 ? filter.year : filter.year + 1, filter.month % 12 + 1, 1, 0, 0, 0, TimeSpan.FromHours(identityService.TimezoneOffset));
                Query = Query.Where(w => w.DeliveryDate >= min && w.DeliveryDate < max);
            }
            else
            {
                throw new Exception("Invalid Year or Month");
            }
            if (!string.IsNullOrWhiteSpace(filter.unit))
            {
                Query = Query.Where(w => w.UnitCode == filter.unit);
            }
            if (!string.IsNullOrWhiteSpace(filter.section))
            {
                Query = Query.Where(w => w.Section == filter.section);
            }
            if (!string.IsNullOrWhiteSpace(filter.buyer))
            {
                Query = Query.Where(w => w.BuyerBrandCode == filter.buyer);
            }

            var costCalculations = Query.Select(s => new CostCalculationGarment
            {
                DeliveryDate = s.DeliveryDate,
                BuyerBrandName = s.BuyerBrandName,
                Section = s.Section,
                Commodity = s.Commodity,
                Article = s.Article,
                RO_Number = s.RO_Number,
                CreatedUtc = s.CreatedUtc,
                Quantity = s.Quantity,
                UOMUnit = s.UOMUnit,
                ConfirmPrice = s.ConfirmPrice,
                ConfirmDate = s.ConfirmDate,
                IsApprovedPPIC = s.IsApprovedPPIC
            });

            var garmentProductionOrders = costCalculations.ToList()
                .GroupBy(cc => $"W - {Math.Ceiling((decimal)cc.DeliveryDate.DayOfYear / 7)}")
                .Select(groupWeek => new GarmentProductionOrderReportViewModel
                {
                    Week = groupWeek.Key,
                    Buyers = groupWeek
                        .GroupBy(week => week.BuyerBrandName)
                        .Select(groupBuyer => new GarmentProductionOrderReportBuyerViewModel
                        {
                            Buyer = groupBuyer.Key,
                            Details = groupBuyer
                                .Select(detail => GetGarmentProductionOrder(detail))
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();

            garmentProductionOrders.ForEach(po =>
            {
                po.Buyers.ForEach(buyer =>
                {
                    buyer.Quantities = buyer.Details.Sum(s => s.Quantity);
                    buyer.Amounts = buyer.Details.Sum(s => s.Quantity * s.ConfirmPrice);
                });
            });

            return garmentProductionOrders.AsQueryable();
        }

        GarmentProductionOrderReportDetailViewModel GetGarmentProductionOrder(CostCalculationGarment cc)
        {
            return new GarmentProductionOrderReportDetailViewModel
            {
                Section = cc.Section,
                Commodity = cc.Commodity,
                Article = cc.Article,
                RONo = cc.RO_Number,
                Date = cc.CreatedUtc.AddHours(identityService.TimezoneOffset),
                Quantity = cc.Quantity,
                Uom = cc.UOMUnit,
                ConfirmPrice = cc.ConfirmPrice,
                Amount = cc.Quantity * cc.ConfirmPrice,
                ConfirmDate = cc.ConfirmDate.ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).DateTime,
                ShipmentDate = cc.DeliveryDate.ToOffset(TimeSpan.FromHours(identityService.TimezoneOffset)).DateTime,
                ValidationPPIC = cc.IsApprovedPPIC ? "SUDAH" : "BELUM"
            };
        }

        private class Filter
        {
            public int year { get; set; }
            public int month { get; set; }
            public string unit { get; set; }
            public string section { get; set; }
            public string buyer { get; set; }
        }
    }
}
