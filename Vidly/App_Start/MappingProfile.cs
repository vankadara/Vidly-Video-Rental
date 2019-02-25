using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Migrations;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Map the customer model properties with Dto properties by using names
            Mapper.CreateMap<Customer, CustomerDto>();
            
            //Map the customerDto properties with customer model properties by using names
            Mapper.CreateMap<CustomerDto, Customer>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Genres,GenreDto>();
            Mapper.CreateMap<Movie,MovieDto>();

            Mapper.CreateMap<MovieDto, Movie>();

            //Ignore the ID exception
            Mapper.CreateMap<MovieDto, Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}