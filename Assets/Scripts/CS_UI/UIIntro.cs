using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIntro : MonoBehaviour
{
    void Start()
    {
        SceneLoadManager.Instance.LoadScene(SceneType.LobyScene);
    }
}
