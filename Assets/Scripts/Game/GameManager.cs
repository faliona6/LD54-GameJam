// Load Order: priority in script load order (otherwise _instance might not be set when used)

using System.Collections;
using System.Collections.Generic;
using Customer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance => _instance;
    static GameManager _instance;

    public Canvas WorldCanvas => _worldCanvas;
    [SerializeField] Canvas _worldCanvas;
    
    public CustomerManager CustomerManager => _customerManager;
    [SerializeField] CustomerManager _customerManager;

    // Level Vars
    public List<SO_Level> levels = new List<SO_Level>();
    public int curDay = 0;

    // Money Vars
    public int Money => _money;
    [SerializeField] int _money;

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
        CustomerManager.OnCustomersDone += Night;
        //TODO: add listerner for upgrademanager done --> Day

        Day(curDay);

        // Debug
        // ModifyMoney(25);
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
    void LostGame() {
        OnLostGame.Invoke();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single); // TODO: change to load Title screen
    }

    /**********************   Money Funcs   *********************/

    // public bool ModifyMoney(int value) {
    //     int newMoney = _money + value;
    //     if (newMoney < 0) {
    //         return false;
    //     }
    //
    //     _money = newMoney;
    //     EventManager.Invoke(gameObject, EventID.ModifyMoney, new DeltaArgs {newValue = _money, deltaValue = value});
    //
    //     return true;
    // }
}