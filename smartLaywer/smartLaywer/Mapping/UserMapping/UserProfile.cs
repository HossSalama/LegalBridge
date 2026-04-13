using smartLaywer.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Mapping.UserMapping
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterDto, User>()
              .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
              .ForMember(dest => dest.Role, opt => opt.Ignore())
              .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
              .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleId));
            CreateMap<User, UserViewDTO>()
                .ForMember(dest => dest.RoleName,opt => opt.MapFrom(src => src.Role.RoleName.ToString()));

            CreateMap<User, UserDetailDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName.ToString()))
                .ForMember(dest => dest.AssignedCases, opt => opt.MapFrom(src => src.CaseAssignedLawyers));

            CreateMap<Case, CaseSummaryDTO>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FullName))
                .ForMember(dest => dest.CaseType, opt => opt.MapFrom(src => src.CaseType.TypeName.ToString()));
        }
    }
}
