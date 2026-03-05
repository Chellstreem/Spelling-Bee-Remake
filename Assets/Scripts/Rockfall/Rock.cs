using System.Collections;
using UnityEngine;
using Zenject;

public class Rock : MonoBehaviour
{
    private IRockReturner returner;
    private float fallSpeed;

    [Inject]
    public void Construct(IRockReturner returner, RockConfig config)
    {
        this.returner = returner;
        fallSpeed = config.RockFallSpeed;
    }

    private void OnEnable()
    {
        StartCoroutine(FallRoutine());
    }

    private IEnumerator FallRoutine()
    {
        while (true)
        {
            float delta = fallSpeed * Time.deltaTime;
            transform.position += Vector3.down * delta;            
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        returner.ReturnRock(gameObject);
    }
}
