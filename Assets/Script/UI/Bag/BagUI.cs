using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class BagUI : MonoBehaviour
{
    
    [SerializeField] BagCellUI Prefab;
    private static BagUI instance;

    public static BagUI Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<BagUI>();
            }
            return instance;
        }
    }
   
    public void Render(List<AssetItem> list)
    {
        var ChildCount = transform.childCount;
        for (int i = 0; i < ChildCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        var offSet = 0;
        foreach (var item in list)
        {
            var Button = Instantiate(Prefab, transform);
            Button.transform.position -= new Vector3(offSet, 0, 0);
            offSet += 100;
            Button.Render(item);
            Debug.Log(offSet);
        }
    }

}
