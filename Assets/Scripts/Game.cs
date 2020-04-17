using UnityEngine;

public class Game : MonoBehaviour
{

    public AudioClip destroySound;

    public Tank tank1;
    public Tank tank2;
    public Tank tank3;
    public GameObject explosion;

    private AudioSource audioSource;

    void Awake()
    {
        if (Application.isEditor)
        {
            Application.runInBackground = true;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void tankHit(Tank tank)
    {
 
    }

    public void tankDestroyed(Tank tank)
    {
        GameObject newExplosion = Instantiate(explosion);
        newExplosion.transform.position = tank.transform.position;
        audioSource.PlayOneShot(destroySound);
        int tankID = tank.tankID;
        Destroy(tank.gameObject);
        if (tankID == 1)
        {
            Instantiate(tank1);
        }
        else if (tankID == 2)
        {
            Instantiate(tank2);
        }
        else if (tankID == 3)
        {
            Instantiate(tank3);
        }
        
    }

}
