using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

namespace DemiurgEngine.StatSystem
{
    public class StatsControllersBeforeGameStartInitializer
    {
        StatsController[] _temp;

        public void InitControllers()
        {
            StatsController[] sc = GameObject.FindObjectsOfType<StatsController>();
            foreach (var item in sc)
            {
                item.Init();
            }

            _temp = sc;
        }
        public List<StatsController> CreateAndFillControllersCollection()
        {
            List<StatsController    > collection = new();
            if (_temp == null)
            {
                _temp = GameObject.FindObjectsOfType<StatsController>();
            }
            return collection;
        }
    }
}