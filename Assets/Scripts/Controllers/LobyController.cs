using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobyController : MonoBehaviour
{
    [SerializeField] private GameObject lobyMap2D;
    [SerializeField] private GameObject lobyMap3D;
    private GameObject playerGo;
    
    void Start()
    {
        SoundManager.Instance.PlayBGM(SoundType.BGM);
        UIManager.Instance.OpenUI<UILoby>();
        var ui = UIManager.Instance.GetUI<UILoby>().GetComponent<UILoby>();
        ui.lobyMapSetting = MapSetting;
        ui.CharChange = CharChange;
        MapSetting();
    }
    
    public void CharChange()
    {
        if (playerGo !=null)
        {
            Destroy(playerGo);
        }
        playerGo = Instantiate(ResourcesLoadManager.Instance.LoadCharacter(GameManager.Instance.SellectChar));
        playerGo.transform.position = Vector3.zero;
    }

    void MapSetting()
    {
        switch (GameManager.Instance.gameType)
        {
            case GameType.Game2D:
                ModSetting2D();
                break;
            case GameType.Game3D:
                ModSetting3D();
                break;
        }
    }

    void ModSetting3D()
    {
        CharChange();
        On2DMap(false);
        Vector3 pos = new Vector3(5, 10, -30);
        Vector3 rot = new Vector3(5, 0.5f, 0);
        CameraManager.Instance.Setting(pos,rot);
    }
    
    void ModSetting2D()
    {
        CharChange();
        On2DMap(true);
        Vector3 pos = new Vector3(0, 1, -10);
        CameraManager.Instance.Setting(pos,Vector3.zero);
    }

    void On2DMap(bool on)
    {
        lobyMap2D.SetActive(on);
        lobyMap3D.SetActive(!on);
    }
}
