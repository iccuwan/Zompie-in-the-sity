using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float panSpeed = 20f; // скорость перемещения камеры
    public float panBorderThickness = 10f; // Расстояние от края экрана до мыши когда та ничинает перемещение
    public Vector2 panLimit; // ограничение камеры по x и y координатам которое устанавливается в инспекторе
    public float scrollSpeed = 2f; // скорость колесика мыши
    public float minSize = 1f;
    public float maxSize = 20f;
    public float shiftSpeedMultiply = 2f;

    void Update()
    {
        Vector3 pos = transform.position;
        float speed = panSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= shiftSpeedMultiply;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness )
        {
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= speed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scroll * scrollSpeed * 100f * Time.deltaTime; // приблежение камеры
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x); // максимум минимум куда может уйти камера по x
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minSize, maxSize); // максимум минимум насколько может приблежать и отдалять камера
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y); // максимум минимум куда может уйти камера по y

        transform.position = pos;
    }
}
