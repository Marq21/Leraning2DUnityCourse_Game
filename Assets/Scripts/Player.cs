using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    bool isAlive;
    public Main main;
    public GameObject deafultBullet;
    public GameObject tripleBullet;
    [SerializeField]
    private Transform shootPoint;
    [SerializeField] float shootTiming;
    public SoundEffector effector;
    public BulletType bulletType = BulletType.DEFAULT;
    [SerializeField]
    private Transform bonusShootPoint;
    [SerializeField] 
    private GameObject laser;

    public enum BulletType
    {
        DEFAULT,
        TRIPLE,
        LASER,
    }

    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        isAlive = true;
        StartCoroutine(Shooting());
    }

    void Update()
    {
        Turn();
    }

    void FixedUpdate()
    {
        //Set object's speed
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime);
    }

    void Turn()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 179f));
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 304f, 179f));
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, -304f, 179f));
        }       
    }

    public void Death(bool isAlive)
    {
        isAlive = false;
        effector.playLoseSound();
        Invoke("GameOver", 1f);
    }

    public void GameOver()
    {
        main.GetComponent<Main>().GameOver();
    }


    IEnumerator Shooting()
    {
        switch (bulletType)
        {
            case BulletType.DEFAULT:
                shootTiming = 0.4f;
                yield return new WaitForSeconds(shootTiming);
                GameObject deafualtBulletObject = Instantiate(deafultBullet, shootPoint.transform.position, transform.rotation);
                break;
            case BulletType.TRIPLE:
                shootTiming = 0.3f;
                yield return new WaitForSeconds(shootTiming);
                GameObject tripleBulletObject = Instantiate(tripleBullet, bonusShootPoint.transform.position, transform.rotation);
                StartCoroutine(OffBonusEffect(6f));
                break;
            case BulletType.LASER:
                yield return new WaitForSeconds(0.5f);
                laser.SetActive(true);
                StartCoroutine(OffBonusEffect(10f));
                break;
        }

        StartCoroutine(Shooting());
    }

    public int getPlayerScore()
    {
        return Enemy.destroyedScore;
    }

    public void setPlayerScore(int score)
    {
        Enemy.destroyedScore = score;
    }

    IEnumerator OffBonusEffect(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        laser.SetActive(false);
        yield return new WaitForSeconds(2f);
        bulletType = BulletType.DEFAULT;
    }

}
