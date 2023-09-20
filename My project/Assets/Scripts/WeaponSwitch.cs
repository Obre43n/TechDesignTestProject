using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject revolver;
    public GameObject bat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(revolver.activeInHierarchy == true)
            {
                revolver.SetActive(false);
                bat.SetActive(true);
            }
            else if (bat.activeInHierarchy == true)
            {
                revolver.SetActive(true);
                bat.SetActive(false);
            }
            
        }
    }
}
