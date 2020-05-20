using Scripts.Manager.Entity;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Manager.Quiz {

    //퀴즈 버튼 - E키로 상호작용하는 부분
    public class QuizButtonInteractable : InteractableObject {
        /// <summary>
        /// 개념적 버튼
        /// </summary>
        private QuizButton Button;
        /// <summary>
        /// 텍스트 부분
        /// </summary>
        private Text text;
        /// <summary>
        /// 초기화
        /// </summary>
        /// <param name="btn">개념적 버튼</param>
        /// <param name="value">지문</param>
        public void Initialize(QuizButton btn, string value) {
            Button = btn;
            text = GetComponentInChildren<Text>(); //자동으로 찾아 지정
            text.text = value;
        }
        /// <summary>
        /// 커서가 바라보고 있을 때
        /// </summary>
        public override void OnMouseOver() {
            base.OnMouseOver(); //부모클래스의 자막출력
            if (Vector3.Distance(Camera.main.transform.position, transform.position) > maxInteractDistance) return; //너무멀면 작동X
            if (Input.GetKeyDown(KeyCode.E)) //E누르면 작동
            {
                Button.Click();
            }
        }
    }

}