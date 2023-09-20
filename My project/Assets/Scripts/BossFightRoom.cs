using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightRoom : MonoBehaviour
{
    public GameObject boss;
    public Vector2 vector;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))

        {
            Instantiate(boss, vector, Quaternion.identity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
