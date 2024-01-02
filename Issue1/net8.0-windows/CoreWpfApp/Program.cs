using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CoreDependencyLib;

namespace CoreWpfApp
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                AccessDependencyLib();
                LaunchApp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static void AccessDependencyLib()
        {
            Trace.WriteLine(typeof(Foo).FullName);
        }

        private static void LaunchApp()
        {
            var app = new App();
            app.Run();
        }
    }
}
