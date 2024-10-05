using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    private int energy = 100;
    private bool isHit = false;
    private float impactVolumeLevel = 0.1f;
    AudioSource audioSource;
    [SerializeField]
    AudioClip impactShield;
    [SerializeField]
    AudioClip emptyEnerySound;
    private int coroutineIterationCounter;
    public int Energy => energy;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "ArmoredAsteroid" ||
            collision.gameObject.tag != "Asteroid" ||
            collision.gameObject.tag != "BonusAsteroid")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    public void CheckEnergy()
    {
        if (energy > 10)
        {
            audioSource.PlayOneShot(impactShield, impactVolumeLevel);
            StopCoroutine(EnergyShieldDamagedColor());
            energy -= 10;
            isHit = true;
            StartCoroutine(EnergyShieldDamagedColor());
        }
        else
        {
            energy -= 10;
            emptyEnergySound();
            gameObject.SetActive(false);
        }
    }

    private void emptyEnergySound()
    {
        audioSource.PlayOneShot(emptyEnerySound, impactVolumeLevel);
    }

    IEnumerator EnergyShieldDamagedColor()
    {
        if (isHit)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a + 0.10f);
            coroutineIterationCounter++;
        }
                
        if (coroutineIterationCounter >= 8)
        {
            isHit = false;
            if (GetComponent<SpriteRenderer>().color.a > 0.30)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a - 0.10f);
            } else
            {
                coroutineIterationCounter = 0;
            }
        }

        yield return new WaitForSeconds(0.10f);
        StartCoroutine(EnergyShieldDamagedColor());
    }
}
