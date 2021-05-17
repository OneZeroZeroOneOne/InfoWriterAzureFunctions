using AutoMapper;
using InfoWriterAzureFunction.Database.Models;
using InfoWriterAzureFunction.Database.Models.Out;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfoWriterAzureFunction
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<StatusStorage, OutDeviceStatus>()
                .ForMember(x => x.ComputerName, x => x.MapFrom(y => y.Device.ComputerName))
                .ForMember(x => x.OSName, x => x.MapFrom(y => y.Device.Osname))
                .ForMember(x => x.Status, x => x.MapFrom(y => y.Status.Tittle))
                .ForMember(x => x.StatusId, x => x.MapFrom(y => y.StatusId));
        }
    }
}
