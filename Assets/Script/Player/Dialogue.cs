using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public float TalkSpeed;
    public bool Timer;
    public float timer;
    public int X, Y, Z, W;

    public GameObject DialoguePanel, CloseButton, DialogueImage, CloseImage;
    public bool DialogueBool;
    public Text Dialoguetext, DialogueImagetext;
    public Image image;
    public char[] letters;

    public AudioSource Wet_Click;

    public NonPlayerInfo Pink, Blue, Green;

    void Start()
    {
        
    }

    void Update()
    {
        if (Timer)
        {
            timer += 1 * Time.deltaTime;
        }
    }

    public void TalkPink()
    {
        Y++;

        if (Y > Pink.texts2.Length - 1)
        {
            Y = 0;
        }

        Talk(Pink.texts2[Y], Pink);
    }


    public void Talk(string a)
    {
        X = 0;
        Dialoguetext.text = "";
        CloseButton.SetActive(false);
        letters = a.ToCharArray();
        DialoguePanel.SetActive(true);
        DialogueBool = true;
        Invoke("Write", TalkSpeed);
    }

    public void Write()
    {
        if (X < letters.Length)
        {
            Dialoguetext.text += letters[X].ToString();
            X++;
            Invoke("Write", TalkSpeed);
        }
        else
        {
            CloseButton.SetActive(true);
        }
    }

    public void Talk(NonPlayerInfo Info)
    {
        X = 0;
        DialoguePanel.SetActive(true);
        Dialoguetext.text = "";
        DialogueImagetext.text = Info.Name;
        CloseImage.SetActive(false);
        image.sprite = Info.sprite;
        letters = Info.texts[Random.Range(0, Info.texts.Length)].ToCharArray();
        DialogueImage.SetActive(true);
        Invoke("Write2", TalkSpeed);
    }

    public void Talk(string a, NonPlayerInfo Info)
    {
        X = 0;
        DialoguePanel.SetActive(true);
        Dialoguetext.text = "";
        DialogueImagetext.text = Info.Name;
        CloseImage.SetActive(false);
        image.sprite = Info.sprite;
        letters = a.ToCharArray();
        DialogueImage.SetActive(true);
        Invoke("Write2", TalkSpeed);
    }

    public void Write2()
    {
        if (X < letters.Length)
        {
            Dialoguetext.text += letters[X].ToString();
            X++;
            Invoke("Write2", TalkSpeed);
        }
        else
        {
            CloseImage.SetActive(true);
        }
    }
}
