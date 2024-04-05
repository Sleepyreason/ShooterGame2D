using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable
{
    Animator animator;
    Collider2D collider2D;

    [SerializeField] int health;
    [SerializeField] Transform target;
    [SerializeField] int damage;
    

    NavMeshAgent agent;

    public UnityAction onDeath;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    public void OnDamage(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            animator.SetTrigger("Death");
            Destroy(collider2D);
            Destroy(gameObject, 2);
            onDeath.Invoke();
        }

    }
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance){
            animator.SetTrigger("Attack");
            OnAttack();
        }
        else if( !agent.isStopped ){
            animator.SetBool("Run", true);
        }
        agent.SetDestination(target.position);
        
    }
    public void OnAttack(){
        IHittable hittableTarget = target.GetComponent<IHittable>();
        if(hittableTarget != null)
        {

            hittableTarget.OnDamage(damage);
            
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
