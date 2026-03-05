using UnityEngine;

[CreateAssetMenu(fileName = "Endgame Object Collection", menuName = "Scriptable Objects/Endgame Objects/Endgame Object Collection")]
public class EndgameObjectCollection : ScriptableObject
{
    [SerializeField] private EndgameObjectSpawnConfig[] sceneObjects;

    public EndgameObjectSpawnConfig[] SceneObjects => sceneObjects;
}
