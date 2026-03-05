using System;

public class OnWordCompleted : IEvent
{
    public GameplayActionType GameplayAction { get; }

    public OnWordCompleted()
    {
        int amount = Enum.GetValues(typeof(GameplayActionType)).Length;
        int randomNUmber = UnityEngine.Random.Range(0, amount);

        switch (randomNUmber)
        {
            case 0:
                GameplayAction = GameplayActionType.CameraShake;
                break;
            case 1:
                GameplayAction = GameplayActionType.Missiles;
                break;           
        }
    }
}
