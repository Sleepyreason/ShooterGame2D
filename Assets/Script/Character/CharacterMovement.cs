using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.IK;

public class CharacterMovement : MonoBehaviour, IHittable
{
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] LimbSolver2D limbLeft;
    [SerializeField] LimbSolver2D rightLeft;
    [SerializeField] Weapon weapon; // Заменил Weapon на weapon, так как в C# имена переменных обычно начинаются с маленькой буквы
    [SerializeField] Transform skelet;
    [SerializeField] int health;
    int MaxHealth;
    private Animator animator;
    [SerializeField] SpriteRenderer HealthBox;

    bool flip = false;

    Rigidbody2D rigidbody2D;
    Collider2D collider2D;
    public void OnShoot()
    {
        if (weapon != null)
        {
            weapon.Shoot();
        }
    }
    void OnTakeWeapon(AssetItem assetItem)
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }
        weapon = Instantiate(assetItem._prefab, transform);
        weapon.transform.localPosition = Vector3.zero;
        limbLeft.weight = weapon.LHandLimb != null ? 1 : 0;
        rightLeft.weight = weapon.RHandLimb != null ? 1 : 0;

    }
    void Start()
    {
        MaxHealth = health;
        animator = GetComponent<Animator>();
        Global.EventManager.OnTakeWeapon.AddListener(OnTakeWeapon);
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed;
        if (horizontalInput == 0 && verticalInput == 0)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Run", true);
        }
        rigidbody2D.velocity = movement;



        if (weapon != null)
        {
            if (weapon.LHandLimb != null)
            {
                limbLeft.GetChain(0).target.transform.position = weapon.LHandLimb.position;
            }
            if (weapon.RHandLimb != null)
            {
                rightLeft.GetChain(0).target.transform.position = weapon.RHandLimb.position;
            }

        }
        if (weapon != null && weapon.HasTarget())
        {
            if (weapon.dir.x < 0 && !flip) Flip();
            else if (weapon.dir.x > 0 && flip) Flip();
        }
        else
        {
            if (movement.x < 0 && !flip) Flip();
            else if (movement.x > 0 && flip) Flip();
            if (weapon != null)
            {
                if (horizontalInput != 0 && verticalInput != 0)
                {
                    weapon.LookIt(movement);
                }
            }

        }
    }


    void Flip()
    {
        skelet.Rotate(0, 180, 0);
        flip = !flip;
    }

    public Transform GetTransform()
    {
        throw new System.NotImplementedException();
    }

     public void OnDamage(int Damage)
    {
        health -= Damage;
        Vector2 currentScale = HealthBox.size;
        
        // Уменьшаем значение координаты x на 0.1
        currentScale.x = (float)health/(float)MaxHealth;

        // Применяем новый масштаб объекту
        HealthBox.size = currentScale;
       // HealthBox.transform.localScale = new Vector2(health/MaxHealth, 1);
        if (health <= 0)
        {   

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
