using SocialConnectAPI.MODEL.DTO;

namespace SocialConnectAPI.BAL.IService
{
    public interface IProfileService
    {
        Task<UserProfileDTO> GetProfileAsync( int userId);
    }

}
