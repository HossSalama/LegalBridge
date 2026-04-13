using smartLaywer.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services.InterfaceService
{
    public interface IAuthService
    {
        Task<User> CreateUserByAdminAsync(RegisterDto dto);
        Task<User?> LoginAsync(LoginDto dto);
        Task<IEnumerable<UserViewDTO>> GetAllUsersAsync();
        Task<UserDetailDTO> GetUserDetailsAsync(int id);
        Task DeleteUserAsync(int id);
        Task LogoutAsync();

    }
}
