using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleTon<UIManager>
{
    private Dictionary<string, GameObject> UI = new();
    GameObject _uiGo;
    
    // UI를 찾아서 있으면 활성화 없으면 생성후 저장 합니다.
    public void OpenUI<T>()
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
    
    // UI를 찾아서 있으면 비활성화 합니다.
    public void CloseUI<T>()
    {
        if (UI.TryGetValue(typeof(T).Name, out _uiGo))
        {
            _uiGo.SetActive(false);
        }
    }
    
    // UI GameObject를 반환 해줍니다.
    public GameObject GetUI<T>()
    {
        if (UI.TryGetValue(typeof(T).Name, out _uiGo))
        {
            return _uiGo;
        }

        return null;
    }
    
    // 모든 UI를 삭제 합니다.
    public void ClearUI()
    {
        if (UI == null) return;
        
        foreach (var pair in UI)
        {
            Destroy(pair.Value);
        }
        UI.Clear();
    }
}
