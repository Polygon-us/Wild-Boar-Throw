using UnityEngine;
using General;
using System;
using TMPro;

namespace UI.PopUp
{
    public class YesNoPopUp : MonoBehaviour
    {
        #region Singleton

        static YesNoPopUp instance;

        public static YesNoPopUp Instance
        {
            get
            {
                if (instance)
                    return instance;
                
                instance = Instantiate(Resources.Load<GameObject>("Prefabs/Yes No Popup"))
                    .GetComponent<YesNoPopUp>();

                instance.panel = instance.gameObject.transform.GetChild(1).GetComponent<RectTransform>();
                instance.blocker = instance.gameObject.transform.GetChild(0).GetComponent<RectTransform>();

                return instance;
            }
        }

        #endregion

        #region Information

        [SerializeField] private Canvas canvas;
        [SerializeField] private GameObject msgText;
        [SerializeField] private GameObject yes, no;
        [SerializeField] private AnimationCurve openCurve;
        [SerializeField] private AnimationCurve closeCurve;

        public static bool IsOpen { get; private set; }

        #endregion

        #region Components

        [HideInInspector] public RectTransform blocker;
        [HideInInspector] public RectTransform panel;

        #endregion

        #region Events

        Action onYes;
        Action onNo;

        #endregion

        private void Open(Action onYesAction = null, Action onNoAction = null, string yesText = "Yes", string noText = "No")
        {
            yes.GetComponentInChildren<TMP_Text>().text = yesText;
            no.GetComponentInChildren<TMP_Text>().text = noText;

            onYes = onYesAction;
            onNo = onNoAction;

            OpenTween();
        }

        public void Open(string msgText = "", string continueText = "", string cancelText = "",
            Action onYesAction = null, Action onNoAction = null)
        {
            this.msgText.GetComponent<TMP_Text>().text = msgText;

            Open(onYesAction, onNoAction, continueText, cancelText);
        }
        
        private void OpenTween()
        {
            ExitController.AddAction(No);

            IsOpen = true;

            msgText.SetActive(false);
            yes.SetActive(false);
            no.SetActive(false);

            panel.localScale = Vector3.zero;
            blocker.gameObject.SetActive(true);

            LeanTween.alpha(blocker, 0f, 0f).setIgnoreTimeScale(true);
            LeanTween.alpha(blocker, 0.6f, 0.2f).setEase(openCurve).setIgnoreTimeScale(true);
            LeanTween.scale(panel, Vector3.one, 0.2f).setEase(openCurve).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                msgText.SetActive(true);
                yes.SetActive(true);
                no.SetActive(true);
            });
        }

        public void Yes()
        {
            Close(0.2f);

            onYes?.Invoke();
        }

        public void No()
        {
            Close(0.2f);

            onNo?.Invoke();
        }

        private void Close(float timeToClose)
        {
            ExitController.RemoveAction(No);

            CloseTween(timeToClose);
        }

        private void CloseTween(float timeToClose)
        {
            IsOpen = false;

            msgText.SetActive(false);
            yes.SetActive(false);
            no.SetActive(false);

            LeanTween.scale(panel, Vector3.zero, timeToClose).setEase(closeCurve).setIgnoreTimeScale(true);
            LeanTween.alpha(blocker, 0f, timeToClose).setEase(closeCurve).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                blocker.gameObject.SetActive(false);
            });
        }
    }
}