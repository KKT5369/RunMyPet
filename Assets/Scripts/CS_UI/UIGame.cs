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
            ButtonPopupData data = new() { btnText1 = "계속하기", btnText2 = "다시하기", btnText3 = "로비로" };
            PopupManager.Instance.ButtonPopup(data,
                (() => 
                {
                    Time.timeScale = 1;
                    UIManager.Instance.CloseUI<UIPopupMenu>();
                    
                }),
                (() =>
                {
                    GameManager.Instance.Restart();
                    PopupManager.Instance.CloseButtonPopupUI();
                }),
                (() =>
                {
                    ConfirmData data = new ConfirmData() { title = "로비로?", body = "진짜 로비로 가려고?" };
                    PopupManager.Instance.ConfirmPopup(data, () =>
                    {
                        SceneLoadManager.Instance.LoadScene(SceneType.LobyScene);
                    });
                })
            );
        }));
    }
}
