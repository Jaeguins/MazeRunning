using System.Collections;
using UnityEngine;

namespace Scripts.Manager.Entity {

    //플레이어 스크립트
    public class Player : MonoBehaviour {
        [Header("머리")] [SerializeField] private Transform Head;
        [Header("몸")] [SerializeField] private Rigidbody Body;
        /// <summary>
        /// 회전 위한 계수
        /// </summary>
        private float XRot = 0,
                      YRot = 0;
        [Header("마우스 민감도, 이동속도")] [SerializeField] private float XSensitivity = 1f,
                                                                 YSensitivity = 1f,
                                                                 speed = .01f;
        [Header("마우스 X,Y 기값")] [SerializeField] private string MouseX = "Mouse X",
                                                               MouseY = "Mouse Y";
        [Header("이동키")] [SerializeField] private KeyCode forward,
                                                         backward,
                                                         left,
                                                         right;
        /// <summary>
        /// 관리자
        /// </summary>
        private MainSceneManager manager;
        [Header("움직일수 있는지 여부")] public bool CanMove = false;
        /// <summary>
        /// 실시간 갱신
        /// </summary>
        public void FixedUpdate() {
            ProcessInput(); //입력처리
            Head.localRotation = Quaternion.Euler(XRot, YRot, 0); //계산각도에 따라 머리 회전
        }
        /// <summary>
        /// 입력처리
        /// </summary>
        private void ProcessInput() {
            if (!CanMove) return; //못움직이면 아무것도 안함

            //머리 회전 처리
            XRot += (Input.GetAxis(MouseY)) * YSensitivity;
            YRot += (Input.GetAxis(MouseX)) * XSensitivity;

            //보는 방향 앞, 오른쪽
            Vector3 movingForward = Head.forward,
                    movingRight = Head.right;

#region 위아래 간섭 제거

            movingForward.y = 0;
            movingRight.y = 0;
            movingForward.Normalize();
            movingRight.Normalize();

#endregion

#region 이동처리

            Vector3 toMove = Vector3.zero;
            if (Input.GetKey(forward)) toMove += movingForward;
            if (Input.GetKey(backward)) toMove -= movingForward;
            if (Input.GetKey(right)) toMove += movingRight;
            if (Input.GetKey(left)) toMove -= movingRight;
            toMove *= speed;
            Body.MovePosition(transform.position + toMove);

#endregion
        }
        /// <summary>
        /// 초기화
        /// </summary>
        /// <param name="manager">관리자</param>
        public void Initialize(MainSceneManager manager) {
            this.manager = manager;
        }
        /// <summary>
        /// 맵 생성 후 초기화 해야되는 부분
        /// </summary>
        /// <returns></returns>
        public IEnumerator InitializeCoroutine() {
            yield return new WaitUntil(() => manager.MazeManager.Generated);
            transform.position = manager.MazeManager.nodes[manager.start.x, manager.start.y].NodePos + Vector3.up * .1f;
            Body.useGravity = true;
            CanMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}