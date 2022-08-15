using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool IsAttacking;
    public GameObject SlashEffectLeft;
    public GameObject SlashEffectRight;
    public GameObject SlashEffectUp;
    public GameObject SlashEffectDown;
    public GameObject Slash;

    // Start is called before the first frame update
    void Start()
    {
        IsAttacking = false;
        SlashEffectLeft = GameObject.Find("SlashEffectLeft");
        SlashEffectLeft.SetActive(false);

        SlashEffectRight = GameObject.Find("SlashEffectRight");
        SlashEffectRight.SetActive(false);

        SlashEffectUp = GameObject.Find("SlashEffectUp");
        SlashEffectUp.SetActive(false);

        SlashEffectDown = GameObject.Find("SlashEffectDown");
        SlashEffectDown.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SlashEffectLeft.SetActive(true);
            Invoke("DeactivateSlash", 0.5f);
            
        }
        
        

    }

    void DeactivateSlash()
    {
        SlashEffectLeft.SetActive(false);
    }
}
