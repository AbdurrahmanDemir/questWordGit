using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class enemy
{
    public GameObject _enemy;
    public int _damage;
    public int _healh;
    public float _attackSpeed;

    public enemy(GameObject enemy, int damage, int health, float attackSpeed)
    {
        this._enemy = enemy;
        this._damage = damage;
        this._healh = health;
        this._attackSpeed = attackSpeed;
    }
}
public class enemy1Controller : MonoBehaviour
{
    [Header("Enemy Scriptable")]
    public enemyContainer _enemyContainer;
    private enemy myEnemy;

    [Header("Enemy Stats")]
    GameObject inGameEnemy;
    public int inGameDamage;
    int maxHealh;
    public int inGameHealh;
    float maxAttackSpeed;
    float inGameattackSpeed;

    [Header("Enemy Elements")]
    gameManager _gameManager;
    particleManager _particleManager;
    Animator animator;
    public TextMeshProUGUI enemyHealtText;
    public TextMeshProUGUI enemyAttackSpeedText;
    public Slider enemyHealthSlider;
    public Slider enemyAttackSpeedSlider;

    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>();
        _particleManager = GameObject.FindGameObjectWithTag("particleManager").GetComponent<particleManager>();
        animator = GetComponent<Animator>();
        Config();
    }
    private void Start()
    {
        maxHealh = inGameHealh;        
        enemyHealthSlider.maxValue = maxHealh;
        enemyHealthSlider.value = inGameHealh;

        maxAttackSpeed = inGameattackSpeed;
        enemyAttackSpeedSlider.maxValue = maxAttackSpeed;
        enemyAttackSpeedSlider.value = inGameattackSpeed;

        enemyHealtText.text = inGameHealh.ToString();

        StartCoroutine(enemyAttack());

    }
    private void Update()
    {        
        maxAttackSpeed -= Time.deltaTime;
        enemyAttackSpeedSlider.value = maxAttackSpeed;
        if (maxAttackSpeed <= 0)
        {
            
            maxAttackSpeed = inGameattackSpeed;
            
            //enemyAttackSpeedText.text = Mathf.Round(maxAttackSpeed).ToString();
        }
        enemyAttackSpeedText.text = Mathf.Round(maxAttackSpeed).ToString();
    }
    void Config()
    {
        myEnemy = new enemy(_enemyContainer.enemy, _enemyContainer.damage, _enemyContainer.health, _enemyContainer.attackSpeed);
        inGameEnemy = myEnemy._enemy;
        inGameDamage = myEnemy._damage;
        inGameHealh = myEnemy._healh;
        inGameattackSpeed = myEnemy._attackSpeed;
    }

    public void enemyHealtDamage(int heroDamage)
    {
        inGameHealh -= heroDamage;

        enemyHealthSlider.value = inGameHealh;

        enemyHealtText.text = inGameHealh.ToString();


        if (inGameHealh<=0)
        {
            _gameManager.enemyDead();
            gameObject.SetActive(false);
            if (_gameManager.activeEnemy >= _gameManager.enemy.Length)
            {
                _gameManager.winFinishPanel();
            }

        }
    }

    IEnumerator enemyAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(inGameattackSpeed);

            if (inGameHealh>0)
            {
                _gameManager.enemyAttack();
                StartCoroutine(_gameManager.popUpCreat("Dusman atak yapti!"));
            }
            else
            {
                //_gameManager.activeEnemy++;
            }
            
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
