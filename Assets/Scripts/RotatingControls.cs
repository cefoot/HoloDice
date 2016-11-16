using UnityEngine;
using System.Collections;

namespace Assets
{
    public class RotatingControls : MonoBehaviour
    {
        private Transform cam;
        public GameObject Focus;
        private Quaternion startRotation;
        private Vector3 startPosition;

        // Use this for initialization
        void Start()
        {
            cam = transform.GetChild(0);
            startRotation = transform.rotation;
            startPosition = cam.position;
        }

        private Vector3 scale = new Vector3(0.01F, 0.01F, 0.01F);

        // Update is called once per frame
        void Update()
        {
            if (Focus != null)
            {
                cam.LookAt(Focus.transform);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.up, -2);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.up, 2);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                cam.Translate(Vector3.Scale(cam.forward, scale));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                cam.Translate(Vector3.Scale(-cam.forward, scale));
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                transform.rotation = startRotation;
                cam.position = startPosition;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
