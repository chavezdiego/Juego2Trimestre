using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego2Trimestre
{
    abstract class Disparos:ObjetoFisico
    {
        protected Punto pos = new Punto();

        public Disparos()
        {
        }

        public Disparos(long delta):base (delta)
        {

        }

        public Disparos(int x,int y)
        {
            pos.x = x;
            pos.y = y;
        }

        public int obtenerX()
        {
            return pos.x;
        }

        public int obtenerY()
        {
            return (int)pos.y;
        }

        public void Borrar()
        {
            Console.SetCursorPosition(pos.x, (int)pos.y);
            Console.WriteLine(" ");
        }


    }
}