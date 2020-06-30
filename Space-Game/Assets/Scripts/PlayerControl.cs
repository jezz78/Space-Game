using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    public float speed = 5f;
    public float min_Y, max_Y;  //oś Y granice
    public float min_X, max_X;  //oś Y granice
    public GameObject explode, gameOverText, restartButton;

    [SerializeField] private GameObject p_bullet_2;

    [SerializeField] private Transform attack_Point;    //punt narodzin pocisku

    public float attack_Timer = 0.35f;
    private float current_Attack_Timer;     //zmienna czasu pomiedzy strzalami
    private bool can_Attack;

    private AudioSource laserAudio, explosionSound;
    private Animator anim;

    public static int fuel;

    void Awake()
    {
        laserAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        current_Attack_Timer = attack_Timer;
        gameOverText.SetActive(false);
        restartButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Attack();
    }

    void MovePlayer()
    {
        if(Input.GetAxisRaw("Vertical") > 0f)
        {
            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;

            if (temp.y > max_Y)
                temp.y = max_Y;

            transform.position = temp;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;

            if (temp.y < min_Y)
                temp.y = min_Y;

            transform.position = temp;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;

            if (temp.x > max_X)
                temp.x = max_X;

            transform.position = temp;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;

            if (temp.x < min_X)
                temp.x = min_X;

            transform.position = temp;
        }
    }

    void Attack()
    {
        attack_Timer += Time.deltaTime;     //czas pomiedzy strzalami
        if(attack_Timer > current_Attack_Timer)
        {
            can_Attack = true;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (can_Attack)
            {
                can_Attack = false;
                attack_Timer = 0f;          //reset zmiennej czasu zeby nie strzelal za szybko
                Instantiate(p_bullet_2, attack_Point.position, Quaternion.identity);

                laserAudio.Play();
            }
            
        }
    }

    void TurnOffObject()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag.Equals("Bullet"))
        {
            Invoke("TurnOffObject", 0.8f);
            explosionSound.Play();
            anim.Play("p_explosion");
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
            //Debug.Log("hit detected");
        }
        else if (hit.gameObject.tag.Equals("Enemy"))
        {
            Invoke("TurnOffObject", 0.8f);
            explosionSound.Play();
            anim.Play("p_explosion");
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
            //Debug.Log("collision detected");
        }
    }
}
