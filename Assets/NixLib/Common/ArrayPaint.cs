using Geometric;
using System;
using UnityEngine;

namespace NixLib.Common {
    public class ArrayPaint<T> {
        static public void Rect(T[,] array, int x, int y, T data, int size = 3) {
            for(int row = -size; row < size + 1; row++) {
                for(int col = -size; col < size + 1; col++) {
                    array[y + row, x + col] = data;
                }
            }
        }

        static public void Diamond(T[,] array, int x, int y, T data, int size = 3) {
            for(int row = -size; row < size + 1; row++) {
                for(int col = -size; col < size + 1; col++) {
                    int dis = Math.Abs(row) + Math.Abs(col);
                    if(dis <= size) {
                        array[y + row, x + col] = data;
                    }
                }
            }
        }

        static public void Line(T[,] array, Line2DInt line, T data, int width = 0) {
            Diamond(array, line.a.x, line.a.y, data, width);
            Diamond(array, line.b.x, line.b.y, data, width);

            Vector2 dif = new Vector2(line.Dir.x, line.Dir.y);

            int xLength = Math.Abs(line.Dir.x);
            for(int i = 0; i < xLength; i++) {
                for(int w = -width; w <= width; w++) {
                    array[line.a.y + (int)(i * dif.y / xLength) + w,
                        line.a.x + (int)(i * dif.x / xLength)] = data;
                }
            }

            int yLength = Math.Abs(line.Dir.y);
            for(int i = 0; i < yLength; i++) {
                for(int w = -width; w <= width; w++) {
                    array[line.a.y + (int)(i * dif.y / yLength),
                        line.a.x + (int)(i * dif.x / yLength) + w] = data;
                }
            }
        }
    }
}
