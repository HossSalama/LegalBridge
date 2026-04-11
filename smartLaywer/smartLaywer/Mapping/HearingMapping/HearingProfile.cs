using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Mapping.HearingMapping
{
    internal class HearingProfile :Profile
    {
        public HearingProfile()
        {
            CreateMap<Hearing, HearingDisplayDto>()
                .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.Case.CaseNumber))
                .ForMember(dest => dest.CaseTitle, opt => opt.MapFrom(src => src.Case.Title))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Case.Client.FullName))
                .ForMember(dest => dest.CourtName, opt => opt.MapFrom(src => src.Court.CourtName))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Dept.DeptName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                src.AttendanceStatus == AttendanceStatusEnum.Incoming ? "ÞÇÏãÉ" :
                src.AttendanceStatus == AttendanceStatusEnum.Attended ? "ãßÊãáÉ" :
                src.AttendanceStatus == AttendanceStatusEnum.Absent ? "ãáÛíÉ" : "ÛíÑ ãÍÏÏ"))
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src =>
                src.AttendanceStatus == AttendanceStatusEnum.Attended ? src.Result : "-"));

            CreateMap<HearingCreationDto, Hearing>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src =>
                    string.IsNullOrWhiteSpace(src.Result) ? "Ýí ÇäÊÙÇÑ ÚÞÏ ÇáÌáÓÉ" : src.Result))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Case, opt => opt.Ignore())
                .ForMember(dest => dest.Court, opt => opt.Ignore())
                .ForMember(dest => dest.Dept, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByNavigation, opt => opt.Ignore());
            CreateMap<UpdateHearingResultDto, Hearing>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.AttendanceStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result))
                .ForMember(dest => dest.NextHearingDate, opt => opt.MapFrom(src => src.NextHearingDate))
                .ForMember(dest => dest.NextHearingPeriod, opt => opt.MapFrom(src => src.NextHearingPeriod));
 
            CreateMap<Hearing, Hearing>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.AttendanceStatus, opt => opt.MapFrom(src => AttendanceStatusEnum.Incoming))
                .ForMember(dest => dest.Result, opt => opt.MapFrom(src => "Ýí ÇäÊÙÇÑ ÇáÌáÓÉ ÇáÞÇÏãÉ"))
                .ForMember(dest => dest.Case, opt => opt.Ignore())
                .ForMember(dest => dest.Court, opt => opt.Ignore())
                .ForMember(dest => dest.Dept, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByNavigation, opt => opt.Ignore());
        }

        
    }
}
