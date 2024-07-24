using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerScript : MonoBehaviour
{
    public float Distance;
    public float x, y;
    public Vector3 target;
    public float Speed = 2;
    SpriteRenderer rd;
    Rigidbody2D rb;
    public Sprite Top, Down, Left, Right;

    [Header("Follow")]
    public Vector2[] followLoop;
    public bool RandomMovement, LoopMovement, RandomRotation;
    public int LoopX;

    void Start()
    {
        rd = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (RandomRotation)
        {
            Invoke("Npc_Rotation", 5);
        }
        if (RandomMovement)
        {
            Invoke("hareket", 1);
        }
        if (LoopMovement)
        {
            target = followLoop[LoopX];
            MovementDirection(target);
        }
    }

    void Update()
    {
        if (RandomRotation)
        {
            rb.velocity = Vector2.zero;
        }

        bool newtarget = false;
        if (x != 0 && x == 1)
        {
            if (target != null && gameObject.transform.position.x < target.x)
            {
                rb.velocity = new Vector3((Speed * 5) * Time.deltaTime, 0, 0);
                rd.sprite = Right;
            }
            else if (gameObject.transform.position.x >= target.x && RandomMovement)
            {
                rb.velocity = new Vector3(0, 0, 0);
                x = 0;
                Invoke("hareket", 1);
            }
            else if (gameObject.transform.position.x >= target.x && LoopMovement)
            {
                newtarget = true;
            }
        }
        if (x != 0 && x == -1)
        {
            if (target != null && gameObject.transform.position.x > target.x)
            {
                rb.velocity = new Vector3((-Speed * 5) * Time.deltaTime, 0, 0);
                rd.sprite = Left;
            }
            else if (gameObject.transform.position.x <= target.x && RandomMovement)
            {
                rb.velocity = new Vector3(0, 0, 0);
                x = 0;
                Invoke("hareket", 1);
            }
            else if (gameObject.transform.position.x <= target.x && LoopMovement)
            {
                newtarget = true;
            }
        }
        if (y != 0 && y == 1)
        {
            if (target != null && gameObject.transform.position.y < target.y)
            {
                rb.velocity = new Vector3(0, (Speed * 5) * Time.deltaTime, 0);
                rd.sprite = Top;
            }
            else if (gameObject.transform.position.y >= target.y && RandomMovement)
            {
                rb.velocity = new Vector3(0, 0, 0);
                y = 0;
                Invoke("hareket", 1);
            }
            else if (gameObject.transform.position.y >= target.y && LoopMovement)
            {
                newtarget = true;
            }
        }
        if (y != 0 && y == -1)
        {
            if (target != null && gameObject.transform.position.y > target.y)
            {
                rb.velocity = new Vector3(0, (-Speed * 5) * Time.deltaTime, 0);
                rd.sprite = Down;
            }
            else if (gameObject.transform.position.y <= target.y && RandomMovement)
            {
                rb.velocity = new Vector3(0, 0, 0);
                y = 0;
                Invoke("hareket", 1);
            }
            else if (gameObject.transform.position.y <= target.y && LoopMovement)
            {
                newtarget = true;
            }
        }
        if (newtarget)
        {
            LoopX++;
            if (LoopX > followLoop.Length - 1)
            {
                LoopX = 0;
            }
            target = followLoop[LoopX];
            MovementDirection(target);
        }

    }

    public void MovementDirection(Vector2 Target)
    {
        Vector2 me = gameObject.transform.position;
        if(target.x < me.x)
        {
            x = -1;
        }
        if (target.x > me.x)
        {
            x = 1;
        }
        if (target.y < me.y)
        {
            y = -1;
        }
        if (target.y > me.y)
        {
            y = 1;
        }
    }

    public void Npc_Rotation()
    {
        int yön = Random.Range(0, 4);
        if(yön == 0)
        {
            rd.sprite = Top;
        }if(yön == 1)
        {
            rd.sprite = Down;
        }if(yön == 2)
        {
            rd.sprite = Left;
        }if(yön == 3)
        {
            rd.sprite = Right;
        }
        Invoke("Npc_Rotation", 5);
    }

    public void hareket()
    {
        if (Random.Range(0, 2) == 1)
        {
            int yön = Random.Range(0, 4);
            float yer = Random.Range(1, 6);

            if (yön == 0)
            {
                target = gameObject.transform.position;
                target.x = target.x + yer;
                x = 1;
            }
            if (yön == 1)
            {
                target = gameObject.transform.position;
                target.x = target.x - yer;
                x = -1;
            }
            if (yön == 2)
            {
                target = gameObject.transform.position;
                target.y = target.y + yer;
                y = 1;
            }
            if (yön == 3)
            {
                target = gameObject.transform.position;
                target.y = target.y - yer;
                y = -1;
            }
        }
        if (x == 0 && y == 0)
        {
            Invoke("hareket", 1);
        }
    }
}
