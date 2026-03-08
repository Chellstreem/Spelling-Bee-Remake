using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControl
{
    public class CameraMover
    {
        private readonly CoroutineRunner runner;
        private readonly Dictionary<CameraStateType, CameraState> stateMap = new();
        private Coroutine _coroutine;

        public CameraMover(CoroutineRunner runner, CameraConfig config)
        {
            this.runner = runner;
            InitializeStateMap(config.States);
        }

        public void SetState(Transform cameraTransform, CameraStateType stateType)
        {
            if (_coroutine != null)
                runner.Stop(_coroutine);

            _coroutine = runner.StartCoroutine(RunCameraMovementCoroutine(cameraTransform, stateType));
        }

        private void InitializeStateMap(CameraState[] states)
        {
            foreach (CameraState gameState in states)
            {
                if (!stateMap.ContainsKey(gameState.State))
                    stateMap[gameState.State] = gameState;
            }
        }

        private IEnumerator RunCameraMovementCoroutine(Transform cameraTransform, CameraStateType stateType)
        {
            cameraTransform.GetPositionAndRotation(out Vector3 originalPosition, out Quaternion originalRotation);

            CameraState state = stateMap[stateType];
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
                cameraTransform.position = targetPosition;
                cameraTransform.rotation = targetRotation;
                _coroutine = null;
            }
        }
    }
}

