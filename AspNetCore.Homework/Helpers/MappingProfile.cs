using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Homework.Models;
using AutoMapper;
using Northwind.DAL.Models;

namespace AspNetCore.Homework.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Categories, CategoryViewModel>()
                .ForMember(c => c.Id, opt => opt.MapFrom(c => c.CategoryId))
                .ForMember(c => c.Name, opt => opt.MapFrom(c => c.CategoryName))
                .ReverseMap();
            CreateMap<Products, ProductViewModel>()
                .ReverseMap();
        }
    }
}
