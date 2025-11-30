using DG.Tweening;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Container")]
    [SerializeField] private GameObject ContainerGo;

    [Header("Jump and Land Animation")]
    [SerializeField] private Tween jumpLandTween;
    
    private Animator anim;

    private readonly int MaxIdleAnims = 5;
    
    private readonly string IdleAnimTag = "IdleIdx";
    private readonly string RunAnimTag = "Run";
    private readonly string JumpAnimTag = "Jump";
    private readonly string FallAnimTag = "Fall";
    
    // Ref
    private Player player;

    #region Set Up

    public void SetUp(Player player)
    {
        anim = GetComponentInChildren<Animator>();

        this.player = player;
        
        PlayIdleAnimation();
        SetUpJumpAndLandTween();
    }

    #endregion

    #region Animations

    public void PlayIdleAnimation()
    {
        int randIdleIdx = Random.Range(0, MaxIdleAnims);
        float idleVal = 1.0f * randIdleIdx / MaxIdleAnims;
        
        anim.SetFloat(IdleAnimTag, idleVal);
        
        anim.SetBool(RunAnimTag, false);
        anim.SetBool(JumpAnimTag, false);
    }
    
    public void PlayRunAnimation()
    {
        anim.SetBool(RunAnimTag, true);
    }
    
    public void StopRunAnimation()
    {
        anim.SetBool(RunAnimTag, false);
    }
    
    public void PlayJumpAnimation()
    {
        anim.SetBool(JumpAnimTag, true);
    }
    
    public void StopJumpAnimation()
    {
        anim.SetBool(JumpAnimTag, false);
    }
    
    public void PlayFallAnimation()
    {
        anim.SetBool(FallAnimTag, true);
    }
    
    public void StopFallAnimation()
    {
        anim.SetBool(FallAnimTag, false);
    }

    #endregion

    #region Jump and Land Animation

    private void SetUpJumpAndLandTween()
    {
        jumpLandTween.tweenCollection[0].tdScale.tweenDuration = player.playerMovement.JumpMaxDuration;
    }
    
    public void PlayJumpTween()
    {
        jumpLandTween.PlayTween("Jump");
    }
    
    public void PlayLandTween()
    {
        jumpLandTween.PlayTween("Land");
    }

    public void ResetScale()
    {
        ContainerGo.transform.localScale = Vector3.one;
    }

    #endregion
}
