using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using DependencyLib;

namespace WpfApp
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
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DependencyLibImpl.dll");
            var an = AssemblyName.GetAssemblyName(path);
            var assembly = Assembly.Load(an);

            var allContractImplementations = assembly.GetTypes().Where(t => t.IsPublic && t.IsClass && !t.IsAbstract && contractType.IsAssignableFrom(t)).ToList();

            Trace.WriteLine($"Found {allContractImplementations.Count} implementations");
        }
    }
}
