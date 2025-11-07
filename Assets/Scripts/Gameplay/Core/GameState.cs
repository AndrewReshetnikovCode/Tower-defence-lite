using System;
using System.Collections.Generic;

[Serializable]
public class GameState
{
    public int lives = 10;
    public int money;
    public int enemiesAlive = 0;

    public List<Unit> currentWave;
    public float currentWaveStartTime;
    public float currentWaveDuration;

    public float lastSpawnTime;
}
