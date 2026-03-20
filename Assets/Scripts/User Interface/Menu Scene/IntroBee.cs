using UnityEngine;

public class IntroBee : MonoBehaviour
{
    private readonly float speed = 25f;
    private readonly float positionX = 29f;

    public bool isLeft;

    private void Start()
    {
        if (transform.position.x >= 29f)
        {
            isLeft = true;
        }
    }

    private void Update()
    {
        float movement = speed * Time.deltaTime;
        Vector3 direction = isLeft ? Vector3.left : Vector3.right;
        transform.position += direction * movement;

        float limit = isLeft ? -positionX : positionX;
        if ((isLeft && transform.position.x <= limit) || (!isLeft && transform.position.x >= limit))
        {
            Destroy(gameObject);
        }
    }
}
