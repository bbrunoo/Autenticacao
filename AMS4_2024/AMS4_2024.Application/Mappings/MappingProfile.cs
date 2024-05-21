using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMS4_2024.Application.DTOs;
using AutoMapper;
using AMS4_2024.Domain.Models;


namespace AMS4_2024.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, Usuario>();
        }
    }
}
