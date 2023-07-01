using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemy", menuName = "enemy/enemyData")]

public class enemyContainer : ScriptableObject
{
    public GameObject enemy;
    public int damage;
    public int health;
    public int attackSpeed;
}
