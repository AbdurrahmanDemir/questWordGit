using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class buttonManager : MonoBehaviour
{
    [SerializeField] private WordManager _wordManager;

    public void clickButton()
    {
        string letter = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        _wordManager.keyboardControl(letter);
    }
}
