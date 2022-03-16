using bnbAPI.CostumException;
using bnbAPI.DTO;
using bnbAPI.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.Service
{
    public class HttpStatusCodeService
    {
        public static int GetHttpStatusIntFromEnum(HttpStatusEnum httpStatusEnum)
        {
            switch (httpStatusEnum)
            {
                case HttpStatusEnum.OK:
                    return 200; //OK response
                case HttpStatusEnum.ClientFormatError:
                    return 400; //Bad Request
                case HttpStatusEnum.ForbiddenAction:
                    return 403;
                case HttpStatusEnum.PrimaryKeyFailed:
                    return 409; // Conflict response
                default:
                    return 418; // IM a TeaCup Tasked with brewing Coffee.... This should only be used when developing. All of the above should have their own Status codes
                case HttpStatusEnum.UnspecifiedError:
                    return 500;
                    
            }
        }

        public static MessageDTO GetMessageDTOFromException(Exception e)
        {
            MessageDTO toReturn;

            switch (e.GetType().Name)
            {
                case nameof(UserNotAuthorizedForActionException):
                    toReturn = new MessageDTO("Missing required rights to perform action",GetHttpStatusIntFromEnum(HttpStatusEnum.ForbiddenAction));
                    break;
                default:
                    toReturn = CreateUnSpecifedError(e);
                    break;
                    
            }

            return toReturn;
        }

        /// <summary>
        /// Generate a Message DTO without any specifed Error Handling, with a given ID so it can be tracked within System debug window.
        /// </summary>
        /// <param name="exception">the exception called</param>
        /// <returns></returns>
        public static MessageDTO CreateUnSpecifedError(Exception exception)
        {
            string uuid = Guid.NewGuid().ToString();

            System.Diagnostics.Debug.WriteLine(string.Format("ErrorID: {0} Message: {1}",uuid, exception.Message));
            System.Diagnostics.Debug.WriteLine(string.Format("Unspecified Error ID {0}", exception.GetType().Name));
            return new MessageDTO(string.Format("Unspecified Error ID {0}", uuid), GetHttpStatusIntFromEnum(HttpStatusEnum.UnspecifiedError));
        }
    }
}
