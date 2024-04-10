using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagCellUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    [SerializeField] Image icon;
    AssetItem weapon;
    private bool isHolding = false;
    private bool isLongPress = false;
    private float holdDuration = 0f;
    private float longPressDuration = 1.0f; // Время, после которого считается, что это долгое нажатие
    
    public void Render(AssetItem item) {
        weapon = item;
        icon.sprite = ((IItem)item).UIIcon;   
    }

    public void OnPointerDown(PointerEventData eventData) {
        isHolding = true;
        holdDuration = 0f;
        Invoke("CheckLongPress", longPressDuration);
    }

    public void OnPointerUp(PointerEventData eventData) {
        isHolding = false;
        CancelInvoke("CheckLongPress");
        if (isLongPress) {
            isLongPress = false;
            // Удаление предмета из инвентаря и уничтожение ячейки
            InventoryManager.Instance.RemoveItem(weapon);
            Destroy(gameObject); // Уничтожить эту ячейку
        } else {
            // Просто взять предмет в руку
            Global.EventManager.TakeWeapon(weapon);
        }
    }

    private void CheckLongPress() {
        if (isHolding) {
            isLongPress = true;
        }
    }
}
