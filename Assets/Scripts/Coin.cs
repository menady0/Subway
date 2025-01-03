using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int rotationSpeed = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider player)
    {
        if(player.tag == "Player")
        {
            PlayerManager.collectedCoins++;
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().playSound("coin");
        }
    }
}
