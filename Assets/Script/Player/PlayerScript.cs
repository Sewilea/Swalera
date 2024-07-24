using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator Animator;
    Inventory inventory;
    Dialogue dialogue;
    Clock clock;
    WorldScript worldScript;
    public Vector2 PlayerPosition;
    public GameObject Camera;

    [Header("Movement")]
    public float x;
    public float y;
    public float Speed = 6;
    public direction Direction;
    public bool isWalk;
    public bool isHit;
    bool Bed_problem = true;

    [Header("Power")]
    public float Power;
    public float Defence;
    public Text PowerText;

    [Header("Energy_Stamina")]
    public float energy;
    public float maxenergy;
    public Text EnergyText;
    public float Stamina;
    public float Staminaspeed;
    public float maxStamina;
    public Text StaminaText;
    bool staminabool = true;
    public Text DirectionText;

    [Header("Musics")]
    public AudioSource Collect;
    public AudioSource Door;
    public Text PanelText;


    void Start()
    {
        inventory = GameObject.Find("World").GetComponent<Inventory>();
        dialogue = GameObject.Find("World").GetComponent <Dialogue>();
        clock = GameObject.Find("World").GetComponent<Clock>();
        worldScript = GameObject.Find("World").GetComponent<WorldScript>();
        rb2d = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        PowerText.text = "Speed : " + Speed.ToString() + "\n" + "Power : " + Power.ToString() + "\n" + "Defence : " + Defence.ToString();
        DirectionText.text = "Direction : " + Direction.ToString() + "\n" + "Choose : " + inventory.Chooseitem.name;
        PanelText.text = (int)transform.position.x + " " + (int)transform.position.y + " " + (int)transform.position.z;
        PlayerPosition = gameObject.transform.position;
        Movement();
        Energy_Stamina();
        HouseIn();
    }

    public void HouseIn()
    {
        Vector2 Vk = new Vector2(Mathf.Round(gameObject.transform.position.x), Mathf.Round(gameObject.transform.position.y));

        if(Vector2.Distance(Vk,worldScript.House) < 1 && Input.GetMouseButton(1))
        {
            clock.DayPassed();
            rb2d.velocity = Vector2.zero;
            Door.Play();
            Invoke("HouseEnter", 0.5f);
            
        }
        if (Vector2.Distance(Vk, worldScript.HouseIn) < 1 && Input.GetMouseButton(1))
        {
            clock.DayPassed();
            rb2d.velocity = Vector2.zero;
            Door.Play();
            Invoke("HouseExit", 0.5f);
        }
        if (Vector2.Distance(Vk, worldScript.Mine) < 1 && Input.GetMouseButton(1))
        {
            clock.DayPassed();
            rb2d.velocity = Vector2.zero;
            Door.Play();
            Invoke("MineEnter", 0.5f);
        }
        if (Vector2.Distance(Vk, worldScript.MineIn) < 1 && Input.GetMouseButton(1))
        {
            clock.DayPassed();
            rb2d.velocity = Vector2.zero;
            Door.Play();
            Invoke("MineExit", 0.5f);
        }
        if (Vector2.Distance(Vk, worldScript.Bed) < 3 && Input.GetMouseButton(1) && Bed_problem)
        {
            Bed_problem = false;
            clock.DayPassed();
            rb2d.velocity = Vector2.zero;
            Invoke("DayClock", 0.5f);
        }
    }

    public void DayClock()
    {
        clock.NextDay();
        Bed_problem = true;
    }

    public void HouseEnter()
    {
        gameObject.transform.position = worldScript.HouseIn;
        Camera.transform.position = worldScript.HouseIn;
    }
    public void HouseExit()
    {
        gameObject.transform.position = worldScript.House;
        Camera.transform.position = worldScript.House;
    }

    public void MineEnter()
    {
        gameObject.transform.position = worldScript.MineIn;
        Camera.transform.position = worldScript.MineIn;
    }
    public void MineExit()
    {
        gameObject.transform.position = worldScript.Mine;
        Camera.transform.position = worldScript.Mine;
    }


    public void Energy_Stamina()
    {
        EnergyText.text = "Energy : " + Mathf.Round(energy) + " / " + maxenergy;
        StaminaText.text = "Stamina : " + Mathf.Round(Stamina) + " / " + maxStamina;

        //healtbar.transform.localScale = new Vector2(1, healt / maxhealt);

        if (energy >= maxenergy)
        {
            energy = maxenergy;
        }

        if (energy < 0)
        {
            energy = 0;
        }

        if (Stamina >= maxStamina)
        {
            Stamina = maxStamina;
        }

        if (Stamina < 0)
        {
            Stamina = 0;
        }

    }

    void Movement()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (y != 0)
        {
            if (y > 0.1f)
            {
                Direction = direction.Top;
            }
            if (y < -0.1f)
            {
                Direction = direction.Bottom;
            }

        }
        if (x != 0)
        {
            if (x > 0.1f)
            {
                Direction = direction.Right;
            }
            if (x < -0.1f)
            {
                Direction = direction.Left;
            }
        }

        if(y != 0 || x != 0)
        {
            isWalk = true;
            
        }
        else
        {
            isWalk = false;

        }

        if(Input.GetKey(KeyCode.Z) && Stamina > 0 && staminabool)
        {
            Speed = 6;
            Stamina -= 0.7f;

            if (Stamina <= 0)
            {
                Stamina = 0;
                staminabool = false;
                Invoke("staminaboolenvoid", 3f);
            }
        }
        else
        {
            Speed = 4;
        }

        if (Stamina < maxStamina)
        {
            Stamina += Staminaspeed * Time.deltaTime;
        }

        if (!isHit)
        {
            if (isWalk)
            {
                rb2d.velocity = new Vector2(x, y) * Speed;
                AnimationWalk();
            }
            else
            {
                rb2d.velocity = Vector2.zero;
                AnimationIdle();
            }
        }
        
    }

    public void AnimationIdle()
    {
        if (Direction == direction.Top)
        {
            Animator.Play("TopIdle");
        }
        if (Direction == direction.Bottom)
        {
            Animator.Play("BottomIdle");
        }
        if (Direction == direction.Right)
        {
            Animator.Play("RightIdle");
        }
        if (Direction == direction.Left)
        {
            Animator.Play("LeftIdle");
        }
    }

    public void AnimationWalk()
    {
        if (Direction == direction.Top)
        {
            Animator.Play("TopWalk");
        }
        if (Direction == direction.Bottom)
        {
            Animator.Play("BottomWalk");
        }
        if (Direction == direction.Right)
        {
            Animator.Play("RightWalk");
        }
        if (Direction == direction.Left)
        {
            Animator.Play("LeftWalk");
        }
    }

    public void AnimationHit()
    {
        isHit = true;
        rb2d.velocity = Vector2.zero;
        if (Direction == direction.Top)
        {
            Animator.Play("TopHit");
        }
        if (Direction == direction.Bottom)
        {
            Animator.Play("BottomHit");
        }
        if (Direction == direction.Right)
        {
            Animator.Play("RightHit");
        }
        if (Direction == direction.Left)
        {
            Animator.Play("LeftHit");
        }
        Invoke("WalkContinue", 0.3f);
    }

    public void WalkContinue()
    {
        isHit = false;
    }

    void staminaboolenvoid()
    {
        staminabool = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Throw")
        {
            Throw Th = collision.GetComponent<Throw>();
            inventory.AddItem(Th.myitem, 1);
            Destroy(collision.gameObject);
            Collect.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Pink" && Input.GetMouseButton(1))
        {
            dialogue.Talk(dialogue.Pink);
        }
        if (collision.name == "Blue" && Input.GetMouseButton(1))
        {
            dialogue.Talk(dialogue.Blue);
        }
        if (collision.name == "Green" && Input.GetMouseButton(1))
        {
            dialogue.Talk(dialogue.Green);
        }
    }

    public enum direction
    {
        Top,
        Bottom,
        Left,
        Right
        
    } 
}
