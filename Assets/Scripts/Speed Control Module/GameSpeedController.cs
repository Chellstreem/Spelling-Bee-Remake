using System;
using UnityEngine;

namespace SpeedControlModule
{
    public class GameSpeedController
    {
        private readonly float originalSpeed;
        private readonly float minSpeed;

        public float CurrentSpeed { get; private set; }
        public event Action OnSpeedChanged;

        public GameSpeedController(GameConfig gameConfig)
        {
            originalSpeed = gameConfig.GameSpeed;
            minSpeed = gameConfig.MinGameSpeed;

            CurrentSpeed = gameConfig.GameSpeed;
        }

        public void ModifySpeed(float speedChange)
        {
            CurrentSpeed = Mathf.Max(minSpeed, CurrentSpeed + speedChange);
            OnSpeedChanged?.Invoke();
        }

        public void SetOriginalSpeed()
        {
            if (CurrentSpeed != originalSpeed)
            {
                CurrentSpeed = originalSpeed;
                OnSpeedChanged?.Invoke();
            }
        }
    }
}
