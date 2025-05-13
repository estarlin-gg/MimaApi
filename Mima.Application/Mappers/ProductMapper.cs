using AutoMapper;
using Mima.Application.Dtos;
using Mima.Domain.Model;

namespace Mima.Application.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) 
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock > 0 ? src.Stock : 0)); 

            CreateMap<Product, ProductDto>();
        }
    }
}
