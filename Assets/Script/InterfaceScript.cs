using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceScript : MonoBehaviour
{
    public GameObject info;
    public GameObject info_own, info_pack;
    public AudioSource sc, click;

    public Vector2 CursorPosition;
    public GameObject Cursors;

    private void Start()
    {
        sc.Play();
    }

    private void Update()
    {
        CursorPosition = Input.mousePosition;
        Cursor.visible = false;
        Cursors.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    public void _quit()
    {
        Application.Quit();
    }

    // info


    public void OpenPanel(GameObject Thing)
    {
        Thing.SetActive(true);
        click.Play();
    }

    public void ClosePanel(GameObject Thing)
    {
        Thing.SetActive(false);
        click.Play();
    }


}
