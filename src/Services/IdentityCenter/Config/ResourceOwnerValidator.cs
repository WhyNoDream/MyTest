using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityCenter.Config
{
    /// <summary>
    /// 我的校验逻辑
    /// </summary>
    public class ResourceOwnerValidator : IResourceOwnerPasswordValidator
    {
        public ResourceOwnerValidator()
        {
            // 可以注入服务
        }

        /// <summary>
        /// 校验方法
        /// </summary>
        /// <param name="context">上下文信息(包含了用户名密码等信息)</param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {


            //try
            //{
            //    var loginfo = _loginService.Login(context.UserName, context.Password).Result;
            //    if (loginfo.StatusCode == (int)ResponseStatus.Success)//是否登录成功
            //    {
            //        List<Claim> claims = new List<Claim>();
            //        claims.Add(new Claim(ClaimTypes.Name, context.UserName));
            //        claims.Add(new Claim("LoginName", context.UserName));
            //        claims.Add(new Claim("userId", loginfo.Result.Id.ToString()));
            //        claims.Add(new Claim("LastName", loginfo.Result.LastName ?? ""));
            //        claims.Add(new Claim("FamilyName", loginfo.Result.FamilyName ?? ""));
            //        claims.Add(new Claim("Email", loginfo.Result.Email));
            //        claims.Add(new Claim("AdminCode", loginfo.Result.AdminCode ?? ""));
            //        claims.Add(new Claim("MobilePhone", loginfo.Result.MobilePhone ?? ""));
            //        claims.Add(new Claim("RoleId", loginfo.Result.RoleId ?? ""));
            //        claims.Add(new Claim("DepartmentId", loginfo.Result.DepartmentId ?? ""));
            //        claims.Add(new Claim("CompanyId", "1"));

            //        // 其它端请求过来的时候，设置了的扩展
            //        claims.Add(new Claim("Extra", context.Request.Raw["extra"] ?? ""));

            //        //添加用户陈述
            //        context.Result = new GrantValidationResult(loginfo.Result.Id.ToString(), OidcConstants.AuthenticationMethods.Password, authTime: _clock.UtcNow.UtcDateTime, claims);
            //    }
            //    else
            //    {
            //        //验证失败
            //        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, loginfo.Message);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, ex.Message);
            //}

            //return Task.CompletedTask;


            //await Task.Run(() =>
            //{
            //    //校验逻辑...

            //    // 校验成功
            //    if (true)
            //    {
            //        context.Result = new GrantValidationResult(Guid.NewGuid().ToString(), "DIY", new List<Claim>()
            //        {
            //            new Claim ("DIYClaim","This is DIYClaim")
            //        });
            //    }
            //    else
            //    {
            //        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "认证失败",
            //                               new Dictionary<string, object>()
            //                               {
            //                                    { "Test","This Is Test" }
            //                               });
            //    }
            //});
        }

    }
}
