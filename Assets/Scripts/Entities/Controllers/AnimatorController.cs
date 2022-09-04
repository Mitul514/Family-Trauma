using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator mAnimator => GetComponent<Animator>();

    public void StopAnimation()
    {
        mAnimator.speed = 0;
    }

    public void PlayAnimation()
    {
        mAnimator.speed = 1;
    }
}
