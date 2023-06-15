using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleTon<UIManager>
{
    private Dictionary<string, GameObject> UI = new();
    GameObject _uiGo;
    
    public void CreateUI<T>()
    {
        if (UI.TryGetValue(typeof(T).Name, out _uiGo))
        {
            _uiGo.SetActive(true);
        }
        else
        {
            GameObject go = Resources.Load<GameObject>($"UI/{typeof(T).Name}");
            var ui = Instantiate(go);
            UI.Add(typeof(T).Name,ui);
        }
    }

    public void CloseUI<T>()
    {
        if (!UI.TryGetValue(typeof(T).Name, out _uiGo))
        {
            _uiGo.SetActive(false);
        }
    }
}
