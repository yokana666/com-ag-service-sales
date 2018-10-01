using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models;
using Com.Danliris.Service.Sales.Lib.ViewModels.CostCalculationGarment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles
{
    public class RateMapper : Profile
    {
        public RateMapper()
        {
            CreateMap<Rate, RateViewModel>()
              .ForPath(d => d.Code, opt => opt.MapFrom(s => s.Code))
              .ForPath(d => d.Name, opt => opt.MapFrom(s => s.Name))
              .ForPath(d => d.Value, opt => opt.MapFrom(s => s.Value)).ReverseMap();
        }
    }
}
