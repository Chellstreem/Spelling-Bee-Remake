using UnityEngine;

[CreateAssetMenu(fileName = "Rock Collection", menuName = "Scriptable Objects/Rocks/Rock Collection")]
public class RockCollection : ScriptableObject
{
    [SerializeField] private GameObject[] rocks;

    public GameObject[] Rocks => rocks;
}
