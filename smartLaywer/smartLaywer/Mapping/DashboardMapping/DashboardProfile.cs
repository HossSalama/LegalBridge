using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using smartLaywer.DTO.Dashboard;

public class DashboardProfile : Profile
{
    public DashboardProfile()
    {
        CreateMap<Hearing, UpcomingHearingDto>()
            .ForMember(dest => dest.CaseNumber,
                opt => opt.MapFrom(src => src.Case != null ? src.Case.CaseNumber : ""))
            .ForMember(dest => dest.ClientName,
                opt => opt.MapFrom(src => src.Case != null && src.Case.Client != null
                    ? src.Case.Client.FullName : ""))
            .ForMember(dest => dest.CourtName,
                opt => opt.MapFrom(src => src.Court != null ? src.Court.CourtName : ""))
            // ✅ إضافة تجاهل صريح لكل الحقول التي تسبب المشكلة
            .ForMember(dest => dest.Period, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore());

        // إذا كان هناك حقل باسم NextHearingPeriod في الـ DTO أضف هذا السطر أيضاً:
        // .ForMember(dest => dest.NextHearingPeriod, opt => opt.Ignore());
    }
}
