using System;
using Cafe.App.DryIoc;
using DryIoc.Experimental;
using WinForms = System.Windows.Forms;

namespace Cafe.App
{
    public class Program
    {
        public Program()
        { }

        [STAThread]
        public static void Main()
        {
            var container = Bootstrapper.Bootstrap();
            var program = container.Get<Program>();

            program.Run();
        }

        private void Run()
        {
            WinForms.Application.EnableVisualStyles();
            WinForms.Application.SetCompatibleTextRenderingDefault(false);
            WinForms.Application.Run(new MainForm());
        }
    }
}