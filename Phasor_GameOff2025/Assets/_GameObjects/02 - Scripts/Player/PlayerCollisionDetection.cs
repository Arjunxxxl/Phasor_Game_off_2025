using System;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    [Header("Layer")] 
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask checkPointLayer;
    [SerializeField] private LayerMask inventoryItemLayer;
    [SerializeField] private LayerMask doorLayer;
    [SerializeField] private LayerMask checkPointInfoCollider;
    [SerializeField] private LayerMask phasePickUpLayer;
    [SerializeField] private LayerMask diamondLayer;
    
    // Ref
    private Player player;
    private CheckPointManager checkPointManager;
    private DiamondManager diamondManager;
    private HeartManager heartManager;
    private ObjectPooler objectPooler;
    private CameraShake cameraShake;

    private void Update()
    {
        CheckIfPlayerFellBelowLevel();
    }

    #region SetUp

    public void SetUp(Player player)
    {
        checkPointManager = CheckPointManager.Instance;
        diamondManager = DiamondManager.Instance;
        heartManager = HeartManager.Instance;
        objectPooler = ObjectPooler.Instance;
        cameraShake = CameraShake.Instance;
        
        this.player = player;
    }

    #endregion
    
    private void OnTriggerEnter(Collider other)
    {
        if ((obstacleLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            ObstacleHangingAxe obstacleHangingAxe = other.GetComponent<ObstacleHangingAxe>();
            ObstacleRotatingHammer obstacleRotatingHammer = other.GetComponent<ObstacleRotatingHammer>();
            
            if (obstacleHangingAxe != null)
            {
                bool isMovingToRight = obstacleHangingAxe.IsMovingToRight;
                Vector3 direction = other.transform.right * (isMovingToRight ? 1 : -1);
                direction.Normalize();
                direction.y = 3.0f;

                Vector3 hitVelocity = direction * Constants.Player.HangingAxeHitSpeed;
                
                player.playerMovement.AddVelocityOnObstacle(hitVelocity);
                
                bool isDied = heartManager.ConsumeOneHeart();
                if (isDied)
                {
                    player.SetIsDead(true);
                    gameObject.SetActive(false);
                }
                
                cameraShake.ShakeCameraOnObstacleHit();
            }
            else if (obstacleRotatingHammer != null)
            {
                Vector3 direction = obstacleRotatingHammer.GetPerpendicular();
                direction.Normalize();
                direction.y = 3.0f;

                Vector3 hitVelocity = direction * Constants.Player.RotatingHammerHitSpeed;
                
                player.playerMovement.AddVelocityOnObstacle(hitVelocity);
                
                bool isDied = heartManager.ConsumeOneHeart();
                if (isDied)
                {
                    player.SetIsDead(true);
                    gameObject.SetActive(false);
                }
                
                cameraShake.ShakeCameraOnObstacleHit();
            }
        }
        else if ((checkPointLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            CheckPoint checkPoint = other.GetComponent<CheckPoint>();
            if (checkPoint != null)
            {
                checkPointManager.UpdatedLastCheckPoint(checkPoint);
            }
        }
        else if ((inventoryItemLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            InventoryItem inventoryItem = other.GetComponent<InventoryItem>();
            if (inventoryItem != null)
            {
                InventoryItemType itemType = inventoryItem.ItemType;
                player.inventory.AddItemToInventory(itemType, 1);
                
                player.playerEfxManager.PlayItemPickup();
                
                inventoryItem.OnItemPickedUp();
            }
        }
        else if ((doorLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            ObstacleDoor door = other.GetComponent<ObstacleDoor>();
            if (door != null)
            {
                bool isOpen = door.IsDoorOpen;

                if (!isOpen)
                {
                    int keyAmtInInventory = player.inventory.GetAmount(InventoryItemType.Key);

                    if (keyAmtInInventory > 0)
                    {
                        door.OpenDoor();
                        player.inventory.RemoveItemFromInventory(InventoryItemType.Key, 1);
                    }
                    else
                    {
                        // Show Key is requied message in ui and ask player to find he key
                    }
                }
            }
        }
        else if ((checkPointInfoCollider.value & (1 << other.gameObject.layer)) != 0)
        {
            InfoPanelManager.Instance.ShowInfoPanel(true, PhasesType.Unknown);
        }
        else if ((phasePickUpLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            PhasePickUp phasePickUp = other.GetComponent<PhasePickUp>();
            if (phasePickUp != null)
            {
                phasePickUp.OnPhasePickedUp(player);
            }
        }
        else if ((diamondLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            diamondManager.DiamondCollected();
            
            objectPooler.SpawnFromPool(1, other.transform.position, Quaternion.identity);
            other.gameObject.SetActive(false);
        }
    }

    private void CheckIfPlayerFellBelowLevel()
    {
        float yPos = transform.position.y;

        if (yPos <= Constants.Player.LevelBelowYPos)
        {
            bool isDied = heartManager.ConsumeOneHeart();

            if (isDied)
            {
                player.SetIsDead(true);
                gameObject.SetActive(false);
            }
            else
            {
                checkPointManager.RespawnPlayerAtLastCheckPoint();
            }
        }
    }
}
