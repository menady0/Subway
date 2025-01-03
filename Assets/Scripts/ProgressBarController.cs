using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarController : MonoBehaviour
{
    public Slider progressBar;
    public float duration = 10f;
    private float timer = 0f;
    private Image progressBarImage;
    private Color initialColor = Color.white;
    private Color endColor = Color.red;

    void Start()
    {
        progressBarImage = progressBar.fillRect.GetComponent<Image>();

        progressBar.value = 1f;
    }

    void Update()
    {
        if (PowerUps.isPowerUp)
            Duration();
        if (PlayerManager.gameOver)
        {
            progressBar.gameObject.SetActive(false);
            PowerUps.isPowerUp = false;
        }

    }
    void Duration()
    {
        progressBar.gameObject.SetActive(true);

        timer += Time.deltaTime;
        if (timer < duration)
        {
            float progress = 1f - (timer / duration);
            progressBar.value = progress;
            progressBarImage.color = Color.Lerp(initialColor, endColor, 1f - progress);
        }
        else
        {
            progressBar.gameObject.SetActive(false);
            PowerUps.isPowerUp = false;
            timer = 0;
            progressBar.value = 1f;

        }
    }
}
