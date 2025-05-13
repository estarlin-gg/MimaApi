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
    public class CategoryMapper:Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryDto>() 
                .ReverseMap() 
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        }
    }
}
