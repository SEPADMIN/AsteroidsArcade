using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class SceneHelper
{
    public static Vector2 WorldUnitsInCamera; //world units
    public const int SpawnPositionsCount = 6;

    private static Vector2 _worldToPixelAmount; //pixels in a world unit
    private static Camera _camera;
    private static GameObject _spaceship;
    private static Vector2[] _spawnPositions;

    public static void Reload()
    {
        //Finding Pixel To World Unit Conversion Based On Orthographic Size Of Camera
        WorldUnitsInCamera.y = GetCamera().orthographicSize * 2;
        WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

        _worldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
        _worldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;
        
        _spawnPositions = new Vector2[SpawnPositionsCount];
        
        _spawnPositions[0] = ConvertToWorldUnits(new Vector2(Screen.width / 3, Screen.height * 1.2f));
        _spawnPositions[1] = ConvertToWorldUnits(new Vector2(Screen.width / 2, Screen.height * 1.2f));
        _spawnPositions[2] = ConvertToWorldUnits(new Vector2(Screen.width / 3 * 2, Screen.height * 1.2f));
        _spawnPositions[3] = ConvertToWorldUnits(new Vector2(Screen.width * 1.2f, Screen.height / 3));
        _spawnPositions[4] = ConvertToWorldUnits(new Vector2(Screen.width * 1.2f, Screen.height / 2));
        _spawnPositions[5] = ConvertToWorldUnits(new Vector2(Screen.width * 1.2f, Screen.height / 3 * 2));
    }

    public static Vector2 GetSpawnPosition(int index)
    {
        return _spawnPositions[index];
    }
    
    public static Vector2 GetAssetInWorldUnits(float assetWidth, float assetHeight)
    {
        var camera = GetCamera();
        Vector2 assetScreenPoint =
            new Vector2((camera.pixelWidth + assetWidth) / 2, (camera.pixelHeight + assetHeight) / 2);
        Vector2 assetWorldSize = ConvertToWorldUnits(new Vector3(assetScreenPoint.x, assetScreenPoint.y, 0));
        return assetWorldSize;
    }

    public static float GetPixelInWorldUnits()
    {
        return GetAssetInWorldUnits(1, 1).x;
    }

    public static Vector2 ConvertToWorldUnits(Vector2 touchLocation)
    {
        Vector2 returnVec2;

        var position = GetCamera().transform.position;
        returnVec2.x = ((touchLocation.x / _worldToPixelAmount.x) - (WorldUnitsInCamera.x / 2)) +
                       position.x;
        returnVec2.y = ((touchLocation.y / _worldToPixelAmount.y) - (WorldUnitsInCamera.y / 2)) +
                       position.y;

        return returnVec2;
    }

    public static GameObject GetSpaceship()
    {
        if (_spaceship == null)
        {
            _spaceship = GameObject.FindWithTag("Player"); 
        }
        return _spaceship;
    }

    public static int GetRandomMultiplier() //return -1 or 1
    {
        return Random.Range(0, 2) * 2 - 1;
    }
    
    private static Camera GetCamera()
    {
        if (_camera == null)
        {
            _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
        return _camera;
    }
}