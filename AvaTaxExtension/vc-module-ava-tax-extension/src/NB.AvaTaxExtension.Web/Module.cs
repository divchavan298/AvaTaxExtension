using System.Linq;
using AvaTax.TaxModule.Core.Services;
using AvaTax.TaxModule.Data.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NB.AvaTaxExtension.Core;
using NB.AvaTaxExtension.Data.Repositories;
using NB.AvaTaxExtension.Data.Services;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Platform.Core.Settings;

namespace NB.AvaTaxExtension.Web
{
    public class Module : IModule, IHasConfiguration
    {
        public ManifestModuleInfo ModuleInfo { get; set; }
        public IConfiguration Configuration { get; set; }

        public void Initialize(IServiceCollection serviceCollection)
        {
            // Initialize database
            var connectionString = Configuration.GetConnectionString(ModuleInfo.Id) ??
                                   Configuration.GetConnectionString("VirtoCommerce");

            serviceCollection.AddDbContext<AvaTaxExtensionDbContext>(options => options.UseSqlServer(connectionString));

            serviceCollection.AddTransient<AvaCreateTransactionModel, ExtendAvaCreateTransactionModel>();
            //serviceCollection.AddTransient<IOrdersSynchronizationService, NBAvaTaxExtension>();
            // Override models
            //AbstractTypeFactory<OriginalModel>.OverrideType<OriginalModel, ExtendedModel>().MapToType<ExtendedEntity>();
            //AbstractTypeFactory<OriginalEntity>.OverrideType<OriginalEntity, ExtendedEntity>();

            // Register services
            //serviceCollection.AddTransient<IMyService, MyService>();
        }

        public void PostInitialize(IApplicationBuilder appBuilder)
        {
            var serviceProvider = appBuilder.ApplicationServices;

            // Register settings
            var settingsRegistrar = serviceProvider.GetRequiredService<ISettingsRegistrar>();
            settingsRegistrar.RegisterSettings(ModuleConstants.Settings.AllSettings, ModuleInfo.Id);

            // Register permissions
            var permissionsRegistrar = serviceProvider.GetRequiredService<IPermissionsRegistrar>();
            permissionsRegistrar.RegisterPermissions(ModuleConstants.Security.Permissions.AllPermissions
                .Select(x => new Permission { ModuleId = ModuleInfo.Id, GroupName = "AvaTaxExtension", Name = x })
                .ToArray());


            var settingManager = appBuilder.ApplicationServices.GetRequiredService<ISettingsManager>();


            var processJobCommited = settingManager.GetValue(ModuleConstants.Settings.General.SynchronizationIsCommited.Name, false);//newly added IsCommited
            var processJobActive = settingManager.GetValue(ModuleConstants.Settings.General.SynchronizationIsActive.Name, false);//newly added IsCommited
            // Apply migrations
            using var serviceScope = serviceProvider.CreateScope();
            using var dbContext = serviceScope.ServiceProvider.GetRequiredService<AvaTaxExtensionDbContext>();
            dbContext.Database.Migrate();
        }

        public void Uninstall()
        {
            // Nothing to do here
        }
    }
}
