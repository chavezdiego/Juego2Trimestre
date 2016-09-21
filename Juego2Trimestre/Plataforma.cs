using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego2Trimestre
{
    class Plataforma
    {
        Punto pos = new Punto();

        static Random rnd = new Random();

        int w, h;

        ConsoleColor color;
      
        public Plataforma(int posY)
        {
            pos.y = posY;
            w = rnd.Next(5, 10);
            h = 2;
            pos.x = rnd.Next(50);
            color = ConsoleColor.Blue;
        }

        public int obtenerY()
        {
            return (int)pos.y;
        }
        public Plataforma(int posY, int WP, int XP, ConsoleColor clr)
        {
            pos.y = posY;
            w = WP;
            h = 2;
            pos.x = XP;
            color = clr;
        }

        public void Dibujar()
        {
            Console.ForegroundColor = color;

            for (int i = 0; i < w; i++)
            {
                for (int e = 0; e < h; e++)
                {
                    Console.SetCursorPosition(pos.x + i, (int)pos.y + e);
                    Console.WriteLine("*");
                }
            }
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public bool intersecta(int e, int i)
        {
            return (e >= pos.x && e < (pos.x + w) && i >= pos.y && i < (pos.y + h));
        }
    }
}
