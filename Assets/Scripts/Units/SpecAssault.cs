using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecAssault : Unit
{
    public Transform FirePointTransform; // место где спавниться выстрел и пуля
    public GameObject BulletPrefab; // пуля
    public GameObject FirePrefab; // огонь от выстрела
    public float BulletForce = 20f; // cкорость пули

    void Update()
    {
       
    }

    void Shoot()
    {
        float fireSpeed = 1f;
        if (fireSpeed <= 0) // Всегда будет false, зачем?
        {
            Instantiate(FirePrefab, FirePointTransform.position, FirePointTransform.rotation); // спавн эфекта выстрела
            GameObject bullet = Instantiate(BulletPrefab, FirePointTransform.position, FirePointTransform.rotation); // спавн пули и присвоение заспавненной пули переменной bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // присваиваем переменной rb rigidbody2d заспавненной пули
            rb.AddForce(FirePointTransform.up * BulletForce, ForceMode2D.Impulse); // сложная математическая магия заставляющая пулю летать
            fireSpeed = 1f;
        }
        fireSpeed = -Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Shoot();
    }

}
