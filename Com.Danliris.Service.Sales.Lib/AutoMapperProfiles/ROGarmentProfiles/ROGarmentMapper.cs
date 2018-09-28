using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.ROGarments;
using Com.Danliris.Service.Sales.Lib.ViewModels.GarmentROViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.ROGarmentProfiles
{
    public class ROGarmentMapper : Profile
	{
		public ROGarmentMapper()
        {
            CreateMap<RO_Garment, RO_GarmentViewModel>()
              .ForPath(d => d.CostCalculationGarment.Id, opt => opt.MapFrom(s => s.CostCalculationGarmentId))
              .ReverseMap();
        }
    }
}
