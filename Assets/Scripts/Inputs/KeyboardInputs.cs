using UnityEngine;

namespace Game.Inputs
{
    public class KeyboardInputs : MonoBehaviour
    {
        void Update()
        {
            VirtualInputManager.Instance.moveRight = Input.GetKey(KeyCode.D);
            VirtualInputManager.Instance.moveLeft = Input.GetKey(KeyCode.A);
            VirtualInputManager.Instance.jump = Input.GetKeyDown(KeyCode.Space);
            VirtualInputManager.Instance.turbo = Input.GetKey(KeyCode.RightShift);
        }
    }
}
