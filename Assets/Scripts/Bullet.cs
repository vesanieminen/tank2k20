using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float maxBulletLifeTime = 15;

    private float bulletLifeTime = 0;

    private void Start()
    {
        bulletLifeTime = Time.time + maxBulletLifeTime;
    }

    void Update()
    {
        if (Time.time >= bulletLifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Tank tank = collision.gameObject.GetComponent<Tank>();
        if (tank != null)
        {
            tank.hit();
            Destroy(gameObject);
        }
    }

}
