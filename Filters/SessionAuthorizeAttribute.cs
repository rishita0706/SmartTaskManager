using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartTaskManager.Constants;

namespace SmartTaskManager.Filters
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string[] _allowedRoles;

        public SessionAuthorizeAttribute(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Session.GetInt32(SessionKeys.UserId);

            if (userId == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }

            if (_allowedRoles.Length > 0)
            {
                var role = context.HttpContext.Session.GetString(SessionKeys.Role);

                if (role == null || !_allowedRoles.Contains(role))
                {
                    context.Result = new ForbidResult();
                }
            }

            base.OnActionExecuting(context);
        }
    }
}