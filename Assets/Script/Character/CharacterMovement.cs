using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] FixedJoystick  joystick;


    Rigidbody2D rigidbody2D; // Создаем скелет объекта
    Collider2D collider2D; // Создаем косание объекта с другими объектами

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>(); // Получение скелета объекта
        collider2D = GetComponent<Collider2D>(); // Получение соприкосновение объектов

    }

    // Update is called once per frame
   void Update()
    {
        // Получаем ввод от пользователя
        float horizontalInput = joystick.Horizontal; // Ввод по горизонтали (A и D или ← и →)
        float verticalInput = joystick.Vertical;     // Ввод по вертикали (W и S или ↑ и ↓)

        // Вычисляем вектор движения на основе ввода и скорости
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed;
        // Применяем движение к Rigidbody2D
        rigidbody2D.velocity = movement;
        
    }
}
