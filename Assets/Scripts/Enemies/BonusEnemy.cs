using UnityEngine;

public class BonusEnemy : Enemy
{
    public GameObject[] bonusObjects;
    public bool isDestroyed = false;

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (shieldhandler.Energy <= 0)
            {
                PlayerDeathProcessing(collision);
                handleAsteroidDestruction();
            }
            else
            {
                handleAsteroidDestruction();
                shieldhandler.CheckEnergy();
            }
        }
        else if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Laser")
        {
            if (gameObject.tag == "BonusAsteroid")
            {
                handleAsteroidDestruction();
            }
        }
        else if (collision.gameObject.tag == "EnergyShield")
        {
            handleAsteroidDestruction();
            collision.gameObject.GetComponent<ShieldHandler>().CheckEnergy();
        }
    }

    protected new void Update()
    {
        time += Time.deltaTime;
        if (time >= 1f)
        {
            meteorPosition.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            time = 0;
        }
    }

    private void handleAsteroidDestruction()
    {
        destroyedScore += 2;
        audioSource.PlayOneShot(impact, destroyVolumeLevel);
        GameObject bonus = Instantiate(bonusObjects[Random.Range(0, bonusObjects.Length)],
                                                    gameObject.transform.position,
                                                    new Quaternion(0f, 0f, 0f, 1f));
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        DestroyMeteorWrap();
    }
}
