using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;


public class UICharacterSellect : MonoBehaviour
{
    [Header("버튼")]
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnOkey;
    [SerializeField] private Button btnCancel;
    
    [Header("캐릭터선택")]
    [SerializeField] private RectTransform sellectRect;
    [SerializeField] private GameObject characterItem;

    private string _sellectChar;

    public string SellectChar
    {
        set => _sellectChar = value;
    }

    private void Awake()
    {
        SettingItem();
        SetAddListener();
    }

    void SettingItem()
    {
        var objects = Resources.LoadAll("Characters/Char2D");
        
        for (int i = 0; i < objects.Length; i++)
        {
            Sprite sp = objects[i].GetComponent<PlayerController>().sprite.sprite;
            string name = objects[i].name;
            var go = Instantiate(characterItem, sellectRect);
            go.GetComponent<CharSellectItem>().Setting(name,sp);
            go.SetActive(true);
        }
    }

    void SetAddListener()
    {
        btnOkey.onClick.AddListener((() =>
        {
            if (string.IsNullOrEmpty(_sellectChar))
            {
                ConfirmData data = new() { title = "알림", body = "캐릭터를 선택 해주세요" };
                PopupManager.Instance.ConfirmPopup(data);
            }
            else
            {
                
                ConfirmData data = new() { title = "알림", body = $"{_sellectChar} 캐릭터로 달려 볼까요?" };
                PopupManager.Instance.ConfirmPopup(data, () =>
                {
                    GameManager.Instance.SellectChar = _sellectChar;
                    CloseUI();
                });
            }
        }));
        btnClose.onClick.AddListener(CloseUI);
        btnCancel.onClick.AddListener(CloseUI);
    }

    void CloseUI()
    {
        UIManager.Instance.CloseUI<UICharacterSellect>();
    }
    
}
