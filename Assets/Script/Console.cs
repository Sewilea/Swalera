using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public PlayerScript player;
    public Inventory inventory;
    public Finance finance;
    public Clock clock;
    public Dialogue dialogue;
    public GameObject Player;
    public GameObject ConsolePanel ,Massage, Content;
    public InputField field;
    public bool isOpenConsole;
    public string[] Command_Words;
    public KeyCode Enter;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isOpenConsole = !isOpenConsole;
            ConsolePanel.SetActive(isOpenConsole);
        }
        if (Input.GetKeyDown(Enter) && isOpenConsole)
        {
            run();
        }
    }

    public void run()
    {
        string Command = field.text;

        Command.TrimEnd();
        Command.ToLower();
        Command_Words = Command.Split(' ');

        if (Command_Words[0] == "*")
        {
            if (Command_Words[1] == "write")
            {
                Message(Command_Words[2]);
            }

            if (Command_Words[1] == "help")
            {
                Message("* write x | * tp x y | * give x y z | * camera x | * time x y | * money x ");
            }

            if (Command_Words[1] == "tp")
            {
                int x = int.Parse(Command_Words[2]);
                int y = int.Parse(Command_Words[3]);

                Player.transform.position = new Vector2(x, y);
            }

            if (Command_Words[1] == "give")
            {
                int x = int.Parse(Command_Words[2]);
                int y = int.Parse(Command_Words[3]);
                int amount = int.Parse(Command_Words[4]);

                inventory.AddItem(inventory.Find(y, x), amount);
            }

            if (Command_Words[1] == "camera")
            {
                int x = int.Parse(Command_Words[2]);

                Camera.main.orthographicSize = x;
            }

            if (Command_Words[1] == "time")
            {
                int x = int.Parse(Command_Words[2]);
                int y = int.Parse(Command_Words[3]);

                clock.Hour = x;
                clock.Minute = y;
            }

            if (Command_Words[1] == "money")
            {
                int x = int.Parse(Command_Words[2]);

                finance.Money += x;
            }
        }
    }

    public void Message(string message)
    {
        GameObject Obje = Instantiate(Massage, Content.transform);
        Obje.transform.GetChild(0).GetComponent<Text>().text = message;
        Destroy(Obje, 3f);
        _Content();
    }

    public void _Content()
    {
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Content.transform.childCount * 40);
        // şükür
    }
}
