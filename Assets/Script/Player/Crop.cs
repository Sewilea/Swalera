using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "Crop")]
public class Crop : ScriptableObject
{
    public string CropName;
    public Sprite[] Grow;
    public int GrowSpeed;
    public Item ICrop;
}

