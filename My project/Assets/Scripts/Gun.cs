using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Gun : MonoBehaviour
{
    public AudioSource shootSound;
    public AudioSource gunReload;
    public GunType gunType;
    public int startclip;
    public GameObject bullet;
    public Transform shotPoint;
    // public Transform fallPoint;
    public float reloadTime;
    public float startReloadTime;
    private float timeBtwShots;
    public float startTimeBtwShots;
    private float rotZ;
    public float offset;
    private int clip;

    public Text ammo;
    // public GameObject fallingBulletRV;



    private Player player;
    private Vector3 difference;
    public enum GunType { Deffault, Enemy }


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        clip = startclip;


        
    }
    IEnumerator reloadingGun()
    {
        reloadStart();
        yield return new WaitForSeconds(1);
        reload();
        gunReload.Play();
    }

    // Update is called once per frame
    void Update()
    {



        if (gunType == GunType.Deffault)
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        }
        else if (gunType == GunType.Enemy)
        {
            difference = player.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (clip > 0 || gunType == GunType.Enemy)
        {
            if (timeBtwShots <= 0)
            {
                if (Input.GetMouseButton(0) || gunType == GunType.Enemy)
                {
                    Shoot();
                    
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }

        }
        if (Input.GetKeyDown(KeyCode.R) & gunType == GunType.Deffault)
        {

            StartCoroutine(reloadingGun());
        }




    }

    public void Shoot()
    {
        clip = clip - 1;
        timeBtwShots = startTimeBtwShots;

        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        shootSound.Play();
        

        

        if (gunType == GunType.Deffault)
        {
            ammo.text = "Ammo:" + clip + "/" + startclip;
        }


    }
    public void reload()
    {
        clip = startclip;
        if (gunType == GunType.Deffault)
        {
            ammo.text = "Ammo:" + clip + "/" + startclip;

        }
    }
    public void reloadStart()
    {
        clip = 0;
        if (gunType == GunType.Deffault)
        {
            ammo.text ="Ammo:" + clip + "/" + startclip;

        }
    }

    
    
    

}
