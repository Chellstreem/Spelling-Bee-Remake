using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Units;
using UserInterface;

namespace HealthSystem
{
    public class HealthBar : MainStageBar
    {
        [Header("Health Bar")]
        [SerializeField] private Image _lifeBarImage;
        [SerializeField] private ComplexUnit _unit;
        [SerializeField] private float _transitionSpeed = 5f;
        private Health _health;
        private Coroutine _fillCoroutine;

        private void Start()
        {
            if (_unit.TryGetComponent<IHealth>(out var health))
                _health = health.Health;

            _lifeBarImage.fillAmount = _health.HealthRatio;
            _health.OnHealthChanged += UpdateHealthBarSmoothly;
        }

        private void UpdateHealthBarSmoothly()
        {
            if (!gameObject.activeInHierarchy)
                return;

            float targetFill = _health.HealthRatio;
            _fillCoroutine = StartCoroutine(SmoothFillTransition(targetFill));
        }

        private IEnumerator SmoothFillTransition(float targetFill)
        {
            float startFill = _lifeBarImage.fillAmount;
            float elapsed = 0f;

            while (elapsed < 1f)
            {
                elapsed += Time.deltaTime * _transitionSpeed;
                _lifeBarImage.fillAmount = Mathf.Lerp(startFill, targetFill, elapsed);
                yield return null;
            }

            _lifeBarImage.fillAmount = targetFill;
        }

        private void StopFillTransition()
        {
            if (_fillCoroutine != null)
            {
                StopCoroutine(_fillCoroutine);
                _fillCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            StopFillTransition();

            if (_health != null)
                _health.OnHealthChanged -= UpdateHealthBarSmoothly;
        }
    }
}
