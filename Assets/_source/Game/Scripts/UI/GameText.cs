using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Text))]
    internal sealed class GameText : MonoBehaviour
    {
        private Text _selfText;
        private Coroutine _coroutine;

        internal void Awake()
        {
            _selfText = gameObject.GetComponent<Text>();
            gameObject.SetActive(false);
        }

        internal void ShowGameMessage(string message, float timer)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            gameObject.SetActive(true);
            _selfText.text = message;
            if (timer > 0)
            {
                _coroutine = StartCoroutine(HideMessageTimer(timer));
            }
        }

        private void ResetMessageText()
        {
            _selfText.text = "";
            gameObject.SetActive(false);
        }

        private IEnumerator HideMessageTimer(float timer)
        { 
            yield return new WaitForSeconds(timer);
            _coroutine = null;
            ResetMessageText();
        }
    }
}
