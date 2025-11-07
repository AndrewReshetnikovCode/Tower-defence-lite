using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnitState
{
    public TurretGraphicId turretGraphicId;

    public float actualSpeed;
    public float resource;

    [Header("Runtime state")]
    public bool isStunned = false;
    public bool isCasting = false;
    public bool isMoving = false;

    public List<GameAction> buffs;

    [Header("Transform")]
    public Vector3 position;
    public Quaternion rotation;

    [Header("Tower")]
    public Unit currentTurret;
    
    [Header("Turret")]
    public ActionState currentAttack;
    
    [Header("Misc")]
    public Unit target;

    [Header("Health")]
    public float currentHealth;
    public bool isDead = false;
    [Header("Stream")]
    public Unit source;
    [Header("Movement")]
    public ActionState currentMovement;
}