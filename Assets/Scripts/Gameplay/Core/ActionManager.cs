using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ActionManager : MonoBehaviour
{
    public static ActionManager I { get; private set; }

    public CoreSettings settings;
    public GameState gameState;

    public List<Unit> units = new();


    public FillLevelVisualizer coinsPile;

    private void Awake()
    {
        StartCoroutine(MoneyCrt());
        if (I != null && I != this) Destroy(gameObject);
        else I = this;
    }
    private void Update()
    {
        DoUnits();
    }
    

    public void OnCoinHitsTable()
    {
        gameState.money++;

        coinsPile.UpdateVisual(gameState.money, settings.maxPileCountForVisual);
    }
    IEnumerator MoneyCrt()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (gameState.money > settings.maxPileCountForVisual)
            {
                gameState.money--;
            }
        }
        
    }
    void DoUnits()
    {
        DoSceneobjects();

        foreach (var u in units)
        {

            switch (u.data.type)
            {
                case UnitType.tower_attack:

                    break;
                case UnitType.tower_buff:
                    break;
                case UnitType.enemy:
                    break;
                case UnitType.homing_projectile:
                    SimulateHomingProjectile(u);
                    break;
                case UnitType.homing_projectile_dmg_by_range:
                    SimulateProjectileDmgByRange(u);
                    break;
                case UnitType.streamingAction:
                    SimulateStreamingAction(u);
                    break;
                default:
                    break;
            }

        }

    }
    void DoSceneobjects()
    {
        foreach (var u in units)
        {
            DoUnitTransform(u);
        }
    }
    void DoUnitTransform(Unit unit)
    {
        unit.component.transform.position = unit.state.position;
        unit.component.transform.rotation = unit.state.rotation;
    }
    void SimulateAttackingTower(Unit tower)
    {
        Unit turret = tower.state.currentTurret;
        GameAction attack = turret.data.mainAction;

        Collider2D[] hits = Physics2D.OverlapCircleAll(turret.state.position, turret.state.currentAttack.actualRadius, settings.enemiesMask);

        Unit target = null;
        float maxProgress = 0;
        foreach (var h in hits)
        {
            Unit u = h.GetComponent<Unit>();
            if (u.state.currentMovement.pathProgress > maxProgress)
            {
                maxProgress = u.state.currentMovement.pathProgress;
                target = u;
            }
        }
        if (target == null)
            return;

        if (turret.data.faceTarget)
        {
            RotateTowardsTarget(turret);
        }

        turret.state.target = target;

        float cd = turret.data.mainAction.data.cd;
        float timeFromActivasion = Time.time - attack.state.lastActivasionTime;
        attack.state.cdProgress = Mathf.Clamp01(timeFromActivasion/cd);

        if (attack.state.cdProgress >= 1)
        {
            DoAttack(attack);
        }
    }
    void SimulateHomingProjectile(Unit projectile)
    {
        Vector3 trgP;
        ActionState ms = projectile.state.currentMovement;
        Unit target = ms.target;

        bool targetExists = true;
        if (target == null)
        {
            targetExists = false;
        }

        //если цель перестала существовать на сцене, летим к последней ее позиции
        if (targetExists == false)
        {
            trgP = ms.lastTargetPos;
        }
        else
        {
            trgP = target.state.position;
            ms.lastTargetPos = trgP;
        }

        RotateTowardsTarget(projectile);
        MoveTowardsTarget(projectile);

        if (Vector2.Distance(projectile.state.position, trgP) < settings.projectileHitThreshold)
        {
            units.Remove(projectile);
            DoAttack(projectile.data.mainAction);
            Destroy(projectile.component.gameObject, projectile.data.destroyDelay);
        }
        if (projectile.data.lifetime > 0)
        {
            if (Time.time - ms.startTime > projectile.data.lifetime)
            {
                units.Remove(projectile);
                Destroy(projectile.component.gameObject, projectile.data.destroyDelay);
            }
        }
    }
    void SimulateProjectileDmgByRange(Unit projectile)
    {
        UnitState ps = projectile.state;
        Unit target = ps.target;
        Vector3 trgP = target.state.position;

        bool targetExists = target == null;

        //если цель перестала существовать на сцене, взрываем снаряд
        if (targetExists == false)
        {
            units.Remove(projectile);
            CreateExplosion(ps.position);
            Destroy(projectile.component.gameObject, projectile.data.destroyDelay);
            return;
        }

        RotateTowardsTarget(projectile);
        MoveTowardsTarget(projectile);

        if (Vector3.Distance(ps.position, trgP) < settings.projectileHitThreshold)
        {
            units.Remove(projectile);
            DoAttack(projectile.data.mainAction);
            Destroy(projectile.component.gameObject, projectile.data.destroyDelay);
        }
    }
    void SimulateStreamingAction(Unit u)
    {
        UnitState s = u.state;
        Unit t = s.target;
        GameAction a = u.data.mainAction;

        LineRenderer lr = u.component.GetComponentInChildren<LineRenderer>();
        lr.material = a.data.material;
        lr.SetPosition(0, s.position);
        lr.SetPosition(1, t.state.position);

        a.state.castProgress = Mathf.Clamp01((Time.time - a.state.startTime)/a.data.duration); 
        if (a.state.castProgress >= 1)
        {
            units.Remove(u);

            Destroy(u.component.gameObject, u.data.destroyDelay);
        }
    }
    void MoveTowardsTarget(Unit obj)
    {
        UnitState s = obj.state;

        float currentAcceleration = obj.data.accelerationOverLifeTime * (Time.time - s.currentMovement.startTime);
        s.position += (s.rotation * Vector3.right) * (s.actualSpeed + currentAcceleration) * Time.deltaTime;
    }
    void RotateTowardsTarget(Unit obj)
    {
        UnitState s = obj.state;

        Vector3 tp = s.target.state.position;
        Vector3 dir = (s.position - tp).normalized;

        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        s.rotation = Quaternion.Euler(0, 0, z);
    }
    void DoAttack(GameAction action)
    {
        Unit p = null;

        switch (action.data.type)
        {
            case ActionType.Stream:
                p = new Unit() { data = action.data.projectile, state = new() };
                p.component = Instantiate(p.data.prefab, settings.projectilesContainer.transform).GetComponent<UnitComponent>();
                p.component.transform.position = (action.state.subject.state.position + action.state.subject.state.target.state.position) / 2;
                
                p.state.target = action.state.subject.state.target;

                units.Add(p);
                break;
            case ActionType.Projectile:
                p = new Unit() { data = action.data.streamingAction, state = new() };
                p.component = Instantiate(p.data.prefab, settings.projectilesContainer.transform).GetComponent<UnitComponent>();
                p.component.transform.position = action.state.subject.state.position; ;

                p.state.target = action.state.subject.state.target;

                units.Add(p);
                break;
            case ActionType.Instant:
                DoDamage(action);
                break;
            default:
                break;
        }

    }
    void DoDamage(GameAction action)
    {
        Unit t = action.state.subject.state.target;
        t.state.currentHealth -= action.data.damage;
        if (t.state.currentHealth <= 0)
        {
            t.state.currentHealth = 0;
            Die(t);
        }
    }
    void DoHeal(GameAction heal)
    {
        RecalculateHealValue(heal, heal.state.subject);
        heal.state.target.state.currentHealth += heal.data.healValue;

        if (heal.state.target.state.currentHealth <= 0)
        {
            heal.state.target.state.currentHealth = 0;
            Die(heal.state.target);
        }
    }
    void Die(Unit unit)
    {
        units.Remove(unit);

        Destroy(unit.component.gameObject, unit.data.destroyDelay);
    }
    void CreateProjectile()
    {

    }
    void RecalculateTurret(Unit turret)
    {
        GameAction attack = turret.data.mainAction;
        float actualAttackCd = attack.data.cd;
        foreach (var b in turret.state.buffs)
        {
            actualAttackCd += b.data.addCd; 
        }
        

    }
    void RecalculateHealValue(GameAction heal, Unit owner)
    {
        float actualValue = heal.data.healValue;
        float percentIncrease = 0;
        foreach (var b in owner.state.buffs)
        {
            percentIncrease += b.data.increaseHealingPercent;
        }
        actualValue += actualValue * percentIncrease;
    }
    void ApplyBuff(GameAction buff, Unit target)
    {
        target.state.buffs.Add(buff);
    }
    void CreateExplosion(Vector3 pos)
    {
        DamageCircle(pos, settings.explosionAction.range);

        GameObject go = Instantiate(settings.explosionAction.graphicPrefab, settings.projectilesContainer);
        go.transform.position = pos;
        Destroy(go, settings.explosionAction.graphicDestroyDelay);
    }
    void DamageCircle(Vector3 pos, float radius)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, radius, settings.enemiesMask);
        foreach (var item in hits)
        {
            DoAttack(new GameAction() { data = settings.explosionAction, state = new ActionState() });
        }
    }
}

