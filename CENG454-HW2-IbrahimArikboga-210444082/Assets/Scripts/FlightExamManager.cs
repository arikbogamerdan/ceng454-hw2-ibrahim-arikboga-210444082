using UnityEngine;
using TMPro;

public class FlightExamManager : MonoBehaviour
{
    [Header("HUD Text References")]
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private TMP_Text missionText;
    [Header("Warning Message")]
    [SerializeField] private GameObject warningPanel;

    private bool hasTakenOff = false;
    private bool inDangerZone = false;
    private bool threatCleared = false;
    private bool missionComplete = false;
    void Start()
    {
        UpdateHUD("Ready for takeoff.", "");
        if (warningPanel != null) warningPanel.SetActive(false);
    }

    public void NotifyTakeoff()
    {
        if (hasTakenOff) return;
        hasTakenOff = true;
        UpdateHUD("Airborne. Find the corridor.", "");
        Debug.Log("[FlightExamManager] Takeoff detected.");
    }

    public void EnterDangerZone()
    {
        inDangerZone = true;
        UpdateHUD("Entered a Dangerous Zone!", "WARNING: Missile threat incoming!");
        if (warningPanel != null) warningPanel.SetActive(true);
        Debug.Log("[FlightExamManager] Entered danger zone.");
    }

    public void ExitDangerZone()
    {
        inDangerZone = false;
        threatCleared = true;
        UpdateHUD("Threat cleared. Find a landing strip.", "Zone exited safely.");
        if (warningPanel != null) warningPanel.SetActive(false);
        Debug.Log("[FlightExamManager] Exited danger zone. Threat cleared.");
    }

    public void OnMissileHit()
    {
        UpdateHUD("AIRCRAFT HIT! Mission failed.", "");
        Debug.Log("[FlightExamManager] Aircraft was hit by missile.");
    }

    public void TryCompleteMission()
    {
        if (!hasTakenOff)
        {
            UpdateHUD("You must take off first!", "");
            return;
        }
        if (!threatCleared)
        {
            UpdateHUD("You must clear the danger zone first!", "");
            return;
        }
        if (missionComplete) return;

        missionComplete = true;
        UpdateHUD("Mission Complete! Safe landing!", "Congratulations!");
        Debug.Log("[FlightExamManager] Mission complete!");
    }

    public bool HasTakenOff()    => hasTakenOff;
    public bool IsThreatCleared() => threatCleared;
    public bool IsInDangerZone()  => inDangerZone;
    public bool IsMissionComplete() => missionComplete;

    private void UpdateHUD(string status, string mission)
    {
        if (statusText  != null) statusText.text  = status;
        if (missionText != null) missionText.text = mission;
    }
}
