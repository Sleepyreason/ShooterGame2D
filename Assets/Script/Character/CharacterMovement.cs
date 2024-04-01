using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] LimbSolver2D limbLeft;
    [SerializeField] LimbSolver2D rightLeft;
    [SerializeField] Weapon weapon; // Заменил Weapon на weapon, так как в C# имена переменных обычно начинаются с маленькой буквы
    [SerializeField] Animator animator;
    bool flip = false;

    Rigidbody2D rigidbody2D; 
    Collider2D collider2D; 

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>(); 
        collider2D = GetComponent<Collider2D>(); 
    }

    void Update()
    {
        float horizontalInput = joystick.Horizontal; 
        float verticalInput = joystick.Vertical;     

        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed;
        rigidbody2D.velocity = movement;

        limbLeft.GetChain(0).target.transform.position = weapon.LHandLimb.position;
        rightLeft.GetChain(0).target.transform.position = weapon.RHandLimb.position;

        if (weapon.dir.x < 0 && !flip) Flip();
        else if (weapon.dir.x > 0 && flip) Flip();
    }

    void Flip()
    {
        animator.transform.Rotate(0, 180, 0);
        flip = !flip;
    }
}
