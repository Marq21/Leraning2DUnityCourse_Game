using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    public float speed;
    public AudioClip impact;
    public Transform meteorPosition;
    public static int destroyedScore = 0;
    protected AudioSource audioSource;
    protected float time;
    protected static float destroyVolumeLevel = 1f;
    protected Rigidbody2D rigidbody2;
    [SerializeField]
    protected float rotationSpeed;
    protected SpriteRenderer spriteRenderer;
    protected ShieldHandler shieldhandler;

    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rotationSpeed = Random.Range(10, 101);
        shieldhandler = FindObjectOfType<ShieldHandler>();
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (shieldhandler.Energy <= 0)
            {
                PlayerDeathProcessing(collision);
                handleAsteroidDestroy();
            } else
            {
                handleAsteroidDestroy();
                shieldhandler.CheckEnergy();
            }
        }
        else if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Laser")
        {
            if (gameObject.tag == "Asteroid")
            {
                handleAsteroidDestroy();
            }
        }
    }


    protected void Update()
    {
        time += Time.deltaTime;
        if (time >= 1f)
        {
            meteorPosition.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            time = 0;
        }

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Make object to inactive, change anination state for destroying animation
    /// and change bodytype to static for prevent moving
    /// </summary>
    IEnumerator DestroyMeteor()
    {
        GetComponent<Animator>().SetBool("dead", true);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Destroy meteor coroutine wrapper, for calling out of class
    /// </summary>
    public void DestroyMeteorWrap()
    {
        StartCoroutine(DestroyMeteor());
    }


    public static float getDestroyVolmeLevel()
    {
        return destroyVolumeLevel;
    }

    public static void setDestroytVolumeLevel(float volume)
    {
        destroyVolumeLevel = volume;
    }

    protected void OnBecameVisible()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    protected void OnBecameInvisible()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    protected void PlayerDeathProcessing(Collision2D collision)
    {
        collision.gameObject.GetComponent<Animator>().SetBool("dead", true);
        collision.gameObject.GetComponent<Player>().Death(false);
        audioSource.PlayOneShot(impact, destroyVolumeLevel);
    }

    /// <summary>
    /// each enemy object can have own logic of destroying and scoring points
    /// </summary>
    private void handleAsteroidDestroy()
    {
        destroyedScore++;
        audioSource.PlayOneShot(impact, destroyVolumeLevel);
        DestroyMeteorWrap();
    }
}
