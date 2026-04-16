using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartLaywer.Models;
using smartLaywer.DTO.HearingDetails;

namespace smartLaywer.Mapping.HearingDetailsMapping
{
    public class HearingDetailsProfile : Profile
    {
        public HearingDetailsProfile()
        {

            CreateMap<Case, smartLaywer.DTO.HearingDetails.HearingDetailsDto>()
                .ForMember(d => d.Court, o => o.MapFrom(s => s.Court.CourtName))
                .ForMember(d => d.Department, o => o.MapFrom(s => s.Dept != null ? s.Dept.DeptName : ""))
                .ForMember(d => d.ClientName, o => o.MapFrom(s => s.Client.FullName))
                .ForMember(d => d.Hearings, o => o.MapFrom(s => s.Hearings));

            CreateMap<Hearing, smartLaywer.DTO.HearingDetails.HearingItemDto>()
                .ForMember(d => d.HearingType, o => o.MapFrom(s => s.HearingType.ToString()))
                .ForMember(d => d.Period, o => o.MapFrom(s => s.Period.ToString()))
                .ForMember(d => d.AttendanceStatus, o => o.MapFrom(s => s.AttendanceStatus.ToString()))
                .ForMember(d => d.CreatedBy, o => o.MapFrom(s => s.CreatedByNavigation.FullName))
                .ForMember(d => d.NextHearingPeriod, o => o.MapFrom(s => s.NextHearingPeriod.HasValue ? s.NextHearingPeriod.ToString() : null));
        }
    }
}
