using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.PlayerCharacter;

public class KeyboardInputs : MonoBehaviour
{
    void Update()
    {
        VirtualInputManager.Instance.moveRight = Input.GetKey(KeyCode.D);
        VirtualInputManager.Instance.moveLeft = Input.GetKey(KeyCode.A);
        VirtualInputManager.Instance.jump = Input.GetKeyDown(KeyCode.Space);

    }
}
