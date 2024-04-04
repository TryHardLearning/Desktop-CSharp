using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu("MENU");
            menu.Centralizado = true;
            menu.Items.Add(new MenuItem { Rotulo = "Opcao 1" });
            menu.Items.Add(new MenuItem { Rotulo = "Opcao 2" });
            menu.Items.Add(new MenuItem { Rotulo = "Opcao 3" });

            menu.BarItems.Add(new MenuItem { Rotulo = "Opcao 1" });
            menu.BarItems.Add(new MenuItem { Rotulo = "Opcao 2" });
            menu.BarItems.Add(new MenuItem { Rotulo = "Opcao 3" });

            menu.Show();
            Console.ReadKey();  
       }
    }
}
