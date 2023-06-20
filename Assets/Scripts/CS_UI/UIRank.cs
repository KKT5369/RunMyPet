using System;
using System.Collections;
using System.Collections.Generic;
using NetworkConstants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIRank : AddressableUI
{
    [SerializeField] private GameObject helpPopup;
    [SerializeField] private TMP_Text title;
    
    [Header("버튼")]
    [SerializeField] private Button btnHelp;
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnNextGamePage;
    [SerializeField] private Button btnPreGamePage;
    [SerializeField] private Button btnSharing;
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnNextDatePage;
    [SerializeField] private Button btnPreDatePage;
    [SerializeField] private Button btnPopupClose;
    
    [Header("내정보")]
    [SerializeField] private Image imgMyProfile;
    [SerializeField] private TMP_Text txtMyRankNum;
    [SerializeField] private TMP_Text txtMyNicName;
    [SerializeField] private TMP_Text txtMyScore;
    [SerializeField] private TMP_Text txtMyClearTime;
    
    [Header("스크롤뷰 아이템")]
    [SerializeField] private RectTransform selectGameRect;
    [SerializeField] private GameObject selectGameItem;
    [SerializeField] private Scrollbar selectGameScrollbar;
    [SerializeField] private RectTransform rankRect;
    [SerializeField] private GameObject rankItem;
    [SerializeField] private RectTransform datePickerRect;
    [SerializeField] private GameObject datePicker;
    [SerializeField] private Scrollbar datePickerScrollbar;

    public Dictionary<string, Button> selectButtons = new();
    private List<GameObject> _rankObj = new();
    
    // 주간 날짜 변수
    private readonly String _dateFormat = "yyyy.MM.dd HH:MM";
    private DateTime _today;
    private DateTime _monDay;
    private DateTime _sunDay;
    private string[] _dates;
    
    // 현재 선택된 게임 변수
    readonly string _sellectGameKey = "SellectGame";
    private Button _purBtn;
    private GameType _selectGameType;
    private Color _selectColor;
    private Color _textColor;
    
    // Game paging 변수
    private float[] _gamePos;
    private int _gamePageIndex;
    private int _gameLastPage;
    
    // Date paging 변수
    private float[] _datePos;
    private int _datePageIndex;
    private int _dateLastPage;
    private WeekType[] _weekTypes;

    // Popup 메시지
    [SerializeField] private TMP_Text txtPopupTitle;
    [SerializeField] private TMP_Text txtPopupDesc;
    
#if !UNITY_EDITOR && UNITY_ANDROID
    AndroidJavaClass AndroidPluginClass;
    AndroidJavaObject _instance;
#endif
    
    private void Start()
    {
        title.SetLanguage(9240010);
        CreateSelectBtn();
        SetAddListener();
        CreateDatePicker();
        SelectGame(GameType.GMHG);
        GameRankManager.Instance.GetRankingData(_selectGameType,_weekTypes[_datePageIndex],(d => RankDataSeting(d)));

#if !UNITY_EDITOR && UNITY_ANDROID
        if (_instance == null)
        {
            AndroidPluginClass = new AndroidJavaClass("com.example.kakaotest.MyPlugin");
            _instance = AndroidPluginClass.CallStatic<AndroidJavaObject>("instance");
        }
#endif

        FontSetting();
    }

    void FontSetting()
    {
        txtMyRankNum.SetDefaultFont();
        txtMyNicName.SetDefaultFont();
        txtMyScore.SetDefaultFont();
        txtMyClearTime.SetDefaultFont();
        txtPopupTitle.SetDefaultFont();
        txtPopupDesc.SetDefaultFont();
    }

    // 게임 수에 맞게 버튼 생성
    void CreateSelectBtn()
    {
        int gameCount = Enum.GetValues(typeof(GameType)).Length;
        int index = 0;

        for (int i = 0; i < (float)gameCount/2; i++)
        {
            var go = Instantiate(selectGameItem, selectGameRect);
            go.SetActive(true);
            var script = go.GetComponent<SelectGameItem>();
            for (int j = 0; j < 2; j++)
            {
                if(index >= gameCount) break;
                script.SetLButton((GameType)(index++));
            }
        }

        if (1 < (float)gameCount/2)
        {
            btnNextGamePage.gameObject.SetActive(true);

            _gamePos = new float[gameCount - 1];
            _gameLastPage = gameCount - 2;
            _gamePageIndex = 0;

            float distancce = 1f / (_gamePos.Length - 1f);
            for (int i = 0; i < _gamePos.Length; i++)
            {
                _gamePos[i] = distancce * i;
            }
        }

        selectButtons.TryGetValue(GameType.GMHG.ToString(), out _purBtn);
        ColorUtility.TryParseHtmlString("#6DC9F6", out _selectColor);
        ColorUtility.TryParseHtmlString("#BABABA", out _textColor);
    }

    public void SelectGame(GameType gameType)
    {
        Debug.Log($"선택된 게임 >>> {gameType.ToString()}");
        _selectGameType = gameType;
        _purBtn.GetComponent<Image>().color = Color.white;
        _purBtn.transform.GetChild(0).GetComponent<TMP_Text>().color = _textColor;
        selectButtons.TryGetValue(gameType.ToString(),out _purBtn);
        _purBtn.GetComponent<Image>().color = _selectColor;
        _purBtn.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.white;
        GameRankManager.Instance.GetRankingData(_selectGameType,_weekTypes[_datePageIndex],(d => RankDataSeting(d)));
    }

    // 랭킹데이터 가져와서 출력
    void RankDataSeting(RankD rankD)
    {
        if (rankD == null) return;

        KillRanking();
        List<GameRankingD> gameRankingDs = rankD.gameRankingDList;

        for (int i = 0; i < gameRankingDs.Count; i++)
        {
            GameObject go = Instantiate(rankItem, rankRect);
            _rankObj.Add(go);
            go.SetActive(true);
            var script = go.GetComponent<GameRankItem>();
            
            script.Setting(gameRankingDs[i]);
        }

        MyRankSeting(rankD.myGameRankingD);
    }

    void KillRanking()
    {
        if(_rankObj == null) return;

        foreach (var v in _rankObj)
        {
            Destroy(v);
        }
        _rankObj.Clear();
    }

    private void MyRankSeting(MyGameRanking myGameRanking)
    {
        string url = $"{AwsUrl.pet}{myGameRanking.petUserDid}/index.png";
        TextureManager.SetTexture(gameObject,imgMyProfile,url);
        
        txtMyRankNum.text = myGameRanking.gameRank.ToString();
        txtMyNicName.text = myGameRanking.nknmNm;
        txtMyScore.text = myGameRanking.gamePnt.ToString();
        txtMyClearTime.text = myGameRanking.modDtm;
    }

    
    // todo 게임 플레이시 선택한 게임 연결
    public void StartGame()
    {
        PlayerPrefs.SetInt(_sellectGameKey, (int)_selectGameType);
        UIManager.Instance.OpenUI<UIGameList>();
        AdbrixManager.Instance.Difinery_MainGame();
    }

    private void OnEnable()
    {
        SelectGame(GameType.GMHG);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(_sellectGameKey, 0);
    }

    #region 버튼 관련

    void OnClickShare()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        if(AndroidPluginClass != null) {
            string title = "O모O모";
            string desc = "캐릭터로 즐기는 펫 커뮤니티 'O모O모'";
            string btnText = "확인하기";
            string imgUrl = "https://play-lh.googleusercontent.com/EzGuurAotWzFYZTSh588UQuZNMzS3lxOoObMhcK61XNkQiklHrlspS65jUiEbt4CYg";
            string webUrl = "https://play.google.com/store/apps/details?id=com.samsungfire.omoomo&hl=ko&gl=US";
            string moblieWebUrl = "https://play.google.com/store/apps/details?id=com.samsungfire.omoomo&hl=ko&gl=US";
            
            _instance.Call("KakaoShare", title, desc, btnText, imgUrl, webUrl, moblieWebUrl);
        }
    #endif
    }

    void OnClickNextPage(Page page)
    {
        switch (page)
        {
            case Page.Game:
                if (_gamePageIndex < _gameLastPage)
                {
                    btnPreGamePage.gameObject.SetActive(true);
                    selectGameScrollbar.value = _gamePos[++_gamePageIndex];
                }
                if (_gamePageIndex == _gameLastPage)
                {
                    btnNextGamePage.gameObject.SetActive(false);
                }
                break;
            case Page.Date:
                if (_datePageIndex < _dateLastPage)
                {
                    btnPreDatePage.gameObject.SetActive(true);
                    datePickerScrollbar.value = _datePos[++_datePageIndex];
                }
                if (_datePageIndex == _dateLastPage)
                {
                    btnNextDatePage.gameObject.SetActive(false);
                }
                
                GameRankManager.Instance.GetRankingData(_selectGameType,_weekTypes[_datePageIndex],(d => RankDataSeting(d)));
                break;
        }
    }
    
    void OnClickPrePage(Page page)
    {
        switch (page)
        {
            case Page.Game:
                if (_gamePageIndex > 0)
                {
                    btnNextGamePage.gameObject.SetActive(true);
                    selectGameScrollbar.value = _gamePos[--_gamePageIndex];
                }
                if (_gamePageIndex == 0)
                {
                    btnPreGamePage.gameObject.SetActive(false);
                }
                break;
            case Page.Date:
                if (_datePageIndex > 0)
                {
                    btnNextDatePage.gameObject.SetActive(true);
                    datePickerScrollbar.value = _datePos[--_datePageIndex];
                }
                if (_datePageIndex == 0)
                {
                    btnPreDatePage.gameObject.SetActive(false);
                }

                GameRankManager.Instance.GetRankingData(_selectGameType,_weekTypes[_datePageIndex],(d => RankDataSeting(d)));
                break;
            
        }
    }
    
    private void SetAddListener()
    {
        btnHelp.onClick.AddListener((() => helpPopup.SetActive(true)));
        btnSharing.onClick.AddListener(OnClickShare);
        btnNextGamePage.onClick.AddListener((() => OnClickNextPage(Page.Game)));
        btnPreGamePage.onClick.AddListener((() => OnClickPrePage(Page.Game)));
        btnNextDatePage.onClick.AddListener((() => OnClickNextPage(Page.Date)));
        btnPreDatePage.onClick.AddListener((() => OnClickPrePage(Page.Date)));
        btnPopupClose.onClick.AddListener((() => helpPopup.SetActive(false)));
        btnPlay.onClick.AddListener(StartGame);
        
        btnClose.onClick.AddListener((() =>
        {
            UIManager.Instance.CloseUI<UIRank>();
            helpPopup.SetActive(false);
        }));
    }
    
    // 주간 달력 세팅
    public void CreateDatePicker()
    {
        _weekTypes = (WeekType[])Enum.GetValues(typeof(WeekType));
        int weekNum = _weekTypes.Length;
        _today = DateTime.Today;
        _dates = new String[weekNum];
        _datePos = new float[weekNum];
        
        float distancce = 1f / (weekNum - 1f);
        
        for (int i = 0; i < weekNum; i++)
        {
            _monDay = _today.AddDays(Convert.ToInt32(DayOfWeek.Monday) - Convert.ToInt32(_today.DayOfWeek));
            _sunDay = _monDay.AddDays(6);
            _today = _today.AddDays(-7);
            
            _dates[(weekNum - 1) - i] = $"{_monDay.Year}. {_monDay.Month} . {_monDay.Day} ~ {_sunDay.Month} . {_sunDay.Day}";
        }

        for (int i = 0; i < weekNum; i++)
        {
            var go = Instantiate(datePicker, datePickerRect);
            go.SetActive(true);
            go.transform.GetChild(1).GetComponent<TMP_Text>().text = _dates[i];
            _datePos[i] = distancce * i;
        }

        _dateLastPage = weekNum - 1;
        _datePageIndex = weekNum - 1;
        datePickerScrollbar.value = 1f;
        if (weekNum > 1)
        {
            btnPreDatePage.gameObject.SetActive(true);
        }
    }
    #endregion
}

public enum GameType
{
    GMHG, // 눈치게임
    GMBG, // 징검다리
}

public enum Page
{
    Game,
    Date,
}

public enum WeekType
{
    LSWK,
    TSWK,
}




