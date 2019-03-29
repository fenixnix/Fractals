using UnityEngine;

namespace Fractals {

    public class Partical {
        static Vector3Int[] moveTable = new Vector3Int[6];
        public static void InitMovTab() {
            moveTable[0] = new Vector3Int(1, 0, 0);
            moveTable[1] = new Vector3Int(-1, 0, 0);
            moveTable[2] = new Vector3Int(0, 1, 0);
            moveTable[3] = new Vector3Int(0, -1, 0);
            moveTable[4] = new Vector3Int(0, 0, 1);
            moveTable[5] = new Vector3Int(0, 0, -1);
        }

        public Vector3Int position = new Vector3Int();
        public void RndInit(int r) {
            position.x = Random.Range(-r, r + 1);
            var rest = r - Mathf.Abs(position.x);
            position.y = Random.Range(-rest, rest + 1);
            position.z = rest - position.y;
        }

        public int magnitude => Mathf.Abs(position.x) + Mathf.Abs(position.y) + Mathf.Abs(position.z);

        public void BrownianMotion() {
            int index = Random.Range(0, 6);
            position += moveTable[index];
        }
    }

    public class DLA {
        public bool[,,] pixels;
        public int CubeSize = 100;
        int r0 = 5;

        public void Init(int cs) {
            CubeSize = cs;
            Partical.InitMovTab();
            var size = CubeSize * 2 + 1;
            pixels = new bool[size, size, size];
            SetPixel(Vector3Int.zero);
            r0 = 5;
        }

        public void Step() {
            var r = r0 + 5;
            var rmax = r0 + 10;
            Partical p = new Partical();
            p.RndInit(r);
            while(true) {
                var dist = p.magnitude;
                if(IsSurround(p.position)) {
                    SetPixel(p.position);
                    if(dist > r0) r0 = dist;
                    break;
                }
                if(dist > rmax) {
                    break;
                }
                p.BrownianMotion();
            }
        }

        void SetPixel(Vector3Int pos) {
            var index = pos + new Vector3Int(CubeSize, CubeSize, CubeSize);
            pixels[index.z, index.y, index.x] = true;
        }

        bool GetPixel(Vector3Int pos) {
            var index = pos + new Vector3Int(CubeSize, CubeSize, CubeSize);
            return pixels[index.z, index.y, index.x];
        }

        bool IsSurround(Vector3Int pos) {
            return GetPixel(pos + new Vector3Int(1, 0, 0)) ||
                GetPixel(pos + new Vector3Int(-1, 0, 0)) ||
                GetPixel(pos + new Vector3Int(0, 1, 0)) ||
                GetPixel(pos + new Vector3Int(0, -1, 0)) ||
                GetPixel(pos + new Vector3Int(0, 0, 1)) ||
                GetPixel(pos + new Vector3Int(0, 0, -1));
        }
    }
}