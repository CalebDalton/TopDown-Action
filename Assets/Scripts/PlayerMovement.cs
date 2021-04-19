using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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
        //Checks to make sure movement isn't greater than 1.0, especially diagonal movement
        if (vectorCheck.sqrMagnitude > 1)
            vectorCheck.Normalize();
        var correctedMvmt = vectorCheck * playerSpeed * Time.deltaTime;
        transform.Translate(correctedMvmt);
    }
}
