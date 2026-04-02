using System.Collections;
using UnityEngine;

namespace CameraControl
{
    public class CameraMover
    {
        private readonly CoroutineRunner runner;
        private Coroutine _coroutine;

        public CameraMover(CoroutineRunner runner) => this.runner = runner;

        public void SetState(Transform cameraTransform, CameraState state)
        {
            if (_coroutine != null)
                runner.Stop(_coroutine);

            _coroutine = runner.Run(CameraMovementCoroutine(cameraTransform, state));
        }

        private IEnumerator CameraMovementCoroutine(Transform cameraTransform, CameraState state)
        {
            cameraTransform.GetPositionAndRotation(out Vector3 originalPosition, out Quaternion originalRotation);

            Vector3 targetPosition = state.CameraPosition;
            Quaternion targetRotation = Quaternion.Euler(state.CameraRotation);

            float elapsedTime = 0f;
            try
            {
                while (elapsedTime < state.TransitionDuration)
                {
                    float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(elapsedTime / state.TransitionDuration));
                    Vector3 pos = Vector3.Lerp(originalPosition, targetPosition, t);
                    Quaternion rot = Quaternion.Slerp(originalRotation, targetRotation, t);

                    cameraTransform.SetPositionAndRotation(pos, rot);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
            }
            finally
            {
                cameraTransform.SetPositionAndRotation(targetPosition, targetRotation);
                _coroutine = null;
            }
        }
    }
}

