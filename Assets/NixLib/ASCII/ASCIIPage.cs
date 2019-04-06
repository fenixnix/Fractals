using System;
using ToolBox;

public class ASCIIPage
{
    int W = 1;
    int H = 1;
    char[] data;
    ASCIIColor[] colors;

    public ASCIIPage() { }

    public ASCIIPage(int w, int h, char background = ' ')
    {
        this.W = w;
        this.H = h;
        data = new char[w * h];
        colors = new ASCIIColor[w * h];
        for (int i = 0; i < w * h; i++)
        {
            data[i] = background;
            colors[i] = new ASCIIColor(255, 255, 255, 255);
        }
    }

    public void DrawFrame(int x, int y, int w, int h,ASCIIColor color)
    {
        Set(x, y, (char)201,color);Set(x + 1, y, (char)205,color); Set(x + w-2, y, (char)205,color); Set(x + w-1, y, (char)187,color);
        Set(x, y+1, (char)186,color); Set(x+w-1, y + 1, (char)186,color);
        Set(x, y +h- 2, (char)186,color); Set(x + w-1, y +h-2, (char)186,color);
        Set(x, y+h-1, (char)200,color); Set(x + 1, y+h-1, (char)205,color); Set(x + w - 2, y+h-1, (char)205,color); Set(x + w-1, y+h-1, (char)188,color);
    }

    public void Set(int x, int y, char c)
    {
        Set(Index(x, y), c);
    }

    public void Set(int x, int y, char c,ASCIIColor color)
    {
        Set(Index(x, y), c);
        SetColor(Index(x, y), color);
    }

    public void Set(int index, char c)
    {
        data[index] = c;
    }

    public void SetColor(int x, int y, ASCIIColor color)
    {
        SetColor(Index(x, y), color);
    }

    public void SetColor(int index,ASCIIColor color)
    {
        colors[index] = color;
    }

    public char Get(int x, int y)
    {
        return Get(Index(x,y));
    }

    public char Get(int index)
    {
        return data[index];
    }

    public ASCIIColor GetColor(int x, int y)
    {
        return GetColor(Index(x, y));
    }

    public ASCIIColor GetColor(int index)
    {
        return colors[index];
    }

    public void Draw(int x, int y,ASCIIPage page)
    {
        for (int row = 0; row < page.H; row++)
        {
            for (int col = 0; col < page.W; col++)
            {
                Set(col+x, row+y, page.Get(col, row));
                SetColor(col + x, row + y, page.GetColor(col, row));
            }
        }
    }


    public string GetString()
    {
        return new String(data);
    }

    public string Print()
    {
        string s = "";
        for (int row = 0; row < H; row++)
        {
            for (int col = 0; col < W; col++)
            {
                s += data[col + row * W];
            }
            s += "\n";
        }
        return s;
    }

    int Index(int x, int y)
    {
        return x + y * W;
    }
}

