using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class BoxGrabble : MonoBehaviour, IGrabble
    {
        [SerializeField] private Rigidbody _rb;

        public void OnGrab()
        {
            Debug.Log($"Схватили: ${_rb.gameObject.name}");
            _rb.isKinematic = true;
        }

        public void OnRelease()
        {
            Debug.Log($"Отпустили: ${_rb.gameObject.name}");
            _rb.isKinematic = false;
        }
    }
}