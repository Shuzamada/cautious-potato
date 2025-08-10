using UnityEngine;

public class HandController : MonoBehaviour
{

    [SerializeField] private InventoryManager manager;
    [SerializeField] private Transform handSocket;

    private GameObject equip;


    void Awake()
    {
        if (manager == null)
        {
            manager = FindObjectOfType<InventoryManager>();
        }

    }

    void Start()
    {
        manager.SlotChanged += OnSlotChanged;
    }

    public void OnSlotChanged(ItemInstance item)
    {
        if (item == null)
        {
            Destroy(equip);
            return;
        }
        if (equip != null)
        {
            Destroy(equip);
        }

        if (item.ItemPrefab == null)
        {
            Debug.LogWarning($"� �������� '{item?.itemTemplate.Name}' ����������� ItemPrefab");
            return;
        }
        if (handSocket == null)
        {
            Debug.LogWarning($"��� ���� ������");
            return;
        }
        equip = Instantiate(item.ItemPrefab, handSocket);
        equip.transform.localPosition = Vector3.zero;
        equip.transform.localRotation = Quaternion.identity;
        if (equip.TryGetComponent<Rigidbody>(out var rb3D))
        {
            Destroy(rb3D);
        }
        //equip.tag = "EpuipWeapon";
    }
}
