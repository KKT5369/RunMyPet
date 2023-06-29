using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField] private Button btnMenu;
    [SerializeField] private TMP_Text txtPurScore;
    [SerializeField] private TMP_Text txtDistance;
    [SerializeField] private List<GameObject> buffPos;

    private int _distance;
    private Color _color;
    private Tween[] isBuff;
    
    private void Awake()
    {
        _color = buffPos[0].GetComponent<Image>().color;
        isBuff = new Tween[buffPos.Count];
        SetAddListener();
    }

    private void Update()
    {
        txtPurScore.text = GameManager.Instance.Score.ToString();
    }

    private void FixedUpdate()
    {
        _distance = (int)GameManager.Instance.Distance;
        txtDistance.text = $"{_distance} M";
    }

    public void ActiveBuff(ItemType itemType,bool value)
    {
        int i = (int)itemType;
        if (isBuff[i] != null)
        {
            isBuff[i].Kill();
        }
        
        var go = buffPos[i];
        go.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        go.SetActive(value);
    }

    public void Fade(ItemType itemType)
    {
        isBuff[(int)itemType] = buffPos[(int)itemType].GetComponent<Image>().DOFade(0, 0.1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        foreach (var v in buffPos)
        {
            v.SetActive(false);
        }
    }

    void SetAddListener()
    {
        btnMenu.onClick.AddListener((() =>
        {
            SoundManager.Instance.PlayUISound(SoundType.Button);
            Time.timeScale = 0;
            ButtonPopupData data = new() { btnText1 = "계속하기", btnText2 = "다시하기", btnText3 = "로비로" };
            PopupManager.Instance.ButtonPopup(data,
                (() => 
                {
                    SoundManager.Instance.PlayUISound(SoundType.Button);
                    Time.timeScale = 1;
                    UIManager.Instance.CloseUI<UIPopupMenu>();
                    
                }),
                (() =>
                {
                    SoundManager.Instance.PlayUISound(SoundType.Button);
                    GameManager.Instance.Restart();
                    PopupManager.Instance.CloseButtonPopupUI();
                }),
                (() =>
                {
                    SoundManager.Instance.PlayUISound(SoundType.Button);
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
