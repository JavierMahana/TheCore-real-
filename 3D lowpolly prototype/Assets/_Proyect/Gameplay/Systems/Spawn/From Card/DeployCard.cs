using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCard : MonoBehaviour
{
    private const float DISTANCE_FROM_CAMERA = 4f;
    private const float SELECTED_OFFSET = 1f;

    private int Cost;
    private bool displayingEntity;

    private Vector2 defaultPosition;
    private Vector2 selectedDefaultPosition;

    [SerializeField]
    private GameObject entityPrefab;
    [SerializeField]
    private GameObject cardPrefab;


    public bool debugBool;


    private void Awake()
    {
        SetDefaultPositions();

        if (debugBool)
        {
            return;
        }
        cardPrefab.SetActive(true);
        entityPrefab.SetActive(false);
    }
    private void ReturnToDefaulPosition(bool selected)
    {
        if (selected)
        {
            transform.position = selectedDefaultPosition;
        }
        else
        {
            transform.position = defaultPosition;
        }
    }
    private void SetDefaultPositions()
    {
        defaultPosition = transform.position;
        selectedDefaultPosition = new Vector2(defaultPosition.x, defaultPosition.y + SELECTED_OFFSET);
    }

    public void DisplayEntity(Vector2 screenPos)
    {
        if (debugBool)
        {
            Debug.Log($"entity al {screenPos.ToString()}");
            return;
        }
        cardPrefab.SetActive(false);
        entityPrefab.SetActive(true);

        entityPrefab.transform.position = CameraUtility.Instance.MainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, CameraUtility.Instance.MainCamera.nearClipPlane + DISTANCE_FROM_CAMERA));

        displayingEntity = true;
    }
    public void MoveCardPrefab(Vector2 screenPos)
    {
        if (debugBool)
        {
            Debug.Log($"screen card al {screenPos.ToString()}");
            return;
        }
        cardPrefab.SetActive(false);
        entityPrefab.SetActive(true);

        cardPrefab.transform.position = screenPos;
        displayingEntity = false;
    }
    public void ReturnToDefaultPosition()
    {
        cardPrefab.SetActive(true);
        entityPrefab.SetActive(false);

        cardPrefab.transform.position = defaultPosition;
        displayingEntity = false;
    }


    public void Deploy()
    {
        //call the factory to produce a entity of the display prefab type.
        Debug.Log($"Deploy {this.name}");
    }
    private void OnSelect()
    {
    }
    private void OnDeselect()
    {
    }

    private void PrepareEntityForDrag()
    {
        cardPrefab.SetActive(false);
        entityPrefab.SetActive(true);
    }
}
