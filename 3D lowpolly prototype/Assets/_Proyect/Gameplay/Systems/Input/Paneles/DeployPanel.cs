using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DigitalRubyShared;

public class DeployPanel : MonoBehaviour, IBasePanel
{
    //left to rigth
    private const int CARD_AMOUNT = 4;

    [SerializeField]
    private DeployCard[] deployCards = new DeployCard[CARD_AMOUNT];

    private DeployCard posibleCard;
    private DeployCard selectedCard;

    
    private TapGestureRecognizer tapSelectCardGesture;
    private PanGestureRecognizer dragSelectGesture;
    private LongPressGestureRecognizer getInfoGesture;
    private DragAndDropGesture dragAndDropCardGesture; // creare una gesture personalizada alv :v

    private List<RaycastResult> rayCastResults = new List<RaycastResult>();

    private RectTransform mainPanel;
    private RectTransform panelRectTransform;
    private Canvas canvasOfPanel;

    private void Start()
    {
        AssingUIElementReferences();
        CreateGestures();

        ActivateGestures();
    }
    private void AssingUIElementReferences()
    {
        mainPanel = GameObject.FindGameObjectWithTag("MainPanel").GetComponent<RectTransform>();
        panelRectTransform = GetComponent<RectTransform>();
        canvasOfPanel = panelRectTransform.root.GetComponent<Canvas>();
        if (panelRectTransform == null || canvasOfPanel == null || mainPanel == null)
        {
            Debug.LogError("rectTransform or canvas missing");
        }
    }
    private void CreateGestures()
    {
        CreateTapSelectGesture();
        CreateDragAndSelectGesture();
        CreateGetInfoGesture();
        CreateDragAndDropGesture();
    }

    //funciona bien!
    private void CreateTapSelectGesture()
    {
        tapSelectCardGesture = new TapGestureRecognizer();
        tapSelectCardGesture.ThresholdSeconds = 0.6f;
        tapSelectCardGesture.SendBeginState = true;
        tapSelectCardGesture.StateUpdated += TapSelectCardCallBack;
    }
    private void TapSelectCardCallBack(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Began)
        {
            if (! OnCard(gesture.FocusX, gesture.FocusY))
            {
                gesture.Reset();
            }
        }
        else if (gesture.State == GestureRecognizerState.Ended)
        {
            SelectCard(posibleCard);
        }
    }

    //el drag funciona bien!
    private void CreateDragAndSelectGesture()
    {
        dragSelectGesture = new PanGestureRecognizer();
        dragSelectGesture.AllowSimultaneousExecution(tapSelectCardGesture);
        dragSelectGesture.StateUpdated += DragAndSelectCallBack;
    }
    private void DragAndSelectCallBack(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Began)
        {
            if (!OnCard(gesture.FocusX, gesture.FocusY))
            {
                gesture.Reset();
            }
        }
        else if (gesture.State == GestureRecognizerState.Executing)
        {
            if (posibleCard != selectedCard)
            {
                SelectCard(posibleCard);
            }
            Drag(gesture.FocusX, gesture.FocusY);
        }
        else if (gesture.State == GestureRecognizerState.Ended)
        {
            selectedCard.Deploy();
        }
    }

    private void CreateGetInfoGesture()
    {
        getInfoGesture = new LongPressGestureRecognizer();
        getInfoGesture.StateUpdated += GetInfoCallBack;
    }
    private void GetInfoCallBack(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Began)
        {
            if (!OnCard(gesture.FocusX, gesture.FocusY))
            {
                gesture.Reset();
            }
        }
        else if (gesture.State == GestureRecognizerState.Executing)
        {
            //showInfo
        }
        else if (gesture.State == GestureRecognizerState.Ended)
        {
            //select
        }
    }

    private void CreateDragAndDropGesture()
    {
        GameObject[] cardsAsGameObjects = new GameObject[CARD_AMOUNT];
        for (int i = 0; i < CARD_AMOUNT; i++)
        {
            cardsAsGameObjects[i] = deployCards[i].gameObject;
        }
        dragAndDropCardGesture = new DragAndDropGesture(cardsAsGameObjects);
        dragAndDropCardGesture.AllowSimultaneousExecutionWithAllGestures();
        dragAndDropCardGesture.StateUpdated += DragAndDropCallback;
    }
    private void DragAndDropCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            if (selectedCard != null)
            {
                Drag(gesture.FocusX, gesture.FocusY);
            }
            
        }
        else if (gesture.State == GestureRecognizerState.Ended)
        {
            if (selectedCard != null)
            {
                selectedCard.Deploy();
                DeselectCard();
            }
            else
            {
                Debug.Log("cant put a card if there is no selected..... dummy");
            }
        }
    }

    private void ActivateGestures()
    {
        FingersScript.Instance.AddGesture(tapSelectCardGesture);
        FingersScript.Instance.AddGesture(dragSelectGesture);
        FingersScript.Instance.AddGesture(getInfoGesture);
        FingersScript.Instance.AddGesture(dragAndDropCardGesture);
    }
    private void DeactivateGestures()
    {
        FingersScript.Instance.RemoveGesture(tapSelectCardGesture);
        FingersScript.Instance.RemoveGesture(dragSelectGesture);
        FingersScript.Instance.RemoveGesture(getInfoGesture);
        FingersScript.Instance.RemoveGesture(dragAndDropCardGesture);
    }
    public void DeactivatePanel()
    {
        DeactivateGestures();
        selectedCard = null;
    }
    public void InitializePanel()
    {
        ActivateGestures();
    }

    private bool OnCard(float x, float y)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = new Vector2(x, y);
        rayCastResults.Clear();
        EventSystem.current.RaycastAll(pointerEventData, rayCastResults);

        foreach (RaycastResult result in rayCastResults)
        {
            if (result.gameObject.GetComponent<DeployCard>() != null)
            {
                posibleCard = result.gameObject.GetComponent<DeployCard>();
                return true;
            }
        }
        return false;
    }
    private void Drag(float x, float y)
    {
        float viewPortY = CameraUtility.Instance.MainCamera.ScreenToViewportPoint(new Vector2(0, y)).y;
        float panelY = InputUtilities.GetTopCoordinateOfRectTransformInViewPortSpace(mainPanel, canvasOfPanel);
        if (viewPortY <= panelY)
        {
            DragOnPanel(x, y);
        }
        else
        {
            DragOnStage(x, y);
        }
        
    }
    private void DragOnStage(float x, float y)
    {
        selectedCard.DisplayEntity(new Vector2(x, y));

    }
    private void DragOnPanel(float x, float y)
    {
        selectedCard.MoveCardPrefab(new Vector2(x, y));
    }

    private void SelectCard(DeployCard card)
    {
        Debug.Log($"new card selected!!!  {card.ToString()}");
        selectedCard = posibleCard;
    }
    private void DeselectCard()
    {
        selectedCard = null;
    }
}
