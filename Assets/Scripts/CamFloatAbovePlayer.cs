using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFloatAbovePlayer : MonoBehaviour
{
    public GameObject floatAboveObject;

    public Vector3 offset = new Vector3 (0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = floatAboveObject.transform.position + offset;
    }
}
