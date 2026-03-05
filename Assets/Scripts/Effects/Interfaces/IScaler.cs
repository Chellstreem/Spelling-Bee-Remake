using DG.Tweening;
using UnityEngine;

public interface IScaler
{
    public void ActivateWithScale(Transform target, float duration = 1.5f, float startScale = 0f, Ease easeType = Ease.OutBack);
    public void DeactivateWithScale(Transform target, float duration = 1f, float endScale = 0f, Ease easeType = Ease.InBack);
}
