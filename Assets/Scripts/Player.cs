using UnityEngine;

public static class Player
{
    public const float ImmortalityDuration = 2.5f; //seconds, immortality after respawn
    public static int Points { get; private set; }
    public static int Lives { get; private set; }

    private static int _ufoPointsCounter;
    private const int UFOPointsRequired = 1000; //points to spawn next UFO
    private static SpaceshipController _spaceshipController;

    public static void Reload()
    {
        _spaceshipController = GameObject.Find("Spaceship").GetComponent<SpaceshipController>();
        Reset();
    }
    
    public static void AddPoints(int amount)
    {
        Points += amount;
        UIController.SetScore(Points);
        AsteroidBuilder.SetSpawnInterval(AsteroidBuilder.BaseInterval - Points / AsteroidBuilder.PointsOverSecond);
        
        _ufoPointsCounter += amount;
        if (_ufoPointsCounter >= UFOPointsRequired)
        {
            _ufoPointsCounter -= UFOPointsRequired;
            UfoController.Spawn();
        }
    }

    public static void LoseLife()
    {
        Lives--;
        UIController.SetLives(Lives);
        
        _spaceshipController.Disappear();
        
        if (Lives <= 0)
        {
            GameManager.GameOver();
            Reset();
        }
        else
        {
            _spaceshipController.Respawn(2f);
        }
    }

    private static void Reset()
    {
        Points = 0;
        Lives = 4;
        _ufoPointsCounter = 0;
        PlayerState.State = PlayerState.States.Alive;
    }
}