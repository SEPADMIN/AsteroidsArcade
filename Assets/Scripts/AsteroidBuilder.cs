using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidBuilder : MonoBehaviour
{
    public static readonly float BaseInterval = 5.0f;
    public static readonly float MinInterval = 3.0f;
    public static readonly float PointsOverSecond = 5000.0f; //amount of points to decrease BaseInterval by 1 second
    private static float _spawnInterval = 5.0f;

    public static void SetSpawnInterval(float value)
    {
        if (value > BaseInterval)
        {
            value = BaseInterval;
        }
        else if (value < MinInterval)
        {
            value = MinInterval;
        }
        _spawnInterval = value;
    }
        
    public void Start()
    {
        Spawn();
    }

    public void GameOver()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        AsteroidHelper.Stages stage = (AsteroidHelper.Stages) Random.Range(0, Enum.GetNames(typeof(AsteroidHelper.Stages)).Length);
        int type = Random.Range(0, AsteroidHelper.AsteroidTypesCount);
        GameObject asteroid = Instantiate(ResourcesLoader.GetAsteroid(stage, type), GetRandomSpawnPosition(), Quaternion.identity);

        if (GameState.IsActive())
        {
            Invoke(nameof(Spawn), _spawnInterval);
        }
    }
    
    private Vector2 GetRandomSpawnPosition()
    {
        return SceneHelper.GetSpawnPosition(Random.Range(0, SceneHelper.SpawnPositionsCount));
    }
}
