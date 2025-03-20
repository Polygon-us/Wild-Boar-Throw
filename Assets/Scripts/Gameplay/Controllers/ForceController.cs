using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Gameplay.Controllers
{
    public class ForceController : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float maxForce = 100f;
        [SerializeField, Range(0, 1)] private float incrementPercentage = 0.1f;
        [SerializeField, Range(0, 1)] private float decrementPercentage = 0.1f;
        [SerializeField] private int forceChargeTime = 3;
        [SerializeField] private AnimationCurve chargeCurve;

        [SerializeField] private Button clickBtn;
        [SerializeField] private TMP_Text stateText;

        public float MaxForce => maxForce;
        public float IncrementPercentage => incrementPercentage;
        public float DecrementPercentage => decrementPercentage;
        public int ForceChargeTime => forceChargeTime;

        public AnimationCurve ChargeCurve => chargeCurve;

        public Button ClickBtn => clickBtn;

        public TMP_Text StateText => stateText;
    }
}