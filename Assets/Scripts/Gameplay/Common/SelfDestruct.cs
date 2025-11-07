using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float _timer = 1;
    bool _isCounting = true;

    void Update()
    {
        if (_isCounting)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void ResetTimer(float newTimer)
    {
        _timer = newTimer;
        _isCounting = true;
    }
}
