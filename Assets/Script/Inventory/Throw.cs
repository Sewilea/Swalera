using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Item myitem;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        

        if (gameObject.tag == "Log")
        {
            spriteRenderer.sprite = myitem.spritearray[4];
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (myitem.spritearray.Length != 0)
        {
            spriteRenderer.sprite = myitem.spritearray[UnityEngine.Random.Range(0,myitem.spritearray.Length)];
        }
        else
        {
            spriteRenderer.sprite = myitem.sprite;
        }
    }
}
