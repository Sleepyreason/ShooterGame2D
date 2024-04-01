
using UnityEngine;
using UnityEngine.UI;

public class BagCellUI : MonoBehaviour{
    [SerializeField] Image icon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Render(AssetItem item)
    {
        icon.sprite = ((IItem)item).UIIcon;
        
    }
}
