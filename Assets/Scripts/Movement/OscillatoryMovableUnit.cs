using UnityEngine;
using System.Collections;

namespace Movement
{
    public class OscillatoryMovableUnit : MovableUnit
    {
        [Tooltip("How the speed changes compared to other objects.")]
        [SerializeField] private float _speedDelta = 0;
        [SerializeField] private AnimationCurve _verticalCurve;
        [SerializeField] private float _amplitude = 1f;
        [SerializeField] private float _frequency = 1f;

        private void OnEnable() => StartMoving();
        private void OnDisable() => StopMoving();

        protected override IEnumerator MoveCoroutine()
        {
            float time = 0f;

            // базовая позиция (по которой движемся вперед)
            Vector3 basePosition = transform.position;

            while (true)
            {
                float speed = _speedController.CurrentSpeed + _speedDelta;
                float deltaTime = Time.deltaTime;

                time += deltaTime;

                // движение вперёд
                basePosition += _config.MoveDirection * (speed * deltaTime);

                // вычисляем вертикальное смещение
                float curveValue = _verticalCurve.Evaluate(time * _frequency);
                float yOffset = curveValue * _amplitude;

                // итоговая позиция
                Vector3 finalPosition = basePosition;
                finalPosition.y += yOffset;

                if (finalPosition.z <= _config.ReturnThreshold)
                {
                    StopMoving();
                    _pool.ReturnObject(gameObject);
                    yield break;
                }

                transform.position = finalPosition;

                yield return null;
            }
        }
    }
}





