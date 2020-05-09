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
    public float fireRate = 0.5f; // скорострельность
    private float fireRate_check = 0f; // проверка для скорострельности
    public readonly UnitType unitType = UnitType.Spec;
    public SpecType specType = SpecType.Assault;
    public Rigidbody2D SpecRb;


    void Awake()
    {
        GameManager.Instance.SpecList.Add(this);
        lookAtDeg = transform.rotation.eulerAngles.z;
        goTo = transform.position;
    }

    void Update()
    {
       if (fireRate_check < fireRate) // скорострельность
       {
           fireRate_check += Time.deltaTime;
       }
       
       transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, lookAtDeg), rotationSpeed * Time.deltaTime);
       if (goTo != null)
       {
           transform.position = Vector2.MoveTowards(transform.position, goTo, moveSpeed * Time.deltaTime);
       }
    }

    private void Shoot()
    {
        if (fireRate_check >= fireRate) // проверяем скорострельность
        {
            fireRate_check = 0f; // обнуляем скорострельность
            GameObject fire = Instantiate(firePrefab, firePointTransform.position, firePointTransform.rotation); // спавн эфекта выстрела и присвоение заспавненого эфекта переменной fire
            GameObject bullet = Instantiate(bulletPrefab, firePointTransform.position, firePointTransform.rotation); // спавн пули и присвоение заспавненной пули переменной bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // присваиваем переменной rb rigidbody2d заспавненной пули
            rb.AddForce(firePointTransform.up * bulletForce, ForceMode2D.Impulse); // сложная математическая магия заставляющая пулю летать
            Destroy(bullet, 1f);
            Destroy(fire, 0.1f);
        }
    }

    void OnTriggerStay2D(Collider2D Check_col)
    {
        if (Check_col.tag == "Zombie")
        {

            Shoot();
            Vector3 direction = Check_col.gameObject.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            SpecRb.rotation = angle;
        }
    }
  
    public enum SpecType
    {
        Assault,
    }

}
