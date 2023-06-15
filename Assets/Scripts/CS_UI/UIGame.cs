using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField] private Button btnMenu;
    [SerializeField] private TMP_Text txtPurScore;
    [SerializeField] private TMP_Text txtDistance;


    private void Awake()
    {
        SetAddListener();
    }

    private void Update()
    {
        txtPurScore.text = GameManager.Instance.Score.ToString();
    }

    void SetAddListener()
    {
        btnMenu.onClick.AddListener((() =>
        {
            Time.timeScale = 0;
            UIManager.Instance.CreateUI<UIPopupMenu>();
        }));
    }
}