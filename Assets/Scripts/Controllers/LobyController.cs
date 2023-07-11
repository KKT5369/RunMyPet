using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobyController : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.OpenUI<UILoby>();
    }
}
