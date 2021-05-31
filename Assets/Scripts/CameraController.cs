using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 camPos;
    public Vector3 newCamPos;
    Rigidbody2D rb;

    void Start()
    {
        camPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("LeftDoorCollider")) 
        {
            newCamPos = Camera.main.transform.position += new Vector3(-53, 0, 0);
            rb.transform.position += new Vector3(-8.5f, 0, 0);
            StartCoroutine(MoveCamera(newCamPos));
        }

        if (collision.CompareTag("RightDoorCollider"))
        {
            newCamPos = Camera.main.transform.position += new Vector3(53, 0, 0);
            rb.transform.position += new Vector3(8.5f, 0, 0);
            StartCoroutine(MoveCamera(newCamPos));
        }

        if (collision.CompareTag("TopDoorCollider"))
        {
            newCamPos = Camera.main.transform.position += new Vector3(0, 32, 0);
            rb.transform.position += new Vector3(0, 8.5f, 0);
            StartCoroutine(MoveCamera(newCamPos));
        }

        if (collision.CompareTag("BottomDoorCollider"))
        {
            newCamPos = Camera.main.transform.position += new Vector3(0, -32, 0);
            rb.transform.position += new Vector3(0, -8.5f, 0);
            StartCoroutine(MoveCamera(newCamPos));
        }
        
    }

    IEnumerator MoveCamera(Vector3 newCamPos)
    {
        float current = 0;
        float duration = 0.75f;

        GetComponent<PlayerMovement>().enabled = false;
        while (current < duration)
        {
            float lerpValue = Mathf.InverseLerp(0, duration, current);
            Camera.main.transform.position = Vector3.Lerp(camPos, newCamPos, lerpValue);
            current += Time.deltaTime;
            yield return 0;
        }
        GetComponent<PlayerMovement>().enabled = true;
        camPos = newCamPos;
    }
}
