using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TimerText;
    [SerializeField] private CellController m_CellController;
    private float _time = 0f;
    private bool _canRunTimer;

    private void Start()
    {
        m_CellController.OnGameStarted += delegate
        {
            StartStopTimer(true);
        };

        m_CellController.OnGameMatched += delegate (bool value)
        {
            StartStopTimer(!value);
        };
    }

    private void StartStopTimer(bool value) => _canRunTimer = value;

    void Update()
    {
        if (!_canRunTimer) return;
        _time += Time.deltaTime;
        int minutes = Mathf.FloorToInt(_time / 60f);
        int seconds = Mathf.FloorToInt(_time % 60f);
        m_TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public string GetTime()
    {
        return m_TimerText.text;
    }
}
