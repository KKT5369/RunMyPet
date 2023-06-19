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
        
        while (progressBar.fillAmount <= 1f)
        {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.9f, timer);
            yield return new WaitForSeconds(0.5f);
            if (op.progress >= 0.89f)
            {
                progressBar.fillAmount = 1f;
                yield return new WaitForSeconds(0.5f);
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
