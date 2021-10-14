using System;
using UnityEngine;

namespace ZomboTerrain
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private float _gravityFor�e;
        [SerializeField] private float _gravityDetectorSpherRadius;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Transform _head;
        [SerializeField] private Transform _arms;
        [SerializeField] private Transform _groundDetector;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private CharacterController _characterController;

        private Vector3 _gravitation;
        public Action<int> OnChangeAmmo;
        public Action<int> OnChangeMagazineAmmo;
        public Action<Transform> OnChangeRotation = delegate { };

        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Shoot = Animator.StringToHash("Shoot");
        private static readonly int Reload = Animator.StringToHash("Reload");
        private static readonly int Movement = Animator.StringToHash("Movement");
             
        public bool IsGrounded()
        {
            return Physics.CheckSphere(_groundDetector.position, _gravityDetectorSpherRadius, _groundMask);
        }

        public void Gravitation()
        {
            _gravitation.y += _gravityFor�e * Time.deltaTime;

            _characterController.Move(_gravitation * Time.deltaTime);

            if (IsGrounded())
                _gravitation = Vector3.down;
        }

        public void SetPosition(Vector3 direction)
        {
            _characterController.Move(direction * Time.deltaTime);
            _playerAnimator.SetFloat(Movement, Mathf.Clamp01(direction.magnitude));
        }

        public void SetRotation(float mouseLookX, float verticalRotation)
        {
            transform.Rotate(0f, mouseLookX, 0f);
            _head.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
            _arms.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
            OnChangeRotation.Invoke(transform);
        }

        public void StartRunAnim()
        {
            _playerAnimator.SetBool(Run, true);
        }

        public void StopRunAnim()
        {
            _playerAnimator.SetBool(Run, false);
        }

        public void ShootAnim(bool isShootDelay)
        {
            _playerAnimator.SetBool(Shoot, isShootDelay);
        }

        public void Jump(int jumpForce)
        {
            _gravitation.y = Mathf.Sqrt((jumpForce) * -2 * _gravityFor�e);
        }

        public void ReloadAnimation()
        {
            _playerAnimator.SetTrigger(Reload);
        }     
    }
}