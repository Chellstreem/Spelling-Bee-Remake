using UnityEngine;

public class EndgameObject
{
    public GameObject GameObject {  get; private set; }
    public Vector3 Position { get; private set; }

    public EndgameObject(GameObject gameObject, Vector3 position)
    {
        GameObject = gameObject;
        Position = position;
    }
}
