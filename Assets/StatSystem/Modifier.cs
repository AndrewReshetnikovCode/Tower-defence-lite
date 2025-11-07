using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum ModifierType
{
    Add,
    PercentAdd
}
namespace DemiurgEngine.StatSystem
{
    [CreateAssetMenu(menuName = "Stats/Modifier")]
    public class Modifier : ScriptableObject
    {
        public ModifierType type;
        public float value;
    }
    
}
