using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SiteArrangement : MonoBehaviour {
    public Nation nation;
    public GameObject prefab;
    public Collider[] terrainColliders;
    public float ScanLength = 100f;

    [ContextMenu("Arrange")]
    public void Arrange() {
        foreach(var s in nation.sites) {
            var go = Instantiate(prefab, transform);
            go.name = s.id;
            go.transform.localPosition = s.position;

            var pSrc = go.transform.position + new Vector3(0, ScanLength, 0);
            var pDir = new Vector3(0, -ScanLength*2, 0);
            var ray = new Ray(pSrc, pDir);
            Debug.Log(pSrc +"*" +pDir + "*" +ray);
            var hit = new RaycastHit();
            foreach(var c in terrainColliders) {
                if(c.Raycast(ray, out hit, ScanLength * 2)) {
                    go.transform.position = hit.point;
                    Debug.Log(hit.point);
                    break;
                }
            }
            var sc = go.GetComponent<SiteLabel>();
            sc.Init(s, nation.flag);
        }
    }

    [ContextMenu("AutoGenerate")]
    public void Generate() {
        Nation nation = new Nation();
        for(int i = 0; i < transform.childCount; i++) {
            var siteGo = transform.GetChild(i);
            MapSite site = new MapSite();
            site.id = siteGo.name;
            site.position = siteGo.transform.localPosition;
            nation.sites.Add(site);
        }
        AssetDatabase.CreateAsset(nation, "Assets/GenerateNation.asset");
    }

    private void OnDrawGizmos() {
        if(nation == null) return;
        Gizmos.color = Color.green;
        foreach(var p in nation.sites) {
            Gizmos.DrawCube(p.position, Vector3.one);
        }
        Gizmos.color = Color.yellow;
        foreach(var p in nation.paths) {
            for(int i = 0; i < p.sites.Count-1; i++) {
                Gizmos.DrawLine(nation[p.sites[i]].position, nation[p.sites[i + 1]].position);
            }
        }
    }
}
