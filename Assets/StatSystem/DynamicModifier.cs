
using DemiurgEngine.StatSystem;
using UnityEngine;

[CreateAssetMenu(menuName ="Stats/Dynamic modifier")]
public class DynamicModifier : ScriptableObject
{
    public ModifierType type;
    public float value;
    public float interval;
    public float duration;
    
}