using UnityEngine;
using System.Collections;
using System;
using Assets;

public class SlicerState : MonoBehaviour
{
    private enum State
    {
        Default,
        WallsFading,
        CamVisible,
        CamFovVisible,
        CamPosChange,
        AddCam,

        /// <summary>
        /// Last State in Enum (Marked as rollover)
        /// </summary>
        Reset
    }

    private State CurState { get; set; }

    private GameObject addCam { get; set; }

    // Use this for initialization
    void Start()
    {
        CurState = State.Default;
    }

    public void OnSelect()
    {
        CurState++;
        switch (CurState)
        {
            case State.WallsFading:
                WallsFading(true);
                break;
            case State.CamVisible:
                ShowMainCam(true);
                break;
            case State.CamFovVisible:
                ShowMainCamFov(true);
                break;
            case State.CamPosChange:
                ChangeCamFov(true);
                break;
            case State.AddCam:
                CopyCam();
                break;
            case State.Reset:
                CurState = State.Default;
                WallsFading(false);
                ShowMainCamFov(false);
                ChangeCamFov(false);
                ShowMainCam(false);
                if (addCam != null)
                {
                    Destroy(addCam);
                    addCam = null;
                }
                break;
        }
    }

    private void CopyCam()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Cam_main")
            {
                addCam = Instantiate(child.gameObject);
                addCam.transform.SetParent(transform, true);
                addCam.transform.localScale = child.localScale;

                var anim = addCam.GetComponent<Animator>();
                anim.Play("camTransformation");
                addCam.GetComponentInChildren<Light>().color = new Color(0, 255, 33);
            }
        }
    }

    private void ChangeCamFov(bool enabled)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Cam_main")
            {
                var anim = child.gameObject.GetComponent<Animator>();
                if (!enabled)
                {
                    anim.Play("camMoving_r");
                }
                else
                {
                    anim.Play("camMoving");
                }
            }
        }
    }
    private void ShowMainCam(bool enabled)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Cam_main")
            {
                if (enabled)
                {
                    child.gameObject.SetActive(enabled);
                }
            }
            else if (child.gameObject.name == "ir_grid")
            {
                child.gameObject.SetActive(!enabled);
            }
        }
    }

    private void ShowMainCamFov(bool enabled)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Cam_main")
            {
                child.GetChild(0).gameObject.SetActive(enabled);
            }
        }
    }

    private void WallsFading(bool enable)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Walls")
            {
                foreach (var anim in child.GetComponentsInChildren<Animator>())
                {
                    if (!enabled)
                    {
                        anim.Play("fading_r");
                    }
                    else
                    {
                        anim.Play("fading");
                    }
                }
            }
        }
    }
    private float timeState = 0f;

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray();
        bool valid = false;
        if (Input.touchCount >= 1 &&
                Input.touches[0].position.x < Screen.width - 50f &&
                Input.touches[0].position.x > 50f &&
                Time.fixedTime - timeState > 1f)
        {
            OnSelect();
            timeState = Time.fixedTime;
        }
        if (!valid && Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            valid = true;
        }
        if (valid)
        {
            Debug.DrawRay(Camera.main.transform.position, ray.direction, Color.red, 10, false);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject hitObj = hitInfo.collider.gameObject;
                while (hitObj.transform.parent != null)
                {
                    hitObj = hitObj.transform.parent.gameObject;
                }
                if (hitObj == gameObject)
                {
                    OnSelect();
                }
            }
        }


    }
}
