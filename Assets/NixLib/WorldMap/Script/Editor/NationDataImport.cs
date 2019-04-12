using UnityEngine;
using UnityEditor;

public class NationDataImport : ScriptableWizard {
    public TextAsset data;
    public string id = "null";
    public Sprite flag;
    public Color textColor = Color.white;

    [MenuItem("Tools/NationImport")]
    static void NationImport() {
        DisplayWizard<NationDataImport>("Nation Import","Quit","Genarate");
    }

    private void OnWizardOtherButton() {
        var stringList = data.text.Split('\n');
        var nation = new Nation();
        nation.id = id;
        nation.flag = flag;
        nation.textColor = textColor;
        foreach(var s in stringList) {
            var i = s.IndexOf('（');
            string NameTextChs = s.Substring(0, i);
            string dataString = s.Substring(i + 1).TrimEnd('）');
            var dataList = dataString.Split('，');
            string id = dataList[0];
            string county = dataList[1];
            var site = new MapSite {
                id = id,
                nameTextChs = NameTextChs,
                county = county,
            };
            Debug.Log(site.Print());
            nation.sites.Add(site);
        }
        AssetDatabase.CreateAsset(nation, "Assets/Map/" + id +  ".asset");
    }
}