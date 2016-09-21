using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego2Trimestre
{
    abstract class ObjetoFisico
    {
        static Random rnd = new Random();

        long proximo, delta;

        public ObjetoFisico()
        {
            proximo = 1 + rnd.Next(10);
            delta = 200 + rnd.Next(400);
        }

        public ObjetoFisico(long p, long d)
        {
            proximo = p;
            delta = d;
        }

        public ObjetoFisico(long d)
        {
            proximo = 1;
            delta = d;
        }

        abstract public void Mover();

        public void Tick(long t)
        {
            if (t >= proximo)
            {
                Mover();
                proximo += delta;
            }
        }
    }
}
