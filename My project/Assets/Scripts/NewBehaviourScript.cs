using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject openStartRoom;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator startGame()
    {
        yield return new WaitForSeconds(10f);
        Destroy(openStartRoom);
    }
}
