using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaConsole
{
    internal class SwitchItemEventArgs : EventArgs
    {
        public readonly string opcao;

        public SwitchItemEventArgs(string opcao)
        {
            this.opcao = opcao;
        }
    }

    internal class MenuItem
    {
        public string Rotulo { get; set; }
        public string hint { get; set; }
        public string hintSub { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
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

        public void showHint()
        {
            if (posX == 0 || posY == 0)
            {
                posX = Col - (Rotulo.Length + hint.Length) / 2 < 0 ? Console.WindowLeft :
                    Col - (Rotulo.Length + hint.Length) / 2;
                posY = Lin + 1;
            }

            Console.SetCursorPosition(posX, posY);
            Console.Write(hint);
        }
        public void clearHint()
        {
            Console.SetCursorPosition(posX, posY);
            for ( int i = 0; i <= hint.Length; i++ )
                Console.Write(" ");
        }
        
    }
    internal class Menu
    {
        public delegate void OnSwitchHandler(System.Object sender, SwitchItemEventArgs e);
        public event OnSwitchHandler OnSwitch;
        public List<MenuItem> Items { get; set; }
        public int PosAtual { get; set; }
        public string Titulo { get; set; }
        public bool centralizado { get; set; }
        public bool barra { get; set; }
        public bool submenu { get; set; }
        public bool hints { get; set; }

        void OnSwitchHandler(System.Object sender, SwitchItemEventArgs e)
        {
            Console.Write("evento disparado");
        }
        public void setSubmenu()
        {
            centralizado = false;
            submenu = true;
        }
        public void setCentralizado()
        {
            this.centralizado = true;
            this.submenu = false;
        }
        public Menu refSubmenu;
        public ConsoleColor TitleForeground { get; set; }
        public ConsoleColor TitleBackground { get; set; }

        public Menu(string titulo, Boolean hints)
        {
            Items = new List<MenuItem>();
            TitleBackground = ConsoleColor.Blue;
            TitleForeground = ConsoleColor.Yellow;
            Titulo = titulo;
            this.hints = hints;
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

                if ( hints )
                {
                    if (PosAtual - 1 < 0)
                        Items[PosAtual].clearHint();
                    else
                        Items[PosAtual - 1].clearHint();

                    Items[PosAtual].showHint();
                }
                
                OnSwitch(sender, new SwitchItemEventArgs("Trocou de opcao"));
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
            if(centralizado)
            {
                x = (Console.WindowWidth - Titulo.Length) / 2;
                y = (Console.WindowHeight - Items.Count - 2) / 2;
            }
            else if (submenu)
            {
                y = Console.WindowTop;
                x = Console.WindowLeft;
            }
                

            //mostra titulo
            Console.BackgroundColor = TitleBackground;
            Console.ForegroundColor = TitleForeground;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(Titulo);
            Console.WriteLine();
            
            if (centralizado) y += 2;
            foreach (MenuItem m in Items)
            {

                if (centralizado)
                {
                    x = (Console.WindowWidth - m.Rotulo.Length) / 2;
                    y++; //espaco vertical
                }
                else if (submenu)
                { 
                    if ( (x = x + m.Rotulo.Length) > Console.WindowWidth )
                    {
                        x = Console.WindowLeft;
                        y++;
                    }
                    x++; //spaco horizontal
                }

                m.Lin = y;
                m.Col = x;
                m.Show();
            }
            Console.CursorVisible = false;
            var selected = Select();
            Console.CursorVisible = true;
        }        
    }
}
