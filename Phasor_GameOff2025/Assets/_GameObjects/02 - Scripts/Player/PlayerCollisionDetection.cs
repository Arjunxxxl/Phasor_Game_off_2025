using System;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    [Header("Layer")] 
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask checkPointLayer;
    
    // Ref
    private Player player;
    private RespawnManager respawnManager;

    private void Update()
    {
        CheckIfPlayerFellBelowLevel();
    }

    #region SetUp

    public void SetUp(Player player)
    {
        respawnManager = RespawnManager.Instance;
        
        this.player = player;
    }

    #endregion
    
    private void OnTriggerEnter(Collider other)
    {
        if ((obstacleLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            ObstacleHangingAxe obstacleHangingAxe = other.GetComponent<ObstacleHangingAxe>();
            if (obstacleHangingAxe != null)
            {
                bool isMovingToRight = obstacleHangingAxe.IsMovingToRight;
                Vector3 direction = other.transform.right * (isMovingToRight ? 1 : -1);
                direction.Normalize();
                direction.y = 3.0f;

                Vector3 hitVelocity = direction * Constants.Player.HangingAxeHitSpeed;
                
                player.playerMovement.AddVelocityOnObstacle(hitVelocity);
            }
        }
        else if ((checkPointLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            CheckPoint checkPoint = other.GetComponent<CheckPoint>();
            if (checkPoint != null)
            {
                respawnManager.UpdatedLastCheckPoint(checkPoint);
            }
        }
    }

    private void CheckIfPlayerFellBelowLevel()
    {
        float yPos = transform.position.y;

        if (yPos <= Constants.Player.LevelBelowYPos)
        {
            respawnManager.RespawnPlayerAtLastCheckPoint();
        }
    }
}
