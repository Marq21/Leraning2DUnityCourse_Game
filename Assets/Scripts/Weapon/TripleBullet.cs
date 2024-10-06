using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBullet : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    float timeToRemove = 4f;
    public GameObject leftBullet;
    public GameObject centerBullet;
    public GameObject rightBullet;
    private Vector3 leftBulletVector;
    private Vector3 rightBulletVector;


    void Start()
    {
        StartCoroutine(SetRemove());
        Vector3 leftBulletVector = new Vector3(-0.4f, 0);
        Vector3 rightBulletVector = new Vector3(0.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (leftBullet != null)
            leftBullet.transform.Translate((Vector3.up + leftBulletVector) * speed * Time.deltaTime);
        if (centerBullet != null)
            centerBullet.transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (rightBullet != null)
            rightBullet.transform.Translate((Vector3.up + rightBulletVector) * speed * Time.deltaTime);
    }

    IEnumerator SetRemove()
    {
        yield return new WaitForSeconds(timeToRemove);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bonus")
        {
            StopCoroutine(SetRemove());
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
