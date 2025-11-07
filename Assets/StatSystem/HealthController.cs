using UnityEngine;
using DemiurgEngine.StatSystem;
using System;
using System.Collections;
using UnityEngine.Events;
using Unity.VisualScripting;

[RequireComponent(typeof(StatsController))]
public class HealthController : MonoBehaviour
{
    public UnityEvent<float> onHealthChanged;
    public UnityEvent<float, object> onDamageApplied;
    public UnityEvent onDeath;

    [AutoAssignStat]
    Stat _health;

    public float CurrentHealth { get => _health.CurrentValue; set { _health.SetCurrentValue(value, true); } }
    public float MaxHealth { get => _health.BaseValue; set { _health.SetBaseValue(value, true); } }


    [SerializeField] float _invulnerabilityTime;
    float _lastAttackTime;
     
    [SerializeField] float _regenInterval;
    [Tooltip("From 0 to 1")]
    [SerializeField] float _regenPercent;
    float _lastRegenTime;
    public void Start()
    {
        _health.onChange += OnHealthChange;
    }
    private void Update()
    {
        if (Time.time - _lastRegenTime > _regenInterval)
        {
            _health.AddCurrentValue(_health.BaseValue * (_regenPercent/100), true);
            _lastRegenTime = Time.time;
        }
    }
    void OnHealthChange(float current, float max)
    {
        onHealthChanged?.Invoke(current/max);
    }
    public void ApplyDamage(float damage)
    {
        ApplyDamage(damage, null);
    }
    public void ApplyDamage(float damage, object source)
    {
        if (Time.time - _lastAttackTime < _invulnerabilityTime)
        {
            return;
        }
        _lastAttackTime = Time.time;

        if (damage >= CurrentHealth)
        {
            CurrentHealth = 0;
        }
        else
        {
            CurrentHealth -= damage;
        }

        onDamageApplied?.Invoke(_health.CurrentValue, source);

        if (CurrentHealth == 0)
        {
            OnZeroHP(source);
        }
    }

    public virtual void OnZeroHP(object source)
    {
        onDeath.Invoke();
        EventBus.Publish(new UnitKilledArgs() { unit = gameObject });
    }
}