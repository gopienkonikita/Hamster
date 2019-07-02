using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMonster : Monster {

    [SerializeField] private float rate = 2.0F;

    private Color bulletColor = Color.green;

    private Bullet bullet;

    protected override void Awake()
    {
        bullet = Resources.Load<Bullet>("Bullet");
    }

    protected override void Start()
    {
        InvokeRepeating("Shoot", rate, rate);
    } // создание пули

    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.5F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = -newBullet.transform.right; // направление юнита
        newBullet.Color = bulletColor;
    } // задаем направление пули

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is Move)
        {
            unit.ReceiveDamage();
        }
    } // проверка на столкновение пули с объектом
}
