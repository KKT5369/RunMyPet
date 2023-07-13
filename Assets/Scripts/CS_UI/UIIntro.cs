using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIntro : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.gameType = GameType.Game2D;
        SceneLoadManager.Instance.LoadScene(SceneType.LobyScene);
    }
}
