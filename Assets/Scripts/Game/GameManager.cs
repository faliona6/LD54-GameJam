// Load Order: priority in script load order (otherwise _instance might not be set when used)

using System.Collections;
using System.Collections.Generic;
using Customer;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance => _instance;
    static GameManager _instance;

    public Canvas WorldCanvas => _worldCanvas;
    [SerializeField] Canvas _worldCanvas;
    public Canvas DefeatCanvas => _defeatCanvas;
    [SerializeField] Canvas _defeatCanvas;

    public CustomerManager CustomerManager => _customerManager;
    [SerializeField] CustomerManager _customerManager;

    // Level Vars
    public List<SO_Level> levels = new List<SO_Level>();
    public int curDay = 0;

    // Money Vars
    public int Money => _money;
    [SerializeField] int _money;
    [SerializeField] TextMeshProUGUI _moneyText;

    public static UnityEvent OnEndDay = new UnityEvent();
    public static UnityEvent OnEndNight = new UnityEvent();
    public static UnityEvent OnWonGame = new UnityEvent();
    public static UnityEvent OnLostGame = new UnityEvent();

    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    void Start() {
        Debug.Log("started");
        
        CustomerManager.OnCustomersDone += Night;
        //TODO: add listerner for upgrademanager done --> Day

        Day(curDay);

        // Debug
        ModifyMoney(200);
    }

    void Day(int curDay) {
        int numCustomers = levels[curDay].numCustomers;
        CustomerManager.SummonCustomers(numCustomers);
    }

    void Night() {
        // TODO: show upgrade screen
    }
    
    public void WonGame() {
        OnWonGame.Invoke();
    }
    public void LostGame() {
        OnLostGame.Invoke();
        DefeatCanvas.gameObject.SetActive(true);
    }

    public void RestartGame() {
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
        DefeatCanvas.gameObject.SetActive(false);
    }

    /**********************   Money Funcs   *********************/

    public bool ModifyMoney(int value) {
        int newMoney = _money + value;
    
        _money = newMoney;
        _moneyText.text = _money.ToString();
    
        return true;
    }
}