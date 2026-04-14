using Microsoft.AspNetCore.Components.Authorization;
using smartLaywer.DTO.User;
using System.Security.Claims;

namespace smartLaywer.Services.ClassService
{
    public class HostAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private ClaimsPrincipal _currentUser;

        public HostAuthenticationStateProvider()
        {
            _currentUser = _anonymous;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }

        public void Login(User user)
        {
            var claims = new List<System.Security.Claims.Claim>
        {
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.FullName ),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email ),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Role.RoleName.ToString())
        };

            var identity = new ClaimsIdentity(claims, "CustomAuth");
            SecureStorage.Default.SetAsync("UserId", user.Id.ToString());

            _currentUser = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }
        public async Task<int> GetCurrentUserIdAsync()
        {
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;
            var claim = user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : 0;
        }

        public void Logout()
        {
            _currentUser = _anonymous;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }
    }
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<User> CreateUserByAdminAsync(RegisterDto dto)
        {
            bool isEmailExists = await _unitOfWork.Users.GetAllQueryableNoTracking().AnyAsync(u => u.Email == dto.Email);
            if (isEmailExists)
                throw new Exception("⁄–—«° Â–« «·»—Ìœ «·≈·ﬂ —Ê‰Ì „”Ã· ·„ÊŸ› ¬Œ— »«·›⁄·."); 

            bool isPhoneExists = await _unitOfWork.Users.GetAllQueryableNoTracking().AnyAsync(u => u.PhoneNumber == dto.PhoneNumber);
            if(isPhoneExists)
                throw new Exception("⁄–—«° Â–« —ﬁ„ «·Â« › „”Ã· ·„ÊŸ› ¬Œ— »«·›⁄·.");

            if (!string.IsNullOrEmpty(dto.SecondNumber))
            {
                bool isSecoundPhoneExists = await _unitOfWork.Users.GetAllQueryableNoTracking().AnyAsync(u => u.SecondNumber == dto.SecondNumber);
                if (isSecoundPhoneExists)
                    throw new Exception("⁄–—«° Â–« «·—ﬁ„ «·À«‰Ì „”Ã· ·„ÊŸ› ¬Œ— »«·›⁄·.");
            }

            if (!string.IsNullOrEmpty(dto.NationalId))
            {
                bool isNationalIdExists = await _unitOfWork.Users.GetAllQueryableNoTracking().AnyAsync(u => u.NationalId == dto.NationalId);
                if (isNationalIdExists)
                    throw new Exception("⁄–—«° Â–« «·—ﬁ„ «·ﬁÊ„Ì „”Ã· ·„ÊŸ› ¬Œ— »«·›⁄·.");
            }

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.RoleId = dto.RoleId;


            try
            {
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.CompleteAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("ÕœÀ Œÿ√ √À‰«¡ Õ›Ÿ «·»Ì«‰« : " + ex.Message);
            }
        }
        public async Task<User?> LoginAsync(LoginDto dto)
        {
            var user = await _unitOfWork.Users
                .GetAllQueryableTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !user.IsActive)
                throw new Exception("»Ì«‰«  «·œŒÊ· €Ì— ’ÕÌÕ… √Ê «·Õ”«» „⁄ÿ·.");


            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isPasswordValid)
                throw new Exception("»Ì«‰«   ………«·œŒÊ· €Ì— ’ÕÌÕ….");
           
            user.LastLoginAt = DateTime.Now;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();

            return user;
        }

        public async Task<IEnumerable<UserViewDTO>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users
                .GetAllQueryableNoTracking()
                .Where(u=>u.IsActive)
                .Include(u => u.Role)
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserViewDTO>>(users);
        }
        public async Task<UserDetailDTO> GetUserDetailsAsync(int id)
        {
            var user = await _unitOfWork.Users
                .GetAllQueryableNoTracking()
                .Include(u => u.Role)
                .Include(u => u.CaseAssignedLawyers) 
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new Exception("⁄–—«° ·„ Ì „ «·⁄ÀÊ— ⁄·Ï «·„ÊŸ›.");

            return _mapper.Map<UserDetailDTO>(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
                throw new Exception("«·„” Œœ„ €Ì— „ÊÃÊœ.");
            user.IsActive = false;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task LogoutAsync()
        {

            await Task.CompletedTask;
        }
    }
}




