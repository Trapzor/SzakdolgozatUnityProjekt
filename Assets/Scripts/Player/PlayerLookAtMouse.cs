using UnityEngine;

namespace Player
{
    public class PlayerLookAtMouse : MonoBehaviour
    {
        private Vector3 mouse;
        private Camera m_camera;

        private void Start()
        {
            m_camera = Camera.main;
        }
    
        void FixedUpdate()
        {
        
            Vector3 point = m_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        
            float t = m_camera.transform.position.y / (m_camera.transform.position.y - point.y);
        
            Vector3 finalPoint = new Vector3(
                t * (point.x - m_camera.transform.position.x) + m_camera.transform.position.x,
                transform.position.y, 
                t * (point.z - m_camera.transform.position.z) + m_camera.transform.position.z);
        
            transform.LookAt(finalPoint, Vector3.up);
        }
    }
}
