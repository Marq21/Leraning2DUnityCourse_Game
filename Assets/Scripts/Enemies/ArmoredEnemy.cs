using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemy : Enemy
{
    private int hp = 3;
    private bool isHit = false;
    [SerializeField]
    private AudioClip asteroidDamaged;

    protected new void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (shieldhandler.Energy <= 0)
            {
                PlayerDeathProcessing(collision);
                checkHp();
            }
            else
            {
                checkHp();
                transform.position = new Vector3(transform.position.x, transform.position.y + 1f);
                shieldhandler.CheckEnergy();
            }
        }
        else if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Laser")
        {
            if (gameObject.tag == "ArmoredAsteroid")
            {
                checkHp();
            }
        }
        else if (collision.gameObject.tag == "EnergyShield")
        {
            checkHp();
            collision.gameObject.GetComponent<ShieldHandler>().CheckEnergy();
        }
    }

    private void checkHp()
    {
        if (hp > 0)
        {
            audioSource.PlayOneShot(asteroidDamaged, destroyVolumeLevel);
            StopCoroutine(AsteroidDamagedColor());
            hp--;
            isHit = true;
            StartCoroutine(AsteroidDamagedColor());
        }
        else
        {
            destroyedScore += 3;
            audioSource.PlayOneShot(impact, destroyVolumeLevel);
            DestroyMeteorWrap();
        }
    }

    IEnumerator AsteroidDamagedColor()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        if (isHit)
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a - 0.10f);
        else if (GetComponent<SpriteRenderer>().color.a < 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a + 0.10f);
        }
        else if (GetComponent<SpriteRenderer>().color.a == 1)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        if (GetComponent<SpriteRenderer>().color.a <= 0)
            isHit = false;
        yield return new WaitForSeconds(0.10f);
        StartCoroutine(AsteroidDamagedColor());
    }

}
