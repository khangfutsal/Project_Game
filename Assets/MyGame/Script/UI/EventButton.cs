using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private Image image;
    [SerializeField] private List<Sprite> sprites;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = sprites[1];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = sprites[0];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        image.sprite = sprites[0];
    }
}
