using UnityEngine;
using UnityEngine.UI;

public class UITutorialController : MonoBehaviour
{
    [Header("DEBUG BUTTON")]
    [SerializeField] private Button _clearFlags;

    [Header("Panels")]
    [SerializeField] private GameObject _runPanel;
    [SerializeField] private GameObject _flyPanel;
    [SerializeField] private GameObject _balancePanel;
    [SerializeField] private GameObject _hurricanePanel;

    private bool _isTutorialPlaying;

    private bool _isAlreadyRun;
    private bool _isAlreadyFly;
    private bool _isAlreadyBalance;
    private bool _isAlreadyHurricane;

    private SaveDataRepo _save;
    private int _trueValue = 1;
    private int _falseValue = 0;


    private void Awake()
    {
        _save = new SaveDataRepo();

        StopPlaying();

        _isAlreadyRun = LoadFlag(SaveKeyManager.KeyFirstRun);
        _isAlreadyFly = LoadFlag(SaveKeyManager.KeyFirstFly);
        _isAlreadyBalance = LoadFlag(SaveKeyManager.KeyFirstBalance);
        _isAlreadyHurricane = LoadFlag(SaveKeyManager.KeyFirstHurricane);
    }

    private void Start()
    {
        if (_isAlreadyRun == true && _isAlreadyFly == true && _isAlreadyBalance == true && _isAlreadyHurricane == true)
        {
            DestroyMe();
        }

        GameEvents.current.OnRunFirstEntryEvent += PlayRunTutorial;
        GameEvents.current.OnFlyFirstEntryEvent += PlayFlyTutorial;
        GameEvents.current.OnBalanceFirstEntryEvent += PlayBalanceTutorial;
        GameEvents.current.OnHurricaneFirstEntryEvent += PlayHurricaneTutorial;

        GameEvents.current.OnTouchBeganEvent += EndTutorial;

        // DEBUG ---------------------------------
        _clearFlags.onClick.AddListener(() => ClearSaves());
        // DEBUG ---------------------------------
    }

    private bool LoadFlag(string saveKey)
    {
        int value = _save.LoadInt(saveKey);

        if (value == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void ClearSaves()
    {
        _save.SaveData(_falseValue, SaveKeyManager.KeyFirstRun);
        _save.SaveData(_falseValue, SaveKeyManager.KeyFirstFly);
        _save.SaveData(_falseValue, SaveKeyManager.KeyFirstBalance);
        _save.SaveData(_falseValue, SaveKeyManager.KeyFirstHurricane);

    }

    private void PlayRunTutorial()
    {
        if (_isAlreadyRun == true) return;
        _runPanel.SetActive(true);
        _isTutorialPlaying = true;
        _isAlreadyRun = true;
        _save.SaveData(_trueValue, SaveKeyManager.KeyFirstRun);
        Time.timeScale = 0;
    }

    private void PlayFlyTutorial()
    {
        if (_isAlreadyFly == true) return;
        _flyPanel.SetActive(true);
        _isTutorialPlaying = true;
        _isAlreadyFly = true;
        _save.SaveData(_trueValue, SaveKeyManager.KeyFirstFly);
        Time.timeScale = 0;
    }

    private void PlayBalanceTutorial()
    {
        if (_isAlreadyBalance == true) return;
        _balancePanel.SetActive(true);
        _isTutorialPlaying = true;
        _isAlreadyBalance = true;
        _save.SaveData(_trueValue, SaveKeyManager.KeyFirstBalance);
        Time.timeScale = 0;
    }

    private void PlayHurricaneTutorial()
    {
        if (_isAlreadyHurricane == true) return;
        _hurricanePanel.SetActive(true);
        _isTutorialPlaying = true;
        _isAlreadyHurricane = true;
        _save.SaveData(_trueValue, SaveKeyManager.KeyFirstHurricane);
        Time.timeScale = 0;
    }

    private void StopPlaying()
    {
        _runPanel.SetActive(false);
        _flyPanel.SetActive(false);
        _balancePanel.SetActive(false);
        _hurricanePanel.SetActive(false);
        _isTutorialPlaying = false;
    }

    private void EndTutorial(Vector2 huinya)
    {
        if (_isTutorialPlaying == false) return;
        StopPlaying();
        Time.timeScale = 1;
        _isTutorialPlaying = false;
    }

    private void DestroyMe()
    {
        GameEvents.current.OnRunFirstEntryEvent -= PlayRunTutorial;
        GameEvents.current.OnFlyFirstEntryEvent -= PlayFlyTutorial;
        GameEvents.current.OnBalanceFirstEntryEvent -= PlayBalanceTutorial;
        GameEvents.current.OnHurricaneFirstEntryEvent -= PlayHurricaneTutorial;

        GameEvents.current.OnTouchBeganEvent -= EndTutorial;

        Destroy(gameObject);
    }
}