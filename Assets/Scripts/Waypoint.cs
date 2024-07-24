using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Waypoint : MonoBehaviour
{
    public bool occupied = false;

    public AiController occupier;

    private List<AiController> AiList = new List<AiController>();

    public AudioSource Intaract;


    /// <summary> only have 1 manager point, can be any point in the list of ai waypoints </summary>
    [SerializeField] private bool managerPoint = false;

    /// <summary> only needs to be set if runpoint is true </summary>
    [SerializeField] private TextMeshProUGUI AiCountLeftToFind;

    /// <summary> only needs to be set if runpoint is true </summary>
    [SerializeField] private GameObject endScreen;

    public void AddAiToList(AiController ai)
    {
        AiList.Add(ai);
        
        if (managerPoint)
        {
            AiCountLeftToFind.text = AiList.Count.ToString();

        }
    }

    public void RemoveAiFromList(AiController ai)
    {
        AiList.Remove(ai);

        if (managerPoint)
        {
            AiCountLeftToFind.text = AiList.Count.ToString();


            if (AiList.Count == 0)
            {
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        endScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
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

        Intaract.Play();
    }

}
