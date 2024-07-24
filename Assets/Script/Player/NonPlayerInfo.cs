using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game", menuName = "Npc")]
public class NonPlayerInfo : ScriptableObject
{
    public string Name;
    public Sprite sprite;
    public string[] texts , texts2;
}
