using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spec_Assaut : MonoBehaviour
{
    private Units units; // Обьявили переменную units которая содержит в себе переменные и методы из скрипта Units
    public Transform Firepoint; // место где спавниться выстрел и пуля
    public GameObject bulletPrefab; // пуля
    public GameObject fire; // огонь от выстрела
    public float bulletForse = 20f; // cкорость пули

    void Update()
    {
        units = GetComponent<Units>(); // Приравнимаем значение переменной со значениями переменных скрипта
      
    }

    void Shoot()
    {
        float fireSpeed = 1f;
        if (fireSpeed <= 0)
        {
            Instantiate(fire, Firepoint.position, Firepoint.rotation); // спавн эфекта выстрела
            GameObject bullet = Instantiate(bulletPrefab, Firepoint.position, Firepoint.rotation); // спавн пули и присвоение заспавненной пули переменной bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // присваиваем переменной rb rigidbody2d заспавненной пули
            rb.AddForce(Firepoint.up * bulletForse, ForceMode2D.Impulse); // сложная математическая магия заставляющая пулю летать
            fireSpeed = 1f;
        }
        fireSpeed = -Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Shoot();
    }

}
