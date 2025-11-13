using UnityEngine;

public static class Constants
{
    public static class Player
    {
        public static readonly float HangingAxeHitSpeed = 10.0f;
        public static readonly Vector3 ObstacleHitSpeedDecrement = new Vector3(-3.5f, -10.0f, -3.5f);
    }
    
    public static class PhaseData
    {
        public static readonly float PhaseActiveDuration = 5.0f;
        public static readonly float TimeShiftTimeSlow = 0.25f;
    }
}
