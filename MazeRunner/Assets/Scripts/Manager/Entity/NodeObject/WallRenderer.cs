using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager.Entity.NodeObject
{
    public class WallRenderer : MonoBehaviour
    {
        private NodeRenderer node;
        public float dist;
        public WallRenderer Initialize(NodeRenderer node,int dir)
        {
            this.node = node;
            transform.localScale = Vector3.one;
            transform.localRotation=Quaternion.Euler(0,90*(dir+1),0);
            transform.localPosition=new Vector3(Node.Xoffset[dir],0,Node.Yoffset[dir])*dist;
            return this;
        }

        public void Update()
        {
        }
    }
}