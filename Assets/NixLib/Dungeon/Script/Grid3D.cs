using UnityEngine;

public class Grid3D<T>
{
    T[,,] data;
    Vector3Int size;

    public Grid3D(Vector3Int Size,T value = default(T)) {
        size = Size;
        data = new T[Size.z, Size.y, Size.x];
        FillAll(value);
    }

    public T this[int x, int y, int z] => data[z,y,x];
    public T this[Vector3Int pos] => this[pos.x,pos.y,pos.z];

    public Vector3Int Size => size;

    public void FillAll(T value) {
        Fill(Vector3Int.zero, size, value);
    }

    public void Fill(Vector3Int pos, Vector3Int size, T value) {
        for(int z = 0; z < data.GetLength(0); z++) {
            for(int y = 0; y < data.GetLength(1); y++) {
                for(int x = 0; x < data.GetLength(2); x++) {
                    data[pos.z + z, pos.y + y, pos.x + x] = value;
                }
            }
        }
    }
}
