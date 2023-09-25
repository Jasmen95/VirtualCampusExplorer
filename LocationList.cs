using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject listButton = transform.GetChild(0).gameObject;
        GameObject g;
        for(int i=0; i<5; i++)
        {
            g = Instantiate(listButton, transform);
            g.transform.GetChild(1).GetComponent<Text>().text = "Location "+i;
        }

        Destroy(listButton);
    }

    
}
