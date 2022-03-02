using bnbAPI.DTO;
using bnbAPI.Service;

namespace bnbAPI.Logic
{
    public class UserLogic
    {
        UserService userService = new UserService();

        public bool RegisterUser(RegisterUserDTO registerUser)
        {
            return userService.RegisterUser(registerUser);
        }
    }
}
