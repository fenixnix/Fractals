using UnityEngine;
using UnityEngine.SceneManagement;

public class UCS : MonoBehaviour
{
    static public void ClearChild(Transform root)
    {
        for (int i = 0; i < root.childCount; i++)
        {
            Destroy(root.GetChild(i).gameObject);
        }
    }

    static public void DisableScene(string id) {
        SetSceneActive(id, false);
    }

    static public void EnableScene(string id) {
        SetSceneActive(id, true);
    }

    static public void SetSceneActive(string id, bool active = true) {
        var root = SceneManager.GetSceneByName(id).GetRootGameObjects();
        foreach(var g in root) {
            g.SetActive(active);
        }
    }
}