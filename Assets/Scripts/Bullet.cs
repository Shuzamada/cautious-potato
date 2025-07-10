using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float explosionRadius = 0.2f;
    public float lifeTime = 5f; // Время жизни пули

    void Start()
    {
        Destroy(gameObject, lifeTime); 
    }
    void Update()
    {
  
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet")) 
        {
            return; // Не уничтожаем пулю
        }
        if (collision.gameObject.CompareTag("Ground")) // У Island1 должен быть тег Ground
        {

            var destructible = collision.gameObject.GetComponent<IslandDestructible>();
            if (destructible != null)
            {
                Debug.Log("Bullet collided with IslandDestructible at: " + collision.contacts[0].point);
                destructible.ExplodeAt(collision.contacts[0].point, explosionRadius);
            }

        }
        Destroy(gameObject);
    }
}
