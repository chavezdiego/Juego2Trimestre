using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Juego2Trimestre
{
    class Program
    {
        static void Main(string[] args)
        {

            int vidas = 3;
            Console.SetBufferSize(170, 59);

            Console.SetWindowSize(169, 58);

            Console.CursorVisible = false;

            Jugador j1 = new Jugador(ConsoleKey.A, ConsoleKey.D, ConsoleKey.W, ConsoleKey.S, ConsoleKey.J,ConsoleKey.E,ConsoleKey.Q,"*",ConsoleColor.Blue);
        
            List<Plataforma> LPlat = new List<Plataforma>();

            LPlat.Add(new Plataforma(20, 30, 0, ConsoleColor.Cyan)); // Y, W, X
            LPlat.Add(new Plataforma(20, 30, 50, ConsoleColor.Cyan)); // Y, W, X
            LPlat.Add(new Plataforma(20, 30, 100, ConsoleColor.Cyan)); // Y, W, X

            LPlat.Add(new Plataforma(30, 10, 20, ConsoleColor.Red));
            LPlat.Add(new Plataforma(30, 15, 80, ConsoleColor.Red));
            LPlat.Add(new Plataforma(30, 20, 50, ConsoleColor.Red));

            LPlat.Add(new Plataforma(40, 40, 0, ConsoleColor.Green)); // Y, W, X
            LPlat.Add(new Plataforma(40, 40, 70, ConsoleColor.Green)); // Y, W, X          
            LPlat.Add(new Plataforma(40, 30, 0, ConsoleColor.Green)); // Y, W, X

            LPlat.Add(new Plataforma(50, 168, 0, ConsoleColor.DarkBlue)); // Y, W, X

            List<Enemigo> LEneg = new List<Enemigo>();
            
            LEneg.Add(new Enemigo(59));


            Enemigo eneg = new Enemigo(50);

            DateTime start = DateTime.Now;

            DateTime startTick = DateTime.Now;
            
            foreach (Plataforma Plat in LPlat)
            {
                Plat.Dibujar();
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.SetCursorPosition(30,5);
                Console.WriteLine(vidas);

                Console.ForegroundColor = ConsoleColor.Black;

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo tecla = Console.ReadKey(true);
                    j1.mover(tecla);
                }

                j1.Imprimir();

                eneg.Imprimir();

                j1.ImprimirDisparos();

                long t = (long)(DateTime.Now - startTick).TotalMilliseconds;

                eneg.ImprimirDisparos(t);

                foreach (Plataforma Plat in LPlat)
                {
                    if (j1.intersecta(Plat))
                    {
                        j1.ModificarY(Plat.obtenerY()-6);
                        j1.ModificarV(0.0);
                        j1.ModificarA(0.0);
                    }
                    j1.ModificarA(450.0);

                    if (eneg.intersecta(Plat))
                    {
                        eneg.ModificarY(Plat.obtenerY() - 6);
                        eneg.ModificarV(0.0);
                        eneg.ModificarA(0.0);

                    }

                    j1.DispPlataforma(Plat);
                    eneg.DispPlataforma(Plat);
                    eneg.ModificarA(450.0);

                }

                foreach (Enemigo neg in LEneg)
                {
                    if (j1.intersectaDisp(neg))
                    {
                        vidas--;
                    }
                }

                foreach (Plataforma Plat in LPlat)
                {
                    Plat.Dibujar();
                }
                
                //Thread.Sleep(20);

                j1.Borrar();
                eneg.Borrar();

            }
        }
    }
}