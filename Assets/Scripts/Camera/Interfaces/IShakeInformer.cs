using System;

public interface IShakeInformer
{
    public event Action ShakingStarted;
    public event Action ShakingEnded;
}
