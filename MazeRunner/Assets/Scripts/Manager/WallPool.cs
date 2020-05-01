using System.Collections.Generic;
using Assets.Scripts.Manager.Entity.NodeObject;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    [CreateAssetMenu(fileName = "WallPool",menuName = "custom/WallPool")]
    public class WallPool:ScriptableObject
    {
        public List<WallRenderer> wallPrefabs;
        private MainSceneManager Manager;
        public WallRenderer GetWallPrefab(int type = -1) => type == -1 ? wallPrefabs[(int) (Random.value * wallPrefabs.Count)] : wallPrefabs[type];

        public void Initialize(MainSceneManager manager)
        {
            Manager = manager;
        }
    }
}