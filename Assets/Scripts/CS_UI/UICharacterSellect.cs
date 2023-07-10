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

    private void Awake()
    {
        SettingItem();
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
    
}
