using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Manager.Entity
{
    public class ToNextGame : InteractableObject
    {
        public override void OnMouseOver()
        {
            base.OnMouseOver();
            if (Vector3.Distance(Camera.main.transform.position, transform.position) > maxInteractDistance) return;
            if(Input.GetKey(KeyCode.E))
                StartCoroutine(NextGameRoutine());
            
        }

        private IEnumerator NextGameRoutine()
        {
            Fader.TargetStatus = true;
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
