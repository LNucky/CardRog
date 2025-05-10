using UnityEngine;
using UnityEngine.EventSystems;

public class CardStroke : MonoBehaviour, IPointerClickHandler
{
    private Vector3 originalPosition;
    private Vector3 originalScale;
    public float liftHeight = 10f;
    public bool isInspect = false;
    public bool isSelected = false;

    void Start()
    {
        originalPosition = transform.position;
        originalScale = transform.localScale;
    }

    

    public void CardUp()
    {
        Debug.Log(isInspect);
        if (!isInspect)
        {
            transform.position = originalPosition + Vector3.up * liftHeight;
            Debug.Log("CardUp");
        }
    }

    public void CardDown()
    {
        Debug.Log(isInspect);
        if (!isInspect)
        {
            transform.position = originalPosition;
            Debug.Log("CardDown");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (!isInspect && !isSelected)
            {
                transform.position = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
                transform.localScale *= 4f;
                isInspect = true;
            }
        }
        
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!isInspect && !isSelected)
            {
                isSelected = true;
            }
        }
    }

    void Update()
    {
        // Возврат карты в исходное положение после осмотра
        if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && isInspect)
        {
            transform.position = originalPosition;
            transform.localScale = originalScale;
            isInspect = false;
        }

        if (isSelected)
        {
            transform.position = Input.mousePosition;
            
            // Здесь нужно будет прописать действие при применении карты
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                // Затычка. Возврат карты в исходное положение
                transform.position = originalPosition;
                transform.localScale = originalScale;
                isSelected = false;
            }
        }
    }
}