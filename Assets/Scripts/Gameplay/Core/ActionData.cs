using UnityEngine;

[CreateAssetMenu(menuName = "ActionData")]
public class ActionData : ScriptableObject
{
    public string actionName = "NewAction";

    public ActionType type = ActionType.Instant;

    public GameObject graphicPrefab;
    public float graphicDestroyDelay;

    [Header("Common")]
    public float resourceCostPerSecond;
    public float resourceCostOnCast;
    public float duration;
    public float cd;

    [Header("Range / Projectile")]
    public UnitData projectile;
    public bool addDamageByRange = false;
    public float damageByRangeFactor;
    public float range;
    public float projectileSpeed;


    [Header("Buff")] 
    public bool applyToTarget = true;
    public bool applyToCaster = false;

    public float addCd;

    [Header("Streaming action")]
    public UnitData streamingAction;
    public Material material;

    [Header("Cancel conditions")]
    public CancelOn[] cancelOn = new CancelOn[] { CancelOn.Move, CancelOn.Stun, CancelOn.ResourceDepleted };

    [Header("Misc")]
    public bool requiresTarget = true;

    [Header("Attack")]
    public float damage;
    

    [Header("Heal")]
    public float healValue;
    public float increaseHealingPercent;
}
