using UnityEngine.SceneManagement;

public class SceneLoadManager : SingleTon<SceneLoadManager>
{
    private SceneType _scene;

    public SceneType Scene { get => _scene; }
    
    public void LoadScene(SceneType scene)
    {
        _scene = scene;
        SceneManager.LoadScene("LoadingScene");
    }
    
    public void OnSceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
    {
        
    }
}