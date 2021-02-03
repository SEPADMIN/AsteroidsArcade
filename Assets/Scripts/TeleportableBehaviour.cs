using System;
using UnityEngine;
using Object = System.Object;

public class TeleportableBehaviour
{
    private readonly Transform _transform;
    private readonly Vector2 _assetSize; //world units
    private readonly Vector2 _screenSize;  //world units
    private readonly float _pixelSize; //world units

    public TeleportableBehaviour(Transform transform, int imageWidth, int imageHeight)
    {
        _transform = transform;

        _screenSize = SceneHelper.WorldUnitsInCamera;
        _assetSize = SceneHelper.GetAssetInWorldUnits(imageWidth, imageHeight);
        _pixelSize = SceneHelper.GetPixelInWorldUnits();
    }

    public void CheckBorders() //handle screen borders -- out of camera positioning leads to asset teleport to the opposite scene side 
    {
        if (!GameState.IsActive())
        {
            // GameObject.Destroy(_transform.gameObject);
            return;
        }
        
        float safeShift = _pixelSize; //prevent infinite teleports
        Vector2 currentPosition = _transform.position;
        Vector2 screenHalfSize = _screenSize / 2;
        Vector2 assetHalfSize = _assetSize / 2;

        if (
                Math.Abs(currentPosition.x) > 
                (screenHalfSize.x + assetHalfSize.x)
            )
        {
            if (currentPosition.x > 0)
            {
                currentPosition.x = -screenHalfSize.x - assetHalfSize.x;
                currentPosition.x += safeShift;
            }
            else
            {
                currentPosition.x = screenHalfSize.x + assetHalfSize.x;
                currentPosition.x -= safeShift;
            }
        }
        if (
                Math.Abs(currentPosition.y) > 
                (screenHalfSize.y + assetHalfSize.y)
            )
        {
            if (currentPosition.y > 0)
            {
                currentPosition.y = -screenHalfSize.y - assetHalfSize.y;
                currentPosition.y += safeShift;
            }
            else
            {
                currentPosition.y = screenHalfSize.y + assetHalfSize.y;
                currentPosition.y -= safeShift;
            }
        }
        
        _transform.position = currentPosition;
    }
}
