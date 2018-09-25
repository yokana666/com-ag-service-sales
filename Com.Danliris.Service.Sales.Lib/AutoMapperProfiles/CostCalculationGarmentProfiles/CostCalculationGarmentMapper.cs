using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Helpers;
using Com.Danliris.Service.Sales.Lib.Models.CostCalculationGarments;
using Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.CostCalculationGarmentProfiles
{
	public class CostCalculationGarmentMapper : Profile
	{
		public CostCalculationGarmentMapper()
		{
			CreateMap<CostCalculationGarment, CostCalculationGarmentViewModel>()
			  .ForPath(d => d.Unit.Id, opt => opt.MapFrom(s => s.UnitId))
			  .ForPath(d => d.Unit.Code, opt => opt.MapFrom(s => s.UnitCode))
			  .ForPath(d => d.Unit.Name, opt => opt.MapFrom(s => s.UnitName))

			  .ForPath(d => d.FabricAllowance, opt => opt.MapFrom(s => s.FabricAllowance))
			  .ForPath(d => d.AccessoriesAllowance, opt => opt.MapFrom(s => s.AccessoriesAllowance))
			  .ForPath(d => d.SizeRange, opt => opt.MapFrom(s => s.SizeRange))
			  .ForPath(d => d.Buyer.Id, opt => opt.MapFrom(s => s.BuyerId))
			  .ForPath(d => d.Buyer.Code, opt => opt.MapFrom(s => s.BuyerCode))
			  .ForPath(d => d.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
			  .ForPath(d => d.Efficiency.Id, opt => opt.MapFrom(s => s.EfficiencyId))
			  .ForPath(d => d.Efficiency.Value, opt => opt.MapFrom(s => Percentage.ToPercent(s.EfficiencyValue)))
			  .ForPath(d => d.UOM.Id, opt => opt.MapFrom(s => s.UOMID))
			  .ForPath(d => d.UOM.code, opt => opt.MapFrom(s => s.UOMCode))
			  .ForPath(d => d.UOM.Unit, opt => opt.MapFrom(s => s.UOMUnit))
			  .ForPath(d => d.Wage.Id, opt => opt.MapFrom(s => s.WageId))
			  .ForPath(d => d.Wage.Value, opt => opt.MapFrom(s => s.WageRate ))
			  .ForPath(d => d.Comodity.Id, opt => opt.MapFrom(s => s.ComodityID))
			  .ForPath(d => d.Comodity.Code, opt => opt.MapFrom(s => s.ComodityCode))
			  .ForPath(d => d.Comodity.Name, opt => opt.MapFrom(s => s.Commodity))
			  .ForPath(d => d.CommodityDescription, opt => opt.MapFrom(s => s.CommodityDescription))
			  .ForPath(d => d.THR.Id, opt => opt.MapFrom(s => s.THRId))
			  .ForPath(d => d.THR.Value, opt => opt.MapFrom(s => s.THRRate))
			  .ForPath(d => d.Rate.Id, opt => opt.MapFrom(s => s.RateId))
			  .ForPath(d => d.Rate.Value, opt => opt.MapFrom(s => s.RateValue))
			  .ForPath(d => d.UOM.Id, opt => opt.MapFrom(s => s.UOMID))
			  .ForPath(d => d.UOM.code, opt => opt.MapFrom(s => s.UOMCode))
			  .ForPath(d => d.UOM.Unit, opt => opt.MapFrom(s => s.UOMUnit))


			  .ForPath(d => d.CommissionPortion, opt => opt.MapFrom(s => Percentage.ToFraction(s.CommissionPortion)))
			  .ForPath(d => d.Risk, opt => opt.MapFrom(s => Percentage.ToFraction(s.Risk)))
			  .ForPath(d => d.OTL1.Id, opt => opt.MapFrom(s => s.OTL1Id))
			  .ForPath(d => d.OTL1.Value, opt => opt.MapFrom(s => s.OTL1Rate))
			  .ForPath(d => d.OTL1.CalculatedValue, opt => opt.MapFrom(s => s.OTL1CalculatedRate))

			  .ForPath(d => d.OTL2.Id, opt => opt.MapFrom(s => s.OTL2Id))
			  .ForPath(d => d.OTL2.Value, opt => opt.MapFrom(s => s.OTL2Rate))
			  .ForPath(d => d.OTL2.CalculatedValue, opt => opt.MapFrom(s => s.OTL2CalculatedRate))
			  .ForPath(d => d.NETFOBP, opt => opt.MapFrom(s => Percentage.ToFraction(s.NETFOBP))) 
			  .ReverseMap();
		}
	}
}
