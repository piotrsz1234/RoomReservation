using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.User.Models;
using RoomReservation.Domain.Enums;
using RoomReservation.Domain.Services;

namespace RoomReservation.Api.Middleware {
    public class BasicAuthenticationFilter : ActionFilterAttribute {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(HeaderNames.Authorization, out var headerValue))
            {
                context.Result = new ContentResult();
                context.HttpContext.Response.StatusCode = 401;
            }

            var authHeaderVal = AuthenticationHeaderValue.TryParse(headerValue, out var val) ? val : null;

            // RFC 2617 sec 1.2, "scheme" name is case-insensitive
            if (authHeaderVal != null && authHeaderVal.Scheme.Equals("basic",
                    StringComparison.OrdinalIgnoreCase) &&
                authHeaderVal.Parameter != null)
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                var credentials = encoding.GetString(Convert.FromBase64String(authHeaderVal.Parameter));

                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                var userService = context.HttpContext.RequestServices.GetService<IUserService>();

                var result = await userService.SignInAsync(new SignInModel()
                {
                    Email = name,
                    Password = password
                });

                if (string.IsNullOrWhiteSpace(result.Error) == false)
                {
                    context.Result = new ContentResult();
                    context.HttpContext.Response.StatusCode = 401;
                }
                else
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(Constants.UserIdClaimType, result.UserId.Value.ToString()),
                        new Claim(Constants.IsAdminClaimType, (result.Role == UserRole.Admin).ToString())
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    
                    var principal = new ClaimsPrincipal(identity);

                    principal.AddIdentity(identity);

                    context.HttpContext.User = principal;
                }
            }
            else
            {
                context.Result = new ContentResult();
                context.HttpContext.Response.StatusCode = 401;
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}