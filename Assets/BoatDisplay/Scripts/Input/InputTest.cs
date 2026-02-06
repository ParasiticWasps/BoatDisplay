using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    public InputActionReference pressA_Action;

    private void Update()
    {
        if (pressA_Action.action.WasPerformedThisFrame())
        {
            Debug.Log("Ajian");
        }
    }
}
