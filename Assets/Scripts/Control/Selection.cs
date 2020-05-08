using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public RectTransform selectionBox;

    private List<Spec> selectionList = new List<Spec>();
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }

        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }
    }

    private void UpdateSelectionBox(Vector2 curMousePos)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
        {
            selectionBox.gameObject.SetActive(true);
        }
        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    private void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);
        selectionList.Clear();

        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        foreach (Spec spec in GameManager.Instance.SpecList)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(spec.transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectionList.Add(spec);
                Debug.Log("Unit added to selection list");
            }
        }
    }
}
