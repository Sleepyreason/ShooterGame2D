using UnityEngine;

public interface IHittable{
public Transform GetTransform();
public void  OnDamage(int Damage);  

}

