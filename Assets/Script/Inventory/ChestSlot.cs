using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{
    public Item item;
    public Text Text;
    public Image image;
    public bool isCraft;

    void Start()
    {
        if (isCraft)
        {
            Text.text = item.name;
            image.sprite = item.sprite;
        }
        else
        {
            Text.text = item.BuyMoney.ToString();
            image.sprite = item.sprite;
        }
    }
}
