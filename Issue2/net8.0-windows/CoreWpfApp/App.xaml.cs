// #define WORKAROUND
#define USE_ASSEMBLY_NAME


using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using CoreDependencyLibContract;

namespace CoreWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }



        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var contractType = typeof(IContract);
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CoreDependencyImpl.dll");

#if USE_ASSEMBLY_NAME
            var an = AssemblyName.GetAssemblyName(path);
            var assembly = Assembly.Load(an);
#endif

#if WORKAROUND
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
#endif

            var allContractImplementations = assembly.GetTypes().Where(t => t.IsPublic && t.IsClass && !t.IsAbstract && contractType.IsAssignableFrom(t)).ToList();

            Trace.WriteLine($"Found {allContractImplementations.Count} implementations");
        }
    }

}
