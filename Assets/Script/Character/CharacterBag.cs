using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBag : MonoBehaviour
{
  List<AssetItem> Drops = new();
  [SerializeField] BagUI bagUI;
   static public CharacterBag Instance;
  CharacterBag(){
    Instance = this;
  }
  public void RemoveItem(AssetItem item){
    Drops.Remove(item);
  }
  private void OnTriggerEnter2D(Collider2D other)
  {

  }
  private void OnCollisionEnter2D(Collision2D other)
  {
    var drop = other.transform.GetComponent<Drop>();
    if (drop != null)
    {
      Drops.Add(drop.item);
      bagUI.Render(Drops);
      Debug.Log($"Список {Drops.Count}");
      Destroy(drop.gameObject);
    }
  }
}
