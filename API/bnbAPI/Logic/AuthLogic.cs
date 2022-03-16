using bnbAPI.DTO;
using bnbAPI.Service;
using bnbAPI.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace bnbAPI.Logic
{

    public class AuthLogic
    {
        UserService userService = new UserService();
        PasswordHashService passwordHashService = new PasswordHashService();
        AuthService authService = new AuthService();


        public AccessTokenDTO getAccessToken(AccessTokenAuthorizationDTO accessTokenAuthorizationDTO)
        {
            if (Config.ClientKey == accessTokenAuthorizationDTO.ClientKey && Config.ClientSecret == accessTokenAuthorizationDTO.ClientSecret)
            {
                UserCredentialsDTO credentials = userService.GetUserCredentials(accessTokenAuthorizationDTO.Username);

                if (credentials == null)
                {
                    throw new Exception();
                }

                if (passwordHashService.Verify(accessTokenAuthorizationDTO.Password, credentials.PasswordHashed))
                {
                    try
                    {
                        AccessTokenDTO token = authService.GetAccessTokenByUserID(accessTokenAuthorizationDTO.Username);
                        if(token.Expires_in < 200)
                        {
                            authService.UpdateAccessTokenForUser(accessTokenAuthorizationDTO.Username);
                        }

                    }
                    catch (Exception e)
                    {
                        authService.RegisterAccessToken(accessTokenAuthorizationDTO.Username);

                    }
                    AccessTokenDTO accessToken = authService.GetAccessTokenByUserID(accessTokenAuthorizationDTO.Username);

                    return accessToken;
                }
                else
                {
                    throw new CustomException.FailedLoginException();
                }

            }
            else
            {
                throw new Exception();
            }

            throw new Exception();
        
        }

        public AccessTokenDTO UpdateAccessToken(UpdateAccessTokenDTO updateAccessTokenDTO)
        {
            if (Config.ClientKey == updateAccessTokenDTO.ClientKey && Config.ClientSecret == updateAccessTokenDTO.ClientSecret)
            {
                string userID = authService.GetUserIDByRefreshToken(updateAccessTokenDTO.RefreshToken);


                authService.UpdateAccessTokenForUser(userID);

                return authService.GetAccessTokenByUserID(userID);

            }

            throw new Exception();
        }

        public bool LogoutUser(string accessToken)
        {
            try
            {
                string accessTokenDTO = authService.GetUserIDByAccessToken(accessToken);

                authService.DeleteAccessTokenEntryByUserID(accessTokenDTO);
                return true;
            }
        catch(Exception e)
            {
                return false;
            }
        }
    }
}
