using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

    public int maxHitPoints = 5;
    private int currentHitPoints;

    public float rotateSpeed = 50f;
    public float forwardSpeed = 2.5f;
    public float backwardSpeed = 1.5f;
    public float bulletSpeed = 10;
    public float bulletFiringRate = 1.5f;
    //public int maxBullets = 5;
    public Bullet bullet;
    private Transform turret;
    private float bulletNextFiringTime;
    private Rigidbody rigidBody;
    public string tankController = "Joystick1";

    public AudioClip shootSound;
    public AudioClip hitSound;
    private AudioSource audioSource;

    public int tankID;

    private Game game;

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;
        turret = transform.Find("BulletSpawnpoint");
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        UpdateBulletFiringRate();
        game = GameObject.Find("Game").GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        float joystick1Rotate = Input.GetAxis(tankController + "-Rotate");
        rigidBody.angularVelocity = Vector3.up * joystick1Rotate * rotateSpeed;

        float joystick1ForwardInput = Input.GetAxis(tankController + "-Forward");
        float joystick1BackwardInput = Input.GetAxis(tankController + "-Backward");

        float velocity = joystick1ForwardInput * forwardSpeed - joystick1BackwardInput * backwardSpeed;

        rigidBody.velocity = transform.TransformDirection(Vector3.forward * velocity);

        CheckShoot();

    }

    private void CheckShoot()
    {
        bool isShooting = Input.GetButton(tankController + "-Fire");
        if (isShooting && Time.time > bulletNextFiringTime)
        {
            Bullet test = Instantiate(bullet);
            test.transform.position = turret.transform.position;
            test.transform.rotation = turret.transform.rotation;
            test.GetComponent<Rigidbody>().velocity = turret.TransformDirection(Vector3.forward * bulletSpeed);
            UpdateBulletFiringRate();
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void UpdateBulletFiringRate()
    {
        bulletNextFiringTime = Time.time + bulletFiringRate;
    }

    public void hit()
    {
        --currentHitPoints;
        game.tankHit(this);
        audioSource.PlayOneShot(hitSound);
        if (currentHitPoints <= 0)
        {
            game.tankDestroyed(this);
        }
    }

}
