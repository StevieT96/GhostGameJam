using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareObject : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private float hauntDuration = 3;

    [SerializeField] private Waypoint waypoint;

    [SerializeField] private float scareRange = 3;

    public void haunt()
    {
        if (anim.GetBool("BeingHaunted"))
            return;

        anim.SetBool("BeingHaunted", true);

        waypoint.Scare(scareRange);

        StartCoroutine(WaitSecondsStopHaunting(hauntDuration));
    }

    private IEnumerator WaitSecondsStopHaunting(float _waitTime)
    {
        yield return new WaitForSecondsRealtime(_waitTime);

        anim.SetBool("BeingHaunted", false);

    }

}
