using System;
using UnityEngine;

[Serializable]
public class ActionState
{
    public Unit subject;

    [Header("Ability")]
    public float lastActivasionTime;
    public float cdProgress;

    [Header("Streaming")]
    public float castProgress;

    [Header("Projectile")]
    public Vector3 lastTargetPos;
    public float startTime;

    [Header("Enemy")]
    public float pathProgress;

    [Header("Attack")]
    public float actualRadius;

    [Header("Movement")]
    public Unit target;

}
