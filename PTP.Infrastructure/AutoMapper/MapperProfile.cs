using AutoMapper;
using PTP.Core.Dtos;
using PTP.Core.Entitys;
using Security.Core.Dtos;
using Security.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTP.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ClientDto, Cleint>();
            CreateMap<CreateUserDto, Users>();
                
        }

    }

}   

