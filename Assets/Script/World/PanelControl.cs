using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public GameObject MainPanel, ChestPanel, Map, Craft, Buyer, FurnacePanel;
    public GameObject QuestPanel, Settings, Experience, Relationship;
    public Inventory Inventory;
    public KeyCode Menu_code;
    public bool MainPanel_Bool;
    public AudioSource sc, click;

    [Header("CamSlider")]
    public Camera cam;
    public Slider Camslider;
    public Text CamSliderText;
    void Start()
    {
        
    }

    void Update()
    {
        cam.orthographicSize = Camslider.value;
        CamSliderText.text = Camslider.value.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainPanel_Bool = false;
            ObjectSetActive();
        }
        if (Input.GetKeyDown(Menu_code))
        {
            MainPanel_Bool = !MainPanel_Bool;
            click.Play();
            ObjectSetActive();
            if (Inventory.ThisChest != null && MainPanel.activeSelf)
            {
                Inventory.InventorytoChest();
            }
        }
        MainPanel.SetActive(MainPanel_Bool);
    }

    public void ObjectSetActive()
    {
        ChestPanel.SetActive(false);
        FurnacePanel.SetActive(false);
        QuestPanel.SetActive(false);
        Map.SetActive(false);
        Craft.SetActive(false);
        Buyer.SetActive(false);
        Relationship.SetActive(false);
        Experience.SetActive(false);
        Settings.SetActive(false);
    }

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
