using UnityEngine;
using UnityEngine.UI;

public class SoundOnClick : MonoBehaviour
{
    [SerializeField] string Ui_SFX_Name;// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlaySoundOnButton);
    }

    private void PlaySoundOnButton()
    {
        AudioManager.Instance.PlayUI(Ui_SFX_Name);
    }
}
