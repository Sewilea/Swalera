using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public SlotInfo[] SlotInChest = new SlotInfo[24];
    public Item Empty;

    public void Establish()
    {
        for (int i = 0; i < SlotInChest.Length; i++)
        {
            SlotInChest[i].item = Empty;
        }
    }

    public bool IsFull()
    {
        for (int i = 0; i < SlotInChest.Length; i++)
        {
            if (SlotInChest[i].amount > 0)
            {
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class SlotInfo
{
    public Item item;
    public int amount;
}