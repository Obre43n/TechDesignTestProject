using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    [Header("Walls")]
    public GameObject[] walls;
    public GameObject door;

    [Header("Enemies")]
    public GameObject[] enemyTypes;
    public Transform[] enemySpawners;

    [Header("Powerups")]
    public GameObject shield;
    public GameObject healthPotion;
    [Header("Implunts")]
    public GameObject hpUp;
    public GameObject healUp;
    public GameObject speedUp;





    [HideInInspector] public List<GameObject> enemies;
    private RoomVariants variants;
    private bool spawned;
    private bool wallsDestroyed;




    void Awake()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
    }
    public void Start()
    {
        variants.rooms.Add(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !spawned)
        {
            spawned = true;
            foreach (Transform spawner in enemySpawners)
            {

                int rand = Random.Range(0, 31);
                if (rand < 25)
                {
                    GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                    GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                    enemy.transform.parent = transform;
                    enemies.Add(enemy);
                }
                else if (rand == 25 || rand == 26)
                {
                    Instantiate(healthPotion, spawner.position, Quaternion.identity);
                }

                else if (rand == 27 || rand == 28)
                {
                    Instantiate(shield, spawner.position, Quaternion.identity);
                }
                else if (rand == 29)
                {
                    Instantiate(hpUp, spawner.position, Quaternion.identity);
                }
                else if (rand == 30)
                {
                    Instantiate(speedUp, spawner.position, Quaternion.identity);
                }
                else if (rand == 31)
                {
                    Instantiate(healUp, spawner.position, Quaternion.identity);
                }




            }
            StartCoroutine(CheckEnemies());
        }
        else if (other.CompareTag("Player") && spawned)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().playerNotInRoom = false;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().playerNotInRoom = true;
        }
    }
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyWalls();
    }
    public void DestroyWalls()
    {
        foreach (GameObject wall in walls)
        {
            if (wall != null && wall.transform.childCount != 0)
            {
                Destroy(wall);
            }
        }
        wallsDestroyed = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (wallsDestroyed && other.CompareTag("Wall"))
        {
            Destroy(other.gameObject);
        }
    }

    void Update()
    {

    }
}
