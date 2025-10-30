using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SnakeController : MonoBehaviour
{
    public Joystick joystick;
    public float deadzone = 0.5f;

    public float speed = 5.0f;
    public Vector2 direction = Vector2.up;
    public GameObject SnakeBody;
    public bool powerUp;

    public float delayTime = 0.2f;

    public bool hasSpeedBoost = false;
    public float storedBoostAmount;
    public float storedBoostDuration;

    public List<GameObject> bodyParts = new List<GameObject>();

    private GameObject bodyInstance;
    private float _timer = 0;
    private float _originalSpeed;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _originalSpeed = speed;
    }

    void Update()
    {
        if (Mathf.Abs(joystick.Horizontal) > Mathf.Abs(joystick.Vertical))
        {
            if (joystick.Horizontal > deadzone && direction != Vector2.left) direction = Vector2.right;
            else if (joystick.Horizontal < -deadzone && direction != Vector2.right) direction = Vector2.left;
        }
        else
        {
            if (joystick.Vertical > deadzone && direction != Vector2.down) direction = Vector2.up;
            else if (joystick.Vertical < -deadzone && direction != Vector2.up) direction = Vector2.down;
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame && direction != Vector2.down 
        || Keyboard.current.wKey.wasPressedThisFrame && direction != Vector2.down) direction = Vector2.up;

        if (Keyboard.current.downArrowKey.wasPressedThisFrame && direction != Vector2.up 
        || Keyboard.current.sKey.wasPressedThisFrame && direction != Vector2.up) direction = Vector2.down;

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame && direction != Vector2.left 
        || Keyboard.current.dKey.wasPressedThisFrame && direction != Vector2.left) direction = Vector2.right;

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame && direction != Vector2.right 
        || Keyboard.current.aKey.wasPressedThisFrame && direction != Vector2.right) direction = Vector2.left;

        _timer += Time.deltaTime;
        
        if (_timer > 1f / speed)
        {
            _timer = 0;
            Move();
        }
        Debug.Log(speed);

        if (hasSpeedBoost && Input.GetKeyUp(KeyCode.F))
        {
            increasedSpeed(storedBoostAmount, storedBoostDuration);
            hasSpeedBoost = false;
        }
    }

    private void Move()
    {
        Vector3 prev = transform.position;
        transform.position = new Vector3(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y + direction.y),
            0.0f        
        );
        for (int i = 0; i < bodyParts.Count; i++)
        {
            Vector3 part = bodyParts[i].transform.position;
            bodyParts[i].transform.position = prev;
            prev = part;
        }
    }

    public void Grow()
    {
        GameObject BodyInstance = Instantiate( SnakeBody );
        if (bodyParts.Count > 0)
        {
            BodyInstance.transform.position = bodyParts[bodyParts.Count - 1].transform.position;
        }
        else
        {
            BodyInstance.transform.position = transform.position;
        }

        bodyParts.Add (BodyInstance);

        UIController.Instance.DisplayScore(bodyParts.Count);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("SnakeWall"))
        {
            Time.timeScale = 0f;
            Destroy(gameObject);

            UIController.Instance.DisplayGameOver();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SnakeBody"))
        {
            Time.timeScale = 0f;
            Destroy (gameObject);

            UIController.Instance.DisplayGameOver();
        }
    }

    public void increasedSpeed( float boostedSpeed, float  duration)
    {
        speed = boostedSpeed;
        StartCoroutine(RestoreSpeedAfterDelay(duration));
    }
    private IEnumerator RestoreSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        speed = _originalSpeed;
    }

    public void ActivateSpeedBoost()
    {
        if (hasSpeedBoost)
        {
            increasedSpeed(storedBoostAmount, storedBoostDuration);
            hasSpeedBoost = false;
        }
    }
}
