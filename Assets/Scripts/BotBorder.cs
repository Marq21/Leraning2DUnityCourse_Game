using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBorder : MonoBehaviour
{
    public Main main;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Invoke("GameOver", 0.5f);
        }

        switch (collision.gameObject.tag)
        {
            case "Asteroid":
            case "ArmoredAsteroid":
            case "BonusAsteroid":
                Invoke("GameOver", 0.5f);
                break;
        }
    }

    public void GameOver()
    {
        main.GetComponent<Main>().GameOver();
    }
}
