using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.FinishingPrintingCostCalculation;
using Com.Danliris.Service.Sales.Lib.ViewModels.FinishingPrintingCostCalculation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.FinishingPrintingCostCalculationProfiles
{
    public class FinishingPrintingCostCalculationMachineMapper : Profile
    {
        public FinishingPrintingCostCalculationMachineMapper()
        {
            CreateMap<FinishingPrintingCostCalculationMachineModel, FinishingPrintingCostCalculationMachineViewModel>()
                .ReverseMap();
        }
    }
}
