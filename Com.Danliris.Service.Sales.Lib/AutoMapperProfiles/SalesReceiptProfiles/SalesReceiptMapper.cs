using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.SalesReceipt;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesReceipt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.SalesReceiptProfiles
{
    public class SalesReceiptMapper : Profile
    {
        public SalesReceiptMapper()
        {
            CreateMap<SalesReceiptModel, SalesReceiptViewModel>()
                .ReverseMap();
        }
    }
}
