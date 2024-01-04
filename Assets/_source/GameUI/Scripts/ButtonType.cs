using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    [RequireComponent(typeof(Button))]
    internal class ButtonType : MonoBehaviour
    {
        [SerializeField]
        private ButtonTypes _type;

        internal ButtonTypes Type => _type;
    }

    internal enum ButtonTypes
    {
        Start,
        Pause,
        Resume
    }
}
