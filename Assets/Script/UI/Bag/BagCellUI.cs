
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagCellUI : MonoBehaviour, IPointerClickHandler{
    [SerializeField] Image icon;
    AssetItem weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Render(AssetItem item)
    {
        weapon = item;
        icon.sprite = ((IItem)item).UIIcon;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Global.EventManager.TakeWeapon(weapon);
    }
}
