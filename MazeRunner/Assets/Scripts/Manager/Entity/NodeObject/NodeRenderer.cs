using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager.Entity.NodeObject
{
    public enum NodeType
    {
        Normal,
        Start,
        End
    }
    public class NodeRenderer:MonoBehaviour
    {
        [NonSerialized]public Node Manager;
        public MainSceneManager SceneManager;
        public WallRenderer[] Walls;
        
        public NodeRenderer Initialize(MainSceneManager scene,Node parent,NodeType nodeType=NodeType.Normal)
        {
            SceneManager = scene;
            Manager = parent;
            transform.position = parent.NodePos;
            Walls=new WallRenderer[4];
            List<int>walls=new List<int>(4);
            if(Manager.X==0)walls.Add(3);
            if(Manager.Y==0)walls.Add(0);
            walls.Add(1);
            walls.Add(2);
            
            foreach (int i in walls)
            {
                
                if (Manager.Wall[i])
                {
                    Walls[i] = Instantiate(scene.GetWallPrefab(), transform).Initialize(this,i);
                }
            }

            switch (nodeType)
            {
                case NodeType.Start:
                    Instantiate(SceneManager.WallPoolManager.StartPrefab, transform);
                    break;
                case NodeType.End:
                    Instantiate(SceneManager.WallPoolManager.EndPrefab, transform);
                    break;
            }
            return this;
        }

        
    }
}