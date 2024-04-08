using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour, IHittable
{
    Animator animator;
    Collider2D collider2D;

    [SerializeField] int health;
    [SerializeField] Transform target;
    [SerializeField] int damage;
    [SerializeField] float attackCooldown = 1f; // Время задержки между атаками
    float lastAttackTime;
    bool dead = false;
    NavMeshAgent agent;
    bool flip = false;
    public UnityAction onDeath;
    [SerializeField] SpriteRenderer HealthMob;
    int MaxHealth;
    public GameObject item;
    public float prob = 15;
    float rnd;
    public void Drop()
    {
        rnd = Random.Range (0,100);
        if (rnd <= prob){
            GameObject Item = Instantiate(item,transform.position,Quaternion.identity);
            Destroy(gameObject, 2);
        }
        else{
            Destroy(gameObject, 2);
        }
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    public void OnDamage(int Damage)
    {
        health -= Damage;
        Vector2 currentScale = HealthMob.size;
        
        currentScale.x = (float)health/(float)MaxHealth;

        HealthMob.size = currentScale;

        if (health <= 0)
        {
            animator.SetTrigger("Death");
            dead = true;
            Destroy(collider2D);
            Drop();
            onDeath.Invoke();
            agent.isStopped = true;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = health;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    /*void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance){
            animator.SetTrigger("Attack");
        }
        else if( !agent.isStopped ){
            animator.SetBool("Run", true);
        }
        agent.SetDestination(target.position);
        
    }
    */
    public void OnAttack()
    {
        IHittable hittableTarget = target.GetComponent<IHittable>();
        if (hittableTarget != null)
        {

            hittableTarget.OnDamage(damage);

        }
    }

    void Update()
    {
        if (dead)
        {
            return;
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {

            // Проверяем, прошло ли достаточно времени с момента предыдущей атаки
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                animator.SetTrigger("Attack");

                lastAttackTime = Time.time;
            }
        }
        else if (!agent.isStopped)
        {
            animator.SetBool("Run", true);
        }
        agent.SetDestination(target.position);
        if (agent.velocity.x < 0 && !flip) Flip();
        else if (agent.velocity.x > 0 && flip) Flip();
    }
    void Flip()
    {
        transform.Rotate(0, 180, 0);
        flip = !flip;
    }
    public Transform GetTransform()
    {
        return transform;
    }
}
