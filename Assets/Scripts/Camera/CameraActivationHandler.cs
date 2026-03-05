using UnityEngine;

namespace Cameras
{
    public class CameraActivationHandler
    {        
        private readonly ICameraSwitcher cameraSwitcher;

        public CameraActivationHandler(ICameraSwitcher cameraSwitcher)
        {
            this.cameraSwitcher = cameraSwitcher;

            this.cameraSwitcher.SwitchCamera(CameraType.MainCamera);
        }
    }
}
