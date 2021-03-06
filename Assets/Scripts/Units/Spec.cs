﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spec : Unit
{
    public Transform firePointTransform; // место где спавниться выстрел и пуля
    public GameObject bulletPrefab; // пуля
    public GameObject firePrefab; // огонь от выстрела
    public float bulletForce = 20f; // cкорость пули
    public float bulletDamage = 1f; // дамаг пули
    public float bulletLifeTime = 10f; // время жизни пули
    public float fireRate = 0.5f; // скорострельность
    public float fireLifeTime = 0.1f; // время жизни огня при выстреле
    public readonly UnitType unitType = UnitType.Spec;
    public SpecType specType = SpecType.Assault;
    public Rigidbody2D SpecRb;

    private float fireRateСheck = 0f; // проверка для скорострельности
    private List<Zombie> targetList = new List<Zombie>();
    private Zombie target = null;

    void Awake()
    {
        GameManager.Instance.SpecList.Add(this);
        lookAtDeg = transform.rotation.eulerAngles.z;
        goTo = transform.position;
    }

    void Update()
    {
        if (fireRateСheck < fireRate) // скорострельность
        {
             fireRateСheck += Time.deltaTime;
        }

        if (unitStatus == UnitStatus.Walking)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, lookAtDeg), rotationSpeed * Time.deltaTime); // Поворачивается в сторону, куда нажали
            transform.position = Vector2.MoveTowards(transform.position, goTo, moveSpeed * Time.deltaTime); // Делает шаг в сторону, куда нажали
            if (Vector2.Distance(transform.position, goTo) < 0.1f)
            {
                unitStatus = UnitStatus.Idle;
            }
        }
        else
        {
            if (targetList.Count > 0)
            {
                Zombie nearest = null;
                float nearestDistance = 0f;
                foreach (Zombie z in targetList)
                {
                    float distance = Vector2.Distance(transform.position, z.transform.position);
                    if (nearest != null)
                    {
                        if (nearestDistance > distance)
                        {
                            nearest = z;
                            nearestDistance = distance;
                            continue;
                        }
                        continue;
                    }
                    nearest = z;
                    nearestDistance = distance;
                }
                target = nearest;
            }
            if (target != null && unitStatus != UnitStatus.Walking)
            {
                Shoot(target);
            }
        }
    }

    private void Shoot(Zombie z)
    {
        Vector3 direction = z.gameObject.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle); // Так правильней и логичней
        if (fireRateСheck >= fireRate) // проверяем скорострельность
        {
            fireRateСheck = 0f; // обнуляем скорострельность
            GameObject fire = Instantiate(firePrefab, firePointTransform.position, firePointTransform.rotation); // спавн эфекта выстрела и присвоение заспавненого эфекта переменной fire
            GameObject bullet = Instantiate(bulletPrefab, firePointTransform.position, firePointTransform.rotation); // спавн пули и присвоение заспавненной пули переменной bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // присваиваем переменной rb rigidbody2d заспавненной пули
            rb.AddForce(firePointTransform.up * bulletForce, ForceMode2D.Impulse); // сложная математическая магия заставляющая пулю летать
            Destroy(bullet, bulletLifeTime);
            Destroy(fire, fireLifeTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            targetList.Add(collision.GetComponent<Zombie>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            targetList.Remove(collision.GetComponent<Zombie>());
        }
    }

    public enum SpecType
    {
        Assault,
    }

}
