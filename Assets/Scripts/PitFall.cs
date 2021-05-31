using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFall : MonoBehaviour
{
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DeathPit"))
        {
            GetComponent<PlayerMovement>().enabled = false;
            if ((rb.transform.position.x - collision.transform.position.x) < 0)
            {
                rb.transform.position += new Vector3(1.69f, 0, 0);
            }
            if ((rb.transform.position.x - collision.transform.position.x) > 0)
            {
                rb.transform.position += new Vector3(-1.69f, 0, 0);
            }
            if ((rb.transform.position.y - collision.transform.position.y) < 0)
            {
                rb.transform.position += new Vector3(0, -0.89f, 0);
            }
            if ((rb.transform.position.y - collision.transform.position.y) > 0)
            {
                rb.transform.position += new Vector3(0, 0.89f, 0);
            }
        }
    }
}
