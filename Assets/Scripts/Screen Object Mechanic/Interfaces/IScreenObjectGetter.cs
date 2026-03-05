using UnityEngine;

public interface IScreenObjectGetter
{
    public ScreenObject GetObject();
    public void ReturnObject(ScreenObject obj);
}
