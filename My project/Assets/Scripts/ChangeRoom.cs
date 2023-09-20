using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private Camera cam;
    public bool isBossPortal;
    


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
     
        if (other.CompareTag("Player"))
        {
            if (isBossPortal == false)
            {
                other.transform.position += playerChangePos;
                cam.transform.position += cameraChangePos;
            }
            if (isBossPortal == true)
            {
                other.transform.position = playerChangePos;
                cam.transform.position = cameraChangePos;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
