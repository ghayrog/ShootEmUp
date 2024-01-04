using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace GameUI
{
    internal class ButtonCollection
    {
        private List<Button> _buttons = new();

        internal int ButtonCount => _buttons.Count;

        internal ButtonCollection(ButtonTypes buttonType, Scene scene)
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();

            List<ButtonType> buttonTypeList = new();

            foreach (var gameObj in gameObjects)
            {
                AppendButtonTypes(buttonType, gameObj.transform, buttonTypeList);
            }

            foreach (ButtonType button in buttonTypeList)
            {
                _buttons.Add(button.GetComponent<Button>());
            }
        }

        private void AppendButtonTypes(ButtonTypes buttonType, Transform targetTransform, List<ButtonType> buttonTypeList)
        { 
            ButtonType button = targetTransform.GetComponent<ButtonType>();
            if (button != null && button.Type == buttonType)
            {
                Debug.Log($"Found button of type: {buttonType}");
                buttonTypeList.Add(button);
            }
            foreach (Transform child in targetTransform)
            {
                AppendButtonTypes(buttonType, child, buttonTypeList);
            }
        }

        internal void AddAllListeners(UnityAction listener)
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].onClick.AddListener(listener);
            }
        }

        internal void DeactivateAll()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].gameObject.SetActive(false);
            }
        }

        internal void ActivateAll()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].gameObject.SetActive(true);
            }
        }

        internal void RemoveAllListeners(UnityAction listener)
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i].onClick.RemoveListener(listener);
            }
        }

        internal Button GetButton(int i)
        { 
            return _buttons[i];
        }

    }
}
