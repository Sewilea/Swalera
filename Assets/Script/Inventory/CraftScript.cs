using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftScript : MonoBehaviour
{
    public Item need, craft;
    public int needAmount, craftAmount;

    Inventory inventory;

    private void Start()
    {
        inventory = GameObject.Find("World").GetComponent<Inventory>();
    }

    public void Craft()
    {
        inventory.Craft(need, needAmount, craft, craftAmount);
    }
}
