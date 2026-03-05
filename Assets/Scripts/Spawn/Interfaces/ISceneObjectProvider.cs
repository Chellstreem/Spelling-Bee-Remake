using UnityEngine;

public interface ISceneObjectProvider
{
    public GameObject[] GetObjects(EndgameObjectType objectType);
}
