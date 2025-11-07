using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystemRef;

    private void Reset()
    {
        // Автоприсваивание при добавлении скрипта
        particleSystemRef = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        other.SendMessage("OnParticleHit", SendMessageOptions.DontRequireReceiver);

        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            particleSystemRef.Emit(1);
        }
    }
}
