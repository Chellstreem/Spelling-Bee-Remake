using DG.Tweening;
using System;
using UnityEngine;

public class ScreenObjectMover : IScreenObjectMover, IScreenObjectOnTargetPositionInformer
{
    private readonly IScreenObjectGetter objectGetter;    
    private readonly float moveDuration;
    private readonly float pauseDuration;

    private Sequence currentSequence;
    public event Action OnReachingTargetPosition;

    public ScreenObjectMover(IScreenObjectGetter objectGetter, ScreenObjectConfig config)
    {
        this.objectGetter = objectGetter;        
        moveDuration = config.MoveDuration;
        pauseDuration = config.PauseDuration;
    }

    public void ShowScreenObject()
    {
        ScreenObject screenObject = objectGetter.GetObject();
        if (screenObject == null) return;

        GameObject gameObj = screenObject.GameObj;        
        Transform screenObjTransform = gameObj.transform;

        if (currentSequence != null && currentSequence.IsActive())
        {
            currentSequence.Kill();
        }

        gameObj.SetActive(true);
        screenObjTransform.localPosition = screenObject.StartLocalPosition;
        screenObjTransform.localRotation = Quaternion.Euler(screenObject.TargetLocalRotation);
        
        StartMovementSequence(screenObject, screenObjTransform);
    }

    private void StartMovementSequence(ScreenObject screenObject, Transform screenObjectTransform)
    {
        bool returned = false;

        currentSequence = DOTween.Sequence()
            .Append(screenObjectTransform.DOLocalMove(screenObject.TargetLocalPosition, moveDuration))
            .AppendCallback(() => OnReachingTargetPosition?.Invoke())
            .AppendInterval(pauseDuration)
            .Append(screenObjectTransform.DOLocalMove(screenObject.StartLocalPosition, moveDuration))
            .OnKill(() => EnsureReturn(screenObject, ref returned))
            .OnComplete(() => EnsureReturn(screenObject, ref returned));

        currentSequence.Play();
    }

    private void EnsureReturn(ScreenObject screenObject,ref bool returned)
    {
        if (returned) return;
        returned = true;
        screenObject.GameObj.SetActive(false);
        objectGetter.ReturnObject(screenObject);
    }
}
