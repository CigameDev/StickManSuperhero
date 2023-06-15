using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float timer = 0f;
    [SerializeField] Transform player;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        //lineRenderer.SetPosition(0, (Vector2)player.transform.position);
        lineRenderer.SetPosition(0, Vector2.zero);
        lineRenderer.SetPosition(1, new Vector2(3,3));
    }

    private void Start()
    {
        StartCoroutine(AnimateLinePoint1());
    }
    private void Update()
    {
       //lineRenderer.SetPosition(0,(Vector2)player.transform.position);
    }
    private IEnumerator AnimateLinePoint1()//di chuyen Point1 di
    {
        float startTime = Time.time;

        Vector2 startPosition = lineRenderer.GetPosition(0);
        Vector2 endPosition = lineRenderer.GetPosition(1);

        Vector2 pos = startPosition;
        while(pos != endPosition)
        {
            float t =(Time.time - startTime)/2f;
            pos = Vector2.Lerp(startPosition,endPosition,t);
            lineRenderer.SetPosition(1, pos);
            yield return null;
        }
    }    
}

