using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Manager.Entity
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform Head;
        [SerializeField] private Rigidbody Body;
        private float XRot = 0, YRot = 0;

        [SerializeField] private float XSensitivity = 1f, YSensitivity = 1f, speed = .01f;
        [SerializeField] private string MouseX = "Mouse X", MouseY = "Mouse Y";
        [SerializeField] private KeyCode forward, backward, left, right;
        private MainSceneManager manager;
        public bool CanMove = false;
        public void Update()
        {
            ProcessInput();
            Head.localRotation = Quaternion.Euler(XRot, YRot, 0);
        }

        private void ProcessInput()
        {
            XRot += (Input.GetAxis(MouseY)) * YSensitivity;
            YRot += (Input.GetAxis(MouseX)) * XSensitivity;
            Vector3 movingForward = Head.forward, movingRight = Head.right;
            movingForward.y = 0;
            movingRight.y = 0;
            movingForward.Normalize();
            movingRight.Normalize();
            Vector3 toMove = Vector3.zero;
            if (!CanMove) return;
            if (Input.GetKey(forward)) toMove += movingForward;

            if (Input.GetKey(backward)) toMove -= movingForward;

            if (Input.GetKey(right)) toMove += movingRight;

            if (Input.GetKey(left)) toMove -= movingRight;

            toMove *= speed;
            Body.MovePosition(transform.position+toMove);
        }

        public void Initialize(MainSceneManager manager)
        {
            this.manager = manager;
            StartCoroutine(InitializeCoroutine());
        }

        private IEnumerator InitializeCoroutine()
        {
            yield return new WaitUntil(() => manager.MazeManager.Generated);
            transform.position = manager.MazeManager.nodes[manager.start.x, manager.start.y].NodePos + Vector3.up * .1f;
            Body.useGravity = true;
            CanMove = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}