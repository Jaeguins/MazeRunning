using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Manager.Entity {

    /// <summary>
    /// 자막 관리자
    /// </summary>
    public class CommentController : MonoBehaviour {
        /// <summary>
        /// 자막 출력
        /// </summary>
        /// <param name="msg">메세지</param>
        public static void Comment(string msg) {
            if (instance != null)//컨트롤러가 있을때만 출력
                instance.CommentMsg(msg);
            else
                Debug.LogWarning("No CommentController");
        }
        /// <summary>
        /// 내부 출력 메소드 - 이미 있는 자막은 덮어쓰기
        /// </summary>
        /// <param name="msg">메세지</param>
        private void CommentMsg(string msg) {
            nowTime = maxTime;
            textPart.text = msg;
            group.alpha = 1;
        }
        [Header("최대 자막 유지시간")]
        [SerializeField] private float maxTime = 5f;
        [Header("텍스트")]
        [SerializeField] private Text textPart;
        [Header("그래픽")]
        [SerializeField] private CanvasGroup group;
        /// <summary>
        /// 현재시간
        /// </summary>
        private float nowTime;
        /// <summary>
        /// 싱글톤 레퍼런스
        /// </summary>
        private static CommentController instance;
        /// <summary>
        /// 뒤처리
        /// </summary>
        public void OnDestroy() {
            instance = null;
        }
        /// <summary>
        /// 초기화
        /// </summary>
        public void Start() {
            instance = this;
        }
        /// <summary>
        /// 투명도 실시간 조절
        /// </summary>
        public void Update() {
            if (nowTime < 0) {
                if (group.alpha > 0) group.alpha -= Time.deltaTime;
            } else {
                nowTime -= Time.deltaTime;
            }
        }
    }

}