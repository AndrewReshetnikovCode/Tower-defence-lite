using DemiurgEngine.StatSystem;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class StatLinkFiller
{
    public static void InitializeStats(GameObject gameObject, StatsController statsController)
    {
        var components = gameObject.GetComponents<MonoBehaviour>();

        foreach (var component in components)
        {
            foreach (var field in GetStatFields(component))
            {
                var stat = field.GetValue(component) as Stat;
                
                var statName = field.Name;

                Stat statEntry = statsController.GetStat(statName);
                    
                field.SetValue(component, statEntry);
            }
        }
    }
    static FieldInfo[] GetStatFields(MonoBehaviour component)
    {
        if (component == null)
        {
            Debug.Log("Component null!");
        }
        var allFields = component.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        List<FieldInfo> statFields = new(allFields.Length);
        foreach (var field in allFields)
        {
            AutoAssignStatAttribute statAttribute = field.GetCustomAttribute<AutoAssignStatAttribute>();
            if (statAttribute != null)
            {
                statFields.Add(field);
            }
        }
        return statFields.ToArray();
    }

}