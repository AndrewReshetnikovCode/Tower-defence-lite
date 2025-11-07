using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemiurgEngine.StatSystem
{
    public static class GameObjectRuntimeInitializer
    {
        static List<StatsController> _statControllers = new();
        static bool _initialized = false;

        static List<StatsController> _controllersToInit = new();
        public static void InitStatControllers()
        {
            foreach (var controller in _statControllers)
            {
                controller.Init();
            }

            _controllersToInit.Clear();
            _initialized = true;
        }
        public static void InitStatController(StatsController sc)
        {
            if (_initialized == false)
            {
                _controllersToInit.Add(sc);
                return;
            }
            _statControllers.Add(sc);
            sc.Init();
        }
        public static void RemoveStatsController(StatsController sc)
        {

        }
    }
}