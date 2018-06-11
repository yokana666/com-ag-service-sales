using AutoMapper;
using Com.Danliris.Service.Sales.Lib.Models.Weaving;
using Com.Danliris.Service.Sales.Lib.ViewModels.Weaving;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.AutoMapperProfiles.WeavingProfiles
{
    public class WeavingSalesContract : Profile
    {
        public WeavingSalesContract()
        {
            CreateMap<WeavingSalesContractModel, WeavingSalesContractViewModel>()
                .ForPath(d => d.Buyer.Id, opt => opt.MapFrom(s => s.BuyerId))
                .ForPath(d => d.Buyer.Code, opt => opt.MapFrom(s => s.BuyerCode))
                .ForPath(d => d.Buyer.Name, opt => opt.MapFrom(s => s.BuyerName))
                .ForPath(d => d.Buyer.Type, opt => opt.MapFrom(s => s.BuyerType))

                .ForPath(d => d.Product.Id, opt => opt.MapFrom(s => s.ProductId))
                .ForPath(d => d.Product.Code, opt => opt.MapFrom(s => s.ProductCode))
                .ForPath(d => d.Product.Name, opt => opt.MapFrom(s => s.ProductName))
                .ForPath(d => d.Product.Price, opt => opt.MapFrom(s => s.ProductPrice))
                .ForPath(d => d.Product.Tags, opt => opt.MapFrom(s => s.ProductTags))

                .ForPath(d => d.Uom.Id, opt => opt.MapFrom(s => s.UomId))
                .ForPath(d => d.Uom.Unit, opt => opt.MapFrom(s => s.UomUnit))

                .ForPath(d => d.MaterialConstruction.Id, opt => opt.MapFrom(s => s.MaterialConstructionId))
                .ForPath(d => d.MaterialConstruction.Code, opt => opt.MapFrom(s => s.MaterialConstructionCode))
                .ForPath(d => d.MaterialConstruction.Name, opt => opt.MapFrom(s => s.MaterialConstructionName))
                .ForPath(d => d.MaterialConstruction.Remark, opt => opt.MapFrom(s => s.MaterialConstructionRemark))

                .ForPath(d => d.YarnMaterial.Id, opt => opt.MapFrom(s => s.YarnMaterialId))
                .ForPath(d => d.YarnMaterial.Code, opt => opt.MapFrom(s => s.YarnMaterialCode))
                .ForPath(d => d.YarnMaterial.Name, opt => opt.MapFrom(s => s.YarnMaterialName))
                .ForPath(d => d.YarnMaterial.Remark, opt => opt.MapFrom(s => s.YarnMaterialRemark))

                .ForPath(d => d.YarnMaterial.Id, opt => opt.MapFrom(s => s.YarnMaterialId))
                .ForPath(d => d.YarnMaterial.Code, opt => opt.MapFrom(s => s.YarnMaterialCode))
                .ForPath(d => d.YarnMaterial.Name, opt => opt.MapFrom(s => s.YarnMaterialName))
                .ForPath(d => d.YarnMaterial.Remark, opt => opt.MapFrom(s => s.YarnMaterialRemark))

                .ReverseMap();
        }
    }
}
