using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private Image progressBar;

    private void Start()
    {
        StartCoroutine(nameof(SceneLoading));
    }
    
    IEnumerator SceneLoading()
    {
        SceneType scene = SceneLoadManager.Instance.Scene;
        AsyncOperation op = SceneManager.LoadSceneAsync(scene.ToString());
        op.allowSceneActivation = false;
        progressBar.fillAmount = 0f;

        title.text = "준비가 거이다 끝났어요!";
        float timer = Time.unscaledDeltaTime;
        
        while (progressBar.fillAmount <= 0.85f)
        {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.9f, timer);
            yield return null;
        }
        if (op.progress >= 0.8f)
        {
            progressBar.fillAmount = 1f;
            op.allowSceneActivation = true;
        }
    }
}
