// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace ChessBurgas64.Web.Areas.Identity.Pages.Account.Manage
{
#nullable disable

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using System.Text.Unicode;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    public class DownloadPersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMembersService membersService;
        private readonly ILogger<DownloadPersonalDataModel> logger;

        public DownloadPersonalDataModel(
            UserManager<ApplicationUser> userManager,
            IMembersService membersService,
            ILogger<DownloadPersonalDataModel> logger)
        {
            this.userManager = userManager;
            this.membersService = membersService;
            this.logger = logger;
        }

        public IActionResult OnGet()
        {
            return this.NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            this.logger.LogInformation("User with ID '{UserId}' asked for their personal data.", this.userManager.GetUserId(this.User));

            // Only include personal data for download
            var personalData = new Dictionary<string, string>();
            var personalDataUserProps = typeof(ApplicationUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataUserProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            var member = await this.membersService.GetMemberByIdAsync(user.MemberId);
            if (member != null)
            {
                var personalDataMemberProps = typeof(Member).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
                foreach (var p in personalDataMemberProps)
                {
                    personalData.Add(p.Name, p.GetValue(member)?.ToString() ?? "null");
                }
            }

            var logins = await this.userManager.GetLoginsAsync(user);
            foreach (var l in logins)
            {
                personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
            }

            personalData.Add($"Authenticator Key", await this.userManager.GetAuthenticatorKeyAsync(user));

            var encoderSettings = new TextEncoderSettings();
            encoderSettings.AllowCharacters('\u0022');
            encoderSettings.AllowRange(UnicodeRanges.All);

            var serializingOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(encoderSettings),
                WriteIndented = true,
            };

            this.Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(personalData, serializingOptions), "application/json");
        }
    }
}
