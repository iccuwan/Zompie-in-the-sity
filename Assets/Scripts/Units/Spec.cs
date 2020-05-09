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
    public float bulletLifeTime = 10f; // время жизни пули
    public float fireRate = 0.5f; // скорострельность
    public float fireLifeTime = 0.1f; // время жизни огня при выстреле
    public readonly UnitType unitType = UnitType.Spec;
    public SpecType specType = SpecType.Assault;
    public Rigidbody2D SpecRb;

    private float fireRateСheck = 0f; // проверка для скорострельности

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
    }

    private void Shoot()
    {
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

    void OnTriggerStay2D(Collider2D checkCol)
    {
        if (checkCol.tag == "Zombie" && unitStatus != UnitStatus.Walking)
        {
            Shoot();
            Vector3 direction = checkCol.gameObject.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle); // Так правильней и логичней
        }
    }
  
    public enum SpecType
    {
        Assault,
    }

}
