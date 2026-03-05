using UnityEngine;

public class EndgameObjectActivator : IEndgameObjectActivator
{
    private readonly IEndgameObjectGetter objectGetter;

    public EndgameObjectActivator(IEndgameObjectGetter objectGetter)
    {  
        this.objectGetter = objectGetter;
    }

    public void ActivateObject(EndgameObjectType objectType)
    {
        EndgameObject endgameObject = objectGetter.GetObject(objectType);
        GameObject gameObject = endgameObject.GameObject;
        gameObject.transform.position = endgameObject.Position;
        gameObject.SetActive(true);
    }    
}
