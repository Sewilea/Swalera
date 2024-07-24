using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public Text ProgressText;
    public Slider slider;
    public GameObject Panel;

    public void Load_Level(int levelindex)
    {
        StartCoroutine(Level_Progress(levelindex));
    }

    IEnumerator Level_Progress(int levelindex)
    {
        AsyncOperation Operation = SceneManager.LoadSceneAsync(levelindex);

        Panel.SetActive(true);

        while (!Operation.isDone)
        {
            float progress = Mathf.Clamp01(Operation.progress / 0.9f);
            slider.value = progress;

            ProgressText.text = progress * 100 + "%";

            yield return null;
        }
    }
}
