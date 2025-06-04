using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace UI.Login
{
    public class SingInView : MonoBehaviour
    {
        [SerializeField] private TMP_Text loadingTxt;
        [SerializeField] private Button loginBtn;

        public void ShowLoading()
        {
            loadingTxt.gameObject.SetActive(true);
            loginBtn.gameObject.SetActive(true);
        }

        public void ShowLogin(Action callback)
        {
            loadingTxt.gameObject.SetActive(false);
            loginBtn.gameObject.SetActive(true);
            
            loginBtn.onClick.RemoveAllListeners();
            loginBtn.onClick.AddListener(() => callback?.Invoke());
        }
    }
}