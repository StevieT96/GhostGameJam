using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Waypoint : MonoBehaviour
{
    public bool occupied = false;

    public AiController occupier;

    private List<AiController> AiList = new List<AiController>();


    public void AddAiToList(AiController ai)
    {
        AiList.Add(ai);
    }

    public void Scare(float _range)
    {
        foreach (AiController ai in AiList)
        {
            if (ai != null)
            {
                float distFromScare = Vector3.Distance(gameObject.transform.position, ai.transform.position);

                if (distFromScare <= _range)
                {
                    ai.AddtoScareMeter(1);

                    if (occupier == ai)
                        ai.AddtoScareMeter(1);
                }
            }
        }
    }
}
