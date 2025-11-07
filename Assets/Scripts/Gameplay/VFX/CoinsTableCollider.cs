using UnityEngine;


public class CoinsTableCollider : MonoBehaviour
{
    [SerializeField] Animator _a;
    [SerializeField] Animator _a1;
    public void OnParticleHit()
    {
        _a.SetTrigger("Bump");
        _a1.SetTrigger("Bump");
        ActionManager.I.OnCoinHitsTable();
    }
}
