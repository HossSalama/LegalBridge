using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartLaywer.DTO.Hearing;

namespace smartLaywer.Mapping.HearingMapping
{
    public class HearingMapping : Profile
    {
        public HearingMapping()
        {
            // Hearing → HearingListDto
            CreateMap<Hearing, HearingListDto>()
                .ForMember(d => d.CaseNumber,
                    o => o.MapFrom(s => s.Case != null ? s.Case.CaseNumber : string.Empty))
                .ForMember(d => d.CaseTitle,
                    o => o.MapFrom(s => s.Case != null ? s.Case.Title : string.Empty))
                .ForMember(d => d.ClientName,
                    o => o.MapFrom(s => s.Case != null && s.Case.Client != null
                        ? s.Case.Client.FullName : string.Empty))
                .ForMember(d => d.CourtName,
                    o => o.MapFrom(s => s.Court != null ? s.Court.CourtName : string.Empty))
                .ForMember(d => d.DeptName,
                    o => o.MapFrom(s => s.Dept != null ? s.Dept.DeptName : string.Empty))
                .ForMember(d => d.HearingType,
                    o => o.MapFrom(s => GetHearingTypeLabel(s.HearingType)))
                .ForMember(d => d.Period,
                    o => o.MapFrom(s => GetPeriodLabel(s.Period)))
                .ForMember(d => d.AttendanceStatus,
                    o => o.MapFrom(s => GetAttendanceLabel(s.AttendanceStatus)))
                .ForMember(d => d.NextHearingPeriod,
                    o => o.MapFrom(s => s.NextHearingPeriod.HasValue
                        ? GetPeriodLabel(s.NextHearingPeriod.Value) : null));

            // HearingCreateDto → Hearing
            CreateMap<HearingCreateDto, Hearing>()
                .ForMember(d => d.HearingType,
                    o => o.MapFrom(s => (HearingTypeEnum)s.HearingType))
                .ForMember(d => d.Period,
                    o => o.MapFrom(s => (HearingPeriodEnum)s.Period))
                .ForMember(d => d.AttendanceStatus,
                    o => o.MapFrom(s => (AttendanceStatusEnum)s.AttendanceStatus))
                .ForMember(d => d.NextHearingPeriod,
                    o => o.MapFrom(s => s.NextHearingPeriod.HasValue
                        ? (HearingPeriodEnum?)s.NextHearingPeriod.Value : null))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.Now))
                .ForMember(d => d.CreatedBy, o => o.Ignore()); // يتحدد من الـ auth
        }

        private static string GetHearingTypeLabel(HearingTypeEnum t) => t switch
        {
            HearingTypeEnum.Hearing => "جلسة",
            HearingTypeEnum.Investigation => "تحقيق",
            HearingTypeEnum.Expert => "خبرة",
            HearingTypeEnum.Other => "أخرى",
            _ => t.ToString()
        };
        private static string GetPeriodLabel(HearingPeriodEnum p) => p switch
        {
            HearingPeriodEnum.Morning => "صباحي",
            HearingPeriodEnum.Evening => "مسائي",
            _ => p.ToString()
        };
        private static string GetAttendanceLabel(AttendanceStatusEnum a) => a switch
        {
            AttendanceStatusEnum.Incoming => "قادم",
            AttendanceStatusEnum.Absent => "غائب",
            AttendanceStatusEnum.Postponed => "مؤجل",
            _ => a.ToString()
        };
    }
}
