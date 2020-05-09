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
           Debug.Log(fireRate_check);
           Debug.Log(fireRate);
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
            Debug.Log("Время пострелять");
        }
    }

    void OnTriggerStay2D(Collider2D Check_col)
    {
        Debug.Log("что то в тригере");
        if (Check_col.tag == "Zombie")
        {
            Shoot();
            Debug.Log("зомби найден");
        }
    }
    /* план Б
    private void OnTriggerExit(Collider Check_coli)
    {
        if (Check_coli.tag == "bullet")
        {
            Destroy(Check_coli);
        }
    }
    */
    public enum SpecType
    {
        Assault,
    }

}
