using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject key;
    public GameObject gun;
    public GameObject awm;

    [HideInInspector] public List<GameObject> rooms;



    private void Start()
    {
        StartCoroutine(RandomSpawner());

    }
    IEnumerator RandomSpawner()
    {
        yield return new WaitForSeconds(10f);
        AddRoom lastRoom = rooms[rooms.Count - 1].GetComponent<AddRoom>();
        int rand = Random.Range(0, rooms.Count-2);
        //Спавнит ключ key в случайной (Не последней и не предпоследней комнате)
        Instantiate(key, rooms[rand].transform.position, Quaternion.identity);
        int rand2 = Random.Range(0, rooms.Count - 2);
        Instantiate(awm, rooms[rand2].transform.position, Quaternion.identity);
        //Спавнит оружие gun в предпоследней комнате
        Instantiate(gun, rooms[rooms.Count - 2].transform.position, Quaternion.identity);
        
        

        lastRoom.door.SetActive(true);
        lastRoom.DestroyWalls();

    }
}
