using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScreenObjectPool : IScreenObjectGetter
{
    private readonly ScreenObjectData[] screenObjectDatas;
    private readonly Transform mainCameraTransform;

    private Queue<ScreenObject> screenObjects;

    public ScreenObjectPool(ICameraGetter cameraGetter, ScreenObjectConfig screenObjectConfig)
    {
        mainCameraTransform = cameraGetter.GetCamera(CameraType.MainCamera).transform;
        screenObjectDatas = screenObjectConfig.ScreenObjects;

        InitializePool();
    }

    private void InitializePool()
    {
        screenObjects = new Queue<ScreenObject>();
        foreach (var screenObject in screenObjectDatas)
        {
            GameObject instantiatedObject = Object.Instantiate(screenObject.Prefab, mainCameraTransform);
            instantiatedObject.SetActive(false);

            ScreenObject newScreenObject = new ScreenObject(instantiatedObject, screenObject.StartLocalPosition,
                screenObject.TargetLocalPosition, screenObject.TargetLocalRotation);

            screenObjects.Enqueue(newScreenObject);
        }
    }

    public ScreenObject GetObject() => screenObjects.Dequeue();
    
    public void ReturnObject(ScreenObject screenObject) => screenObjects.Enqueue(screenObject);    
}
