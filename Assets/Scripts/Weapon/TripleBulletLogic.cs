using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBulletLogic : MonoBehaviour
{
    private float timeToRemove = 4f;

    void Start()
    {
        StartCoroutine(SetRemove());
    }

    // Update is called once per frame
    void Update()
    {
        
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
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
