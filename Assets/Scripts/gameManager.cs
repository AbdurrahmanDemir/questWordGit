using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    [Header("Enemy & Hero")]
    [SerializeField] public GameObject[] enemy;
    public GameObject activeHero;
    public int activeEnemy;

    [Header("Elements")]
    public Slider bossSlider;
    public GameObject winPanel;
    public GameObject _heroLosePanel;
    public GameObject _wordLosePanel;
    public GameObject _popUpPrefabs;
    public TextMeshProUGUI _popUpText;
    public GameObject _newWordPanel;
    particleManager _particleManager;
    public Transform heroPos;
    public Transform enemyPos;

    [Header("Level Manager")]
    private int levelNumber;

    private void Awake()
    {
        levelNumber = PlayerPrefs.GetInt("Level", 1);
        bossSlider.maxValue = enemy.Length;
        bossSlider.value = activeEnemy + 1;
        activeEnemy = 0;

        _particleManager = GameObject.FindWithTag("particleManager").GetComponent<particleManager>();
    }

    public IEnumerator popUpCreat(string massage)
    {

        _popUpPrefabs.SetActive(true);
        _popUpText.text = massage;

        yield return new WaitForSeconds(1f);
        _popUpPrefabs.SetActive(false);

    }

    public IEnumerator newWordPanel()
    {
        _newWordPanel.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        _newWordPanel.SetActive(false);
    }

    public void winFinishPanel()
    {
        if (levelNumber >= SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("Level", levelNumber + 1);
        }
        Time.timeScale = 0;
        winPanel.SetActive(true);

    }
    public void heroLosePanel()
    {
        Time.timeScale = 0;
        _heroLosePanel.SetActive(true);
    }
    public void wordLosePanel()
    {
        Time.timeScale = 0;
        _wordLosePanel.SetActive(true);
    }


    public void heroAttack()
    {
        heroAttackAnim();
        enemyDamageAnim();
        enemyBloodParticle();
        StartCoroutine(popUpCreat("Dogru harf, Dusman hasar yedi!"));
        
        enemy[activeEnemy].GetComponent<enemy1Controller>().enemyHealtDamage(activeHero.GetComponent<hero1Controller>().inGameDamage);
    }
    public void enemyAttack()
    {
        heroDamageAnim();
        enemyAttackAnim();
        heroBloodParticle();

        activeHero.GetComponent<hero1Controller>().heroHealtDamage(enemy[activeEnemy].GetComponent<enemy1Controller>().inGameDamage);
    }


    public void heroAttackAnim()
    {
        activeHero.GetComponent<Animator>().SetBool("attack", true);
    }
    public void heroDamageAnim()
    {
        activeHero.GetComponent<Animator>().SetBool("damage", true);
    }

    public void enemyAttackAnim()
    {
        enemy[activeEnemy].GetComponent<Animator>().SetBool("attack", true);
    }
    public void enemyDamageAnim()
    {
        enemy[activeEnemy].GetComponent<Animator>().SetBool("damage", true);
    }


    public void heroBloodParticle()
    {
        _particleManager.bloodParticle(heroPos.transform.position);
    }
    public void enemyBloodParticle()
    {
        _particleManager.bloodParticle(enemyPos.transform.position);
    }

    public void enemyDead()
    {
        activeEnemy++;
        if (activeEnemy >= enemy.Length)
        {

            //heroLosePanel();
        }
        else
        {
            enemy[activeEnemy].SetActive(true);
        }
        

        _particleManager.smokeParticle(enemyPos.transform.position);
        bossSlider.value = activeEnemy + 1;

        //if (activeEnemy >= enemy.Length)
        //{
        //    loseFinishPanel();
        //}

    }

    IEnumerator FadeIn(int SceneIndex)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneIndex);
    }

    public void Restart()
    {
        StartCoroutine(FadeIn(SceneManager.GetActiveScene().buildIndex));
    }

    public void NextLevel()
    {
        StartCoroutine(FadeIn(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Exit()
    {
        StartCoroutine(FadeIn(0));

    }
}
