using DemiurgEngine.StatSystem;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DemiurgEngine.StatSystem
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class AutoAssignStatAttribute : PropertyAttribute
    {
    }
}