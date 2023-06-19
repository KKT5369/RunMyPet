using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : SingleTon<SceneLoadManager>
{
    private SceneType _scene;

    public SceneType Scene { get => _scene; }
    
    public void LoadScene(SceneType scene)
    {
        _scene = scene;
        GameManager.Instance.purScene = scene;
        Time.timeScale = 1;
        UIManager.Instance.ClearUI();
        SceneManager.LoadScene("LoadingScene");
    }
    
    public void OnSceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
    {
        
    }
}