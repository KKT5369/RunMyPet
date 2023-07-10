using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class CharSellectItem : MonoBehaviour
{
    [SerializeField] private Button btnSellect;
    [SerializeField] private Image img;
    
    public void Setting(string name,Sprite sprite)
    {
        btnSellect.onClick.AddListener((() =>
        {
            GameManager.Instance.SellectChar = name;
        }));
        img.sprite = sprite;
    }
    
    

}
