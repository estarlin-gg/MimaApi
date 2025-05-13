using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mima.Application.Dtos;
using Mima.Domain.Model;

namespace Mima.Application.Mappers
{
    public class SaleMapper:Profile
    {
        public SaleMapper()
        {
            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.SalesProducts, opt => opt.MapFrom(src => src.SalesProducts))
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate));

            CreateMap<SaleProduct, SaleProductDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ReverseMap();

            CreateMap<SaleDto, Sale>()
                .ForMember(dest => dest.SalesProducts, opt => opt.Ignore());
        }
    }
}
