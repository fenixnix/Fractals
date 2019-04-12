using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiteLabel : MonoBehaviour
{
    public Text nameText;
    

    public void Init(MapSite site,Sprite flag) {
        nameText.text = gameObject.name;
    }
}
