using System;
using Zenject;

public class RockFallHandler : IInitializable, IDisposable
{
    private readonly IRockDropper rockDropper;


    public RockFallHandler(IRockDropper rockDropper)
    {
        this.rockDropper = rockDropper;

    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }
}
