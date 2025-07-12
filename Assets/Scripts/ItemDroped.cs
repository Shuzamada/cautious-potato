using UnityEngine;
using UnityEngine.UI;


public class ItemDroped : MonoBehaviour
{

    public int count = 1;
    public int maxCount = 64;
    public string name;
    public Text text;

    void Start()
    {
        // Инициализация объекта, если необходимо
        text.text = name + " " + count.ToString();
        Debug.Log("ItemDroped initialized with count: " + count);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Item"))
        {
            var other = collision.gameObject.GetComponent<ItemDroped>();
            if (other.name != name)
            {
                return;
            }
            int colisionCount = other.count;
            if (colisionCount + count > maxCount)
            {
                int overflow = colisionCount + count - maxCount;
                other.count = maxCount;
                count = overflow;
            }
            else
            {
                if (collision.gameObject.GetInstanceID() > gameObject.GetInstanceID())
                {
                    other.count += count;
                    other.transform.localPosition = (other.transform.localPosition + transform.localPosition) / 2;
                    Destroy(gameObject);
                    Debug.Log("Merged item with count: " + other.count);
                }
                
            }          
        }
    }
    
    void OnMouseEnter()
    {
         text.text = name + " " + count.ToString();
    }
}
