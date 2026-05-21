using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CameraModule
{
    public class CameraMover
    {
        private CancellationTokenSource _cts;

        public void SetState(Transform cameraTransform, CameraState state)
        {
            _cts?.Cancel();
            _cts?.Dispose();

            _cts = new CancellationTokenSource();
            MoveCameraAsync(cameraTransform, state, _cts.Token).Forget();
        }

        private async UniTaskVoid MoveCameraAsync(Transform cameraTransform, CameraState state, CancellationToken token)
        {
            cameraTransform.GetPositionAndRotation(out Vector3 originalPosition, out Quaternion originalRotation);

            Vector3 targetPosition = state.CameraPosition;
            Quaternion targetRotation = Quaternion.Euler(state.CameraRotation);

            float elapsedTime = 0f;

            try
            {
                while (elapsedTime < state.TransitionDuration)
                {
                    token.ThrowIfCancellationRequested();

                    float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(elapsedTime / state.TransitionDuration));

                    Vector3 pos = Vector3.Lerp(originalPosition, targetPosition, t);
                    Quaternion rot = Quaternion.Slerp(originalRotation, targetRotation, t);

                    cameraTransform.SetPositionAndRotation(pos, rot);

                    elapsedTime += Time.deltaTime;
                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                }
            }
            finally
            {
                if (!token.IsCancellationRequested)
                {
                    cameraTransform.SetPositionAndRotation(targetPosition, targetRotation);
                }
            }
        }
    }
}