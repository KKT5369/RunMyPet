using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField] private Button btnMenu;
    [SerializeField] private TMP_Text txtPurScore;
    [SerializeField] private TMP_Text txtDistance;
    private int sd;
    private void Update()
    {
        
        txtPurScore.text = GameManager.Instance.Score.ToString();
    }
}
