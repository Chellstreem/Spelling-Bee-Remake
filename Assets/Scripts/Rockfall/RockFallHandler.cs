using System;
using Zenject;

public class RockFallHandler : IInitializable, IDisposable
{
    private readonly IRockDropper rockDropper;
    private readonly IShakeInformer shakeInformer;

    public RockFallHandler(IRockDropper rockDropper, IShakeInformer shakeInformer)
    {
        this.rockDropper = rockDropper;
        this.shakeInformer = shakeInformer;
    }

    public void Initialize()
    {
        shakeInformer.ShakingStarted += rockDropper.StartRockFall;
        shakeInformer.ShakingEnded += rockDropper.StopRockFall;
    }

    public void Dispose()
    {        
        shakeInformer.ShakingStarted -= rockDropper.StartRockFall;
        shakeInformer.ShakingEnded -= rockDropper.StopRockFall;
    }
}
