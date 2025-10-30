using UnityEngine;
using UnityEngine.Video;

public class PowerUpController : MonoBehaviour
{
    public float boostedSpeed = 15f;
    public float boostDuration = 5f;
 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("SnakeHead"))
        {
            SnakeController ctr = col.GetComponent<SnakeController>();
            ctr.hasSpeedBoost = true;
            ctr.storedBoostAmount = boostedSpeed;
            ctr.storedBoostDuration = boostDuration;

            PowerUpSpawner spawner = GameObject.Find("PowerUpSpawner").GetComponent<PowerUpSpawner>();
            spawner.powerUpActive = false;
            spawner.delayTime = Random.Range(spawner.minTime, spawner.maxTime);
           
            Destroy(gameObject);
        }
    }
}
