using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool keepgoing = true;
            ConsoleKeyInfo keypressed;

            Console.SetWindowSize(120, 52);
            Console.SetBufferSize(120, 52);

            ConsolePainter painter = new ConsolePainter();

            painter.drawRect(58, 2, 3, 3,'8');

            painter.drawRect(30, 14, 8, 8, '.', '.');

            painter.drawShadedCircle(0, 0, 50);

            painter.drawCircle(24, 20, 9);

            painter.paint();

            double x = 30;
            double y = 30;
            double i = 0;

            do
            {
                if(Console.KeyAvailable)
                {
                    keypressed = Console.ReadKey();

                    if (keypressed.Key == ConsoleKey.Enter)
                    {
                        keepgoing = false;
                    }
                    if (keypressed.Key == ConsoleKey.DownArrow)
                    {
                        painter.moveGraphicDown(2);
                    }
                }

                painter.moveGraphic(3, Convert.ToInt32(x + 10*Math.Sin(i/100*Math.PI)), Convert.ToInt32(y + 5*Math.Cos(i/100*Math.PI)));
                painter.moveGraphic(2, Convert.ToInt32(10 + 10 * Math.Sin((i + 20) / 100 * Math.PI)), Convert.ToInt32(10+ 5*Math.Cos((i + 20) / 100 * Math.PI)));
                i+=1.0;
                if (i == 200)
                    i = 0;

                System.Threading.Thread.Sleep(10);
                
                painter.paint();
            } while (keepgoing);


            return;
        }

    }
}
