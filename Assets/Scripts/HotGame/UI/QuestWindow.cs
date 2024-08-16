using HotGame;
using MVC.Extensions;
using MVC.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class QuestWindow : UIWindow
    {
        private Button buttonStart;
        private Button buttonSkip;
        private Button buttonConfirm;
        private Button buttonRefuse;

        private TextMeshProUGUI textSpeaker;
        private Image imageSpeaker;

        private HeroProxy heroProxy;

        protected override void OnCreate(GameObject gameObject, object userdata)
        {
            base.OnCreate(gameObject, userdata);
            heroProxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;

            textSpeaker = transform.Find("Panel/Bottom/DialogueBox/Character/Name").GetComponent<TextMeshProUGUI>();
            imageSpeaker = transform.Find("Panel/Bottom/DialogueBox/Character/Avatar").GetComponent<Image>();
            imageSpeaker.LoadSprite("ui_beast_levelup_shangchengjiantou");


            buttonStart.onClick.AddListener(Start);
            EnableUpdate(true);
        }


        protected override void OnUpdate(float deltaTime)
        {

        }
        protected override void OnDelete()
        {
            //buttonTime.onClick.RemoveAllListeners();
            EnableUpdate(false);
            base.OnDelete();
        }

        public void RefreshQuestUI(QuestState state)
        {

        }

        public void RefreshUI()
        {
        }

        public void Start()
        {
            SendNotification(LoadStage.NAME);
        }
    }
}