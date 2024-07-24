using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventSlot : MonoBehaviour , IPointerDownHandler
{
    Inventory inventory;
    public bool chest;

    void Start()
    {
        inventory = GameObject.Find("World").GetComponent<Inventory>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (chest)
        {

        }
        else
        {
            inventory.PlaceItem(gameObject);
        }
    }
}
