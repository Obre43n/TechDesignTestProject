using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
    private RoomVariants variants;
    private int rand;
    private bool spawned = false;
    private float waitTime = 2f;

    public enum Direction
    {
        Top,
        Bottom, 
        Left,
        Right,
        None

    }
    // Start is called before the first frame update
    void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        float rand = Random.Range(0.2f, 0.3f);
        Invoke("Spawn", rand);
        Destroy(gameObject, waitTime);

    }
    public void Spawn()
    {
        if(!spawned)
        {
            if (direction == Direction.Top)
            {
                rand = Random.Range(0, variants.topRooms.Length);
                Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation);
                
            }
            else if (direction == Direction.Bottom)

            {
                rand = Random.Range(0, variants.bottomRooms.Length);
                Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation);
                
            }
            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, variants.leftRooms.Length);
                Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation);
               
            }
           else  if (direction == Direction.Right)
            {
                rand = Random.Range(0, variants.rightRooms.Length);
                Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation);
                
            }
            else if (direction == Direction.None)
            {
                spawned = true;
            }
            spawned = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if( other.CompareTag("RoomPoint")&& other.GetComponent<RoomSpawner>().spawned)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
