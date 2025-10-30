using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUp;
    public Vector2 gridsize = new Vector2(40, 30);
    public float timer = 0f;
    public bool powerUpActive = false;
    public float minTime = 6f;
    public float maxTime = 30f;
    public float delayTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        delayTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    private void SpawnPowerUp()
    {
        float x = Random.Range(-gridsize.x / 2, gridsize.x / 2);
        float y = Random.Range(-gridsize.y / 2, gridsize.y / 2);
        GameObject powerUpObject = Instantiate(powerUp, new Vector2(Mathf.Round(x), Mathf.Round(y)), Quaternion.identity);
        powerUpObject.name = "Snake PowerUp";
    }
    public void Timer()
    {
        SnakeController player = GameObject.FindWithTag("SnakeHead").GetComponent<SnakeController>();
        if (powerUpActive || player.hasSpeedBoost) return;

        timer += Time.deltaTime;

        if (timer > delayTime)
        {
            SpawnPowerUp();
            timer = 0f;
            powerUpActive = true;
        }
    }
}
