using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchHandAnimation;
    public InputActionProperty grabHandAnimation;

    public Animator handAnimator;

    public void Update()
    {
        float triggervalue = pinchHandAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggervalue);

        float grabvalue = grabHandAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", grabvalue);
    }
}
