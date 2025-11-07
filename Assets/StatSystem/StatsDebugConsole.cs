using DemiurgEngine.StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDebugConsole : MonoBehaviour
{
    [SerializeField] StatsController _statsController;
    [SerializeField] DynamicModifier _dynamicModifier;
    [ContextMenu("poison")]
    public void ApplyPoison()
    {
        _statsController.GetStat("Health").AddDynamicModifier(_dynamicModifier);
    }
}
