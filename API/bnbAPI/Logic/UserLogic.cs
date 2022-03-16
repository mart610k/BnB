using bnbAPI.CustomException;
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
        AuthService authService = new AuthService();
        UserTypeRightsService userrightsService = new UserTypeRightsService();

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
                    messageDTO = new MessageDTO(string.Format("The user \"{0}\" was registered", registerUser.Username), HttpStatusCodeService.GetHttpStatusIntFromEnum(HttpStatusEnum.OK));
                }
                catch(MySqlException myexep)
                {
                    System.Diagnostics.Debug.WriteLine(myexep.Message);
                    if (myexep.Message.StartsWith("Duplicate entry"))
                    {
                        messageDTO = new MessageDTO("The username is already in use", HttpStatusCodeService.GetHttpStatusIntFromEnum(HttpStatusEnum.PrimaryKeyFailed));
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

        public void RequestUserAsHost(string access_token, RequestHostDTO requestHostDTO)
        {
            try
            {
                string userID = authService.GetUserIDByAccessToken(access_token);

                if (userID != null && !userrightsService.GetIfUserIsHost(userID))
                {
                    if (!userService.UserHaveOutstandingHostRequest(userID))
                    {
                        userService.RequestUserAsHost(userID, requestHostDTO);

                    }
                    else
                    {
                        throw new OutstandingRequestPresentException();
                    }


                }
                else
                {
                    throw new UserNotAuthorizedForActionException();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void UpdatePassword(UpdatePassDTO updatePass)
        {
            try
            {
                userService.UpdateUserPassword(updatePass);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateEmail(UpdateEmailDTO updateEmail)
        {
            try
            {
                userService.UpdateEmail(updateEmail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
