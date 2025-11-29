using System.Collections;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [Header("Spawn Point")]
    [SerializeField] private CheckPoint lastCheckPoint;
    
    // Player
    private Player player;
    private CameraController cameraController;
    private UiManager uiManager;
    
    #region Singleton

    public static CheckPointManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public void SetUp()
    {
        player = Player.Instance;
        uiManager = UiManager.Instance;
        cameraController = CameraController.Instance;
    }

    public void UpdatedLastCheckPoint(CheckPoint checkPoint)
    {
        if (lastCheckPoint == checkPoint)
        {
            return;
        }
        
        lastCheckPoint = checkPoint;
        
        CheckPointType checkPointType = lastCheckPoint.CheckPointType;
        if (checkPointType == CheckPointType.LevelEnd)
        {
            //TODO: Move to next level
        }
        else if (checkPointType == CheckPointType.PickUp)
        {
            checkPoint.PlayCheckPointActivatedEfx();
        }
        else
        {
            checkPoint.PlayCheckPointActivatedEfx();
            uiManager.GameplayUi.ShowCheckPointReachUi();
        }
    }

    public Vector3 GetLastCheckPointSpawnPos()
    {
        return lastCheckPoint.SpawnT.position;
    }

    public void RespawnPlayerAtLastCheckPoint()
    {
        player.playerMovement.ResetAllMovement();
        player.playerMovement.StopAllMovement(true);
        
        Vector3 lastCheckPointSpawnPos = GetLastCheckPointSpawnPos();
        player.transform.position = lastCheckPointSpawnPos;

        cameraController.SnapCameraToPlayer();
        
        StartCoroutine(EnableAllPlayerMovement());
    }

    IEnumerator EnableAllPlayerMovement()
    {
        yield return new WaitForFixedUpdate();
        player.playerMovement.StopAllMovement(false);
    }
}
