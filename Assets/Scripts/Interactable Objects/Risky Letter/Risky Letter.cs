
using UnityEngine;

public class RiskyLetter : MonoBehaviour
{
    [SerializeField] private GameObject letterObj;

    private void OnEnable()
    {
        if (!letterObj.activeSelf)
        {
            letterObj.SetActive(true);
        }
    }
}
