using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandAnimator : MonoBehaviour
{
    public float speed = 5.0f;
    private Animator animator = null;
    public InputActionReference gripReference = null;
    public InputActionReference triggerReference = null;

    private readonly List<Finger> gripFingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky)
    };

    private readonly List<Finger> pointFingers = new List<Finger>()
    {
        new Finger(FingerType.Index),
        new Finger(FingerType.Thumb)
    };

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // store input
        CheckGrip();
        CheckPointer();

        // smooth input
        SmoothFinger(pointFingers);
        SmoothFinger(gripFingers);

        // apply smoothed values
        AnimateFinger(pointFingers);
        AnimateFinger(gripFingers);
    }

    private void CheckGrip()
    {
        float gripValue = gripReference.action.ReadValue<float>();    
        if(gripValue > 0)
            SetFingerTargets(gripFingers, gripValue);
    }

    private void CheckPointer()
    {
        float triggerValue = triggerReference.action.ReadValue<float>();
        if(triggerValue > 0)
            SetFingerTargets(pointFingers, triggerValue);
    }

    private void SetFingerTargets(List<Finger> fingers, float value)
    {
        foreach(Finger finger in fingers)
        {
            finger.target = value;
        }
    }

    private void SmoothFinger(List<Finger> fingers)
    {
        foreach(Finger finger in fingers)
        {
            float time = speed * Time.unscaledDeltaTime;
            finger.current = Mathf.MoveTowards(finger.current, finger.target, time);
        }
    }

    private void AnimateFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
            AnimateFinger(finger.type.ToString(), finger.current);    
    }

    private void AnimateFinger(string finger, float blend)
    {
        animator.SetFloat(finger, blend);
    }
}