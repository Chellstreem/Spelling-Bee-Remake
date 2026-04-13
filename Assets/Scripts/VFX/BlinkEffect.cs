using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace VFX
{
    public class BlinkEffect : MonoBehaviour
    {
        [Tooltip("Image component to apply the blink/fade effect to")]
        [SerializeField] private Image _image;
        [Tooltip("Duration of one fade cycle in seconds")]
        [SerializeField] private float _duration = 0.8f;
        [Tooltip("Minimum alpha value reached during blink")]
        [SerializeField] private float _minAlpha = 0.1f;
        [Tooltip("Whether the blink animation should loop indefinitely")]
        [SerializeField] private bool _isLooped = true;
        [Tooltip("Easing function used for the fade tween")]
        [SerializeField] private Ease _ease = Ease.InOutSine;

        private Tween tween;

        private void OnEnable() => StartBlinking();

        public void StartBlinking()
        {
            if (_image == null)
                return;
            tween?.Kill();

            tween = _image.DOFade(_minAlpha, _duration).SetLoops(_isLooped ? -1 : 2, LoopType.Yoyo).SetEase(_ease);
        }

        public void StopBlinking()
        {
            tween?.Kill();
            _image.color = new(_image.color.r, _image.color.g, _image.color.b, 1f);
        }

        private void OnDisable() => StopBlinking();
    }
}
