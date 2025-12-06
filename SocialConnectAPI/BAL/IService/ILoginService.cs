using MODEL.DTO;
using SocialConnectAPI.MODEL.DTO;

namespace BAL.IService
{
    public interface ILoginService
    {
        Task<RegisterResultDTO> Register(RegisterDTO inputUser);
        Task<LoginResultDTO> UserLogin(LoginRequestDTO inputUser);
        Task<LogoutResultDTO> UserLogout(string token);
    }
}
