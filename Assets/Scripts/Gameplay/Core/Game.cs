using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public static Game I { get; private set; }

    [Serializable]
    public class GameState
    {
        public int lives = 10;

        public int enemiesAlive = 0;
    }

    public GameState State = new GameState();

    private void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        Links.Init();
    }
    private void Update()
    {
    }

    public void LoseLife(int amount)
    {
        State.lives -= amount;
        UIController.I?.Refresh();
        if (State.lives <= 0)
        {
            SetTimeScale(0);

            UIController.I?.ShowLooseMenu();
        }
    }
    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }
    public void ForceNextWave()
    {
    }
}
