using UnityEngine;

namespace WheelOfFortuneGame
{
    /// <summary>
    /// Represents the configuration settings for the Wheel of Fortune game.
    /// </summary>
    [CreateAssetMenu(menuName = "WheelOfFortune/Configuration", fileName = "WheelOfFortuneConfig")]
    public class WheelOfFortuneConfig : ScriptableObject
    {
        [Header("Winning Conditions")]

        [Tooltip("Specific spin counts that guarantee a win.")]
        public int[] winSpinCounts = { 2, 7, 8 };

        [Tooltip("The maximum number of spins after which the win probability is considered.")]
        [Range(1, 10)]
        public int maxSpinCount = 10;

        [Tooltip("The probability of winning after reaching the maxSpinCount.")]
        [Range(0f, 1f)]
        public float winProbability = 0.05f;

        [Header("Number Range")]

        [Tooltip("The minimum number that can be selected on the wheel.")]
        public int minNumber = 1;

        [Tooltip("The maximum number that can be selected on the wheel.")]
        public int maxNumber = 8;

        [Header("Wheel Spin Settings")]

        [Tooltip("Duration of the initial fast spin of the wheel.")]
        [Range(0.5f, 10f)]
        public float wheelSpinRotationDuration = 3f;

        [Tooltip("Duration of the smooth rotation to the target angle.")]
        [Range(0.5f, 3f)]
        public float wheelSmoothRotationDuration = 1f;
    }
}