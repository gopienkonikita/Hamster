using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private GameObject parent;
    public GameObject Parent {set { parent = value; } }


    private float speed = 20.0F;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    private SpriteRenderer sprite;


    public Color Color 
    {
        set { sprite.color = value; }
    } // задание цвета

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    } // получение спрайта

    private void Start() // уничтожение пули через промежуток времени
    {
        Destroy(gameObject, 0.7F);
    }

    private void Update() // задание направления пули
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider) // проверка на столкновение пули с объектами
    {
        Unit unit = collider.GetComponent<Unit>();
        MoveableMonster mmoster = collider.GetComponent<MoveableMonster>();

        if (unit && unit.gameObject != parent && !mmoster) // проверка на столкновение пули с тем кто выпустил пулю и ходячим монстром
        {
            unit.ReceiveDamage();                            
            Destroy(gameObject);
        }
    }

}
