using UnityEngine;

namespace Player
{
    internal sealed class PlayerInput
    {
        internal float HorizontalDirection { get; private set; }
        internal bool FireRequired { get; private set; }

        internal PlayerInput()
        {
            HorizontalDirection = 0;
            FireRequired = false;
        }

        internal void ResetFireStatus()
        { 
            FireRequired = false;
        }

        internal void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FireRequired = true;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalDirection = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalDirection = 1;
            }
            else
            {
                HorizontalDirection = 0;
            }
        }
    }
}
