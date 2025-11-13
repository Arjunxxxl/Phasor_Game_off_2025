using System;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    [Header("Layer")] 
    [SerializeField] private LayerMask obstacleLayer;
    
    // Ref
    private Player player;

    #region SetUp

    public void SetUp(Player player)
    {
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
    }
}
