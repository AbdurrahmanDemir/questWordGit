using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class hero
{
    public GameObject _hero;
    public int _damage;
    public int _healh;

    public hero(GameObject hero, int damage, int health)
    {
        this._hero = hero;
        this._damage = damage;
        this._healh = health;
    }
}
public class hero1Controller : MonoBehaviour
{
    [Header("Hero Scriptable")]
    public heroContainer _heroContainer;
    private hero myHero;

    [Header("Hero Stats")]
    GameObject inGamehero;
     public int inGameDamage;
    int maxHealth;
     int inGameHealh;

    [Header("Hero Elements")]
    gameManager _gameManager;
    Animator animator;
    public TextMeshProUGUI heroHealtText;
    public Slider heroHealthSlider;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        Config();
        
    }
    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>();
        maxHealth = inGameHealh;
        heroHealthSlider.maxValue = maxHealth;
        heroHealthSlider.value = inGameHealh;
        heroHealtText.text = inGameHealh.ToString();
    }
    void Config()
    {
        myHero = new hero(_heroContainer.hero,_heroContainer.damage,_heroContainer.health);
        inGamehero = myHero._hero;
        inGameDamage = myHero._damage;
        inGameHealh=myHero._healh;
    }

    public void heroHealtDamage(int enemyDamage)
    {
        inGameHealh -= enemyDamage;

        heroHealthSlider.value = inGameHealh;
        heroHealtText.text = inGameHealh.ToString();

        if (inGameHealh <= 0)
        {
            _gameManager.heroLosePanel();
        }
    }

    public void attakAnimStop()
    {
        animator.SetBool("attack", false);
    }
    public void damageAnimStop()
    {
        animator.SetBool("damage", false);
    }




}
