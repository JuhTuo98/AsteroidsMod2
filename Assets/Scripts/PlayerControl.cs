using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] KeyCode ThrustInput;
    [SerializeField] KeyCode ShootInput;

    [SerializeField] float ThrustForce;
    [SerializeField] float TorqueForce;

    [SerializeField] GameObject Bullet;
    [SerializeField] float BulletForce;
    [SerializeField] float BulletLife;

    public bool hasShotgun;
    public bool hasShield;

    public PowerUp Shield;
    public PowerUp Shotgun;

    bool isThrusting;
    bool isShooting;
    float ThrustCD;
    float AudioCD;

    Rigidbody2D rb;
    ParticleSystem ps;
    AudioSource aus;

    public static PlayerControl instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
        
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponent<ParticleSystem>();
        aus = GetComponent<AudioSource>();
    }

    void CheckKeys()
    {
        isThrusting = Input.GetKeyDown(ThrustInput);
        isShooting = Input.GetKeyDown(ShootInput);
    }

    void Thrust()
    {
        rb.AddForce(transform.up.normalized * ThrustForce * Time.deltaTime);
        ps.Play();
        ThrustCD = 3f;
        
    }

    void Turn() => rb.AddTorque(-Input.GetAxisRaw("Horizontal") * TorqueForce * Time.deltaTime);

    void Shoot()
    {
        GameObject newBullet = Instantiate(Bullet, transform);
        Rigidbody2D newBulletRB = newBullet.GetComponent<Rigidbody2D>();
        newBulletRB.AddForce(transform.up * BulletForce);
        Destroy(newBullet, BulletLife);

        AudioCD = 1f;
    }

    void ShotGunShoot()
    {
        for (int i = -1; i < 2; i++)
        {
            Quaternion newRot = Quaternion.Euler(0, 0, i * 20);
            GameObject newBullet = Instantiate(Bullet, transform.position, newRot * transform.rotation);
            Rigidbody2D newBulletRB = newBullet.GetComponent<Rigidbody2D>();
            newBulletRB.AddForce(newRot * transform.up * BulletForce * .5f);
            Destroy(newBullet, BulletLife);
        }

        AudioCD = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        
        CheckKeys();
        if (isThrusting) Thrust();
        else ThrustCD -= Time.deltaTime;
        if (isShooting) 
            if (hasShotgun) 
                ShotGunShoot();
                else Shoot();
            else AudioCD -= Time.deltaTime;

        Turn();
        
        if (ThrustCD <= 0) 
        {
            rb.AddForce(-rb.velocity * .25f);
            ps.Stop();
        }

        if (AudioCD <= 0)
        aus.Stop();
        else aus.Play();

        
    }
}
