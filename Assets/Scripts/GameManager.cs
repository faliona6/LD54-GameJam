// Load Order: priority in script load order (otherwise _instance might not be set when used)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance => _instance;
    static GameManager _instance;

    public Canvas WorldCanvas => _worldCanvas;
    [SerializeField] Canvas _worldCanvas;

    // Time Vars
    public int day = 1;
    
    public int TimeScale => _timeScale;
    [SerializeField] int _timeScale;
    [SerializeField] int dayDuration = 1;
    [SerializeField] int nightDuration = 1;
    [SerializeField] float curTime;
    
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
        StartCoroutine(GameLoop());

        // Debug
        // ModifyMoney(25);
    }

    IEnumerator GameLoop() {
        while (true) {
            yield return StartCoroutine(TimeCycle(dayDuration, OnEndDay));
            yield return StartCoroutine(TimeCycle(nightDuration, OnEndNight));
            
            day++;
            // UIManager.Instance.UpdateDayText(day);
        }
    }

    IEnumerator TimeCycle(float duration, UnityEvent endEvent) {
        while (curTime < duration) {
            curTime += Time.deltaTime * _timeScale;
            // UIManager.Instance.UpdateTimeProgressBar(curTime / duration);
            yield return null;
        }

        curTime = 0;
        // UIManager.Instance.UpdateTimeProgressBar(curTime / duration);
        
        endEvent.Invoke();
    }
    void EndDay() {
        curTime = dayDuration;
    }
    void EndNight() {
        curTime = nightDuration;
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