using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using smartLaywer.DTOs.Report;

namespace smartLaywer.Mapping.ReportMapping
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            // Mapping للموديل الأساسي للتقارير
            CreateMap<Report, ReportDto>()
                .ForMember(dest => dest.ReportTypeName,
                    opt => opt.MapFrom(src => GetReportTypeArabic(src.ReportType)))
                .ForMember(dest => dest.GeneratedByName,
                    opt => opt.MapFrom(src => src.GeneratedByNavigation != null
                        ? src.GeneratedByNavigation.FullName
                        : "النظام"));

            // Mapping لمكونات لوحة التحكم
            // لاحظ: استخدمت CaseType.TypeName إذا كان جدول منفصل 
            // أو TypeName.ToString() إذا كان Enum
            CreateMap<Case, CaseTypeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CaseType != null ? src.CaseType.TypeName.ToString() : "عام"));

            CreateMap<Case, CaseStatusDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Status != null ? src.Status.StatusName.ToString() : "غير محدد"));
        }

        private static string GetReportTypeArabic(ReportTypeEnum type) => type switch
        {
            ReportTypeEnum.Cases => "تقرير القضايا",
            ReportTypeEnum.Hearings => "تقرير الجلسات",
            // تم حذف Clients إذا لم يكن موجوداً في الـ Enum الخاص بك
            // أضف النوع الصحيح الموجود عندك في ReportTypeEnum
            _ => type.ToString()
        };
    }
}
