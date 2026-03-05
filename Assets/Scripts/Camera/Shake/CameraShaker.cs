using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Cameras
{
    public class CameraShaker : ICameraShaker, IInitializable, IDisposable, IShakeInformer
    {
        private readonly CoroutineRunner coroutineRunner;
        private readonly IActiveCamera activeCamera;
        
        private Transform currentCameraTransform;        
        private Coroutine coroutine;

        public event Action ShakingStarted;
        public event Action ShakingEnded;

        public CameraShaker(CoroutineRunner coroutineRunner, IActiveCamera activeCamera)
        {
            this.coroutineRunner = coroutineRunner;  
            this.activeCamera = activeCamera;            
        }

        public void Initialize()
        {            
            UpdateCurrentCamera();
            activeCamera.OnCameraSwitched += UpdateCurrentCamera;
        }

        public void ShakeCamera(float intensity, float duration)
        {
            if (coroutine == null)
            {
                coroutine = coroutineRunner.StartCor(ShakeCoroutine(intensity, duration));
                ShakingStarted?.Invoke();
            }
                
        }

        public void StopShaking()
        {
            if (coroutine != null)
            {
                coroutineRunner.StopCor(coroutine);
                coroutine = null;
                ShakingEnded?.Invoke();
            }
        }

        public void Dispose()
        {
            activeCamera.OnCameraSwitched -= UpdateCurrentCamera;
        }

        private void UpdateCurrentCamera()
        {
            currentCameraTransform = activeCamera.ActiveCamera.transform;            
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