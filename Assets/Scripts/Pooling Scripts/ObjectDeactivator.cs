using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JoaoMilone
{
    public class ObjectDeactivator : MonoBehaviour
    {
        [Tooltip("The ammount of time that the object will be activated in scene!")]
        [SerializeField]
        private float deactivateTime = 1.5f;

        private void OnEnable()
        {
            Invoke("HideObject", deactivateTime);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        void HideObject()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}
