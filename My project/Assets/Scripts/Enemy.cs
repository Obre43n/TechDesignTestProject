using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public int health;
    
    public float timeBtwAttack;
    public float startTimeBtwAttack;
    public int damage;
    public float stopTime;
    public float startStopTime;
    public float speed;
    public Animator anim;
    private Player player;
    public GameObject floatingDamage;
    public bool isEnemyMelee;
    private AddRoom room;
    [HideInInspector] public bool playerNotInRoom;
    private bool stopped;
    public bool oleg;
    public Text olegText;
    public PauseMenu pauseMenu;




    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        room= GetComponentInParent<AddRoom>();
      
        
    }
    private void Update()
    {
        
        if (health <= 0)
        {
                Destroy(gameObject);
                room.enemies.Remove(gameObject);

            if (oleg==true)
            {
                pauseMenu.loadMenu();
            }
        }
            
        if (player.transform.position.x > transform.position.x)
        {
                transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
                transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        

    }

    public void TakeDamage(int damage)
    {
        
        stopTime = startStopTime;
        health -= damage;
        Vector2 damagePos = new Vector2(transform.position.x + 23f, transform.position.y + 8f);
        floatingDamage.GetComponentInChildren<FloatingDamage>().damage = damage;
        Instantiate(floatingDamage, damagePos, Quaternion.identity);
        if (oleg == true)
        {
            olegText.text = "нкец " + health + "/" + 200;
        }
        
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if ( isEnemyMelee == true)
        {

            if (other.CompareTag("Player"))
            {
                if (timeBtwAttack <= 0)
                {
                    anim.SetTrigger("enemyAttack");
                }
                else
                {
                    timeBtwAttack -= Time.deltaTime;
                }
            }
        }
        
    }
    public void OnEnemyAttack()
    {
        player.ChangeHealth(-damage);
        timeBtwAttack = startTimeBtwAttack;
    }
}
