using UnityEngine;

namespace Scripts
{
    public class Tester : MonoBehaviour
    {
        public Tester Initialize()
        {
            return this;
        }
        public void Start()
        {
            Tester prefab = null;
            Tester temp1 = new Tester();
            Tester temp2 = Instantiate(prefab).Initialize();
        }
    }
}