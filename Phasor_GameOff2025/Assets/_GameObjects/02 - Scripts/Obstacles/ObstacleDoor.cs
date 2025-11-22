using System;
using UnityEngine;

public class ObstacleDoor : MonoBehaviour
{
    [SerializeField] private GameObject stoppingCollider;
    [SerializeField] private GameObject doorBars;
    [SerializeField] private Vector3 barsClosePos;
    [SerializeField] private Vector3 barsOpenPos;
    [SerializeField] private float openDur = 1.5f;
    private float openTimeElapsed = 0.0f;
    
    private bool isDoorOpen = false;
    private bool playOpenAniamtion = false;
    
    private void Start()
    {
        isDoorOpen = false;
        playOpenAniamtion = false;
        
        stoppingCollider.SetActive(true);
        
        doorBars.transform.localPosition = barsClosePos;
    }

    private void Update()
    {
        PlayOpenAnimation();
    }

    #region Door open

    public void OpenDoor()
    {
        if (isDoorOpen)
        {
            return;
        }
        
        isDoorOpen = true;
        playOpenAniamtion = true;
        
        openTimeElapsed = 0.0f;
    }

    private void PlayOpenAnimation()
    {
        if(!playOpenAniamtion)
        {
            return;
        }
        
        openTimeElapsed += Time.deltaTime;
        float fac = openTimeElapsed / openDur;
        doorBars.transform.localPosition = Vector3.Lerp(barsClosePos, barsOpenPos, fac);

        if (fac > 1)
        {
            playOpenAniamtion = false;
            stoppingCollider.SetActive(false);
        }
    }
    
    #endregion

    #region Getters

    public bool IsDoorOpen => isDoorOpen;

    #endregion
}
