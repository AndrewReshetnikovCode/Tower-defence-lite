using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu]
public class UnitData : ScriptableObject
{
    public UnitState InitialState;

    public GameObject prefab;
    public GameAction mainAction;

    [Header("AI")]
    public UnitType type;

    [Header("Base stats")]
    public float baseAttackSpeed = 1;
    public float baseDamage = 10;
    public float maxResource = 100;
    public float baseMoveSpeed = 5;
    public float accelerationOverLifeTime = 0;

    public List<ActionData> buffs;

    public Unit turret;

    [Header("Projectile/Turret")]
    public bool faceTarget;

    public float lifetime = -1;
    public float destroyDelay;

    [Header("Stream")]
    public Material lineMaterial;
    public float lineWidth = 0.05f;

    [Header("Health")]
    public float maxHealth;
}
