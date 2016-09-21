using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Juego2Trimestre
{
    class Jugador
    {
        ConsoleKey izq, der, up, down, disp,salto_der,salto_izq;

        protected double v = 0.0;
        protected double a = 450.0;
        
        protected Punto pos = new Punto();

        static Random rnd = new Random();

        ConsoleKeyInfo UltTecla;

        List<Disparos> LDisparos = new List<Disparos>();

        protected ConsoleColor color;

        string imagen;

        protected int wP = 5;
        protected int hP = 6;

        int w = 3;
        int h = 3;

        char[] personaje=
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

        public Jugador(ConsoleKey tecla_izq, ConsoleKey tecla_der, ConsoleKey tecla_up, ConsoleKey tecla_down, ConsoleKey tecla_disp, ConsoleKey tecla_salto_der, ConsoleKey tecla_salto_izq, string img, ConsoleColor clr)
        {
            izq = tecla_izq;
            der = tecla_der;
            up = tecla_up;
            down = tecla_down;
            disp = tecla_disp;
            salto_izq = tecla_salto_izq;
            salto_der = tecla_salto_der; 

            color = clr;
            imagen = img;

            pos.x = 2;
            pos.y = 2;
        }

        public Jugador(ConsoleKey teclaA, ConsoleKey teclaD)
        {
            izq = teclaA;
            der = teclaD;
        }

        public int obtenerH()
        {
            return h;
        }

        public int obtenerW()
        {
            return w;
        }

        public int obtenerX()
        {
            return pos.x;
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

        public int obtenerY()
        {
            return (int)pos.y;
        }

        public ConsoleColor obtenerColor()
        {
            return color;
        }

        public void ReiniciarJug()
        {
            pos.x = 2;
            pos.y = 1 + rnd.Next(50);
        }

        public void mover(ConsoleKeyInfo tecla)
        {
         
            //Teclas
            if (tecla.Key == izq)
            {               
                pos.x--;
                personaje = PerfilIzq;
            }

            if (tecla.Key == up) v= v- 100.0;

            if (tecla.Key == der)
            {               
                pos.x++;
                personaje = PerfilDer;
            }

            if (tecla.Key == disp)
            {
                if (UltTecla.Key == izq)
                {
                    LDisparos.Add(new DispIzquierda(pos.x-1,(int)pos.y+2,color, 100));
                }
                if (UltTecla.Key == der)
                {
                    LDisparos.Add(new DispDerecha(pos.x+5, (int)pos.y+2,color, 100));
                }
            }

            if(tecla.Key != disp)UltTecla = tecla;
        }
        
        public void Imprimir()
        {
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
            if (pos.y + h >= 57)
            {
                Borrar();
                pos.y = 57 - h;
            }
            Console.ForegroundColor = ConsoleColor.Black;
        }


        public void ImprimirDisparos()
        {
            foreach (Disparos disp in LDisparos)
            {
                disp.Mover();
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

        public bool intersecta(Plataforma p)
        {
            int posi = 0;
            for (int i = 5; i < hP; i++)
            {
                for (int j = 0; j < wP; j++)
                {
                    if (personaje[posi] != '*')
                    {
                        if (p.intersecta(pos.x + j, (int)pos.y + i+1))
                        {
                            return true;                  
                        }
                    }
                    posi++;
                }
            }

            return false;
        }

        public bool intersectaDisp(Enemigo disp)
        {
            int posi = 0;
            for (int i = 0; i < hP; i++)
            {
                for (int j = 0; j < wP; j++)
                {
                    if (personaje[posi] != '*')
                    {
                        if (disp.DispIntersecta(pos.x + j, (int)pos.y + i))  
                        {
                            return true;
                        }
                    }
                    posi++;
                }
            }

            return false;
        }
        public void DispPlataforma(Plataforma Lp)
        {
            foreach (Disparos disp in LDisparos)
            {
                //Limites
                if (Lp.intersecta(disp.obtenerX(), disp.obtenerY()))
                {
                    disp.Borrar();
                    LDisparos.Remove(disp);
                    break;
                }

            }
        }

    }
}