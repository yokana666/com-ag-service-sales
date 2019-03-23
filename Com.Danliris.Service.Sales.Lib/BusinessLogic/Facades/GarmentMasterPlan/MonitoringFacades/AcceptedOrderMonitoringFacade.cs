using Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface.GarmentMasterPlan.MonitoringInterfaces;
using Com.Danliris.Service.Sales.Lib.BusinessLogic.Logic.GarmentMasterPlan.MonitoringLogics;
using Com.Danliris.Service.Sales.Lib.Models.GarmentSewingBlockingPlanModel;
using Com.Danliris.Service.Sales.Lib.Services;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentMasterPlan.MonitoringViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Data;
using OfficeOpenXml;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Facades.GarmentMasterPlan.MonitoringFacades
{
    public class AcceptedOrderMonitoringFacade : IAcceptedOrderMonitoringFacade
    {
        private readonly SalesDbContext DbContext;
        private readonly DbSet<GarmentSewingBlockingPlanItem> DbSet;
        private IdentityService IdentityService;
        private AcceptedOrderMonitoringLogic AcceptedOrderMonitoringLogic;

        public AcceptedOrderMonitoringFacade(IServiceProvider serviceProvider, SalesDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentSewingBlockingPlanItem>();
            IdentityService = serviceProvider.GetService<IdentityService>();
            AcceptedOrderMonitoringLogic = serviceProvider.GetService<AcceptedOrderMonitoringLogic>();
        }
        public Tuple<MemoryStream, string> GenerateExcel(string filter = "{}")
        {
            Dictionary<string, string> FilterDictionary = new Dictionary<string, string>(JsonConvert.DeserializeObject<Dictionary<string, string>>(filter), StringComparer.OrdinalIgnoreCase);
            var Query = AcceptedOrderMonitoringLogic.GetQuery(filter);
            var year = short.Parse(FilterDictionary["year"]);
            var unitFilter = FilterDictionary.ContainsKey("unit") ? FilterDictionary["unit"] : "";
            var weeks = DbContext.GarmentWeeklyPlans.Include(a=>a.Items).Where(a=>a.Year==year);
            var data = Query.ToList();

            if (!string.IsNullOrWhiteSpace(unitFilter))
            {
                weeks = DbContext.GarmentWeeklyPlans.Include(a => a.Items).Where(a => a.Year == year && a.UnitCode== unitFilter);
            }
            DataTable result = new DataTable();

            List<object> rowValuesForEmptyData = new List<object>();
            Dictionary<string, List<UnitDataTable>> rowData = new Dictionary<string, List<UnitDataTable>>();
            Dictionary<string, string> column = new Dictionary<string, string>();

            result.Columns.Add(new DataColumn() { ColumnName = "Unit", DataType = typeof(string) });
            rowValuesForEmptyData.Add("");
            foreach (var d in weeks.OrderByDescending(a=>a.UnitCode))
            {
                string unitCol = d.UnitCode;
                if (!column.ContainsKey(d.UnitCode))
                {
                    column.Add(unitCol, unitCol);
                    result.Columns.Add(new DataColumn() { ColumnName = d.UnitCode, DataType = typeof(string) });
                }
                   

                rowValuesForEmptyData.Add(0);

                foreach (var item in weeks.FirstOrDefault().Items.OrderBy(a=>a.WeekNumber))
                {
                    string week = string.Concat("W", item.WeekNumber);

                    var dataQty = data.FirstOrDefault(a => a.Unit.Equals(d.UnitCode) && a.WeekNumber.Equals(item.WeekNumber));

                    UnitDataTable unitDataTable = new UnitDataTable
                    {
                        Unit = d.UnitCode,
                        qty= dataQty==null? "-" : dataQty.Quantity.ToString()
                    };

                    if (rowData.ContainsKey(week))
                    {
                        var rowValue = rowData[week];
                        var unit = rowValue.FirstOrDefault(a => a.Unit == d.UnitCode);
                        if (unit == null)
                        {
                            rowValue.Add(unitDataTable);
                        }
                    }
                    else
                    {
                        rowData.Add(week , new List<UnitDataTable> { unitDataTable });
                    }
                }

                var sumQty = data.Where(a => a.Unit == d.UnitCode);
                if (!rowData.ContainsKey("TOTAL"))
                {
                    UnitDataTable unitDataTable1 = new UnitDataTable
                    {
                        Unit = d.UnitCode,
                        qty = sumQty==null ? "-" : sumQty.Sum(a => a.Quantity).ToString() 
                    };
                    rowData.Add("TOTAL", new List<UnitDataTable> { unitDataTable1 });
                }
                else
                {
                    var sumData = rowData["TOTAL"];
                    UnitDataTable unitDataTable2 = new UnitDataTable
                    {
                        Unit = d.UnitCode,
                        qty = sumQty == null ? "-" : sumQty.Sum(a => a.Quantity).ToString()
                    };
                    sumData.Add(unitDataTable2);
                }
            }
            

            if (data.Count == 0)
            {
                result.Rows.Add(rowValuesForEmptyData.ToArray());
            }
            else
            {
                foreach (var rowValue in rowData)
                {
                    List<object> rowValues = new List<object>();
                    rowValues.Add(rowValue.Key);
                    foreach(var a in rowValue.Value)
                    {
                        rowValues.Add(a.qty);
                    }
                    result.Rows.Add(rowValues.ToArray());
                }

                
            }

            ExcelPackage package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Monitoring Order Diterima");
            sheet.Cells["A1"].LoadFromDataTable(result, true, OfficeOpenXml.Table.TableStyles.Light16);
            sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            MemoryStream streamExcel = new MemoryStream();
            package.SaveAs(streamExcel);

            string fileName = string.Concat("Monitoring Order Diterima dan Booking ", FilterDictionary["year"], ".xlsx");

            return Tuple.Create(streamExcel, fileName);
        }

        public Tuple<List<AcceptedOrderMonitoringViewModel>, int> Read(int page = 1, int size = 25, string filter = "{}")
        {
            var Query = AcceptedOrderMonitoringLogic.GetQuery(filter);
            var data = Query.ToList();
            return Tuple.Create(data, data.Count);
        }

        class UnitDataTable
        {
            public string Unit { get; set; }
            public string qty { get; set; }
        }
    }
}
