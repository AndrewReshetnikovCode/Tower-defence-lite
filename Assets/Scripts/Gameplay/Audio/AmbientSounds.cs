using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AmbientSounds : MonoBehaviour
{
    [Header("Звуки для проигрывания")]
    public AudioClip[] clips; // массив звуков

    [Header("Интервал между звуками (в секундах)")]
    public float minDelay = 5f;
    public float maxDelay = 15f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayAmbient());
    }

    IEnumerator PlayAmbient()
    {
        while (true)
        {
            if (clips.Length > 0)
            {
                AudioClip clip = clips[Random.Range(0, clips.Length)];
                audioSource.PlayOneShot(clip);
            }

            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
        }
    }
}
