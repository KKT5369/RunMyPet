using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoby : MonoBehaviour
{
    public Button btn;

    private void Awake()
    {
        btn.onClick.AddListener((() =>
        {
            SceneLoadManager.Instance.LoadScene(SceneType.GameScene);
        }));
    }
}
