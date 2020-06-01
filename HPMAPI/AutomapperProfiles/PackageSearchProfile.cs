using AutoMapper;
using HPMAPI.Entities;
using System;

namespace HPMAPI.AutomapperProfiles
{
    public class PackageSearchProfile : Profile
    {
        public PackageSearchProfile()
        {
            CreateMap<Package, CognitiveSearchPackage>().ForMember(d => d.location, opt => opt.MapFrom(src => Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(src.location))));
            CreateMap<CognitiveSearchPackage, Package>().ForMember(d => d.location, opt => opt.MapFrom(src => System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(src.location))));
        }
    }
}
