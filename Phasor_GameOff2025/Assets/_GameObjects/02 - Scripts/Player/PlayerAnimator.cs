using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;

    private readonly int MaxIdleAnims = 5;
    
    private readonly string IdleAnimTag = "IdleIdx";
    private readonly string RunAnimTag = "Run";
    private readonly string JumpAnimTag = "Jump";
    private readonly string FallAnimTag = "Fall";

    #region Set Up

    public void SetUp()
    {
        anim = GetComponentInChildren<Animator>();

        PlayIdleAnimation();
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
}
