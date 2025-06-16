using UnityEngine.Localization;
using UnityEngine;
using TMPro;

namespace Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizationText : MonoBehaviour
    {
        [SerializeField] private LocalizedString localizedString;
       
        private TMP_Text text;
        
        private void Awake()
        {
            text = GetComponent<TMP_Text>();
            text.text = localizedString.GetLocalizedString();
        }
    }
}