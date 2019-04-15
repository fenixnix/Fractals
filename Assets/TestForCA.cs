using Nixlib.CellularAutomata;
using Nixlib.Grid;
using UnityEngine;
using UnityEngine.UI;

public class TestForCA : MonoBehaviour
{
    public Text text;
    CellularAutomata2D ca = new CellularAutomata2D();
    Grid2D<int> world = Grid2D<int>.Create(40, 40);

    void Start()
    {
        world.Noise(255, 0.45f);
        ca = new CellularAutomata2D("s45678|b5678");
        Debug.Log(world[-1, -1]);

        string tmp = "";
        for(int y = 0; y < world.Height; y++) {
            for(int x = 0; x < world.Width; x++) {
                if(world[x, y] == 255) {
                    tmp += "#";
                }
                else {
                    tmp += "_";
                }
            }
            tmp += "\n";
        }

        text.text = tmp;
    }

    [ContextMenu("Step")]
    public void Step() {
        world = ca.RunStep(world);
        string tmp = "";
        for(int y = 0; y < world.Height; y++) {
            for(int x = 0; x < world.Width; x++) {
                if(world[x, y] == 255) {
                    tmp += "#";
                }
                else {
                    tmp += "_";
                }
            }
            tmp += "\n";
        }

        text.text = tmp;
    }



}
