using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveWSAD : MonoBehaviour, IPlayerMove
{   
    private Animator playerAnimator;
    private CharacterController charControl;
    private void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        charControl = GetComponent<CharacterController>();
    }

    public void PlayerMove(int speed)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
       
        var moveDirection = (transform.right * x + transform.forward * z);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection *= 2;
            playerAnimator.SetBool("Run", true);
        }
        else
        {
            playerAnimator.SetBool("Run", false);
        } 

        charControl.Move(moveDirection * speed * Time.deltaTime);        

        playerAnimator.SetFloat("Movement", Mathf.Clamp01(moveDirection.magnitude));
    }
}