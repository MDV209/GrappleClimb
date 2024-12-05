using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations;

public class ControlsLogic : MonoBehaviour
{
    public TextMeshProUGUI controlText;

    public void changeControl()
    {
        StartCoroutine(WaitForKeyPress());
    }
    private KeyCode getCurrentKeyDown()
    {
        KeyCode finalKeyCode = KeyCode.None;
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode))) { if (Input.GetKey(kcode)) { finalKeyCode = kcode; } }
        if (finalKeyCode == KeyCode.None)
        {
            //Couldn't find key
            return KeyCode.None;
        }
        return finalKeyCode;
    }
    IEnumerator WaitForKeyPress()
    {
        KeyCode key = getCurrentKeyDown();
        while (key == KeyCode.None)
        {
            key = getCurrentKeyDown();
            yield return null;
        }
        controlText.text = key.ToString();
        /*
        // Get the "Vertical" axis
        Axis verticalAxis = Input.GetAxis("Vertical");
        Input.

        // Set the new negative button (e.g., "Down Arrow" key)
        verticalAxis.negativeButton = "Down Arrow";

        // Save changes (optional)
        Input.SaveInputSettings()
        // Code to execute after the key is pressed
        */
    }
}
