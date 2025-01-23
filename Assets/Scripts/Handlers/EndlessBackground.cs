using UnityEngine;

public class EndlessBackground : MonoBehaviour
{
    public float speed;

    [SerializeField] Renderer bgRender;

    private void Update()
    {
        bgRender.material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
    }
}
