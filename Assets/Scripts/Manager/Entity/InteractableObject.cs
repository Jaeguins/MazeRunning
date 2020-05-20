using UnityEngine;

namespace Scripts.Manager.Entity
{
    public class InteractableObject:MonoBehaviour
    {
        [SerializeField] private bool looking = false;
        [SerializeField] private string commentMessage;
        [SerializeField] private CanvasGroup canvas;
        [SerializeField] internal float maxInteractDistance;

        public const float AlphaChangeSpeed=.1f;

        public void OnMouseEnter()
        {
            looking = true;
        }
        public void OnMouseExit()
        {
            looking = false;
        }
        public void Update()
        {
            if(canvas!=null)
                canvas.alpha =Mathf.Clamp01(canvas.alpha+(looking&&Vector3.Distance(Camera.main.transform.position,transform.position)<maxInteractDistance? AlphaChangeSpeed:-AlphaChangeSpeed));
        }
        public virtual void OnMouseOver()
        {
            if(Vector3.Distance(Camera.main.transform.position,transform.position)<maxInteractDistance)
                CommentController.Comment(commentMessage);
        }
    }
}
