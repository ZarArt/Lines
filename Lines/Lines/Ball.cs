using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines
{
    public struct Ball
    {
        public int x;
        public int y;
        public int color;
        public Ball(int x, int y, int color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
    }
}
