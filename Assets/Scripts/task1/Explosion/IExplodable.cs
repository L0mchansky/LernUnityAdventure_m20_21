using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public interface IExplodable
    {
        void OnExplode(float force, Vector3 direction);
    }
}