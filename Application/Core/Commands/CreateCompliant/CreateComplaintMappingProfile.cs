using Application.Core.Commands.CreateCompliant;
using AutoMapper;
using Domain;

namespace Application.Core.Commands.CreateComplaint;

public class CreateComplaintMappingProfile : Profile
{
    //public CreateComplaintMappingProfile()
    //{
    //    CreateMap<CreateCompliantCommand, CitizinRequierment>()
    //        .ForMember(
    //            dest => dest.Status,
    //            opt => opt.MapFrom(src => ComplaintStatus.Pending))
    //        .ForMember(
    //            dest => dest.CreatedAt,
    //            opt => opt.MapFrom(src => DateTime.UtcNow))
    //        .ForMember(
    //            dest => dest.UpdatedAt,
    //            opt => opt.Ignore())
    //        .ForMember(
    //            dest => dest.Id,
    //            opt => opt.Ignore());
    //}
}