using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juego2Trimestre
{
    class Enemigo
    {
        Punto pos = new Punto();

        double v = 0.0;
        double a = 450.0;

        int dir = 0;

        DateTime start = DateTime.Now;

        DateTime timedisp = DateTime.Now;

        DateTime timesalto = DateTime.Now;

        List<ObjetoFisico> LDisparos = new List<ObjetoFisico>();

        static Random rnd = new Random();

        int wP = 5;
        int hP = 6;

        

        public Enemigo(int x)
        {
            pos.x = x;
        }

        public void ModificarY(int y)
        {
            pos.y = y;
        }
        public void ModificarV(double vel)
        {
            v = vel;
        }
        public void ModificarA(double acel)
        {
            a = acel;
        }

        char[] personaje =
            {
            '*', '#', '#', '*', '*',
            '*', '#', '#', '*', '*',
            'x', 'x', '-', '-', '-',
            '*', '*', 'x', 'x', '*',
            '*', 'x', '*', 'x', '*',
            'x', '*', '*', '*', 'x'     
        };

        char[] PerfilIzq = 
        {
            '*', '*', '#', '#', '*',
            '*', '*', '#', '#', '*',
            '-', '-', '-', 'x', 'x',
            '*', '*', 'x', 'x', '*',
            '*', 'x', '*', 'x', '*',
            'x', '*', '*', '*', 'x'     
        };
        char[] PerfilDer = 
        {
            '*', '#', '#', '*', '*',
            '*', '#', '#', '*', '*',
            'x', 'x', '-', '-', '-',
            '*', '*', 'x', 'x', '*',
            '*', 'x', '*', 'x', '*',
            'x', '*', '*', '*', 'x'     
        };

        ConsoleColor color = ConsoleColor.Red;

        public void Imprimir()
        {
            if ((DateTime.Now - start).TotalSeconds >= 3)
            {                
                dir = rnd.Next(2);

                start = DateTime.Now;
            }

            if ((DateTime.Now - timesalto).TotalSeconds >= 4)
            {
                v = v - 70.0;
                timesalto = DateTime.Now;
            }

            switch (dir)
            {
                case 0: pos.x++;
                        personaje = PerfilDer;
                    break;
                case 1: pos.x--;
                        personaje = PerfilIzq;
                    break;
            }
            
            if ((DateTime.Now - timedisp).TotalSeconds >= 1)
            {
                if (dir == 0)
                {
                    if (pos.x>=5)
                    {                       
                    LDisparos.Add(new DispDerecha(pos.x + 5, (int)pos.y + 2, color));
                    }
                }
                if (dir == 1)
                {
                    if (pos.x<=165)
                    {                        
                        LDisparos.Add(new DispIzquierda(pos.x - 1, (int)pos.y + 2, color)); 
                    }                  
                }
                timedisp = DateTime.Now;
            }

            int posi = 0;
            Console.ForegroundColor = color;
            v += a / 100;
            pos.y += v / 100;

            for (int i = 0; i < hP; i++)
            {
                for (int e = 0; e < wP; e++)
                {
                    if (personaje[posi] != '*')
                    {
                        Console.SetCursorPosition(pos.x + e, (int)pos.y + i);
                        Console.WriteLine(personaje[posi]);
                    }
                    posi++;
                }
            }

            //Limites
            if (pos.x < 2)
            {
                Borrar();
                pos.x = 2;
                
            }
            if (pos.x >= 160)
            {
                Borrar();
                pos.x = 160;
                
            }
            if (pos.y < 1)
            {
                Borrar();
                pos.y = 1; 
                
            }
            if (pos.y + hP >= 57)
            {
                Borrar();
                pos.y = 57 - hP; 
                
            }
            
            Console.ForegroundColor = ConsoleColor.Black;
        }
        public bool intersecta(Plataforma p)
        {
            int posi = 0;
            for (int i = 5; i < hP; i++)
            {
                for (int j = 0; j < wP; j++)
                {
                    if (personaje[posi] != '*')
                    {
                        if (p.intersecta(pos.x + j, (int)pos.y + i + 1))
                        {
                            return true;
                        }
                    }
                    posi++;
                }
            }

            return false;
        }

        public void ImprimirDisparos(long t)
        {

            foreach (Disparos disp in LDisparos)
            {  
                //Limites
                if (disp.obtenerX() <= 2)
                {
                    disp.Borrar();
                    LDisparos.Remove(disp);
                    break;
                }

                if (disp.obtenerX() >= 150)
                {
                    disp.Borrar();
                    LDisparos.Remove(disp);

                    break;
                }
            }

            foreach (ObjetoFisico obj in LDisparos)
            {
                obj.Tick(t);
            }
        }
            
        public void Borrar()
        {
            int posi = 0;

            for (int i = 0; i < hP; i++)
            {
                for (int e = 0; e < wP; e++)
                {
                    if (personaje[posi] != '*')
                    {
                        Console.SetCursorPosition(pos.x + e, (int)pos.y + i);
                        Console.WriteLine(" ");
                    }
                    posi++;
                }
            }
        }

        public bool DispIntersecta(int e, int i)
        {
            foreach (Disparos disp in LDisparos)
            {
                if (e==disp.obtenerX() && i==disp.obtenerY())
                {
                    return true;
                }

            }
            return false;
        }

        public void DispPlataforma(Plataforma Lp)
        {
            foreach (Disparos disp in LDisparos)
            {
                //Limites
                if (Lp.intersecta(disp.obtenerX() , disp.obtenerY()) )
                {
                    disp.Borrar();
                    LDisparos.Remove(disp);
                    break;
                }

            }  
        }

    }
}