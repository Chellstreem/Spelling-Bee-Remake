using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace CameraControl
{
    public class CameraShaker : IInitializable
    {
        private readonly CoroutineRunner coroutineRunner;
        private readonly Camera activeCamera;

        private Transform currentCameraTransform;
        private Coroutine coroutine;

        public event Action ShakingStarted;
        public event Action ShakingEnded;

        public CameraShaker(CoroutineRunner coroutineRunner, Camera activeCamera)
        {
            this.coroutineRunner = coroutineRunner;
            this.activeCamera = activeCamera;
        }

        public void Initialize()
        {
            currentCameraTransform = activeCamera.transform;
        }

        public void ShakeCamera(float intensity, float duration)
        {
            if (coroutine == null)
            {
                coroutine = coroutineRunner.StartCoroutine(ShakeCoroutine(intensity, duration));
                ShakingStarted?.Invoke();
            }

        }

        public void StopShaking()
        {
            if (coroutine != null)
            {
                coroutineRunner.Stop(coroutine);
                coroutine = null;
                ShakingEnded?.Invoke();
            }
        }

        private IEnumerator ShakeCoroutine(float intensity, float duration)
        {
            Vector3 originalPosition = currentCameraTransform.position;
            float elapsed = 0f;
            float shakeInterval = 0.09f; // Задержка между толчками

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                if (elapsed % shakeInterval < Time.deltaTime)
                {
                    float offsetX = UnityEngine.Random.Range(-1f, 1f) * intensity;
                    float offsetY = UnityEngine.Random.Range(-1f, 1f) * intensity;

                    currentCameraTransform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
                }

                yield return null;
            }

            currentCameraTransform.position = originalPosition;
            coroutine = null;
            ShakingEnded?.Invoke();
        }
    }
}