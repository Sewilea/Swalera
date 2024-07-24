using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Slot> slots;
    public List<GameObject> slotobjects; // 32 den sonrası sandık
    public List<Item> items;
    public GameObject Throw;

    public WorldScript WorldScript;
    public PlayerScript player;

    [Header("Chest")]
    public Chest ThisChest;
    public List<Slot> Chest_Slots;
    public GameObject ChestPanel;
    public Item Empty;

    [Header("Mouse")]
    public Slot MouseSlot;
    public GameObject MouseSlotObjects;

    [Header("InventoryLog")]
    public GameObject InvenLog;
    public GameObject TextLog;

    public Vector2 CursorPosition;
    public GameObject Cursors;

    public int chooseorder;
    public Item Chooseitem;

    void Start()
    {
        EmptyItems();
        AddItem(items[12]);
        AddItem(items[3]);
        AddItem(items[4]);
        AddItem(items[6]);
        AddItem(items[11]);
        
    }

    void Update()
    {
        CursorPosition = Input.mousePosition;
        Cursor.visible = false;
        Cursors.GetComponent<RectTransform>().position = Input.mousePosition;

        SlottoSlot();
        KeyK();
    }
    void KeyK()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            chooseorder = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            chooseorder = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            chooseorder = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            chooseorder = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            chooseorder = 4;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            chooseorder = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            chooseorder = 6;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            chooseorder = 7;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            chooseorder--;
            if (chooseorder < 0)
            {
                chooseorder = 7;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            chooseorder++;
            if (chooseorder > 7)
            {
                chooseorder = 0;
            }
        }

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Choose = false;
        }

        slots[chooseorder].Choose = true;
        Chooseitem = slots[chooseorder].item;
    }

    public void EmptyItems()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].item = Empty;
        }
    }

    public void SlottoSlot()
    {
        if (MouseSlot.Amount <= 0)
        {
            MouseSlot.item = Empty;
            MouseSlotObjects.transform.GetChild(0).GetComponent<Image>().sprite = null;
            MouseSlotObjects.transform.GetChild(0).GetComponent<Image>().enabled = false;
            MouseSlot.Amount = 0;
        }

        
        if (MouseSlot.item != Empty)
        {
            MouseSlotObjects.transform.GetChild(0).GetComponent<Image>().enabled = true;
            MouseSlotObjects.transform.GetChild(0).GetComponent<Image>().sprite = MouseSlot.item.sprite;
            MouseSlotObjects.transform.GetChild(1).GetComponent<Text>().text = MouseSlot.Amount.ToString();

            if(MouseSlot.item.TypeID == 2)
            {
                MouseSlotObjects.transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }
        if (MouseSlot.Amount < 2)
        {
            MouseSlotObjects.transform.GetChild(1).GetComponent<Text>().text = "";
        }


        for (int i = 0;i < slots.Count; i++)
        {
            if (!slots[i].Choose)
            {
                slotobjects[i].transform.GetChild(2).GetComponent<Image>().enabled = false;
            }
            if (slots[i].Choose)
            {
                slotobjects[i].transform.GetChild(2).GetComponent<Image>().enabled = true;
            }

            if (slots[i].Amount <= 0)
            {
                slots[i].item = Empty;
                slotobjects[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slotobjects[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].Amount = 0;
            }
            
            if (slots[i].item != Empty)
            {
                slotobjects[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slotobjects[i].transform.GetChild(0).GetComponent<Image>().sprite = slots[i].item.sprite;
                slotobjects[i].transform.GetChild(1).GetComponent<Text>().text = slots[i].Amount.ToString();

                if (slots[i].item.TypeID == 2)
                {
                    slotobjects[i].transform.GetChild(1).GetComponent<Text>().text = "";
                }
            }
            if (slots[i].Amount < 2)
            {
                slotobjects[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }
    }

    public void AddItem(Item item, int Amout)
    {
        ItemLog(item, Amout);
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == Empty)
            {
                slots[i].item = item;
                slots[i].Amount = Amout;
                break;
            }
            else if (slots[i].item == item)
            {
                slots[i].Amount = slots[i].Amount + Amout;
                if (slots[i].Amount > 16)
                {
                    Amout = slots[i].Amount - 16;
                    slots[i].Amount = 16;

                    continue;
                }
                else if (slots[i].Amount == 16)
                {
                    break;
                }
                else
                {
                    break;
                }
            }

        }
    }

    public void AddItem(Item item)
    {
        ItemLog(item, 1);
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == Empty)
            {
                slots[i].item = item;
                slots[i].Amount = 1;
                break;
            }
            else if (slots[i].item == item)
            {
                slots[i].Amount = slots[i].Amount + 1;
                if (slots[i].Amount > 16)
                {
                    slots[i].Amount = 16;

                    continue;
                }
                else if (slots[i].Amount == 16)
                {
                    break;
                }
                else
                {
                    break;
                }
            }
        }
    }

    public void AddDecor(Item item, Vector2 Vk)
    {
        GameObject Obje = Instantiate(WorldScript.Stone, Vk, Quaternion.identity, gameObject.transform);
        Obje.GetComponent<Throw>().myitem = item;
        WorldScript.Daro daro = new WorldScript.Daro(Vk);
        daro.Object = Obje;
        WorldScript.Daros.Add(daro);

        //Parp ParpMouse = WorldScript.Parps[WorldScript.Parps.IndexOf(new Parp(Vk))];

        //if (ParpMouse != null)
        //{
        //    ParpMouse.Top = Obje;
        //    ParpMouse.isTop = true;
        //    ParpMouse.isDaro = true;
        //}
    }

    public void ItemLog(Item item, int Amout)
    {
        string txt = item.name + "   + " + Amout.ToString();
        GameObject Object = Instantiate(TextLog, InvenLog.transform);
        Object.GetComponent<Text>().text = txt;
        Destroy(Object,2f);
    }

    public void AnyLog(string txt)
    {
        GameObject Object = Instantiate(TextLog, InvenLog.transform);
        Object.GetComponent<Text>().text = txt;
        Destroy(Object, 2f);
    }

    public void ChesttoInventory()
    {
        for (int i = 32; i < slots.Count; i++)
        {
            slots[i].item = ThisChest.SlotInChest[i - 32].item;
            slots[i].Amount = ThisChest.SlotInChest[i - 32].amount;
        }
    }

    public void ChestDelete()
    {
        for (int i = 32; i < slots.Count; i++)
        {
            slots[i].item = Empty;
            slots[i].item.sprite = null;
            slots[i].Amount = 0;
        }
        ThisChest = null;
    }

    public void InventorytoChest()
    {
        for (int i = 32; i < slots.Count; i++)
        {
            ThisChest.SlotInChest[i - 32].item = slots[i].item;
            ThisChest.SlotInChest[i - 32].amount = slots[i].Amount;
        }
        ChestDelete();
    }

    public Item Find(float id, float typeid)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == id && items[i].TypeID == typeid)
            {
                return items[i];
            }
        }
        return null;
    }

    public void DeleteItem(Slot slot)
    {
        slot.Amount -= 1;
    }

    public void DeleteItem(Slot slot , int Amout)
    {
        slot.Amount -= Amout;
    }

    public void PlaceItem(GameObject Object)
    {
        
        Slot slot = slots[slotobjects.IndexOf(Object)];

        if (slot.item == MouseSlot.item)
        {
            slot.Amount += MouseSlot.Amount;

            if (slot.Amount == 16)
            {
                int Amount2 = slot.Amount;
                slot.Amount = MouseSlot.Amount;
                MouseSlot.Amount = Amount2;
            }
            if (slot.Amount > 16)
            {
                MouseSlot.Amount = slot.Amount - 16;
                slot.Amount = 16;
            }
            if (slot.Amount < 16)
            {
                MouseSlot.Amount = 0;
            }
        }
        else
        {
            Item item2 = slot.item;
            slot.item = MouseSlot.item;
            MouseSlot.item = item2;

            int Amount2 = slot.Amount;
            slot.Amount = MouseSlot.Amount;
            MouseSlot.Amount = Amount2;
        }
    }

    public void CreateThrow(Item item , Vector2 Vk)
    {
        GameObject Obje = Instantiate(Throw, Vk, Quaternion.identity, gameObject.transform);
        Obje.GetComponent<Throw>().myitem = item;
    }

    public void Craft(Item need, int amount, Item give, int give_amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Slot slot = slots[i];

            if (slot.item.ID == need.ID && slot.item.TypeID == need.TypeID)
            {
                if (slot.Amount >= amount)
                {
                    slot.Amount -= amount;

                    AddItem(give, give_amount);
                    break;
                }
            }
        }
    }

    [System.Serializable]
    public class Slot : IEquatable<Slot>
    {

        public Slot(Item item , int Amount)
        {
            this.item = item;
            this.Amount = Amount;
        }
        public Item item;
        public int Amount;
        public bool Choose;

        public bool Equals(Slot other)
        {
            if (this.item == other.item)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


