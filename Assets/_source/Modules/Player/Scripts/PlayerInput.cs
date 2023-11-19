using UnityEngine;
using GameUnits;
using ShootingSystem;

namespace Player
{
    public sealed class PlayerInput : MonoBehaviour
    {

        [SerializeField]
        private StarshipMovement _starshipMovement;

        [SerializeField]
        private WeaponComponent _weapon;

        private float _horizontalDirection;
        private bool _fireRequired = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _fireRequired = true;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _horizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _horizontalDirection = 1;
            }
            else
            {
                _horizontalDirection = 0;
            }
        }

        private void FixedUpdate()
        {
            if (_horizontalDirection != 0) _starshipMovement.Move(_horizontalDirection, 0);
            if (_fireRequired)
            {
                _weapon.Shoot();
                _fireRequired = false;
            }
        }
    }
}
