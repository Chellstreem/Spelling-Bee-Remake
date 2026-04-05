using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class ObjectScaler : IVisualEffectService
    {
        private readonly Dictionary<Transform, Vector3> originalScales = new();

        public void ActivateWithScale(Transform target, float duration = 1f, float startScale = 0f, Ease easeType = Ease.OutBack)
        {
            DOTween.Kill(target);

            Vector3 originalScale = GetOrCacheOriginalScale(target);

            target.localScale = originalScale * startScale;
            target.gameObject.SetActive(true);

            bool completed = false;

            target.DOScale(originalScale, duration)
                .SetEase(easeType)
                .SetId(target)
                .OnComplete(() => completed = true)
                .OnKill(() =>
                {
                    if (!completed)
                    {
                        target.localScale = originalScale;
                        target.gameObject.SetActive(true);
                    }
                });
        }

        public void DeactivateWithScale(Transform target, float duration = 1f, float endScale = 0f, Ease easeType = Ease.InBack)
        {
            DOTween.Kill(target);

            Vector3 originalScale = GetOrCacheOriginalScale(target);

            bool completed = false;

            target.DOScale(originalScale * endScale, duration)
                .SetEase(easeType)
                .SetId(target)
                .OnComplete(() =>
                {
                    completed = true;
                    target.gameObject.SetActive(false);
                    target.localScale = originalScale;
                })
                .OnKill(() =>
                {
                    if (!completed && target.gameObject.activeSelf)
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
}