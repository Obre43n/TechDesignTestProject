using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Bat;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage;
    public Animator anim;
    public AudioSource bonkSound;
    void Update()
    {
        if (Bat.activeInHierarchy == true)
        {
            if (timeBtwAttack <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    anim.Play("attack");

                    timeBtwAttack = startTimeBtwAttack;
                }
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    private void Bonk()
    {
        bonkSound.Play();
    }
    private void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);

    }
}
