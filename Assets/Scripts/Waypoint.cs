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

    [SerializeField] private bool runpoint = false;

    /// <summary> only needs to be set if runpoint is true </summary>
    [SerializeField] private TextMeshProUGUI AiCountLeftToFind;

    /// <summary> only needs to be set if runpoint is true </summary>
    [SerializeField] private GameObject endScreen;

    public void AddAiToList(AiController ai)
    {
        AiList.Add(ai);
        
        if (runpoint)
        {
            AiCountLeftToFind.text = AiList.Count.ToString();

        }
    }

    public void RemoveAiFromList(AiController ai)
    {
        AiList.Remove(ai);

        if (runpoint)
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
    }
}
