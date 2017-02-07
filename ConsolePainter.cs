using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingTest
{
    class ConsolePainter
    {

        private int BufferHeight;   // Awareness of window
        private int BufferWidth;    // Awareness of window
        private char[] Buffer;      // Used to 'paint' to the console window.
        private Queue<Graphic> Graphics;    // LIFO graphic objects (first layer bottom, last layer top);
        private Queue<Graphic> Canvas;      // LIFO graphic objects for trimming to buffer

        public ConsolePainter()
        {
            BufferHeight = Console.WindowHeight-2;
            BufferWidth = Console.WindowWidth;
            Buffer = new char[BufferHeight * BufferWidth];
            Graphics = new Queue<Graphic>();
            Canvas = new Queue<Graphic>();
        }

        public void addGraphic(Graphic graphic)
        {
            Graphics.Enqueue(graphic);
            return;
        }

        /*
        private void cropCanvas()
        {
            // iterate through all graphics in Canvas and remove points that exceed Console Buffer size;
            foreach(Graphic graphic in Canvas)
            {
                graphic.cropPoints(BufferHeight, BufferWidth);
            }

            return;
        }//*/
        
        private void clear()
        {
            Graphics.Clear();
            Canvas.Clear();

            return;
        }

        public void Clear()
        {
            clear();
            paint();
        }

        private void resizeCanvas(int height, int width)
        {
            BufferHeight = height;
            BufferWidth = width;

            Console.SetWindowSize(width, height + 2);
            Console.SetBufferSize(width, height + 2);
            
            return;
        }

        private void mapCanvasToBuffer()
        {
            int x = 0;
            int y = 0;
            int index = 0;

            clearBuffer();
            foreach(Graphic graphic in Graphics) // if done below to get rid of canvas, change this to Graphics.
            {
                x = graphic.GetOrigin().x;
                y = graphic.GetOrigin().y;

                foreach(Point point in graphic.GetPoints())
                { // Opportunity to verify if point is within windowbuffer, instead of doubling the objects.
                    if (point.x + x < BufferWidth && point.y + y < BufferHeight)
                    {
                        index = (y + point.y) * BufferWidth + x + point.x;
                        Buffer[index] = point.c;
                    }
                }
            }

        }

        private void clearBuffer()
        {
            Buffer = new char[BufferHeight * BufferWidth];
            for(int i = 0; i < BufferHeight; i++)
            {
                for (int j=0; j<BufferWidth; j++)
                {
                    Buffer[i * BufferWidth + j] = ' ';
                }
            }

            return;
        }

        /*
        private void copyToCanvas()
        {
            Canvas.Clear();
            foreach(Graphic graphic in Graphics)
            {
                List<Point> points = new List<Point>();
                foreach (Point point in graphic.GetPoints())
                {
                    points.Add(new Point(point));
                }
                Canvas.Enqueue(new Graphic(points, graphic.GetOrigin().x, graphic.GetOrigin().y));
            }
        } // if mapCanvasToBuffer is updated, get rid of this.
        //*/

        public void paint()
        {
            Canvas.Clear();
            //Canvas = new Queue<Graphic>(Graphics);
            //copyToCanvas();
            resizeCanvas(Console.WindowHeight - 2, Console.WindowWidth);
            //cropCanvas();

            Console.Clear();
            mapCanvasToBuffer();

            Console.WriteLine(Buffer);

        }

        public void drawPoint(int x, int y, char c = '+')
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(0, 0, c));
            addGraphic(new Graphic(points, x, y));

            return;
        }

        public void drawRect(int x, int y, int width, int height, char l = '+', char f = ' ')
        {
            
            List<Point> points = new List<Point>();
            for (int j=0; j<width*2; j++)
            {
                points.Add(new Point(j, 0, l));
            }
            for (int i=1; i < height-1; i++)
            {
                points.Add(new Point(0, i, l));

                for (int j=1; j<width*2-1; j++)
                {
                    points.Add(new Point(j, i, f));
                }

                points.Add(new Point(width*2-1, i, l));
            }
            for (int j=0; j<width*2; j++)
            {
                points.Add(new Point(j, height-1, l));
            }

            addGraphic(new Graphic(points, x, y));

            return;

        }

        public void drawCircle(int x, int y, int radius, char l = '+', char f = ' ')
        {
            List<Point> points = new List<Point>();
            double distance;
            int i_distance;
            double jj;
            double ii;
            double rr;
            for(int i = 0; i < radius*2+1; i++)
            {
                for(int j = 0; j < radius*4+1; j++)
                {
                    jj = Convert.ToDouble(j) / 2;
                    ii = Convert.ToDouble(i);
                    rr = Convert.ToDouble(radius);
                    distance = Math.Sqrt((jj - rr) * (jj - rr) + (ii - rr) * (ii - rr));
                    i_distance = Convert.ToInt32(Math.Round(distance));
                    if (distance < rr)
                        points.Add(new Point(j, i, f));
                    if (i_distance == radius)
                        points.Add(new Point(j, i, l));
                }
            }

            addGraphic(new Graphic(points, x, y));

            return;
        }

        public void drawShadedCircle(int x, int y, int radius)
        {
            List<Point> points = new List<Point>();
            double distance;
            int i_distance;
            double jj;
            double ii;
            double rr;
            for (int i = 0; i < radius * 2 + 1; i++)
            {
                for (int j = 0; j < radius * 4 + 1; j++)
                {
                    jj = Convert.ToDouble(j) / 2;
                    ii = Convert.ToDouble(i);
                    rr = Convert.ToDouble(radius);
                    distance = Math.Sqrt((jj - rr) * (jj - rr) + (ii - rr) * (ii - rr));
                    if (distance < rr)
                    {
                        if (distance < rr / 10*2)
                            points.Add(new Point(j, i, 'M'));
                        else if (distance < rr / 10 * 4)
                            points.Add(new Point(j, i, 'X'));
                        else if (distance < rr / 10 * 6)
                            points.Add(new Point(j, i, 'H'));
                        else if (distance < rr / 10 * 7)
                            points.Add(new Point(j, i, '0'));
                        else if (distance < rr / 10 * 8)
                            points.Add(new Point(j, i, 'O'));
                        else if (distance < rr / 10 * 9)
                            points.Add(new Point(j, i, '='));
                        else
                            points.Add(new Point(j, i, '-'));
                    }
                }
            }

            addGraphic(new Graphic(points, x, y));

            return;
        }

        public void moveGraphicDown(int i)
        {
            Graphics.ElementAt(i).MoveDown();
            return;
        }

        public void moveGraphic(int i, int x, int y)
        {
            Graphics.ElementAt(i).reposition(x, y);
        }

    }
}
