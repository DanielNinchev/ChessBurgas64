namespace ChessBurgas64.Web
{
    using Azure.Identity;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Azure.KeyVault;
    using Microsoft.Azure.Services.AppAuthentication;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.AzureKeyVault;
    using Microsoft.Extensions.Hosting;

    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //            {
        //                webBuilder.UseStartup<Startup>();
        //            });

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //    .ConfigureWebHostDefaults(webBuilder =>
        //    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
        //    {
        //        var settings = config.Build();
        //        config.AddAzureAppConfiguration(options =>
        //        {
        //            options.Connect(settings["ConnectionStrings:AppConfig"])
        //                    .ConfigureKeyVault(kv =>
        //                    {
        //                        kv.SetCredential(new DefaultAzureCredential());
        //                    });
        //        });
        //    })
        //    .UseStartup<Startup>());

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                config.AddAzureKeyVault("https://chessburgas64.vault.azure.net/", keyVaultClient, new DefaultKeyVaultSecretManager());
            })
            .UseStartup<Startup>();
    }
}
