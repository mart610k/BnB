using bnbAPI.DTO;
using bnbAPI.Enum;
using bnbAPI.Service;
using bnbAPI.Static;
using MySql.Data.MySqlClient;
using System;

namespace bnbAPI.Logic
{
    public class UserLogic
    {
        UserService userService = new UserService();

        /// <summary>
        /// Responsible for calling the service and decoding the potential error coming from the servixe.
        /// </summary>
        /// <param name="registerUser">the user to register</param>
        /// <returns>message which contains if the user was reigstered</returns>
        public MessageDTO RegisterUser(RegisterUserDTO registerUser)
        {
            MessageDTO messageDTO;


            if (RegexHelper.CheckEmailAddressValidFormat(registerUser.Email))
            {
                try
                {
                    userService.RegisterUser(registerUser);
                    messageDTO = new MessageDTO(string.Format("The user \"{0}\" was registered", registerUser.Email), HttpStatusCodeService.GetHttpStatusIntFromEnum(HttpStatusEnum.OK));
                }
                catch(MySqlException myexep)
                {
                    System.Diagnostics.Debug.WriteLine(myexep.Message);
                    if (myexep.Message.StartsWith("Duplicate entry"))
                    {
                        messageDTO = new MessageDTO("The email is already in use", HttpStatusCodeService.GetHttpStatusIntFromEnum(HttpStatusEnum.PrimaryKeyFailed));
                    }
                    else
                    {
                        messageDTO = HttpStatusCodeService.CreateUnSpecifedError(myexep);
                    }
                }

                catch (Exception e)
                {
                    messageDTO = HttpStatusCodeService.CreateUnSpecifedError(e);
                }
            }
            else
            {
                messageDTO = new MessageDTO("Email address did not fulfill email format", HttpStatusCodeService.GetHttpStatusIntFromEnum(HttpStatusEnum.ClientFormatError));
            }

            return messageDTO;
        }
    }
}
