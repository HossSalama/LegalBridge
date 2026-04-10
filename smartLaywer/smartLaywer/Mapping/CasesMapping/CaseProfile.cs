namespace smartLaywer.Mapping.CaseMapping
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            CreateMap<Case, CaseSummaryDto>()
                .ForMember(dest => dest.ClientName,
                    opt => opt.MapFrom(src => src.Client.FullName))
                .ForMember(dest => dest.CaseType,
                    opt => opt.MapFrom(src => src.CaseType.TypeName.GetDisplayName()))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.StatusName.GetDisplayName()))
                .ForMember(dest => dest.StatusColor,
                    opt => opt.MapFrom(src => src.Status.Color))
                .ForMember(dest => dest.NextHearingDate,
                    opt => opt.MapFrom(src =>
                        src.Hearings
                           .Where(h => h.NextHearingDate > DateTime.Now)
                           .OrderBy(h => h.NextHearingDate)
                           .Select(h => (DateTime?)h.NextHearingDate)
                           .FirstOrDefault()));
        }
    }
}