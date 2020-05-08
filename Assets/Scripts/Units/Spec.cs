using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spec : Unit
{
    public Transform firePointTransform; // место где спавниться выстрел и пуля
    public GameObject bulletPrefab; // пуля
    public GameObject firePrefab; // огонь от выстрела
    public float bulletForce = 20f; // cкорость пули
    public float bulletDamage = 1f; // дамаг пули
    public readonly UnitType unitType = UnitType.Spec;
    public SpecType specType = SpecType.Assault;

    void Awake()
    {
        GameManager.Instance.SpecList.Add(this);
    }

    void Update()
    {
       
    }

    private void Shoot()
    {
        float fireSpeed = 1f;
        if (fireSpeed <= 0) // Всегда будет false, зачем?
        {
            Instantiate(firePrefab, firePointTransform.position, firePointTransform.rotation); // спавн эфекта выстрела
            GameObject bullet = Instantiate(bulletPrefab, firePointTransform.position, firePointTransform.rotation); // спавн пули и присвоение заспавненной пули переменной bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // присваиваем переменной rb rigidbody2d заспавненной пули
            rb.AddForce(firePointTransform.up * bulletForce, ForceMode2D.Impulse); // сложная математическая магия заставляющая пулю летать
            fireSpeed = 1f;
        }
        fireSpeed = -Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Shoot();
    }

    public enum SpecType
    {
        Assault,
    }

}
