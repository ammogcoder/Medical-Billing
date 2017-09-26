using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Waf;
using System.Waf.Applications;
using System.Windows;
using System;
using MedQuist.BillingAdmin.ViewModels;
using MedQuist.ViewModels.Controllers;
using BillingAdmin.Properties;
using MedQuist.BillingAdmin.Presentation;

namespace BillingAdmin
{
    public partial class App : Application
    {
        private CompositionContainer container;
        private ApplicationController controller;

        static App()
        {

#if (DEBUG)
            WafConfiguration.Debug = true;
#endif
        }

        // OnStartup Application wpf popup onexit login
        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);


            AggregateCatalog catalog = new AggregateCatalog();
            // Add the WpfApplicationFramework assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Controller).Assembly));
            // Add the BillingAdmin application to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            // Add the presentation assembly
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ApplicationController).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ShellWindow).Assembly));

            container = new CompositionContainer(catalog);

            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(container);
            container.Compose(batch);

            try
            {
                container.GetExportedValue<ShellViewModel>();
                controller = container.GetExportedValue<ApplicationController>();
                controller.Initialize();
                controller.Run();
            }
            catch (CompositionException ex)
            { 
                throw ex;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if(controller != null)
                controller.Shutdown();
            container.Dispose();

            base.OnExit(e);
        }
    }
}
