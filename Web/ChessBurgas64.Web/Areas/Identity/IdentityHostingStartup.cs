﻿using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ChessBurgas64.Web.Areas.Identity.IdentityHostingStartup))]

namespace ChessBurgas64.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}