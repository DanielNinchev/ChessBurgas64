// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

namespace ChessBurgas64.Web.Areas.Identity.Pages.Account.Manage
{
    using System;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string ExternalLogins => "ExternalLogins";

        public static string MyLessons => "MyLessons";

        public static string MyPayments => "MyPayments";

        public static string MySchedule => "MySchedule";

        public static string PersonalData => "PersonalData";

        public static string Privacy => "Privacy";

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static string MyLessonsNavClass(ViewContext viewContext) => PageNavClass(viewContext, MyLessons);

        public static string MyPaymentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, MyPayments);

        public static string MyScheduleNavClass(ViewContext viewContext) => PageNavClass(viewContext, MySchedule);

        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        public static string PrivacyNavClass(ViewContext viewContext) => PageNavClass(viewContext, Privacy);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
