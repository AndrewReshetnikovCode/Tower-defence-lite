using UnityEngine;

public class SpriteFlipbook : MonoBehaviour
{
    [Header(" адры анимации")]
    public Sprite[] frames;           
    public float frameRate = 10f;     
    public bool loop = true;      

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (frames == null || frames.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= 1f / frameRate)
        {
            timer -= 1f / frameRate;
            currentFrame++;

            if (currentFrame >= frames.Length)
            {
                if (loop)
                    currentFrame = 0;
                else
                    currentFrame = frames.Length - 1; // стоп на последнем кадре
            }

            spriteRenderer.sprite = frames[currentFrame];
        }
    }

    public void Play()
    {
        currentFrame = 0;
        enabled = true;
    }

    public void Stop()
    {
        enabled = false;
    }
}
