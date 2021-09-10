using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform arms;
    
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

    public void SetRotation(float mouseLookX, float verticalRotation)
    {
        transform.Rotate(0f, mouseLookX, 0f);
        head.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        arms.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
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

    public void Jump(Vector3 gravitation)
    {
        
    }

    public void Reload()
    {
        playerAnimator.SetTrigger("Reload");
    }

    public void RefreshAmmoUI(int ammoCount, int ammoMagazineCount)
    {
        GameManager.instance.MainSceneUI.AmmoText.text = ammoCount.ToString();
        GameManager.instance.MainSceneUI.AmmoMagazineText.text = ammoMagazineCount.ToString();
    }


}
