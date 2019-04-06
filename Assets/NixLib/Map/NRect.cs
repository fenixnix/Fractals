using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.Map
{
    public class NRect
    {
        int x = 0;
        int y = 0;
        int w = 1;
        int h = 1;

        public NRect(int dx, int dy, int dw, int dh)
        {
            x = dx;
            y = dy;
            w = dw;
            h = dh;
        }

        public int X { get { return x; }}
        public int Y { get { return y; }}
        public int W { get { return w; }}
        public int H { get { return h; }}

        public NRect RndInside()
        {
            var dw = RandomNum.Value((int)(w / 2), w);
            var dh = RandomNum.Value((int)(h / 2), h);
            var dx = x + RandomNum.Value(0, w - dw);
            var dy = y + RandomNum.Value(0, h - dh);
            return new NRect(dx, dy, dw, dh);
        }
    }
}
