using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 5f;
    public float rotate_Speed = 50f;

    public bool canShoot;
    public bool canRotate;
    private bool canMove = true;

    public float bound_X = -11f;

    public Transform attack_Point;
    public GameObject bulletPrefab;

    private Animator anim;
    private AudioSource explosionSound;

    
    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (canRotate)
        {
            if(Random.Range(0, 2) > 0)
            {
                rotate_Speed = Random.Range(rotate_Speed, rotate_Speed + 20f);
                rotate_Speed *= -1f;
            }
        }

        if (canShoot)
            Invoke("startShooting", Random.Range(1f, 2f));
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        rotateEnemy();
    }

    void Move()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;

            if (temp.x < bound_X)
                gameObject.SetActive(false);
        }
    }

    void rotateEnemy()
    {
        if (canRotate)
        {
            transform.Rotate(new Vector3(0f, 0f, rotate_Speed * Time.deltaTime), Space.World);
        }
    }

    void startShooting()
    {
        GameObject e_bullet_2 = Instantiate(bulletPrefab, attack_Point.position, Quaternion.identity);  //(obiekt, pozycja, obrot)
        e_bullet_2.GetComponent<Bullet>().is_EnemyBullet = true;

        if (canShoot)
            Invoke("startShooting", Random.Range(1f, 3f));
    }

    void TurnOffObject()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet")
        {
            canMove = false;
            if (canShoot)
            {
                canShoot = false;
                CancelInvoke("startShooting");
            }
            Invoke("TurnOffObject", 0.4f); //czas w jakim obiekt pozostaje widoczny po turnoff

            explosionSound.Play();
            anim.Play("destroy");
        }
    }
}
