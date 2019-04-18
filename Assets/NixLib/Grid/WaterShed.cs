using Nixlib.Grid;
using UnityEngine;

public class WaterShed{
    static public Grid2D<T> Run<T>(Grid2D<T> src,Vector2Int pos, T val) {
        Grid2D<T> tmpGrid = Grid2D<T>.Create(src.Width, src.Height);
        WaterFlow<T>(src, tmpGrid, pos, val);
        return tmpGrid;
    }

    static void WaterFlow<T>(Grid2D<T> src, Grid2D<T> dst, Vector2Int pos, T val) {
        if(!dst.Inside(pos.x, pos.y)) return;
        if(!src[pos].Equals(val)) return;
        dst[pos] = val;
        foreach(var dir in Grid2D<T>.ortho) {
            WaterFlow<T>(src, dst, pos + dir, val);
        }
    }
}
