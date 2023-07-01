using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="hero",menuName ="hero/heroData")]

public class heroContainer : ScriptableObject
{
    public GameObject hero;
    public int damage;
    public int health;
}
