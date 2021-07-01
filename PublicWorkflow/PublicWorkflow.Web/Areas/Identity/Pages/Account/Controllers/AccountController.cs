using PublicWorkflow.Web.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PublicWorkflow.Application.Features.ActivityLog.Commands.AddLog;
using PublicWorkflow.Infrastructure.Identity.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PublicWorkflow.Web.Areas.Account.Controllers
{
    [Area("Identity"),Authorize]
    public class AccountController : BaseController<AccountController>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            _notify.Information("Hi There!");
            return View();
        }
        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }
        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            var returnUrl = Url.Content("~/");
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return LocalRedirect("/Identity/Account/Login");

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            //string[] userInfo = { info.Principal.FindFirst(ClaimTypes.Name).Value, info.Principal.FindFirst(ClaimTypes.Email).Value };
            if (result.Succeeded)
            {
                //await _mediator.Send(new AddActivityLogCommand() { userId = result., Action = "Logged In" });
                _logger.LogInformation("User logged in.");
                _notify.Success($"Logged in as {info.Principal.FindFirst(ClaimTypes.Name).Value}.");
                return LocalRedirect(returnUrl);
            }
            //   return View(userInfo);
            else
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    FirstName = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    LastName = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                    EmailConfirmed=true
                };

                IdentityResult identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        //return RedirectToAction("Index","Home");
                        return LocalRedirect(returnUrl);
                    }
                }
                return LocalRedirect("/identity/account/accessDenied");
            }
        }
    }
}