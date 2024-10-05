using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusAction : MonoBehaviour
{   
    public Player player;
    public float timeToRemove = 8f;
    void Start()
    {
        StartCoroutine(SetRemove());      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<Player>();
            if (gameObject.name == "LaserBonus(Clone)")
            {
                player.bulletType = Player.BulletType.LASER;
            } 
            else if (gameObject.name == "TripleShotBonus(Clone)") 
            {
                player.bulletType = Player.BulletType.TRIPLE;
            }           
            StopCoroutine(SetRemove());
            Destroy(gameObject);
        }
    }

    IEnumerator SetRemove()
    {
        yield return new WaitForSeconds(timeToRemove);
        Destroy(gameObject);
    }
}
