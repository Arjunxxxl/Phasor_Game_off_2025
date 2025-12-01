using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    [Header("Volume Reference")]
    [SerializeField] private Volume volume;

    [Header("Chromatic Aberration")]
    [Range(0f, 1f)] public float chromaticDefault = 0f;
    [Range(0f, 1f)] public float chromaticTarget = 0.5f;

    [Header("Vignette")]
    [Range(0f, 1f)] public float vignetteDefault = 0f;
    [Range(0f, 1f)] public float vignetteTarget = 0.4f;

    [Header("Animation Settings")]
    public float toTargetSpeed = 4f;
    public float toDefaultSpeed = 4f;
    public bool useUnscaledTime = true;

    // Internal References
    private ChromaticAberration _chromatic;
    private Vignette _vignette;

    // Animation States
    private bool animateToTarget = false;
    private bool animateToDefault = false;

    #region Singleton

    public static PostProcessingManager Instance;

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
        
        if (volume == null)
            volume = GetComponent<Volume>();

        if (volume == null)
        {
            Debug.LogError("[PostProcessingManager] No Volume assigned!");
            enabled = false;
            return;
        }

        volume.profile.TryGet(out _chromatic);
        volume.profile.TryGet(out _vignette);

        SetValues(chromaticDefault, vignetteDefault);
    }

    #endregion
    
    void Update()
    {
        float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

        if (animateToTarget)
        {
            float speed = toTargetSpeed * dt;
            AnimateValues(chromaticTarget, vignetteTarget, speed);
        }
        else if (animateToDefault)
        {
            float speed = toDefaultSpeed * dt;
            AnimateValues(chromaticDefault, vignetteDefault, speed);
        }
    }

    // -------------------------------------------------------------

    private void AnimateValues(float targetChromatic, float targetVignette, float speed)
    {
        bool doneChromatic = true;
        bool doneVignette = true;

        // Chromatic
        if (_chromatic != null)
        {
            float current = _chromatic.intensity.value;
            float next = Mathf.Lerp(current, targetChromatic, speed);
            _chromatic.intensity.value = next;

            if (Mathf.Abs(next - targetChromatic) > 0.001f)   
                doneChromatic = false;
        }

        // Vignette
        if (_vignette != null)
        {
            float current = _vignette.intensity.value;
            float next = Mathf.Lerp(current, targetVignette, speed);
            _vignette.intensity.value = next;

            if (Mathf.Abs(next - targetVignette) > 0.001f)   
                doneVignette = false;
        }

        if (doneChromatic && doneVignette)
        {
            animateToTarget = false;
            animateToDefault = false;
        }
    }

    // -------------------------------------------------------------

    private void SetValues(float chromaticValue, float vignetteValue)
    {
        if (_chromatic != null)
        {
            _chromatic.intensity.overrideState = true;
            _chromatic.intensity.value = chromaticValue;
        }

        if (_vignette != null)
        {
            _vignette.intensity.overrideState = true;
            _vignette.intensity.value = vignetteValue;
        }
    }

    // -------------------------------------------------------------
    // PUBLIC API
    // -------------------------------------------------------------

    /// <summary>
    /// Animates to the target visual effect.
    /// </summary>
    public void PlayEffect()
    {
        animateToDefault = false;
        animateToTarget = true;
    }

    /// <summary>
    /// Animates back to default values.
    /// </summary>
    public void StopEffect()
    {
        animateToTarget = false;
        animateToDefault = true;
    }

    /// <summary>
    /// Instantly resets without animation.
    /// </summary>
    public void ResetToDefaultInstant()
    {
        animateToDefault = false;
        animateToTarget = false;
        SetValues(chromaticDefault, vignetteDefault);
    }
}
