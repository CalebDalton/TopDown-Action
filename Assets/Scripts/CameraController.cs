using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 camPos;
    public Vector3 newCamPos;

    void Start()
    {
        camPos = transform.position;
    }

    void FixedUpdate()
    {
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftDoorCollider")) 
        {
            newCamPos = Camera.main.transform.position += new Vector3(-53, 0, 0);
        }

        if (collision.CompareTag("RightDoorCollider"))
        {
            newCamPos = Camera.main.transform.position += new Vector3(53, 0, 0);
        }

        if (collision.CompareTag("TopDoorCollider"))
        {
            newCamPos = Camera.main.transform.position += new Vector3(0, 32, 0);
        }

        if (collision.CompareTag("BottomDoorCollider"))
        {
            newCamPos = Camera.main.transform.position += new Vector3(0, -32, 0);
        }
        
        StartCoroutine(MoveCamera(newCamPos));
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftDoorCollider"))
        {
            
        }
    }

    IEnumerator MoveCamera(Vector3 newCamPos)
    {
        float current = 0;
        float duration = 0.5f;
        float lerpValue = 0;

        while (current < duration)
        {
            lerpValue = Mathf.InverseLerp(0, duration, current);
            Camera.main.transform.position = Vector3.Lerp(camPos, newCamPos, lerpValue);
            current += Time.deltaTime;
            yield return 0;
        }
        
        
    }
}
