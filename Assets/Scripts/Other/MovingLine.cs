using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLine : MonoBehaviour
{
    [Tooltip("RectTransform of the UI line that will be moved")]
    [SerializeField] RectTransform lineRectTransform;
    private Vector2 originalPosition;
    [Tooltip("Movement speed in UI units per second")]
    [SerializeField] float speed = 200f;
    public float endX = -1237;

    private void Start()
    {
        originalPosition = lineRectTransform.anchoredPosition;
    }

    private void Update()
    {
        lineRectTransform.anchoredPosition -= Vector2.right * speed * Time.deltaTime;


        if (lineRectTransform.anchoredPosition.x <= endX)
        {
            lineRectTransform.anchoredPosition = originalPosition;
        }
    }
}
