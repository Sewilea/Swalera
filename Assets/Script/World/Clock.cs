using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public float time;
    public bool isTime;
    public int Minute, Hour, Day, Week;

    public Text ClockT, DayT;
    public WeekDay Weekday;

    public Image Night;
    public Animator AnimPass;
    Inventory inventory;
    PlayerScript PlayerSc;
    void Start()
    {
        inventory = GetComponent<Inventory>();
        PlayerSc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //DayPassed();
        }

        DayT.text = Weekday + "\n" + " Day : " + Day + "\n" + " Week : " + Week;
        ClockT.text = Hour + " : " + (Minute * 10).ToString();

        if (isTime)
        {
            time += Time.deltaTime * 1;

            if (time > 10)
            {
                Minute++;
                time = 0;
            }

            if (Minute > 5)
            {
                Hour++;
                Minute = 0;
            }

            if (Hour > 23)
            {
                Hour = 0;
                Day++;
            }

            if (Day > 7)
            {
                Day = 0;
                Week++;
            }

            if (Day == 1)
            {
                Weekday = WeekDay.Monday;
            }
            if (Day == 2)
            {
                Weekday = WeekDay.Tuesday;
            }
            if (Day == 3)
            {
                Weekday = WeekDay.Wednesday;
            }
            if (Day == 4)
            {
                Weekday = WeekDay.Thursday;
            }
            if (Day == 5)
            {
                Weekday = WeekDay.Friday;
            }
            if (Day == 6)
            {
                Weekday = WeekDay.Saturday;
            }
            if (Day == 7)
            {
                Weekday = WeekDay.Sunday;
            }
        }

        if (Hour == 19 && Minute == 0)
        {
            Night.color = new Color(0, 0, 0, 0.0f);
        }
        if (Hour == 19 && Minute == 1)
        {
            Night.color = new Color(0, 0, 0, 0.1f);
        }
        if (Hour == 19 && Minute == 2)
        {
            Night.color = new Color(0, 0, 0, 0.2f);
        }
        if (Hour == 19 && Minute == 3)
        {
            Night.color = new Color(0, 0, 0, 0.3f);
        }
        if (Hour == 19 && Minute == 4)
        {
            Night.color = new Color(0, 0, 0, 0.4f);
        }
        if (Hour == 19 && Minute == 5)
        {
            Night.color = new Color(0, 0, 0, 0.5f);
        }
        if (Hour == 20 && Minute == 0)
        {
            Night.color = new Color(0, 0, 0, 0.6f);
        }

        if (Hour >= 20 || Hour < 4)
        {
            Night.color = new Color(0, 0, 0, 0.6f);
        }
        else
        {
            Night.color = new Color(0, 0, 0, 0.0f);
        }

        if (Hour == 4 && Minute == 0)
        {
            Night.color = new Color(0, 0, 0, 0.6f);
        }
        if (Hour == 4 && Minute == 1)
        {
            Night.color = new Color(0, 0, 0, 0.5f);
        }
        if (Hour == 4 && Minute == 2)
        {
            Night.color = new Color(0, 0, 0, 0.4f);
        }
        if (Hour == 4 && Minute == 3)
        {
            Night.color = new Color(0, 0, 0, 0.3f);
        }
        if (Hour == 4 && Minute == 4)
        {
            Night.color = new Color(0, 0, 0, 0.2f);
        }
        if (Hour == 4 && Minute == 5)
        {
            Night.color = new Color(0, 0, 0, 0.1f);
        }
        if (Hour == 5 && Minute == 0)
        {
            Night.color = new Color(0, 0, 0, 0.0f);
        }
    }

    public void NextDay()
    {
        time = 0;
        Minute = 0;
        Hour = 6;
        Day++;
    }

    public void DayPassed()
    {
        PlayerSc.isWalk = false;
        AnimPass.Play("DayPass");
        Invoke("DayRise", 1f);
    }

    void DayRise()
    {
        PlayerSc.isWalk = true;
    }

    public enum WeekDay
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}
