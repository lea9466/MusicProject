using AutoMapper;
using MusicDTO;
using MusicModels;

namespace Service.services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>()
            .ForMember(cat => cat.SongsCount,
                       opt => opt.MapFrom(src => src.Songs != null ? src.Songs.Count() : 0))
            .ReverseMap();
            CreateMap<Song, SongDto>();
            CreateMap<SongDto, Song>()
                .ForMember(dest => dest.Date, opt =>
                {
                    opt.Condition(src => src.Date != null);
                    opt.MapFrom(src => src.Date);
                });
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore()) // אבטחה: לא מחזירים סיסמה ב-DTO
                .ReverseMap();
            CreateMap<Chord, ChordDto>().ReverseMap();
            CreateMap<WordLine, WordLineDto>().ReverseMap();
            CreateMap<SongRequestVote, SongRequestVoteDto>().ReverseMap();
            CreateMap<SongRequest, SongRequestDto>().ReverseMap();
            CreateMap<SongRequestDto, SongRequest>()
             .ForMember(dest => dest.User, opt => opt.Ignore())
                 .ForMember(dest => dest.Fulfiller, opt => opt.Ignore());

            CreateMap<SongRequest, SongRequestDto>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.User.Name))
                    .ForMember(dest => dest.FulfillerName, opt => opt.MapFrom(src => src.Fulfiller.Name))
                        .ReverseMap();
            CreateMap<Song, SongDto>()
            // ממפה את שם המשתמש מתוך האובייקט User
            .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : string.Empty));

        }
    }
}
