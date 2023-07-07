using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    private void Awake()
    {
        SettingItem();
    }

    void SettingItem()
    {
        int count = Resources.LoadAll("Characters").Length;

        for (int i = 0; i < count; i++)
        { 
            var go = Instantiate(characterItem, sellectRect);
            go.SetActive(true);
        }
        
    }
    
}
