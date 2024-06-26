using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class AssetItem : ScriptableObject, IItem
{
    string IItem.Name { get => _name; }
    Sprite IItem.UIIcon { get => _uiIcon; }
    
    [SerializeField] private string _name;
    [SerializeField] private Sprite _uiIcon;
    [field:SerializeField] public Weapon _prefab {get;private set;}
    
}
  public class InventoryManager {
    private static InventoryManager instance;

    private InventoryManager() {
    }

    public static InventoryManager Instance {
        get {
            if (instance == null) {
                instance = new InventoryManager();
            }
            return instance;
        }
    }
}
