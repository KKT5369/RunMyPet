using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobyController : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM(SoundType.BGM);
        GameManager.Instance.gameType = GameType.Game2D;
        UIManager.Instance.OpenUI<UILoby>();
    }
}
