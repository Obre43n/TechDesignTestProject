using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Control")]
    public float speed;

    [Header("Health")]
    public Text healthDisplay;
    public int maxHealth;
    private int health;
    public int potionHeal;

    [Header("Shield")]
    public Shield shieldTimer;
    public GameObject shield;

    [Header("Weapons")]
    public List<GameObject> unlockedWeapons;
    public GameObject[] allWeapons;
    public Image weaponIcon;
    public Text ammo;
    public GameObject bat;
    [Header("Key")]
    public GameObject keyIcon;

    public GameObject speedUp;
    public GameObject maxHpUp;
    public GameObject healUp;

    public GameObject awm;
    public GameObject rv;
    public GameObject aK;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator anim;
    private bool facingRight = true;
    private bool keyButtonPushed;







    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health = maxHealth;
        healthDisplay.text = "HP:" + health;


    }
    void Update()
    {

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        if (moveInput.x == 0 && moveInput.y == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (!facingRight && moveInput.x > 0)
        {
            Flip();

        }

        else if (facingRight && moveInput.x < 0)
        {
            Flip();

        }
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (rv.activeInHierarchy == true)
            {
                anim.Play("RVReload");
            }
            else if (aK.activeInHierarchy == true)
            {
                anim.Play("AkREload");
            }
            else if (awm.activeInHierarchy == true)
            {
                anim.Play("AWMReload");
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            keyButtonPushed = !keyButtonPushed;
        }

    }
    public void ChangeHealth(int healthValue)
    {
        if (!shield.activeInHierarchy || shield.activeInHierarchy && healthValue > 0)
            health += healthValue;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthDisplay.text = "HP:" + health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpeedUp"))
        {

            speed = speed + 2;
            Destroy(other.gameObject);
            speedUp.SetActive(true);

        }
        else if (other.CompareTag("HPUp"))
        {
            maxHealth = maxHealth + 30;
            Destroy(other.gameObject);
            maxHpUp.SetActive(true);
        }
        else if (other.CompareTag("Potion"))
        {
            ChangeHealth(potionHeal);
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("HealUp"))
        {
            potionHeal = potionHeal + 10;
            Destroy(other.gameObject);
            healUp.SetActive(true);
        }

        else if (other.CompareTag("Shield"))
        {
            if (!shield.activeInHierarchy)
            {
                shield.SetActive(true);
                shieldTimer.gameObject.SetActive(true);
                shieldTimer.isCooldown = true;
                Destroy(other.gameObject);
            }
            else
            {
                shieldTimer.ResetTimer();
                Destroy(other.gameObject);

            }

        }
        else if (other.CompareTag("Weapon"))
        {
            for (int i = 0; i < allWeapons.Length; i++)
            {
                if (other.name == allWeapons[i].name)
                {
                    unlockedWeapons.Add(allWeapons[i]);
                }
            }
            Destroy(other.gameObject);
            SwitchWeapon();

        }
        else if (other.CompareTag("Key"))
        {
            keyIcon.SetActive(true);
            Destroy(other.gameObject);
        }

    }



    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Door") && keyButtonPushed == true && keyIcon.activeInHierarchy == true)
        {
            //keyIcon.SetActive(false);
            other.gameObject.SetActive(false);
            keyButtonPushed = false;
        }
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        //метод переворачивает модель персонажа по оси x ( умножает на -1)

        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void SwitchWeapon()
    {
        for (int i = 0; i < unlockedWeapons.Count; i++)
        {
            if (unlockedWeapons[i].activeInHierarchy)
            {
                unlockedWeapons[i].SetActive(false);

                if (i != 0)
                {
                    unlockedWeapons[i - 1].SetActive(true);
                    weaponIcon.sprite = unlockedWeapons[i - 1].GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    unlockedWeapons[unlockedWeapons.Count - 1].SetActive(true);
                    weaponIcon.sprite = unlockedWeapons[unlockedWeapons.Count - 1].GetComponent<SpriteRenderer>().sprite;
                }
                if (bat.activeInHierarchy == true)
                {
                    ammo.text = "-";
                }
                weaponIcon.SetNativeSize();
                break;
            }
        }
    }


}
