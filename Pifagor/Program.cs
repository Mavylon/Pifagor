using System;
using System.Collections.Generic;
using System.Text;

namespace Pifagor
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App();
            app.Run();
        }
    }
}
