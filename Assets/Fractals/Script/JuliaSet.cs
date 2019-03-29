using UnityEngine;
//分形的计算机图像及其应用
/*
Julia sets:
c = 0.45, -0.1428
c = 0.285, 0.01
c = 0.285, 0
c = -0.8, 0.156
c = -0.835, -0.2321
c = -0.70176, -0.3842
 */

namespace Fractals {
    public delegate int DrawColorIndex(Vector3 point, int nMax);
    public delegate float TransferFunction(float input);
    public enum TransferFunctionEnum { none, tan, sin, cos }

    public class JuliaSet {
        public float[] pixels;
        static float L = 4;
        public Vector3 c = new Vector3(-1f, .5f);
        public DrawColorIndex drawColorFun;
        TransferFunction traFun;
        public TransferFunctionEnum tf = TransferFunctionEnum.none;

        public void GenerateMandelbrot(int nmax, Vector3 center, float zoomSize, int pixelSize) {
            drawColorFun = MSBColorIndex;
            InitTraFun();
            Generate(nmax, center, zoomSize, pixelSize);
        }

        public void GenerateJulia(int nmax, Vector3 center, float zoomSize, int pixelSize) {
            drawColorFun = JSBColorIndex;
            InitTraFun();
            Generate(nmax, center, zoomSize, pixelSize);
        }

        public void InitTraFun() {
            switch(tf) {
                case TransferFunctionEnum.none:traFun = NoneFunction;break;
                case TransferFunctionEnum.tan:traFun = Mathf.Tan;break;
                case TransferFunctionEnum.sin:traFun = Mathf.Sin;break;
                case TransferFunctionEnum.cos:traFun = Mathf.Cos;break;
            }
        }

        void Generate(int nmax, Vector3 startPoint, float zoomSize, int pixelSize) {
            pixels = new float[pixelSize * pixelSize];
            var dif = zoomSize / (float)pixelSize;
            for(int j = 0; j < pixelSize; j++) {
                var y = startPoint.y-zoomSize/2 + j * dif;
                for(int i = 0; i < pixelSize; i++) {
                    var x = startPoint.x-zoomSize/2 + i * dif;
                    var n = drawColorFun(new Vector3(x, y), nmax);
                    pixels[j * pixelSize + i] = ((float)n)/nmax;
                }
            }
        }
        // -0.89f,-0.25f  0,023f N200
        int MSBColorIndex(Vector3 point, int nMax) {
            float x = 0, y = 0;
            int n = 0;
            for(n = 0; n < nMax; n++) {
                var xx = x * x - y * y + point.x;
                var yy = 2 * x * y + point.y;
                var m = xx*xx + yy*yy;
                if(m > L) break;
                x = xx;
                y = yy;
            }
            return n;
        }

        int JSBColorIndex(Vector3 point, int nMax) {
            int n = 0;
            float x = point.x, y = point.y;
            for(n = 0; n < nMax; n++) {
                var xx = x * x - y * y + c.x;
                var yy = 2 * x * y + c.y;
                var m = traFun(xx*xx) + yy*yy;//TODO:delegate 切换各种三角函数
                if(m > L) break;
                x = xx;
                y = yy;
            }
            return n;
        }

        float NoneFunction(float input) {
            return input;
        }
        
    } 
}
