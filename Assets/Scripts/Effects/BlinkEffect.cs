using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    private Image uiElement;
    private readonly float duration = 0.8f;
    private readonly float minAlpha = 0.1f;
    private readonly bool isLooped = true;

    private Tween tween;

    private void Awake()
    {
        uiElement = GetComponent<Image>();        
    }

    private void OnEnable() => StartBlinking();    

    public void StartBlinking()
    {
        if (uiElement == null) return;
        tween?.Kill();

        tween = uiElement.DOFade(minAlpha, duration)
            .SetLoops(isLooped ? -1 : 2, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void StopBlinking()
    {
        tween?.Kill();        
        uiElement.color = new Color(uiElement.color.r, uiElement.color.g, uiElement.color.b, 1f);
    }

    private void OnDisable() => StopBlinking();    
}
