using UnityEngine;

[CreateAssetMenu]
public class CoreSettings : ScriptableObject
{
    public LayerMask enemiesMask;

    public ActionData explosionAction;

    public float projectileHitThreshold = 0.2f;
    public float minCd;

    public Transform towerUnpgradeScheme;
    public Transform projectilesContainer;
    public Transform enemiesContainer;
    public Transform towersContainer;
    public Transform VfxContainer;

    
    public float maxPileCountForVisual;
}
