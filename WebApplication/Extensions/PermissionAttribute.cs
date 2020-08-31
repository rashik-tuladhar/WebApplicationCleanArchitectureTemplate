using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace WebApplication.Extensions
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        private readonly string _actionName = string.Empty;

        public PermissionAttribute(string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] {new Claim(_actionName, claimValue)};
        }

        /// <summary>
        /// Checks for permission value present in currently logged in user or not
        /// </summary>
        public class ClaimRequirementFilter : IAsyncActionFilter
        {
            private readonly Claim _claim;

            public ClaimRequirementFilter(Claim claim)
            {
                _claim = claim;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    context.HttpContext.Response.Redirect("/Core/?returnUrl=" + context.HttpContext.Request.Path);
                    return;
                }

                var permission = _claim.Value.Split('|')[0];
                var hasPermission = context.HttpContext.User.Claims.FirstOrDefault(x =>
                    x.Type == "permission" && x.Value == permission);

                if (hasPermission != null)
                {
                    await next();
                }
                else
                {
                    if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    {
                        await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            statuscode = HttpStatusCode.Forbidden,
                            message = $"You are not authorized to use this function."
                        }));
                        context.Result = new EmptyResult();
                    }
                    else
                    {
                        context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                        context.HttpContext.Response.Redirect("/Error/403");
                    }
                }

                //using (SqlConnection con = new SqlConnection(DefaultConnections.ConnectionString))
                //{
                //    using (SqlCommand cmd = new SqlCommand())
                //    {
                //        cmd.Parameters.AddWithValue("userId", userId);
                //        cmd.Parameters.AddWithValue("actionName", _claim.Value);
                //        cmd.CommandText = "Select Top 1 1 from Auth_RoleClaims rc join Auth_UserRoles ur on ur.RoleId = rc.RoleId where ur.UserId = @userId And rc.ClaimValue = @actionName";
                //        cmd.CommandType = System.Data.CommandType.Text;
                //        cmd.Connection = con;
                //        con.Open();
                //        var result = cmd.ExecuteScalar();
                //        if (result != null)
                //            await next();
                //        else
                //        {
                //            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                //            {
                //                await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { statuscode = HttpStatusCode.Forbidden, message = $"You are not authorized to use this function." }));
                //                context.Result = new EmptyResult();
                //            }
                //            else
                //            {
                //                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                //                context.HttpContext.Response.Redirect("/error/403");
                //            }
                //        }
                //    }
                //}
            }
        }
    }
}
