using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingTest
{
    class Graphic
    {
        private int x;              // column position from origin (0,0) (Top left)
        private int y;              // row location from origin (0,0) (Top left)
        private List<Point> Points; // a graphic is made up of an array (list) of points
                                    // this is a more memory consumptive way of parsing this
                                    // instead of defining the graphic as a geometric object.
                                    // Somewhat like defining a bitmap, but in ascii.

        public Graphic(List<Point> points, int a = 0, int b = 0)
        {
            Points = points;
            x = a;
            y = b;
        }

        public List<Point> GetPoints()
        {
            return Points;
        }

        public void reposition(int a, int b)
        { x = a; y = b; }

        public void cropPoints(int height, int width)
        {// TODO: Handle negative points (too far up or too far left)
            int count = Points.Count;
            for (int i=count-1; i>=0; i--)
            {
                if (Points[i].x + x >= width || Points[i].y + y >= height)
                { Points.RemoveAt(i); }
            }

            return;
        }

        public Point GetOrigin()
        {
            return new Point(x, y);
        }

        public void MoveRight()
        { x++; }

        public void MoveLeft()
        {
            x--;
            if (x < 0)
                x = 0;
        }

        public void MoveDown()
        { y++; }

        public void MoveUp()
        {
            y--;
            if (y < 0)
                y = 0;
        }
    }
}
