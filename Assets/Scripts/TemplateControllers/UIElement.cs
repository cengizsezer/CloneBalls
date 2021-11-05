using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class UIElement : MonoBehaviour
{
    [SerializeField, ReadOnly] protected bool isInitialized = false;
    [SerializeField, ReadOnly] protected bool isActive = false;
    [Space]
    public bool InitializeOnStart = false;
    public bool ActivateOnStart = false;
    public bool ActivateWithParent = true;
    [Space]
    [SerializeField] protected RectTransform[] UIObjects;
    [Space]
    [SerializeField] protected RectTransform[] SubUIElements;

    protected UIManager UIManager;
    protected GameController GameController;

    public bool IsActive => isActive;


    public virtual void Initialize()
    {
        if (isInitialized)
        {
            //Debug.LogError(gameObject.name + " is already initialized." +
            //          " It is being tried to be initialized more than once which may cause unpredictable issue.");
            return;
        }
        isInitialized = true;
        UIManager = UIManager.request();
        GameController = GameController.request();
        gameObject.SetActive(true);

        //Initialize all children
        foreach (RectTransform element in SubUIElements)
        {
            element.gameObject.GetComponent<UIElement>().Initialize();
        }
           

        //Deactivate all UIObjects because UI elements are deactivated by default
        //This is required for protection. If one activates a ui object from scene this code force it to be deactivated at start
        foreach (RectTransform uiObject in UIObjects)
        {
            uiObject.gameObject.SetActive(false);
        }
           


    }

    protected virtual void Start()
    {
        if (InitializeOnStart)
            Initialize();


        if (ActivateOnStart)
            SetActive(true);

    }

    public virtual void Activate()
    {
        SetActive(true);
    }

    public virtual void Deactivate()
    {
        SetActive(false);
    }

    public virtual void SetActive(bool activate)
    {
        if (isInitialized == false)
        {
            Debug.LogError("UI Element " + gameObject + " couldn't be activated or deactivated because it is not initialized yet.");
            return;
        }

        //Set active all sub ui elements as this element
        foreach (RectTransform uiElement in SubUIElements)
        {
            if (activate && uiElement.gameObject.GetComponent<UIElement>().ActivateWithParent)
                uiElement.gameObject.SetActive(true);
            else if (!activate)
                uiElement.gameObject.SetActive(false);
        }

        //Switch ui objects
        foreach (RectTransform uiObject in UIObjects)
            uiObject.gameObject.SetActive(activate);

        isActive = activate;
    }

    public virtual void PrepareForBuild()
    {
        foreach (RectTransform element in SubUIElements)
        {
            element.gameObject.SetActive(true);
        }

        foreach (RectTransform uiObject in UIObjects)
        {
            uiObject.gameObject.SetActive(false);
        }

        
    }
}
