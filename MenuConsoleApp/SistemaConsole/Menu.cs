using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaConsole
{

    internal class MenuItem
    {
        public string Rotulo { get; set; }
        public int Col { get; set; }
        public int Lin { get; set; }
        public ConsoleColor ItemForeground { get; set; }
        public ConsoleColor ItemBackground { get; set; }
        public ConsoleColor SelectorForeground { get; set; }
        public ConsoleColor SelectorBackground { get; set; }

        public MenuItem()
        {
            ItemForeground = ConsoleColor.White;
            ItemBackground = ConsoleColor.Black;
            SelectorBackground = ConsoleColor.Blue;
            SelectorForeground = ConsoleColor.Yellow;
        }

        public void Show()
        {
            Console.BackgroundColor = ItemBackground;
            Console.ForegroundColor = ItemForeground;
            Console.SetCursorPosition(Col, Lin);
            Console.Write(Rotulo);
            Console.SetCursorPosition(Col, Lin);
        }

        public void ShowSelector()
        {
            Console.BackgroundColor = SelectorBackground;
            Console.ForegroundColor = SelectorForeground;
            Console.SetCursorPosition(Col, Lin);
            Console.Write(Rotulo);
            Console.SetCursorPosition(Col, Lin);
        }
        
    }
    internal class Menu
    {
        public List<MenuItem> Items { get; set; }
        public List<MenuItem> BarItems { get; set;}
        public int PosAtual { get; set; }
        public string Titulo { get; set; }
        public bool Centralizado { get; set; }
        public ConsoleColor TitleForeground { get; set; }
        public ConsoleColor TitleBackground { get; set; }

        public Menu(string titulo)
        {
            Items = new List<MenuItem>();
            BarItems = new List<MenuItem>();
            TitleBackground = ConsoleColor.Blue;
            TitleForeground = ConsoleColor.Yellow;
            Titulo = titulo;
        }

        private int Select()
        {
            Items[PosAtual].ShowSelector();
            while (true)
            {
                var tecla = Console.ReadKey();
                Items[PosAtual].Show();
                switch (tecla.Key)
                {
                    case ConsoleKey.Enter: return PosAtual;
                    case ConsoleKey.Escape: return -1; 
                    case ConsoleKey.DownArrow:
                        {
                            if (++PosAtual == Items.Count) PosAtual = 0;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        {
                            if (--PosAtual < 0) PosAtual = Items.Count - 1;
                        }
                        break;
                }
                Items[PosAtual].ShowSelector();
            }
        }

        public void Show()
        {
            int x = 0, y = 0;
            PosAtual = 0;
            Console.Clear();
            if(Items.Count == 0)
            {
                throw new ArgumentException("Menu não contém items definidos.");
            }
            if (BarItems.Count == 0)
            {
                throw new ArgumentException("Menu não contém items definidos.");
            }
            var XIndex = 4;
            foreach (MenuItem m in BarItems)
            {
                m.Lin = 1;
                m.Col = XIndex;
                m.Show();
                XIndex += m.Rotulo.Length + 4;
            }
            if (Centralizado)
            {
                x = (Console.WindowWidth - Titulo.Length) / 2;
                y = (Console.WindowHeight - Items.Count - 2) / 2;
            }
            //mostra titulo
            Console.BackgroundColor = TitleBackground;
            Console.ForegroundColor = TitleForeground;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(Titulo);
            Console.WriteLine();
            y += 2;
            foreach (MenuItem m in Items)
            {
                if(Centralizado)
                {
                    x = (Console.WindowWidth - m.Rotulo.Length) / 2;
                }
                m.Lin = y++;
                m.Col = x;
                m.Show();
            }
            Console.CursorVisible = false;
            var selected = Select();
            Console.CursorVisible = true;
        }

        
    }
}
