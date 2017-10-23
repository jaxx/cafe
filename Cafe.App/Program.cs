using System;
using System.Windows.Forms;
using Cafe.App.DryIoc;
using DryIoc.Experimental;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}