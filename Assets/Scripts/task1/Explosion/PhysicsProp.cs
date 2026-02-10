using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class PhysicsProp : MonoBehaviour, IExplodable
    {
        public void OnExplode(float force, Vector3 direction)
        {
            if (TryGetComponent(out Rigidbody rb) == false)
                return;

            rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}