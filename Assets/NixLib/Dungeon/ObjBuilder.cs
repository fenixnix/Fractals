using UnityEngine;

namespace Nixlib {
    public class ObjBuilder:MonoBehaviour {
        public static float tileSize = 5;
        static public Transform AddNode(string name, Transform parent, Vector3 pos = new Vector3()) {
            GameObject go = new GameObject(name);
            go.transform.parent = parent;
            go.transform.localPosition = pos * tileSize;
            return go.transform;
        }

        static public GameObject AddObject(GameObject obj, Vector3 pos, Transform parent) {
            var go = Instantiate(obj, parent);
            go.transform.localPosition = pos * tileSize;
            return go;
        }

        static public GameObject AddObject(GameObject obj, Vector3Int pos, Transform parent) {
            return AddObject(obj, pos, parent);
        }

        public enum FaceToward { S, N, E, W };
        static public void AdjFace(Transform tran, FaceToward face = FaceToward.S) {
            switch(face) {
                case FaceToward.N:
                    tran.Translate(Vector3.left * tileSize);
                    tran.Rotate(new Vector3(0, 180, 0));
                    break;
                case FaceToward.E:
                    tran.Translate(Vector3.left * tileSize);
                    tran.Rotate(new Vector3(0, 90, 0));
                    break;
                case FaceToward.W:
                    tran.Translate(Vector3.left * tileSize);
                    tran.Translate(Vector3.forward * tileSize);
                    tran.Rotate(new Vector3(0, -90, 0));
                    break;
            }
        }

        static public void AdjFace2(Transform tran, FaceToward face = FaceToward.N) {
            switch(face) {
                case FaceToward.N:
                    tran.Translate(Vector3.right * tileSize/2);
                    tran.Translate(Vector3.back * tileSize/2);
                    break;
                case FaceToward.S:
                    tran.Translate(Vector3.left * tileSize/2);
                    tran.Translate(Vector3.forward * tileSize/2);
                    tran.Rotate(new Vector3(0, 180, 0));
                    break;
                case FaceToward.E:
                    tran.Translate(Vector3.left * tileSize/2);
                    tran.Translate(Vector3.back * tileSize / 2);
                    tran.Rotate(new Vector3(0, 90, 0));
                    break;
                case FaceToward.W:
                    tran.Translate(Vector3.right * tileSize/2);
                    tran.Translate(Vector3.forward * tileSize/2);
                    tran.Rotate(new Vector3(0, -90, 0));
                    break;
            }
        }
    }
}
