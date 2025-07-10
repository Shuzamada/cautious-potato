using UnityEngine;

public class IslandDestructible : MonoBehaviour
{
    private SpriteRenderer sr;
    private Texture2D tex;
    private Vector2 lastHitPoint; // Для отладки

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tex = Instantiate(sr.sprite.texture);
        tex.Apply();
        sr.sprite = Sprite.Create(tex, sr.sprite.rect, new Vector2(0.5f, 0.5f), sr.sprite.pixelsPerUnit);
    }

    public void ExplodeAt(Vector2 worldPos, float radius)
    {
        lastHitPoint = worldPos;

        // Получаем bounds спрайта
        Bounds bounds = sr.bounds;

        // Преобразуем мировые координаты в локальные координаты спрайта
        Vector2 localPos = transform.InverseTransformPoint(worldPos);

        // Переводим в координаты текстуры с учетом pivot
        Vector2 pivot = sr.sprite.pivot;
        float ppu = sr.sprite.pixelsPerUnit;

        // Правильное преобразование с учетом направления осей
        int texX = Mathf.RoundToInt(pivot.x + localPos.x * ppu);
        int texY = Mathf.RoundToInt(pivot.y + localPos.y * ppu); // Инверсия Y!

        // Проверяем границы
        texX = Mathf.Clamp(texX, 0, tex.width - 1);
        texY = Mathf.Clamp(texY, 0, tex.height - 1);

        // Вырезаем круг
        int r = Mathf.RoundToInt(radius * ppu);

        for (int y = -r; y <= r; y++)
        {
            for (int x = -r; x <= r; x++)
            {
                if (x * x + y * y <= r * r)
                {
                    int px = texX + x;
                    int py = texY + y;

                    if (px >= 0 && px < tex.width && py >= 0 && py < tex.height)
                    {
                        tex.SetPixel(px, py, new Color(0, 0, 0, 0));
                    }
                }
            }
        }

        tex.Apply();
        sr.sprite = Sprite.Create(tex, sr.sprite.rect, new Vector2(0.5f, 0.5f), sr.sprite.pixelsPerUnit);

        // Обновляем коллайдер
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    // Отладочная визуализация
    void OnDrawGizmos()
    {
        if (lastHitPoint != Vector2.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(lastHitPoint, 0.1f);
        }
    }
}
