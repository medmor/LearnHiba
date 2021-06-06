using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public float scrollSpeed = 0.5F;
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (Random.Range(0, 1) > .5f)
            scrollSpeed = Random.Range(.5f, 1);
        else
            scrollSpeed = Random.Range(-1, .5f);

    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}