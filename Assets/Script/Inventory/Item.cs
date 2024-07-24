using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game", menuName = "Item")]
public class Item : ScriptableObject
{
    public string ItemName, ItemInfo;
    public int ID, TypeID;
    public int BuyMoney, SellMoney;
    public Sprite sprite;
    public Sprite[] spritearray;
    public Item neededItem;
    public Crop Crop;
}
