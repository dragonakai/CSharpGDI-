using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingTest
{
    class Point
    {
        public int x;   // horizontal offset from parent graphic object
        public int y;   // vertical offset from parent graphic object
        public char c;  // character to paint with

        public Point(int a, int b, char ch = '.')  // constructor, x, y, paint
        {
            x = a; y = b; c = ch;
        }

        public Point(Point point)  // a copy constructor
        {
            x = point.x;
            y = point.y;
            c = point.c;
        }

        public bool isOutside(int height, int width) // test if point is outside specified distance from origin
        {
            if (x >= width || y >= height)
                return true;
            else
                return false;
        }
    }
}
