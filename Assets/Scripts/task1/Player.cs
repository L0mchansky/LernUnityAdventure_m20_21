using UnityEngine;

namespace LernUnityAdventure_m20_21
{
    public class Player
    {
        private IInteraction _primaryInteraction;
        private IInteraction _secondaryInteraction;

        public Player(IInteraction primary, IInteraction secondary)
        {
            _primaryInteraction = primary;
            _secondaryInteraction = secondary;
        }

        public void PrimaryInteract(Ray ray)
        {
            _primaryInteraction?.Interact(ray);
        }

        public void SecondaryInteract(Ray ray)
        {
            _secondaryInteraction?.Interact(ray);
        }
    }
}
