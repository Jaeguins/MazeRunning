using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager.Entity.NodeObject
{
    public class WallRenderer : MonoBehaviour
    {
        private NodeRenderer node;
        public float dist;
        public Animator animator;
        public List<Light> light;
        public WallRenderer Initialize(NodeRenderer node,int dir)
        {
            this.node = node;
            transform.localScale = Vector3.one;
            transform.localRotation=Quaternion.Euler(0,90*(dir+1),0);
            transform.localPosition=new Vector3(Node.Xoffset[dir],0,Node.Yoffset[dir])*dist;
            StartCoroutine(AppearRoutine());
            return this;
        }
        public IEnumerator AppearRoutine()
        {
            float waitingTime = Vector3.Distance(transform.position, node.SceneManager.Player.transform.position)*-.04f+4+Random.Range(-.1f,.1f);
            while (waitingTime > 0)
            {
                waitingTime -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            animator.SetTrigger("Start");
        }

        public void Update()
        {
            if (light != null)
            {
                foreach(Light li in light)
                    li.enabled = Vector3.Distance(transform.position, node.SceneManager.Player.transform.position) < 5;
            }
        }
    }
}