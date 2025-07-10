using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Параметры движения")]
    public float speed = 2f; // Скорость движения персонажа
    public float jumpForce = 10f; // Сила вертикального прыжка
    public float diagonalJumpAngle = 60f; // Угол диагонального прыжка (в градусах)
    public float diagonalJumpMultiplier = 1.5f; // Множитель силы диагонального прыжка

    private bool isGrounded = false; // Находится ли персонаж на земле
    private Rigidbody2D rb;
    private bool facingRight = true; // Смотрит ли персонаж вправо


    public WeaponController weaponController; // Ссылка на контроллер оружия

    void Start()
    {
        speed = 3;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Горизонтальное движение
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveHorizontal * speed, rb.linearVelocity.y);

        // Разворот персонажа
        if (moveHorizontal > 0 && !facingRight)
            Flip();
        else if (moveHorizontal < 0 && facingRight)
            Flip();

        // Прыжок вверх
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, касается ли персонаж земли
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Если персонаж перестал касаться земли
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
