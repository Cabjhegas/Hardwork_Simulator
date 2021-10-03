using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMan : MonoBehaviour
{
    Rigidbody2D thisRigidbody;
    ObjectPooler mousePointerPooler;

    float minTime = 5f;
    float maxTime = 15f;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
        if (FindObjectOfType<GameManager>().shootMousePointerAllowed)
        {
            ShootTimeToTime();
        }

        ObjectPooler[] objectPoolers = FindObjectsOfType<ObjectPooler>();
        foreach(ObjectPooler o in objectPoolers)
        {
            if (o.pooledObject.GetComponent<MousePointer>())
            {
                mousePointerPooler = o;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PullStickManUp();
    }

    void PullStickManUp()
    {
        float x = Random.Range(-0.4f, 0.4f);
        float y = Random.Range(0f, 0.9f);
        Vector2 randomDirection = new Vector3(x, y).normalized;
        float forceMultiplier = 3f;
        thisRigidbody.AddForce(randomDirection * forceMultiplier, ForceMode2D.Impulse);
    }

    public void ShootTimeToTime()
    {
        StartCoroutine(ShootTimeToTimeRoutine());
    }

    IEnumerator ShootTimeToTimeRoutine()
    {
        float randomTimer = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(randomTimer);
        GameObject mousePointerGO = mousePointerPooler.GetPooledObject();

        mousePointerGO.transform.position = transform.position;
        mousePointerGO.transform.rotation = Quaternion.identity;
        mousePointerGO.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        mousePointerGO.SetActive(true);
        mousePointerGO.GetComponent<MousePointer>().ShootMousePointer();

        StartCoroutine(ShootTimeToTimeRoutine());
    }
}
