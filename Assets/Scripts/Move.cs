using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class Move : Unit {

    [SerializeField] public int live = 5; // жизненные показатели
    [SerializeField] public int coin = 0; // монеты
    [SerializeField] public int bonus = 0; // бонус
    [SerializeField] private float speed = 10.0f; // скорость передвижения
    [SerializeField] float jumpForce = 10.0f; // высота прыжка
    [SerializeField] public int save; // переменная сохранения локации

    private bool isGrounded = false; // нахождение героя на земле
    private Bullet bullet;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    public LivesBar livesBar;
    public AudioSource source;
    public AudioClip[] clips;
    public GameObject text;
    private float timer; // таймер
    public GameObject bonus_text;

    public static int mon;
    public static int hp;
    public Text coinText;

    public int Lives
    {
        get { return live; }
        set
        {
            if (value <= 5) live = value;
            livesBar.Refresh();
        }
    } // свойство жизненных показателей

    public int Coins
    {
        get { return coin; }
        set
        {
            coin++;
            source.clip = clips[0];
            source.Play();
        }
    } // свойство монет

    private void Start()
    {
        if (hp > 0 && Menu_pause.load_level == false) // проверка если мы не загружаем уровень и хп больше 0
        {
            live = hp;
            coin = mon;
            livesBar.Refresh();
            coinText = GameObject.FindGameObjectWithTag("coins").GetComponent<Text>();
            coinText.text = coin.ToString();
        }

        if (Menu_pause.load_level==true) // проверка на загрузку уровня
        {
            Load();
            Menu_pause.load_level = false;
            coinText = GameObject.FindGameObjectWithTag("coins").GetComponent<Text>();
            coinText.text = coin.ToString();
        }

        Time.timeScale = 1;
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;
        source.volume = 0.3f;
    } // воспроизведение музыки

    public int Bonus
    {
        get { return bonus; }
        set
        {
            bonus++;
            source.clip = clips[2];
            source.Play();
            bonus_text.SetActive(true);
            
        }
    } // свойство бонус

    private CharState State
    {
        get { return (CharState)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    } // принимет состояния

    private void FixedUpdate()
    {
        CheckGround();
    } // вызов проврки на нахождение на поверхности

    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
    }

    private void Update()
    {
        EndGame();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump(); // проверка на прыжок
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                source.clip = clips[4];

                source.Play();
            } //проверка на выстрел
            if (isGrounded) State = CharState.Idle; // проверка на нахождение на платформе
            if (Input.GetButton("Horizontal")) Run(); // проверка на бег
            if (Input.GetKeyDown(KeyCode.CapsLock)) // проверка на активацию бонуса
            {
                Bonus_jump();
                text.SetActive(false);
            }

            if (bonus > 0) // проверка на то что бонус существует
            {
                text.SetActive(true);

                timer += 1 * Time.deltaTime;

                if (timer > 7) // проверка времени таймека
                {
                    text.SetActive(false);
                }
            }
    } // воспроизведение анимаций передвижения и музыки

    private void Bonus_jump()
    {
        if (bonus > 0)
        {
            rb.AddForce(transform.up * 50.0f, ForceMode2D.Impulse);
            bonus--;
            source.clip = clips[5];
            source.Play();
        }
    } // бонусный прыжок

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0F;

        if(isGrounded) State = CharState.Walk; // проверка на нахождение на земле и задание анимации ходьбы
    } // задание движение по оси х

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    } // задание прыжка по y

    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.8F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    } // создание пули

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;
    } // проверка на поверхность

    public override void ReceiveDamage()
    {
        Lives--;

        source.clip = clips[3];
        source.Play();
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * 20.0F, ForceMode2D.Impulse); // толчок

        Debug.Log(live);
    } // получение урона

    private void OnTriggerEnter2D(Collider2D collider)
    {

        Unit unit = collider.gameObject.GetComponent<Unit>();
        if (unit) // проверка на юнита
        {
            ReceiveDamage();
        }
    } // проверка на получение урона

    private void EndGame()
    {
        if (live <= 0)
        {
            SceneManager.LoadScene("Die");
        }
    } // проверка на смерть

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/saves/save.sv")) // проверка на директорию
        {
            FileStream fs = new FileStream(Application.dataPath + "/saves/save.sv", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                Menu_pause.Saver saves = (Menu_pause.Saver)formatter.Deserialize(fs);
                transform.position = new Vector3(saves.x, saves.y, saves.z);
                live = saves.health;
                livesBar.Refresh();
                coin = saves.coins;
                bonus = saves.bonus_jump;
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
            finally
            {
                fs.Close();
            }
        }
        else
        {
            //Application.Quit();
        }
    }

}

public enum CharState
{
    Idle,
    Walk,
} // задаем состояния
