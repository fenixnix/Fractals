using System;

namespace ToolBox
{
    public class ASCIIColor
    {
        byte r = 0;
        byte g = 0;
        byte b = 0;
        byte a = 0;

        public float R
        {
            get
            {
                return r/255.0f;
            }
        }

        public float G
        {
            get
            {
                return g/255.0f;
            }
        }

        public float B
        {
            get
            {
                return b / 255.0f;
            }
        }

        public float A
        {
            get
            {
                return a / 255.0f;
            }
        }

        public ASCIIColor()
        {

        }

        public ASCIIColor(byte r, byte g, byte b, byte a)
        {
            this.r = r; this.g = g; this.b = b; this.a = a;
        }

        public ASCIIColor Lighter()
        {
            return new ASCIIColor(r += 3, g += 6, b += 1, a);
        }

        public ASCIIColor Darker()
        {
            return new ASCIIColor(r -= 3, g -= 6, b -= 1, a);
        }

        static public ASCIIColor RndColor()
        {
            return new ASCIIColor(RandomNum.Bit8(), RandomNum.Bit8(), RandomNum.Bit8(), 255);
        }

        public  static ASCIIColor RndBaseColor()
        {
            int c = RandomNum.Value(0, 3);
            if(c == 0)
            {
                return new ASCIIColor(255, 0, 0, 255);
            }
            if (c == 1)
            {
                return new ASCIIColor(0, 255, 0, 255);
            }

            return new ASCIIColor(0, 255, 255, 255);
        }
    }
}
