using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform arms;
    [SerializeField] private Transform groundDetector;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravityForñe;


    public UnityAction<bool> OnGroundDetectionState;
    private CharacterController charController;    
    private Animator playerAnimator;
    private Vector3 gravitation;
    private bool isGrounded;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        GroundDetectionStateProvider();
        Gravitation();
    }

    public void GroundDetectionStateProvider()
    {
        isGrounded = Physics.CheckSphere(groundDetector.position, 0.3f, groundMask);
        OnGroundDetectionState?.Invoke(isGrounded);
    }

    public void Gravitation()
    {
        gravitation.y += gravityForñe * Time.deltaTime;

        charController.Move(gravitation * Time.deltaTime);

        if (isGrounded)
            gravitation = Vector3.down;
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

    public void Jump(int jumpForce)
    {
        gravitation.y = Mathf.Sqrt((jumpForce) * -2 * gravityForñe);
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
