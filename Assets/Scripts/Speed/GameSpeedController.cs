using System;
using System.Collections;
using UnityEngine;

public class GameSpeedController : IService
{
    private readonly CoroutineRunner runner;
    private readonly float originalSpeed;
    private readonly float minSpeed;
    private Coroutine _speedCoroutine;

    public float CurrentSpeed { get; private set; }
    public event Action OnSpeedChanged;

    public GameSpeedController(CoroutineRunner runner, GameConfig gameConfig)
    {
        this.runner = runner;
        originalSpeed = gameConfig.GameSpeed;
        minSpeed = gameConfig.MinGameSpeed;
        CurrentSpeed = gameConfig.GameSpeed;
    }

    public void ModifySpeedTemporarily(float speedChange, float duration)
    {
        if (_speedCoroutine != null)
        {
            runner.Stop(_speedCoroutine);
            SetOriginalSpeed();
            _speedCoroutine = null;
        }

        _speedCoroutine = runner.StartCoroutine(ChangeSpeedWithDuration(speedChange, duration));
    }

    private void ModifySpeed(float speedChange)
    {
        CurrentSpeed = Mathf.Max(minSpeed, CurrentSpeed + speedChange);
        OnSpeedChanged?.Invoke();
    }

    private void SetOriginalSpeed()
    {
        if (CurrentSpeed != originalSpeed)
        {
            CurrentSpeed = originalSpeed;
            OnSpeedChanged?.Invoke();
        }
    }

    private IEnumerator ChangeSpeedWithDuration(float speedChange, float duration)
    {
        ModifySpeed(speedChange);
        yield return new WaitForSeconds(duration);
        SetOriginalSpeed();
    }
}
