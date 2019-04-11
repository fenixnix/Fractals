using UnityEngine;

namespace NixLib {
    public static class TextureToFile {
        public static void Save(Texture2D texture, string fileName = "Assets/ScreenShot.png") {
            byte[] bytes = texture.EncodeToPNG();
            System.IO.File.WriteAllBytes(fileName, bytes);
        }
    }
}
