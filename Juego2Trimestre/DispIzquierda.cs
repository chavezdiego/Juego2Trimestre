using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego2Trimestre
{
    class DispIzquierda:Disparos
    {
        ConsoleColor color;

        public DispIzquierda()
        {
        }

        public DispIzquierda(int x, int y, ConsoleColor clr, long delta) :base (delta)
        {
            pos.x = x;
            pos.y = y;
            color = clr;
        }

        public DispIzquierda(int x, int y, ConsoleColor clr)
        {
            pos.x = x;
            pos.y = y;
            color = clr;
        }

        override public void Mover()
        {
            Console.ForegroundColor = color;

            Console.SetCursorPosition(pos.x, (int)pos.y);
            Console.WriteLine(" ");

            pos.x--;

            Console.SetCursorPosition(pos.x, (int)pos.y);
            Console.WriteLine("-");
        }

    }
}