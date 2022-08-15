using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public Rigidbody2D playerRigidBody;
    private Vector3 change;
    bool CanDash = true;
    bool PitsActive = true;
    GameObject[] pits;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        pits = GameObject.FindGameObjectsWithTag("DeathPit");
    }

    void FixedUpdate()      //FixedUpdate function is used to register movement so speed is consistent
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if(change != Vector3.zero)
        {
            MoveCharacter();
        }
    }

    void Update()           //Must use Update function to read that player pressed Shift. FixedUpdate doesn't properly read player input
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash)
        {
            StartCoroutine(DashCharacter());
            //StartCoroutine(DashCooldownTimer());
        }
    }

    void MoveCharacter()
    {
        var vectorCheck = new Vector3(change.x, change.y);
        //Checks to make sure movement isn't greater than 1.0, normalizes diagonal movement
        if (vectorCheck.sqrMagnitude > 1)
            vectorCheck.Normalize();

        var correctedMvmt = vectorCheck * playerSpeed * Time.deltaTime;
        playerRigidBody.MovePosition(correctedMvmt + transform.position);
    }

    private IEnumerator DashCharacter()         //DashCharacter function changes playerSpeed value so they move faster for .15 secs
    {
        DisablePits();

        var vectorCheck = new Vector3(change.x, change.y);
        //Checks to make sure movement isn't greater than 1.0, normalizes diagonal movement
        if (vectorCheck.sqrMagnitude > 1)
            vectorCheck.Normalize();

        playerSpeed = 40;
        var correctedMvmt = vectorCheck * playerSpeed * Time.deltaTime;
        playerRigidBody.MovePosition(correctedMvmt + transform.position);
        //CanDash = false;
        yield return new WaitForSeconds(.15f);
        playerSpeed = 10;
        DisablePits();
        yield break;
    }

    IEnumerator DashCooldownTimer()
    {
        yield return new WaitForSeconds(5);
        CanDash = true;
    }

    void DisablePits()          //Disables pits so that player can dash over pits
    {
        PitsActive = !PitsActive;
        foreach (GameObject pit in pits)
            pit.gameObject.SetActive(PitsActive);
    }
}
