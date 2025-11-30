using UnityEngine;
using UnityEngine.UI;

public class UiTextureScroller : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Vector2 scrollSpeed = new Vector2(0.1f, 0f);

    private Material runtimeMaterial;
    private Vector2 offset;

    private void Awake()
    {
        if (image == null)
            image = GetComponent<Image>();

        // Create a runtime instance so original material is untouched
        runtimeMaterial = Instantiate(image.material);
        image.material = runtimeMaterial;
    }

    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        runtimeMaterial.SetVector("_Offset", new Vector4(offset.x, offset.y, 0, 0));
    }
}
