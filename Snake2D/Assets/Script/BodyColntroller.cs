using System.Collections;
using UnityEngine;

public class BodyCollider : MonoBehaviour
{
    public Collider2D bodyCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelayCollider());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayCollider()
    {
        yield return new WaitForSeconds(0.5f);
        bodyCollider.enabled = true;
    }

}
