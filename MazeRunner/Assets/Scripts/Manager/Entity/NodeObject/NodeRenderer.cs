using System;
using UnityEngine;

namespace Assets.Scripts.Manager.Entity.NodeObject
{
    public class NodeRenderer:MonoBehaviour
    {
        [NonSerialized]public Node Manager;
        public MainSceneManager SceneManager;
        public WallRenderer[] Walls;
        
        public NodeRenderer Initialize(MainSceneManager scene,Node parent)
        {
            SceneManager = scene;
            Manager = parent;
            transform.position = parent.NodePos;
            Walls=new WallRenderer[4];
            for (int i = 0; i < 4; i++)
            {
                if (Manager.Wall[i])
                {
                    Walls[i] = Instantiate(scene.GetWallPrefab(), transform).Initialize(this,i);
                }
            }

            return this;
        }

        
    }
}