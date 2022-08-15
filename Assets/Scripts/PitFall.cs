using System.Collections;
using UnityEngine;

public class PitFall : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector3 playerDeathPosition;
    public Vector3 playerOriginalScale;
    public GameObject[] pits;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerOriginalScale = rb.transform.localScale;
        pits = GameObject.FindGameObjectsWithTag("DeathPit");

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Disable player movement then record the position of where the player collided with the pit
        //and find where they are to land in the pit
        GetComponent<PlayerMovement>().enabled = false;
        playerDeathPosition = transform.position;
        Vector3 playerPitPosition;

        //When player collides with a pit, they will fall in and then lose health
        if (collision.CompareTag("DeathPit"))
        {
            
            //Disables all pit colliders in cases where multiple colliders get hit simultaneously
            foreach(GameObject pit in pits)
            {
                pit.gameObject.SetActive(false);
            }

            //If statement moves player into the center of the pit
            if (collision.gameObject.name == "DeathPitLeft")
            {
                playerPitPosition = playerDeathPosition + new Vector3(1.69f, 0, 0);
                StartCoroutine(PlayerPitFall(playerDeathPosition, playerPitPosition));
            }
            else if (collision.gameObject.name == "DeathPitRight")
            {
                playerPitPosition = playerDeathPosition + new Vector3(-1.69f, 0, 0);
                StartCoroutine(PlayerPitFall(playerDeathPosition, playerPitPosition));
            }
            else if (collision.gameObject.name == "DeathPitTop")
            {
                playerPitPosition = playerDeathPosition + new Vector3(0, -1.84f, 0);
                StartCoroutine(PlayerPitFall(playerDeathPosition, playerPitPosition));
            }
            else if (collision.gameObject.name == "DeathPitBottom")
            {
                playerPitPosition = playerDeathPosition + new Vector3(0, .8f, 0);
                StartCoroutine(PlayerPitFall(playerDeathPosition, playerPitPosition));
            }
        }
    }

    IEnumerator PlayerPitFall(Vector3 playerDeathPosition, Vector3 playerPitPosition)
    {
        float current = 0;
        float duration = .3f;

        //Player moves toward the pit portion of the hole before shrinking down
        while (current < duration)  //while loop required to manually count down the lerpValue
        {
            float lerpValue = Mathf.InverseLerp(0, duration, current);
            rb.transform.position = Vector3.Lerp(playerDeathPosition, playerPitPosition, lerpValue);
            current += Time.deltaTime;
            yield return 0;
        }

        current = 0;
        duration = 1f;
        
        //Player then shrinks as they fall down the hole
        while (current < duration)
        {
            float lerpValue = Mathf.InverseLerp(0, duration, current);
            rb.transform.localScale = Vector3.Lerp(playerOriginalScale, new Vector3(0, 0, 0), lerpValue);
            current += Time.deltaTime;
            yield return 0;
        }

        yield return new WaitForSecondsRealtime(1f);

        /*if (playerDeathPosition.x < playerPitPosition.x)
            rb.transform.position = playerDeathPosition + new Vector3(-.4f, 0, 0);
        else if (playerDeathPosition.x > playerPitPosition.x)
            rb.transform.position = playerDeathPosition + new Vector3(.4f, 0, 0);
        else if(playerDeathPosition.y > playerPitPosition.y)
            rb.transform.position = playerDeathPosition + new Vector3(0, .4f, 0);
        else if(playerDeathPosition.y < playerPitPosition.y)
            rb.transform.position = playerDeathPosition + new Vector3(0, -1.4f, 0);*/

        rb.transform.position = GetComponent<CameraController>().PlayerDeathResetPosition;

        rb.transform.localScale = playerOriginalScale;


        foreach (GameObject pit in pits)
        {
            pit.gameObject.SetActive(true);
        }

        GetComponent<PlayerMovement>().enabled = true;

        yield return new WaitForSecondsRealtime(1f);
    }
}
