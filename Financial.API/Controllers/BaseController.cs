﻿using Financial.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FinancialAPI.Controllers
{
    public class BaseController : Controller
    {
        protected int Uid { get; set; }

        protected IActionResult BuildResponse(BaseResponse resp) => (!string.IsNullOrEmpty(resp.Error?.Message)) ? BadRequest(resp.Error.Message) : Ok(resp.Content);

        protected int? RecoverUidSession()
        {
            string? uid = null;

            if (HttpContext.User.Identity is ClaimsIdentity identity)
                uid = identity.Claims.FirstOrDefault(x => x.Type == "uid")?.Value;

            return uid != null ? Convert.ToInt32(uid) : null;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Microsoft.Extensions.Primitives.StringValues auth = context.HttpContext.Request.Headers.Authorization;
            if (!string.IsNullOrEmpty(auth))
            {
                int? uid = RecoverUidSession();

                if (uid is null)
                {
                    context.Result = new UnauthorizedObjectResult("user is unauthorized");
                    return;
                }

                Uid = uid.Value;
            }

            base.OnActionExecuting(context);
        }
    }
}
