using Application.Common.Models;
using Application.Common.Models.Enum;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Services
{
    public static class RequestResponse
    {
        #region Public Methods

        public static IActionResult ReturnResponse(ResponseDetail result)
        {
            if (!result.Success)
            {
                result.Exception = null!;
                if (result.MessageType == MessageType.Invalid) return new BadRequestObjectResult(result);
                return new NotFoundObjectResult(result);
            }
            return new OkObjectResult(result);
        }

        #endregion Public Methods
    }
}
