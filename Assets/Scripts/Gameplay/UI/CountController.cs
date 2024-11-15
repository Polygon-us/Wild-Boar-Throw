using TMPro;
using UnityEngine;

public class CountController : MonoBehaviour
{
    [SerializeField] private int count = 3;
    [SerializeField] private TMP_Text countText;
    
    public int Count => count;
    public TMP_Text CountText => countText;

    private void Start()
    {
        Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}