using System;
using UnityEngine;

public static class ResourcesLoader
{
    private static Sprite _spaceshipSprite = null;
    private static Sprite _spaceshipFilledSprite = null;
    private static GameObject _explosion = null;
    private static GameObject[][] _asteroids;
    private static GameObject _ufo = null;

    public static void Reload()
    {
        int asteroidStagesCount = Enum.GetNames(typeof(AsteroidHelper.Stages)).Length;
        _asteroids = new GameObject[asteroidStagesCount][];
        for (int i = 0; i < _asteroids.Length; i++)
        {
            _asteroids[i] = new GameObject[AsteroidHelper.AsteroidTypesCount];
        }
    }

    public static GameObject GetAsteroid(AsteroidHelper.Stages stage, int type)
    {
        var obj = _asteroids[(int) stage][type];
        if (obj == null)
        {
            string stageName = stage.ToString().ToLower();
            obj = Resources.Load<GameObject>("Asteroids/asteroid_" + stageName + "_" + (type + 1));
            _asteroids[(int) stage][type] = obj;
        }
        return obj;
    }

    public static Sprite GetFilledSpaceshipSprite()
    {
        var obj = _spaceshipSprite;
        if (obj == null)
        {
            obj = Resources.Load<Sprite>("spaceship_filled");
            _spaceshipSprite = obj;
        }
        return obj;
    }
    
    public static Sprite GetSpaceshipSprite()
    {
        var obj = _spaceshipFilledSprite;
        if (obj == null)
        {
            obj = Resources.Load<Sprite>("spaceship");
            _spaceshipFilledSprite = obj;
        }
        return obj;
    }
    
    public static GameObject GetExplosion()
    {
        var obj = _explosion;
        if (obj == null)
        {
            obj = Resources.Load<GameObject>("Explosion");
            _explosion = obj;
        }
        return obj;
    }
    
    public static GameObject GetUFO()
    {
        var obj = _ufo;
        if (obj == null)
        {
            obj = Resources.Load<GameObject>("ufo");
            _ufo = obj;
        }
        return obj;
    }
}
