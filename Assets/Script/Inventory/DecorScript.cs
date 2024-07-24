using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorScript : MonoBehaviour
{
    SpriteRenderer rd;
    public WorldScript World;
    public bool IsDecor;
    void Start()
    {
        rd = GetComponent<SpriteRenderer>();
        World = GameObject.Find("World").GetComponent<WorldScript>();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        rd.color = Color.red;
        IsDecor = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rd.color = Color.white;
        IsDecor = true;
    }
}
