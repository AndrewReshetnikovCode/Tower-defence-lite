using DemiurgEngine.StatSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.StatSystem
{
    public class StatsManager : MonoBehaviour
    {
        public static StatsManager Instance
        {
            get
            {
                    return _instance;
            }
        }
        static StatsManager _instance;
        [SerializeField] StatsDatabase _database;
        List<StatsController> _statsControllers = new();
        private void Awake()
        {
            _instance = this;
        }
        void Start()
        {
            StatsManager foundedStatsManager = GameObject.FindObjectOfType<StatsManager>();
            StatsController[] foundedStatsControllerArray = GameObject.FindObjectsByType<StatsController>(FindObjectsSortMode.None);
            StatView[] foundedStatViewArray = GameObject.FindObjectsOfType<StatView>(true);
            List<Stat> allStats = new();

            if (foundedStatsManager != null && foundedStatsManager != this)
            {
                Destroy(gameObject);
                return;
            }

            foreach (var item in foundedStatsControllerArray)
            {
                if (_statsControllers.Contains(item) == false)
                {
                    _statsControllers.Add(item);
                }
            }

            foreach (var sc in _statsControllers)
            {
                for (int i = 0; i < sc.StatsCount; i++)
                {
                    allStats.Add(sc.GetStat(i));
                }
            }

            foreach (var item in _statsControllers)
            {
                item.Init();
            }

            foreach (var item in foundedStatViewArray)
            {
                item.Init();
            }

            if (_database != null)
            {
                GlobalStats.Init(allStats, _database.globalStats);
                _database.SaveGlobalStatsDefaults();
            }

            DontDestroyOnLoad(gameObject);
        }
        public void OnStatsControllerStarted(StatsController sc)
        {
            if (_statsControllers.Contains(sc) == false)
            {
                _statsControllers.Add(sc);
                sc.Init();
            }
            for (int i = 0; i < sc.StatsCount; i++)
            {
                Stat s = sc.GetStat(i);
                if (s.GetType() != typeof(Stat))
                {
                    GlobalStats.AddCustomStat(s);
                }
            }
        }
        public void OnStatsControllerDestroyed(StatsController sc)
        {
            _statsControllers.Remove(sc);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}