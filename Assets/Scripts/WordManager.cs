using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;

public class WordManager : MonoBehaviour
{
    [SerializeField]
    List<WordData> words;

    public gameManager _gameManager;

    [SerializeField] Transform letterPanel, keyboardPanel;
    public GameObject letterPrefabs;
    public TextMeshProUGUI remainingWordText;
    public int remainingWord;
    int numberWord, numberKeyboard;

    List<string> listTrueWord= new List<string>(); //doðrucevap
    List<string> listLetters= new List<string>(); //secilecekharfler

    int whichWord;
    private void Start()
    {
        remainingWord= words.Count;
        remainingWordText.text = remainingWord.ToString();
        _gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<gameManager>();
        whichWord = 0;

        wordOpen();
        
    }

    void wordOpen()
    {
        for (int i = 0; i < words[whichWord].trueWord.Length; i++)
        {
            listTrueWord.Add(words[whichWord].trueWord[i].ToString().ToUpper());
        }

        for (int i = 0; i < listTrueWord.Count; i++)
        {
            GameObject letter = Instantiate(letterPrefabs);
            letter.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listTrueWord[i];
            letter.transform.GetChild(0).gameObject.SetActive(false);
            letter.transform.parent = letterPanel;
        }
        for (int i = 0; i < words[whichWord].letters.Length; i++)
        {
            listLetters.Add(words[whichWord].letters[i].ToString().ToUpper());
        }
        listLetters=listLetters.OrderBy(i => Random.value).ToList();

        for (int i = 0; i < keyboardPanel.childCount; i++)
        {
            //keyboardPanel.GetChild(i).GetComponent<CanvasGroup>().alpha = 0f;
            //keyboardPanel.GetChild(i).GetComponent<RectTransform>().localScale = Vector3.zero;
            //keyboardPanel.GetChild(i).GetComponent<Button>().enabled = false;

            keyboardPanel.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = listLetters[i];

        }
        StartCoroutine(trueWordsOpen());
        StartCoroutine(keyboardsOpen());
    }
    IEnumerator trueWordsOpen()
    {
        numberWord = 0;
        while(numberWord< listTrueWord.Count)
        {
            StartCoroutine(_gameManager.newWordPanel());

            yield return new WaitForSeconds(.2f);
            numberWord++;
        }
    }
    IEnumerator keyboardsOpen()
    {
        numberKeyboard = 0;
        while (numberKeyboard < keyboardPanel.childCount)
        {
            keyboardPanel.GetChild(numberKeyboard).GetComponent<CanvasGroup>().DOFade(1, .3f);
            keyboardPanel.GetChild(numberKeyboard).GetComponent<RectTransform>().DOScale(1, .3f).SetEase(Ease.OutBack);

            yield return new WaitForSeconds(.2f);
            numberKeyboard++;
        }

        for (int i = 0; i < keyboardPanel.childCount; i++)
        {
            keyboardPanel.GetChild(i).GetComponent<Button>().enabled = true;

        }
    }

    public void keyboardControl(string incomingLetter)
    {
        if (listTrueWord.Contains(incomingLetter))
        {
            for (int i = 0; i < letterPanel.childCount; i++)
            {
                if (!letterPanel.GetChild(i).GetChild(0).gameObject.activeInHierarchy)
                {
                    if (incomingLetter == letterPanel.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text)
                    {
                        _gameManager.heroAttack();
                        
                        
                        letterPanel.GetChild(i).GetChild(0).gameObject.SetActive(true);
                        //_gameManager.enemy[_gameManager.activeEnemy].GetComponent<enemy1Controller>().enemyHealtDamage(_gameManager.activeHero.GetComponent<hero1Controller>().inGameDamage);
                        
                        
                        break;

                    }
                    

                    
                }

               
            }
        }
        else
        {

            _gameManager.enemyAttack();
            StartCoroutine(_gameManager.popUpCreat("Yanlis harf, dusman sana vurdu!"));
            //_gameManager.enemyAttackAnim();
            //_gameManager.heroDamageAnim();
            //_gameManager.heroBloodParticle();
            
            //_gameManager.activeHero.GetComponent<hero1Controller>().heroHealtDamage(_gameManager.enemy[_gameManager.activeEnemy].GetComponent<enemy1Controller>().inGameDamage);
        }

        if (allOpeningLetter())
        {
            Invoke("newWord", .5f);
        }
    }

    void newWord()
    {

        whichWord++;
        listLetters.Clear();
        listTrueWord.Clear();



        if (whichWord < words.Count)
        {
            foreach(Transform chid in letterPanel)
            {
                Destroy(chid.gameObject);
            }
            Invoke("wordOpen", 1f);

            remainingWord--;
            remainingWordText.text = remainingWord.ToString();

        }
        else
        {
            _gameManager.wordLosePanel();
        }
    }

    bool allOpeningLetter()
    {
        for (int i = 0; i < letterPanel.childCount; i++)
        {
            if (!letterPanel.GetChild(i).GetChild(0).gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
}
