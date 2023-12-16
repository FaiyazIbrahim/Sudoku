using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private CellController m_CellController;
    private float _time = 0f;
    private bool _canRunTimer;

    private void Start()
    {
        m_CellController.OnGameStarted += delegate
        {
            _canRunTimer = true;
        };

        //m_CellController.OnGameMatched += 
    }

    private void StartStopTimer(bool value) => _canRunTimer = value;

    void Update()
    {
        if (!_canRunTimer) return;
        _time += Time.deltaTime;
        int minutes = Mathf.FloorToInt(_time / 60f);
        int seconds = Mathf.FloorToInt(_time % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
