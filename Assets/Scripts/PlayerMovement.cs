using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public Rigidbody2D playerRigidBody;
    private Vector3 change;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if(change != Vector3.zero)
        {
            MoveCharacter();
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
}
