using System;
using System.Collections;
using UnityEngine;

public class GameSpeed
{
    private readonly CoroutineRunner runner;
    private static float speed;
    private readonly float originalSpeed;
    private readonly float minSpeed;

    public static float Speed => speed;
    private Coroutine speedCoroutine;
    public event Action SpeedChanged;

    public GameSpeed(CoroutineRunner runner, GameConfig gameConfig)
    {
        this.runner = runner;
        speed = gameConfig.GameSpeed;
        originalSpeed = gameConfig.GameSpeed; 
        minSpeed = gameConfig.MinGameSpeed;
    }

    public void ModifySpeedTemporarily(float speedChange, float duration)
    {
        if (speedCoroutine != null)
        {
            runner.StopCor(speedCoroutine);
            speedCoroutine = null;
        }

        speedCoroutine = runner.StartCor(ChangeSpeedWithDuration(speedChange, duration));
    }

    private void ModifySpeed(float speedChange)
    {
        if (speedChange != 0)
        {
            speed = Mathf.Max(minSpeed, speed + speedChange);
            SpeedChanged?.Invoke();
        }                  
    }
    
    private void SetOriginalSpeed()
    {
        if (speed != originalSpeed)
        {
            speed = originalSpeed;
            SpeedChanged?.Invoke();
        }
    }

    private IEnumerator ChangeSpeedWithDuration(float speedChange, float duration)
    {        
        ModifySpeed(speedChange);
        yield return new WaitForSeconds(duration);
        SetOriginalSpeed();        
    }
}
