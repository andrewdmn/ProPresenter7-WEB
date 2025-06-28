using AutoMapper;
using ProPresenter7WEB.Core;
using ProPresenter7WEB.Service.Contracts;

namespace ProPresenter7WEB.Service
{
    public class ServiceMapperProfile : Profile
    {
        public ServiceMapperProfile()
        {
            CreateMap<VersionInfo, ProPresenterInfo>();

            CreateMap<Contracts.Playlist, Core.Playlist>()
                .ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.Id.Uuid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Id.Name));

            CreateMap<PlaylistDetailsItem, PresentationItem>()
                .ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.Id.Uuid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Id.Name));
            
            CreateMap<Contracts.PlaylistDetails, Core.PlaylistDetails>()
                .ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.Id.Uuid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Id.Name))
                .ForMember(dest => dest.Presentations, opt => opt.MapFrom(src => 
                    src.Items.Where(item => item.Type == PlaylistDetailsItemTypeEnum.Presentation)));

            CreateMap<Contracts.Presentation, Core.Presentation>()
                .ForMember(dest => dest.Uuid, opt => opt.MapFrom(src => src.Id.Uuid))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Id.Name))
                .ForMember(dest => dest.SlideCount, opt => opt.MapFrom(src => 
                    src.Groups.SelectMany(gr => gr.Slides).Count()));

            CreateMap<Contracts.PresentationIndex, Core.ActiveSlideIndex>()
                .ForMember(dest => dest.SlideIndex, opt => opt.MapFrom(src => src.Index))
                .ForMember(dest => dest.PresentationUuid, opt => opt.MapFrom(src => src.PresentationId.Uuid));
        }
    }
}
