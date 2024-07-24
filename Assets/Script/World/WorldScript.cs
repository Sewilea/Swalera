using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class WorldScript : MonoBehaviour
{
    Inventory inventory;
    public PanelControl gameinterface;

    [Header("World")]
    public List<ParpObject> ParpObjects;
    public List<Parp> Parps, FieldParps;
    public GameObject Stone, Field;
    public List<Daro> Daros;

    [Header("Player")]
    public PlayerScript Player;
    public GameObject PlayerObject;
    public float DistancetoMouse; 
    public GameObject WayLight;
    public GameObject DecorLight;


    [Header("Building")]
    public Vector2 House;
    public Vector2 HouseIn;
    public Vector2 Mine;
    public Vector2 MineIn;
    public Vector2 Bed;

    [Header("Musics")]
    public AudioSource WoodHit;
    public AudioSource StoneHit;
    public AudioSource FiberHit;
    public AudioSource DirtHit;
    public Item Axe;
    public Item Picaxe;
    public Item Fiber;

    [Header("Mining")]
    public int terDetail = 32;
    public float Size_Y = 16;

    void Start()
    {
        inventory = GameObject.Find("World").GetComponent<Inventory>();
        CreateParp();
        inventory.AddDecor(inventory.items[14],new Vector2(61,7));
        inventory.AddDecor(inventory.items[13], new Vector2(61, 6));
        inventory.AddDecor(inventory.items[13], new Vector2(62, 7));
        inventory.AddDecor(inventory.items[15], new Vector2(61, 4));
    }

    void Update()
    {
        MouseWork();
        
    }

    public void MouseWork()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 MousePosPure = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MousePos.x = Mathf.Round(MousePos.x);
        MousePos.y = Mathf.Round(MousePos.y);

        DistancetoMouse = Vector2.Distance(MousePos, Player.PlayerPosition);
        bool isclose = DistancetoMouse < 1.5f;

        if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && isclose)
        {
            WayLight.SetActive(true);
            WayLight.transform.position = MousePos;
        }
        else
        {
            WayLight.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(inventory.Chooseitem.TypeID == 6)
            {
                inventory.DeleteItem(inventory.slots[inventory.chooseorder]);
                Player.energy += 20;
            }
        }

        if (Input.GetMouseButtonDown(1) && isclose)
        {
            if (Parps.Contains(new Parp(MousePos)) == true)
            {
                Parp ParpMouse = Parps[Parps.IndexOf(new Parp(MousePos))];
                if (ParpMouse.Top != null && !ParpMouse.isDaro)
                {
                    Field field = ParpMouse.Top.GetComponent<Field>();
                    if (field.Item == null)
                    {
                        if (inventory.Chooseitem.TypeID == 4)
                        {
                            field.Grownumber = 0;
                            field.Item = inventory.Chooseitem;
                            field.Crop = inventory.Chooseitem.Crop;
                            inventory.DeleteItem(inventory.slots[inventory.chooseorder]);
                            DirtHit.Play();
                        }
                    }
                    else if(field.isCollect)
                    {
                        inventory.AddItem(field.Crop.ICrop,UnityEngine.Random.Range(1,4));
                        field.Item = null;
                        field.isWet = false;
                        
                    }

                    if(inventory.Chooseitem.TypeID == 2 && inventory.Chooseitem.ID == 4)
                    {
                        field.isWet = true;
                    }
                    
                }

            }

            if (Daros.Contains(new Daro(MousePos)) == true)
            {
                Daro DaroMouse = Daros[Daros.IndexOf(new Daro(MousePos))];
                Item itemm = DaroMouse.Object.GetComponent<Throw>().myitem;

                if(itemm.TypeID == 3 && itemm.ID == 3)
                {
                    inventory.ThisChest = DaroMouse.Object.GetComponent<Chest>();
                    inventory.ChesttoInventory();
                    gameinterface.MainPanel_Bool = true;
                    inventory.ChestPanel.SetActive(true);
                }
                if (itemm.TypeID == 3 && itemm.ID == 4)
                {
                    gameinterface.MainPanel_Bool = true;
                    gameinterface.FurnacePanel.SetActive(true);
                }

            }
        }

        if (Input.GetMouseButtonDown(0) && isclose)
        {
            
            if(Parps.Contains(new Parp(MousePos)) == true)
            {
                //Debug.Log("1");
                Parp ParpMouse = Parps[Parps.IndexOf(new Parp(MousePos))];

                if (ParpMouse.Top == null)
                {
                    //Debug.Log("2");
                    if (!ParpMouse.isTop && !ParpMouse.isFarm && inventory.Chooseitem.ID == 3 && inventory.Chooseitem.TypeID == 2)
                    {
                        //Debug.Log("3");
                        GameObject Obje = Instantiate(Field, MousePos, Quaternion.identity, gameObject.transform);
                        FieldParps.Add(ParpMouse);
                        ParpMouse.Top = Obje;
                        ParpMouse.isFarm = true;
                        DirtHit.Play();
                        Player.energy -= 2;
                        FieldOptimization(ParpMouse);
                    }
                }
                else
                {
                    if (inventory.Chooseitem != null)
                    {
                        if (inventory.Chooseitem.ID == 2 && inventory.Chooseitem.TypeID == 2)
                        {
                            if (ParpMouse.isFarm)
                            {
                                Field field = ParpMouse.Top.GetComponent<Field>();
                                if(field.Item == null)
                                {
                                    Destroy(ParpMouse.Top);
                                    Player.AnimationHit();
                                    FieldParps.Remove(ParpMouse);
                                    ParpMouse.isFarm = false;
                                    DirtHit.Play();
                                    Player.energy -= 2;
                                }
                            }
                        }
                    }

                    Item itemm = null;

                    itemm = ParpMouse.Top.GetComponent<Throw>().myitem;
                    if (ParpMouse.isTop && itemm.TypeID != 3)
                    {
                        itemm = ParpMouse.Top.GetComponent<Throw>().myitem;
                        if (ParpMouse.Top.GetComponent<Throw>().myitem.neededItem != null)
                        {
                            Item Need = ParpMouse.Top.GetComponent<Throw>().myitem.neededItem;

                            if (Need == inventory.Chooseitem)
                            {
                                inventory.CreateThrow(ParpMouse.Top.GetComponent<Throw>().myitem, ParpMouse.Position);
                                ParpMouse.isTop = false;
                                Destroy(ParpMouse.Top);
                                Player.AnimationHit();
                                Player.energy -= 2;

                                if (Need == Axe)
                                {
                                    WoodHit.Play();
                                }
                                if (Need == Picaxe)
                                {
                                    StoneHit.Play();
                                }

                            }
                        }
                        else
                        {
                            inventory.CreateThrow(ParpMouse.Top.GetComponent<Throw>().myitem, ParpMouse.Position);
                            ParpMouse.isTop = false;
                            Destroy(ParpMouse.Top);
                            Player.AnimationHit();
                            Player.energy -= 2;

                            if (itemm == Fiber)
                            {
                                FiberHit.Play();
                            }
                        }
                    }
                }

            }

            if (Daros.Contains(new Daro(MousePos)) == true)
            {
                Daro DaroMouse = Daros[Daros.IndexOf(new Daro(MousePos))];
                Item itemm = DaroMouse.Object.GetComponent<Throw>().myitem;
                Chest chest = DaroMouse.Object.GetComponent<Chest>();

                if (itemm.neededItem == inventory.Chooseitem && !chest.IsFull())
                {
                    if (Parps.Contains(new Parp(MousePos)) == true)
                    {
                        Parp ParpMouse = Parps[Parps.IndexOf(new Parp(MousePos))];
                        ParpMouse.isTop = false;
                        ParpMouse.isDaro = false;
                        Player.energy -= 2;
                    }

                    inventory.CreateThrow(itemm, DaroMouse.Position);
                    Daros.Remove(new Daro(MousePos));
                    Destroy(DaroMouse.Object);
                    Player.AnimationHit();
                }

            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(inventory.Chooseitem.ID == 5 && inventory.Chooseitem.TypeID == 2)
            {
                Player.AnimationHit();
            }
        }

        TakeDecor(MousePos);

        if (Input.GetKeyDown(KeyCode.Q) && isclose)
        {
            if(inventory.MouseSlot.item != null)
            {
                inventory.MouseSlot.Amount -= 1;
                inventory.CreateThrow(inventory.MouseSlot.item, MousePosPure);
            }
        }
    }

    void TakeDecor(Vector2 MousePos)
    {
        Vector2 MousePos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 Vk = new Vector2(Mathf.Round(PlayerObject.transform.position.x), Mathf.Round(PlayerObject.transform.position.y));
        float Distance = Vector2.Distance(Vk, MousePos2);

        if (inventory.Chooseitem != null && inventory.Chooseitem.TypeID == 3)
        {
            DecorLight.SetActive(true);
            DecorLight.transform.position = MousePos;

            DecorScript Dc = DecorLight.GetComponent<DecorScript>();

            if (Dc.IsDecor && Input.GetMouseButtonDown(1) && Distance < 3)
            {
                GameObject Obje = Instantiate(Stone, MousePos, Quaternion.identity, gameObject.transform);
                Obje.GetComponent<Throw>().myitem = inventory.Chooseitem;
                inventory.slots[inventory.chooseorder].Amount--;
                Daro daro = new Daro(MousePos);
                daro.Object = Obje;
                Daros.Add(daro);

                Parp ParpMouse = Parps[Parps.IndexOf(new Parp(MousePos))];

                if (ParpMouse != null)
                {
                    ParpMouse.Top = Obje;
                    ParpMouse.isTop = true;
                    ParpMouse.isDaro = true;
                }
            }
        }
        else
        {
            DecorLight.SetActive(false);
        }

        

    }
    void CreateParp()
    {
        for (int i = 0; i < ParpObjects.Count; i++)
        {
            Vector2 TopLeft = ParpObjects[i].TopLeft;  
            Vector2 TopRight = ParpObjects[i].TopRight;  
            Vector2 BottomLeft = ParpObjects[i].BottomLeft;  
            Vector2 BottomRight = ParpObjects[i].BottomRight;
            
            for (float x = TopLeft.x; x < TopRight.x + 1; x++)
            {
                for (float y = BottomLeft.y; y < TopLeft.y + 1; y++)
                {
                    if(ParpObjects[i].BannedVector.Length == 0)
                    {
                        Parps.Add(new Parp(new Vector2(x, y)));
                    }
                    else
                    {
                        bool isBanned = false;
                        foreach (Vector2 z in ParpObjects[i].BannedVector)
                        {
                            if(z == new Vector2 (x, y))
                            {
                                isBanned = true;
                            }
                        }

                        if (!isBanned)
                        {
                            Parps.Add(new Parp(new Vector2(x, y)));
                            Parp parp = Parps[Parps.IndexOf(new Parp(new Vector2(x, y)))];
                            parp.isCave = true;
                        }
                    }
                }
            }
        }

        for (int t = 0; t < Parps.Count; t++)
        {
            float a = UnityEngine.Random.Range(0,7);
            Parp parp = Parps[t];
            
            if (parp.isCave)
            {
                Vector2 Vk = parp.Position;
                int seed = UnityEngine.Random.Range(1000, 9999);
                int maxY = (int)(Mathf.PerlinNoise((Vk.x + seed) / terDetail, (Vk.y + seed) / terDetail) * Size_Y);
                float b = UnityEngine.Random.Range(0, 20);

                if (maxY < 7)
                {
                    if(b > 0 && b < 9)
                    {
                        Parps[t].isTop = true;

                        GameObject Obje = Instantiate(Stone, Vk, Quaternion.identity, gameObject.transform);
                        Obje.GetComponent<Throw>().myitem = inventory.items[0];
                        Parps[t].Top = Obje;
                    }
                    if (b >= 9 && b < 12)
                    {
                        Parps[t].isTop = true;

                        GameObject Obje = Instantiate(Stone, Vk, Quaternion.identity, gameObject.transform);
                        Obje.GetComponent<Throw>().myitem = inventory.items[8];
                        Parps[t].Top = Obje;
                    }
                    if (b >= 12 && b < 14)
                    {
                        Parps[t].isTop = true;

                        GameObject Obje = Instantiate(Stone, Vk, Quaternion.identity, gameObject.transform);
                        Obje.GetComponent<Throw>().myitem = inventory.items[9];
                        Parps[t].Top = Obje;
                    }
                    if (b == 14)
                    {
                        Parps[t].isTop = true;

                        GameObject Obje = Instantiate(Stone, Vk, Quaternion.identity, gameObject.transform);
                        Obje.GetComponent<Throw>().myitem = inventory.items[10];
                        Parps[t].Top = Obje;
                    }
                }
            }
            else
            {
                if (a == 0)
                {
                    Parps[t].isTop = true;
                    Vector2 Vk = Parps[t].Position;

                    GameObject Obje = Instantiate(Stone, Vk, Quaternion.identity, gameObject.transform);
                    Obje.GetComponent<Throw>().myitem = inventory.items[0];
                    Parps[t].Top = Obje;
                }
                if (a == 1)
                {
                    Parps[t].isTop = true;
                    Vector2 Vk = Parps[t].Position;

                    GameObject Obje = Instantiate(Stone, Vk, Quaternion.identity, gameObject.transform);
                    Obje.GetComponent<Throw>().myitem = inventory.items[1];
                    Parps[t].Top = Obje;
                }
                if (a == 2)
                {
                    Parps[t].isTop = true;
                    Vector2 Vk = Parps[t].Position;

                    GameObject Obje = Instantiate(Stone, Vk, Quaternion.identity, gameObject.transform);
                    Obje.GetComponent<Throw>().myitem = inventory.items[2];
                    Parps[t].Top = Obje;
                }
                if (a == 3)
                {

                    Vector2 Vk = Parps[t].Position;

                    List<Vector2> VectorDistance = new List<Vector2>();
                    bool isPlace = true;

                    for (int x = -5; x < 6; x++)
                    {
                        for (int y = -5; y < 6; y++)
                        {
                            VectorDistance.Add(new Vector2(x + Vk.x, y + Vk.y));
                        }
                    }

                    for (int i = 0; i < Parps.Count; i++)
                    {
                        if (Parps[i].isTop && Parps[i].Top != null)
                        {
                            if (Parps[i].Top.tag == "Log")
                            {
                                Vector2 Vk2 = Parps[i].Position;

                                if (VectorDistance.Contains(Vk2))
                                {
                                    isPlace = false;
                                }
                            }
                        }

                    }

                    if (isPlace)
                    {
                        Parps[t].isTop = true;
                        GameObject Obje = Instantiate(Stone, Vk, Quaternion.identity, gameObject.transform);
                        Obje.GetComponent<Throw>().myitem = inventory.items[1];
                        Obje.tag = "Log";
                        Parps[t].Top = Obje;
                    }
                }
            }

        }
    }

    // Optimization

    public void FieldOptimization(Parp parp)
    {
        bool xp, xm, yp, ym;
        bool fxp = false, fxm = false, fyp = false, fym = false;

        xp = Parps.Contains(new Parp(new Vector2(parp.Position.x + 1,parp.Position.y)));
        xm = Parps.Contains(new Parp(new Vector2(parp.Position.x - 1, parp.Position.y)));
        yp = Parps.Contains(new Parp(new Vector2(parp.Position.x, parp.Position.y + 1)));
        ym = Parps.Contains(new Parp(new Vector2(parp.Position.x, parp.Position.y - 1)));
        
        if (xp)
        {
            Parp Parpxp = Parps[Parps.IndexOf(new Parp(new Vector2(parp.Position.x + 1, parp.Position.y)))];
            if (Parpxp.isFarm)
            {
                fxp = true;
                FieldOptimizationOne(Parpxp);
            }
        }
        if (xm)
        {
            Parp Parpxp = Parps[Parps.IndexOf(new Parp(new Vector2(parp.Position.x - 1, parp.Position.y)))];
            if (Parpxp.isFarm)
            {
                fxm = true;
                FieldOptimizationOne(Parpxp);
            }
        }
        if (yp)
        {
            Parp Parpxp = Parps[Parps.IndexOf(new Parp(new Vector2(parp.Position.x, parp.Position.y + 1)))];
            if (Parpxp.isFarm)
            {
                fyp = true;
                FieldOptimizationOne(Parpxp);
            }
        }
        if (ym)
        {
            Parp Parpxp = Parps[Parps.IndexOf(new Parp(new Vector2(parp.Position.x, parp.Position.y - 1)))];
            if (Parpxp.isFarm)
            {
                fym = true;
                FieldOptimizationOne(Parpxp);
            }
        }
        //Debug.Log(fxp.ToString() + fxm.ToString() + fyp.ToString() + fym.ToString());
        Field field = parp.Top.GetComponent<Field>();

        if(fxp && !fxm && !fyp && !fym)
        {
            field.fieldtype = 1;
        }
        if (!fxp && !fxm && fyp && !fym)
        {
            field.fieldtype = 2;
        }
        if (!fxp && fxm && !fyp && !fym)
        {
            field.fieldtype = 3;
        }
        if (!fxp && !fxm && !fyp && fym)
        {
            field.fieldtype = 4;
        }
        if (!fxp && !fxm && fyp && fym)
        {
            field.fieldtype = 5;
        }
        if (fxp && fxm && !fyp && !fym)
        {
            field.fieldtype = 6;
        }
        if (fxp && !fxm && fyp && !fym)
        {
            field.fieldtype = 7;
        }
        if (fxp && fxm && fyp && !fym)
        {
            field.fieldtype = 8;
        }
        if (fxp && !fxm && !fyp && fym)
        {
            field.fieldtype = 9;
        }
        if (!fxp && fxm && !fyp && fym)
        {
            field.fieldtype = 10;
        }
        if (!fxp && fxm && fyp && !fym)
        {
            field.fieldtype = 11;
        }
        if (fxp && fxm && !fyp && fym)
        {
            field.fieldtype = 12;
        }
        if (fxp && !fxm && fyp && fym)
        {
            field.fieldtype = 13;
        }
        if (!fxp && fxm && fyp && fym)
        {
            field.fieldtype = 14;
        }
        if (fxp && fxm && fyp && fym)
        {
            field.fieldtype = 15;
        }
    }
    public void FieldOptimizationOne(Parp parp)
    {
        bool xp, xm, yp, ym;
        bool fxp = false, fxm = false, fyp = false, fym = false;

        xp = Parps.Contains(new Parp(new Vector2(parp.Position.x + 1, parp.Position.y)));
        xm = Parps.Contains(new Parp(new Vector2(parp.Position.x - 1, parp.Position.y)));
        yp = Parps.Contains(new Parp(new Vector2(parp.Position.x, parp.Position.y + 1)));
        ym = Parps.Contains(new Parp(new Vector2(parp.Position.x, parp.Position.y - 1)));

        if (xp)
        {
            Parp Parpxp = Parps[Parps.IndexOf(new Parp(new Vector2(parp.Position.x + 1, parp.Position.y)))];
            if (Parpxp.isFarm)
            {
                fxp = true;
            }
        }
        if (xm)
        {
            Parp Parpxp = Parps[Parps.IndexOf(new Parp(new Vector2(parp.Position.x - 1, parp.Position.y)))];
            if (Parpxp.isFarm)
            {
                fxm = true;
            }
        }
        if (yp)
        {
            Parp Parpxp = Parps[Parps.IndexOf(new Parp(new Vector2(parp.Position.x, parp.Position.y + 1)))];
            if (Parpxp.isFarm)
            {
                fyp = true;
            }
        }
        if (ym)
        {
            Parp Parpxp = Parps[Parps.IndexOf(new Parp(new Vector2(parp.Position.x, parp.Position.y - 1)))];
            if (Parpxp.isFarm)
            {
                fym = true;
            }
        }
        //Debug.Log(fxp.ToString() + fxm.ToString() + fyp.ToString() + fym.ToString());
        Field field = parp.Top.GetComponent<Field>();

        if (fxp && !fxm && !fyp && !fym)
        {
            field.fieldtype = 1;
        }
        if (!fxp && !fxm && fyp && !fym)
        {
            field.fieldtype = 2;
        }
        if (!fxp && fxm && !fyp && !fym)
        {
            field.fieldtype = 3;
        }
        if (!fxp && !fxm && !fyp && fym)
        {
            field.fieldtype = 4;
        }
        if (!fxp && !fxm && fyp && fym)
        {
            field.fieldtype = 5;
        }
        if (fxp && fxm && !fyp && !fym)
        {
            field.fieldtype = 6;
        }
        if (fxp && !fxm && fyp && !fym)
        {
            field.fieldtype = 7;
        }
        if (fxp && fxm && fyp && !fym)
        {
            field.fieldtype = 8;
        }
        if (fxp && !fxm && !fyp && fym)
        {
            field.fieldtype = 9;
        }
        if (!fxp && fxm && !fyp && fym)
        {
            field.fieldtype = 10;
        }
        if (!fxp && fxm && fyp && !fym)
        {
            field.fieldtype = 11;
        }
        if (fxp && fxm && !fyp && fym)
        {
            field.fieldtype = 12;
        }
        if (fxp && !fxm && fyp && fym)
        {
            field.fieldtype = 13;
        }
        if (!fxp && fxm && fyp && fym)
        {
            field.fieldtype = 14;
        }
        if (fxp && fxm && fyp && fym)
        {
            field.fieldtype = 15;
        }
    }


    [System.Serializable]
    public class Parp : IEquatable<Parp>
    {

        public Parp(Vector2 h)
        {
            this.Position = h;
        }
        public Vector2 Position { get; set; }
        public bool isTop { get; set; }
        public bool isFarm { get; set; }

        public bool isDaro { get; set; }
        public bool isCave { get; set; }
        public GameObject Top { get; set; }

        public bool Equals(Parp other)
        {
            if (this.Position == other.Position)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    [System.Serializable]
    public class Daro : IEquatable<Daro>
    {

        public Daro(Vector2 h)
        {
            this.Position = h;
        }
        public Vector2 Position { get; set; }
        public GameObject Object { get; set; }

        public bool Equals(Daro other)
        {
            if (this.Position == other.Position)
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
