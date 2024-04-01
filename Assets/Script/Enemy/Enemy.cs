using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable
{
    Animator animator;
    Rigidbody2D rigidbody2D;
    Collider2D collider2D;

    [SerializeField] int health;
    public UnityAction onDeath; 

    public void OnDamage(int Damage)
    {
        health-=Damage;
        if(health <= 0){
            animator.SetTrigger("Death");
            Destroy(collider2D);
            Destroy(rigidbody2D);
            Destroy(gameObject, 2);
            onDeath.Invoke();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
          rigidbody2D = GetComponent<Rigidbody2D>();
          collider2D = GetComponent<Collider2D>();
          animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
