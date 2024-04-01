using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBag : MonoBehaviour
{
  List<AssetItem> Drops = new();
  [SerializeField] BagUI bagUI;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

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
