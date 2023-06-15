using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Rigidbody2D rigid;
    private Vector2 direction;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 targetPoint;
    private bool isMouseReleased = false;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        lineRenderer.enabled = false;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            rigid.velocity = Vector2.zero;
            rigid.gravityScale = 0.1f;

            isMouseReleased = false;
        }
        if (Input.GetMouseButton(0)) 
        {
            endPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if(Input.GetMouseButtonUp(0) && !isMouseReleased)
        {
            isMouseReleased = true;

            direction = endPoint - startPoint;
            direction = direction.normalized;
            rigid.gravityScale = 1f;
            rigid.velocity = direction * 20;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,10);
            if(hit.collider !=null)
            {
                Vector2 posPlayer = transform.position;
                Debug.Log("ban phai diem " + hit.point);
                lineRenderer.enabled = true;
                //DrawLine(transform.position, hit.point);


                DrawLine(startPoint, hit.point);
                StartCoroutine(AnimateLinePoint1());
            }    
        }
    }
    private IEnumerator AnimateLinePoint1()//di chuyen Point1 di
    {
        float startTime = Time.time;

        Vector2 startPosition = lineRenderer.GetPosition(0);
        Vector2 endPosition = lineRenderer.GetPosition(1);

        Vector2 pos = startPosition;
        while (pos != endPosition)
        {
            float t = (Time.time - startTime) / 0.3f;
            pos = (endPoint - startPosition) * t + startPosition;
            //pos = Vector2.Lerp(startPosition, endPosition, t);
            lineRenderer.SetPosition(1, pos);
            yield return null;
        }
    }

    private IEnumerator AnimateLinePoint0()//di chuyen Point0 di
    {
        float startTime = Time.time;

        Vector2 startPosition = lineRenderer.GetPosition(0);
        Vector2 endPosition = lineRenderer.GetPosition(1);

        Vector2 pos = endPosition;
        while (pos != startPosition)
        {
            float t = (Time.time - startTime) / 0.15f;
            pos = Vector2.Lerp(endPosition, startPosition, t);
            lineRenderer.SetPosition(0, pos);
            yield return null;
        }

        //rigid.gravityScale = 1f;
        //rigid.velocity = direction * 20;
    }
    private void DrawLine(Vector2 startPoint,Vector2 endPoint)
    {
        lineRenderer.enabled = true;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1,endPoint);
    }    
}
