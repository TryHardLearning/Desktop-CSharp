using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SistemaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu ( "MENU", true );
            menu.setSubmenu();
            menu.Items.Add(new MenuItem { Rotulo = "Opcao 1", hint = "É a opção 1" });
            menu.Items.Add(new MenuItem { Rotulo = "Opcao 2", hint = "É a opção 2" });
            menu.Items.Add(new MenuItem { Rotulo = "Opcao 3", hint = "É a opção 3" });
            menu.Show();
            Console.ReadKey();  
       }
    }
}
