using UnityEngine;

namespace Aezakmi
{
    public class ArrowIndicator : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private new Renderer renderer;
        [SerializeField] private Color arrowsColor = Color.white;

        public Transform Target { get => target; set => target = value; }
        public Renderer Renderer { get => renderer; set => renderer = value; }
      //  private Vector3 worldPos=Vector3.zero;
        private void Start()
        {
            renderer.material.SetColor("_Color", arrowsColor);
        }

        private void LateUpdate()
        {
            if (target == null) return;
            // Set size
           // worldPos=transform.TransformPoint(transform.position);
            
            var distance = Vector3.Distance(transform.position, target.position);// / ReferenceManager.Instance.player.lossyScale.x;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, distance/3.254228f); //hard coded correction for distance
            renderer.material.SetFloat("_Length", distance);

            // Rotate
            Vector3 direction = (target.position - transform.position);
            transform.rotation = Quaternion.LookRotation(direction);
            transform.localEulerAngles = new Vector3
            (
                0f,
                transform.localEulerAngles.y,
                0f
            );
        }
    }
}
