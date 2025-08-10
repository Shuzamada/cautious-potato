using UnityEngine;

[System.Serializable]
public class PickupItem : MonoBehaviour
{
    [SerializeField] private ItemInterface template;
    [SerializeField] public ItemInstance itemInstance;
    [SerializeField] public int startCount = 1;
    void Awake()                               // вызывается ДО OnCollisionEnter
    {
        // создаём правильный инстанс однажды при спавне/старте
        if (template is TurretItem turretSO)
        {
            itemInstance = new TurretItemInstance(turretSO, startCount);
            Debug.Log("turret drop");
        }

        else
            itemInstance = new ItemInstance(template, startCount);
    }
    public void Start()
    {
        // ������������� �������, ���� ����������
        Debug.Log("PickupItem initialized with item: " + itemInstance.itemTemplate.Name + " with type: " + itemInstance.itemTemplate.Type + " and count: " + itemInstance.count);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!itemInstance.IsStackable || itemInstance == null)
        {
            return;
        }
        //Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Item"))
        {
            var other = collision.gameObject.GetComponent<PickupItem>();
            ItemInstance otherItem = other.itemInstance;
            if (!otherItem.IsStackable)
            {
                return;
            }
            if (itemInstance.Id != otherItem.Id)
            {
                return;
            }
            int colisionCount = otherItem.count;
            if (colisionCount + itemInstance.count > itemInstance.MaxStackSize)
            {
                int overflow = colisionCount + itemInstance.count - itemInstance.MaxStackSize;
                other.itemInstance.count = itemInstance.MaxStackSize;
                itemInstance.count = overflow;
            }
            else
            {
                if (collision.gameObject.GetInstanceID() > gameObject.GetInstanceID())
                {
                    otherItem.count += itemInstance.count;
                    other.transform.localPosition = (other.transform.localPosition + transform.localPosition) / 2;
                    Destroy(gameObject);
                    Debug.Log("Merged item with count: " + otherItem.count);
                }

            }
        }
    }
}
