using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    [System.Serializable]
    public class ShakeData
    {
        public bool use;
        public float duration;
        public float strength;
        public int vibrato;
        public float randomness;
        public bool snapping;
        public bool fadeOut;
        public ShakeRandomnessMode shakeRandomnessMode;
    }
    
    [Header("Container")] 
    [SerializeField] private GameObject containerGo;

    [Header("Shake Data")]
    [SerializeField] private ShakeData landingShakeData;
    [SerializeField] private ShakeData hitShakeData;

    #region Singleton

    public static CameraShake Instance;

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
    
    public void ShakeCameraForLanding()
    {
        if (!landingShakeData.use)
        {
            return;
        }
        
        containerGo.transform.DOShakePosition(
            landingShakeData.duration, 
            landingShakeData.strength, 
            landingShakeData.vibrato, 
            landingShakeData.randomness, 
            landingShakeData.snapping, 
            landingShakeData.fadeOut,
            landingShakeData.shakeRandomnessMode);
    }
    
    public void ShakeCameraOnObstacleHit()
    {
        if (!hitShakeData.use)
        {
            return;
        }
        
        containerGo.transform.DOShakePosition(
            hitShakeData.duration, 
            hitShakeData.strength, 
            hitShakeData.vibrato, 
            hitShakeData.randomness, 
            hitShakeData.snapping, 
            hitShakeData.fadeOut,
            hitShakeData.shakeRandomnessMode);
    }
}
