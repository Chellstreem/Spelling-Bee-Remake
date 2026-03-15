using UnityEngine;

namespace CameraControl
{
    public class CameraShakeHandler : IEventSubscriber<OnWordCompleted>
    {
        private readonly EventManager eventManager;
        private readonly CameraShaker cameraShaker;
        private readonly float intensity;
        private readonly float duration;

        public CameraShakeHandler(EventManager eventManager, CameraShaker cameraShaker, CameraConfig cameraConfig)
        {
            this.cameraShaker = cameraShaker;
            this.eventManager = eventManager;
            intensity = cameraConfig.CameraShakeIntensity;
            duration = cameraConfig.CameraShakeDuration;

            SubscribeToEvents();
        }

        public void OnEvent(OnWordCompleted eventData)
        {
            if (eventData.GameplayAction == GameplayActionType.CameraShake)
            {
                cameraShaker.ShakeCamera(intensity, duration);
            }
        }



        private void SubscribeToEvents()
        {

        }
    }
}


