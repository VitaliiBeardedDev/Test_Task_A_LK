using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float gravityValue = -9.81f; // гравитация

    private Vector3 moveDirection;
    private CharacterController controller;
    private Animator anim;
    private float velocityY;  // скорость падения

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // вектор движения
        moveDirection = new Vector3(moveX, 0, moveZ);

        // нормализация 
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        // обычное движение или бег
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        moveDirection *= currentSpeed;

        // локальное движение в глобальное
        moveDirection = transform.TransformDirection(moveDirection);

        // анимация
        anim.SetFloat("MoveX", moveX);
        anim.SetFloat("MoveZ", moveZ);

        // гравитация
        if (controller.isGrounded)
        {
            velocityY = -1f;
        }
        else
        {
            velocityY += gravityValue * Time.deltaTime;
        }

        moveDirection.y = velocityY;

        // двигаем персонажа
        controller.Move(moveDirection * Time.deltaTime);
    }
}
