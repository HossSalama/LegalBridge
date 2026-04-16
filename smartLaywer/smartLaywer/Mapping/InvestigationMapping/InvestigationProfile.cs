using smartLaywer.DTO.Investigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Mapping.InvestigationMapping
{
    public class InvestigationProfile : Profile
    {
        public InvestigationProfile()
        {
            CreateMap<Case, InvestigationDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FullName))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => MapType(src.CaseType.TypeName)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => MapStatus(src.Status.StatusName)))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.OpenDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Prosecutor, opt => opt.MapFrom(src => src.AssignedLawyer.FullName))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Court.CourtName))

                // Sessions
                .ForMember(dest => dest.Sessions, opt => opt.MapFrom(src => src.Hearings))

                // Evidence
                .ForMember(dest => dest.Evidence, opt => opt.MapFrom(src => src.Documents))

                // Witness (manual empty)
                .ForMember(dest => dest.Witnesses, opt => opt.MapFrom(src => new List<WitnessDto>()));



            // 🔥 Hearing → Session
            CreateMap<Hearing, SessionDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.HearingDateTime.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.HearingDateTime.ToString("hh:mm tt")))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Result))
                .ForMember(dest => dest.Decisions, opt => opt.MapFrom(src => src.Result ?? "-"))
                .ForMember(dest => dest.Attendees, opt => opt.MapFrom(src =>
                    new List<string>() // هنا ممكن تزود logic بعدين
                ));



            // 🔥 Document → Evidence
            CreateMap<Document, EvidenceDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => MapDocumentType(src.DocType)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Notes) ? src.Title : src.Notes))
                .ForMember(dest => dest.DateCollected, opt => opt.MapFrom(src => src.UploadedAt.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    src.IsArchived ? "rejected" : "approved"));
        }

        // 🔥 Helpers
        private string MapStatus(CaseStatusEnum s) => s switch
        {
            CaseStatusEnum.Open => "ongoing",
            CaseStatusEnum.Closed => "completed",
            _ => "suspended"
        };

        private string MapType(CaseTypeEnum t) => t switch
        {
            CaseTypeEnum.Criminal => "criminal",
            CaseTypeEnum.Civil => "civil",
            _ => "administrative"
        };

        private string MapDocumentType(DocumentTypeEnum type) => type switch
        {
            DocumentTypeEnum.Report => "تقرير",
            DocumentTypeEnum.Image => "صور",
            DocumentTypeEnum.Contract => "عقد",
            _ => "مستند"
        };
    }
}
