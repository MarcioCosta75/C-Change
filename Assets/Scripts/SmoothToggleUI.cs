using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmoothToggleUI : MonoBehaviour, IPointerClickHandler
{
    public float targetYPosition = 200f; // Change this value to the desired y position
    public float movementSpeed = 5f; // Speed of movement
    public RectTransform rectTransform;
    private Vector3 originalPosition;
    private bool isMoved = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.localPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isMoved)
        {
            MoveToTargetPosition();
            isMoved = true;
        }
        else
        {
            MoveToOriginalPosition();
            isMoved = false;
        }
    }

    void MoveToTargetPosition()
    {
        float distanceToTarget = Mathf.Abs(originalPosition.y - targetYPosition);
        float movementDuration = distanceToTarget / movementSpeed;
        LeanTween.moveLocalY(gameObject, targetYPosition, movementDuration).setEaseInOutSine();
    }

    void MoveToOriginalPosition()
    {
        float distanceToOriginal = Mathf.Abs(originalPosition.y - rectTransform.localPosition.y);
        float movementDuration = distanceToOriginal / movementSpeed;
        LeanTween.moveLocalY(gameObject, originalPosition.y, movementDuration).setEaseInOutSine();
    }
}

