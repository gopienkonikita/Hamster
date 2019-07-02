using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveableMonster : Monster {

    [SerializeField] private float speed = 10.0F;

    [SerializeField] private float hp = 30;

    private Vector3 direction;

    private SpriteRenderer sprite;

    public GameObject next;



    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Start()
    {
        direction = transform.right;    // начальное направление
    }

    protected override void Update()
    {
        Move();
    }

    private void Move()
    {
        sprite.flipX = direction.x > 0.0F;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.5F + transform.right * direction.x * 0.7f, 0.2F); // проверка на препядствие

        if (colliders.All(x=> !x.GetComponent<Bullet>()) &&  colliders.Length > 0 && colliders.All(x => !x.GetComponent<Move>()))  // проверка на столкновение с препядствиями и пулями
        {
            direction *= -1.0F;
        } // проверка на хомяка

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime); // задаем движение
    } // задаем движение

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet) // проверка на пулю
        {
            hp--;
            Destroy(bullet.gameObject);
        }
        if (hp <= 0) // проверяем если хп меньше либо равно 0 удаляем объект
        {
            if (GameObject.FindWithTag("dieBoss") && next !=null)
            {
                next.SetActive(true);
                ReceiveDamage();
            }
            else
            ReceiveDamage();
        }
    } // проверки на столкновения
}
