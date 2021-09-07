using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{

    private CharacterController charController;
    private Animator playerAnimator;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
    } 

    public void SetPosition(int speed, Vector3 direction)
    {
        charController.Move(direction * speed * Time.deltaTime);
        playerAnimator.SetFloat("Movement", Mathf.Clamp01(direction.magnitude));
    }

    public void StartRunAnim()
    {
        playerAnimator.SetBool("Run", true);       
    }

    public void StopRunAnim()
    {
        playerAnimator.SetBool("Run", false);
    }

    public void ShootAnim(bool isShootDelay)
    {
        playerAnimator.SetBool("Shoot", isShootDelay);
    }
}
