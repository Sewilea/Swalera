using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finance : MonoBehaviour
{
    Inventory inventory;

    [Header("Money")]
    public int Money;
    public Text MoneyText;

    [Header("Experience")]
    public int Farming;
    public int Mining;
    public int Foraging;
    public int Fighting;
    public Text ExperienceText;

    [Header("Relationship")]
    public int MelinaRela;
    public int StefanRela;
    public int DaisyRela;
    public Text Relationship;


    void Start()
    {
        inventory = GameObject.Find("World").GetComponent<Inventory>();
    }

    void Update()
    {
        MoneyText.text = "Money : " + Money.ToString();
        ExperienceText.text = "Foraging : " + Foraging.ToString() + "\n" + "Farming : " + Farming.ToString() + "\n" + "Foraging : " + Foraging.ToString() + "\n" + "Fighting : " + Fighting.ToString();
        Relationship.text = "Melina : " + MelinaRela.ToString() + "/10" + "\n" + "Stefan : " + StefanRela.ToString() + "/10" + "\n" + "Daisy : " + DaisyRela.ToString() + "/10";
    }

    public void Sell_Button()
    {
        Item sellitem = inventory.MouseSlot.item;
        int amount = inventory.MouseSlot.Amount;

        if(sellitem.SellMoney != 0)
        {
            Money += amount * sellitem.SellMoney;

            inventory.MouseSlot.item = null;
            inventory.MouseSlot.Amount = 0;
        }
    }

    public void Buy_Button(Item item)
    {
        if(Money >= item.BuyMoney)
        {
            inventory.AddItem(item);
            Money -= item.BuyMoney;
        }
    }
}
