using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Global{


static public class EventManager
{
    static public UnityEvent<AssetItem> OnTakeWeapon = new();
    static public void TakeWeapon(AssetItem Weapon){
        OnTakeWeapon.Invoke(Weapon);
    }
}

}