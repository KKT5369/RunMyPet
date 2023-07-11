using UnityEngine;
using UnityEngine.UI;

public class CharSellectItem : MonoBehaviour
{
    [SerializeField] private Button btnSellect;
    [SerializeField] private Image img;

    private UICharacterSellect _ui;

    private void Start()
    {
        _ui = UIManager.Instance.GetUI<UICharacterSellect>().GetComponent<UICharacterSellect>();
    }

    public void Setting(string name,Sprite sprite)
    {
        btnSellect.onClick.AddListener((() =>
        {
            _ui.SellectChar = name;
        }));
        img.sprite = sprite;
    }
    
    

}
