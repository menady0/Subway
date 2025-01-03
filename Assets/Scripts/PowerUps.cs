using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour
{
    float attractionRadius = 8f;
    float moveSpeed = 15f;
    bool isMagnetActive = false;
    public static bool isPowerUp = false;
    Transform playerTransform;

    int rotationSpeed = 30;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        if (isMagnetActive)
            CollectNearbyCoins();
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            isPowerUp = true;
            FindObjectOfType<AudioManager>().playSound("powerUps");
            if (gameObject.name == "magnetBooster")
            {
                Transform powerUp = FindChildByName(player.transform, "magnetBoosterWithPlayer");
                if (powerUp != null)
                {
                    powerUp.gameObject.SetActive(true);
                    isMagnetActive = true;
                    playerTransform = player.transform;
                    GetComponent<Renderer>().enabled = false;
                    StartCoroutine(DestroyPowerUps(gameObject, player));
                }
                else
                {
                    Debug.Log("cant find magnetBooster");
                }
            }
            if (gameObject.name == "boost1")
            {
                GetComponent<Renderer>().enabled = false;
                PlayerController.runningSpeed += 5;

                StartCoroutine(DestroyPowerUps(gameObject, player));
            }
            if (gameObject.name == "multiplier")
            {
                GetComponent<Renderer>().enabled = false;
                PlayerManager.scoreValue = 5f;

                StartCoroutine(DestroyPowerUps(gameObject, player));
            }
        }
    }

    void CollectNearbyCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject coin in coins)
        {
            float distance = Vector3.Distance(coin.transform.position, playerTransform.position);

            if (distance <= attractionRadius)
                coin.transform.position = Vector3.MoveTowards(coin.transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }
            Transform grandchild = FindChildByName(child, name);
            if (grandchild != null)
            {
                return grandchild;
            }
        }
        return null;
    }
    IEnumerator DestroyPowerUps(GameObject power, Collider parent)
    {
        yield return new WaitForSeconds(10);
        if (power.gameObject.name == "magnetBooster")
        {
            Transform powerUp = FindChildByName(parent.transform, "magnetBoosterWithPlayer");
            powerUp.gameObject.SetActive(false);
            isMagnetActive = false;
            //Debug.Log("magnetBooster 2");
        }
        if (power.gameObject.name == "multiplier")
            PlayerManager.scoreValue = 1f;

        if(gameObject.name == "boost1")
            PlayerController.runningSpeed -= 5;

        isPowerUp = false;
        FindObjectOfType<AudioManager>().playSound("powerDown");
        Destroy(power);
    }
}
