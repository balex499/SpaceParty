using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUI : MonoBehaviour
{
    public static GeneralUI Instance;
    [SerializeField] private List<StarsCounter> _starsCounters;

    private void Awake()
    {
        Instance = this;
    }

    public static void AddStar(Star star)
    {
        StarsCounter starsCounter = Instance._starsCounters.Find(x => x.playerID == star.Target.ID);
        starsCounter?.Change(star.value);
    }

    public static void DeathPenalty(Player player)
    {
        StarsCounter starsCounter = Instance._starsCounters.Find(x => x.playerID == player.ID);
        starsCounter?.Change(-5);
    }

    [SerializeField] private TMPro.TMP_Text _timer;
    public static void StartTimer(int time)
    {
        Instance.StartCoroutine(Instance.TimerRoutine(time));
    }
    private IEnumerator TimerRoutine(int time)
    {
        while (time != 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            time--;
            _timer.text = time.ToString();
        }
    }
}
