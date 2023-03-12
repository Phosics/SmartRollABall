using Common.Menus;
using Common.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public MenuManager menuManager;
        public PlayerController playerController;
    
        private void OnMove(InputValue movementVal)
        {
            playerController.OnMove(movementVal);
        }
    
        private void OnJump()
        {
            playerController.OnJump();
        }

        private void OnPause()
        {
            menuManager.OnPause();
        }
    }
}
