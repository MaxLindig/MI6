using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    #region Events
    public static UnityAction OnTouchPadUp = null;
    public static UnityAction OnTouchPadDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;
    #endregion

    #region Anchors
    public GameObject m_LeftAnchor;
    public GameObject m_RightAnchor;
    public GameObject m_HeadAnchor;
    #endregion

    #region Input
    private Dictionary<OVRInput.Controller, GameObject> m_ControllerSets = null;
    private OVRInput.Controller m_InputSource = OVRInput.Controller.None;
    private OVRInput.Controller m_Controller = OVRInput.Controller.None;
    private bool m_InputActive = true;
    #endregion

    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;

        m_ControllerSets = CreateControllerSets();
    }

    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;

    }

    private void Update()
    {
        //Check for active Input
        if (!m_InputActive)
            return;

        //Check if a Controller exists
        CheckForController();

        //Check for input Source
        CheckInputSource();

        //Check for acutal input
        Input();
    }

    private void CheckForController()
    {
        OVRInput.Controller controllerCheck = m_Controller;

        //Right Remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
            controllerCheck = OVRInput.Controller.RTouch;


        //Left Remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTouch))
            controllerCheck = OVRInput.Controller.LTouch;

        //If no controllers, headset
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.LTouch) &&
            !OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
            controllerCheck = OVRInput.Controller.Touch;

        //Update
        m_Controller = UpdateSource(controllerCheck, m_Controller);
    }

    private void CheckInputSource()
    {

        //Update 
        m_InputSource = UpdateSource(OVRInput.GetActiveController(), m_InputSource);

    }

    private void Input()
    {
        //TouchPad down 
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (OnTouchPadDown != null)
                OnTouchPadDown();
        }

        //Touchpad up
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (OnTouchPadUp != null)
                OnTouchPadUp();
        }

    }


    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {
        //If values are the same, return
        if (check == previous)
            return previous;

        //Get controller object
        GameObject controllerObject = null;
        m_ControllerSets.TryGetValue(check, out controllerObject);

        //If no controller, set to the head 
        if (controllerObject == null)
            controllerObject = m_HeadAnchor;

        //Send out event 
        if (OnControllerSource != null)
            OnControllerSource(check, controllerObject);

        //Return new value
        return check;
    }

    private void PlayerFound()
    {
    
    }

    private void PlayerLost()
    {

    }

    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets()
    {
        Dictionary<OVRInput.Controller, GameObject> newSets = new Dictionary<OVRInput.Controller, GameObject>()
        {
            {OVRInput.Controller.LTouch, m_LeftAnchor },
            {OVRInput.Controller.RTouch, m_RightAnchor},
            {OVRInput.Controller.Touch, m_HeadAnchor }
        };

        return newSets;
    }
}
