using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragMoveUI : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public float targetYPosition = 200f; // Change this value to the desired y position
    public float movementSpeed = 5f; // Speed of movement
    private float originalYPosition;
    private RectTransform rectTransform;
    private bool isDragging = false;
    private bool dragUpCompleted = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalYPosition = rectTransform.localPosition.y;
    }

    void Update()
    {
        if (!isDragging)
        {
            float targetPosition = dragUpCompleted ? targetYPosition : originalYPosition;
            float newYPosition = Mathf.Lerp(rectTransform.localPosition.y, targetPosition, Time.deltaTime * movementSpeed);
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, newYPosition, rectTransform.localPosition.z);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        float deltaY = eventData.delta.y;
        // Aqui não limitamos a direção do arrasto, permitindo arrastar em qualquer direção vertical
        float newYPosition = rectTransform.localPosition.y + deltaY;
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, newYPosition, rectTransform.localPosition.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        // Avaliamos a posição final para decidir se concluímos o arrasto para cima ou para baixo
        if (rectTransform.localPosition.y >= targetYPosition)
        {
            dragUpCompleted = true; // Indica que o arrasto para cima foi concluído
        }
        else
        {
            dragUpCompleted = false; // Volta para a posição original se não atingiu o limite para ser considerado como arrasto para cima concluído
        }
    }
}