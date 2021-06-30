using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLoadingScreen : MonoBehaviour
{
    public Animator animator;

    public void endAnimation()
    {
        animator.SetTrigger("end loading");
    }
}
