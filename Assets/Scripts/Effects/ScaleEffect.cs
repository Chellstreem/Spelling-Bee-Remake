using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ScaleEffect : IScaler
{
    private readonly Dictionary<Transform, Vector3> originalScales = new();

    private Tween activateTween;
    private Tween deactivateTween;
    
    public void ActivateWithScale(Transform target, float duration = 1.5f, float startScale = 0f, Ease easeType = Ease.OutBack)
    {
        activateTween?.Kill();
        Vector3 originalScale = GetOrCacheOriginalScale(target);

        target.localScale = originalScale * startScale;
        target.gameObject.SetActive(true);

        activateTween = target.DOScale(originalScale, duration)
            .SetEase(easeType)
            .OnKill(() =>
            {
                target.gameObject.SetActive(true);
                target.localScale = originalScale;
            });
    }

    public void DeactivateWithScale(Transform target, float duration = 1f, float endScale = 0f, Ease easeType = Ease.InBack)
    {
        deactivateTween?.Kill();
        Vector3 originalScale = GetOrCacheOriginalScale(target);

        deactivateTween = target.DOScale(originalScale * endScale, duration)
            .SetEase(easeType)
            .OnComplete(() => target.gameObject.SetActive(false))
            .OnKill(() =>
         {
             if (target.gameObject.activeSelf)
             {
                 target.gameObject.SetActive(false);
                 target.localScale = originalScale;
             }
         });
    }

    private Vector3 GetOrCacheOriginalScale(Transform target)
    {
        if (!originalScales.TryGetValue(target, out Vector3 originalScale))
        {
            originalScale = target.localScale;
            originalScales[target] = originalScale;
        }
        return originalScale;
    }
}
