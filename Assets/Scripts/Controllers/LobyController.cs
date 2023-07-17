using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobyController : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM(SoundType.BGM);
        UIManager.Instance.OpenUI<UILoby>();
    }
}
