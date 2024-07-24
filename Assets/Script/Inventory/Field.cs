using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public SpriteRenderer fieldrenderer;
    public GameObject FieldObject;
    Inventory Inventory;

    [Header("Field")]
    public Item Item;
    public Crop Crop;
    public bool time;
    public float timer;
    public int Grownumber;
    public bool isCollect;
    public bool isWet;

    [Header("FieldSprite")]
    public Sprite[] Dryfields;
    public Sprite[] Wetfields;
    public int fieldtype;

    void Start()
    {
        Inventory = GameObject.Find("World").GetComponent<Inventory>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Item != null)
        {
            time = true;
            Growing();
            fieldrenderer.enabled = true;
        }
        else
        {
            fieldrenderer.enabled = false;
            isCollect = false;
        }

        if (isWet)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Wetfields[fieldtype];
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Dryfields[fieldtype];
        }

        if (time && isWet)
        {
            timer += 1 * Time.deltaTime;

            if (timer > Crop.GrowSpeed)
            {
                Grownumber++;
                timer = 0;
            }
        }
    }


    public void Growing()
    {
        if (Grownumber < 3)
        {
            fieldrenderer.sprite = Crop.Grow[Grownumber];
        }

        if (Grownumber >= 3)
        {
            Grownumber = 3;
            fieldrenderer.sprite = Crop.Grow[Grownumber];
            time = false;
            isCollect = true;
        }
    }
}
