using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragZone : MonoBehaviour, IDragHandler,IEndDragHandler,IBeginDragHandler
{
    [SerializeField] private Camera MainCamera;
    Vector2 FirstDrag = Vector2.zero;
    Vector3 SelectedPosition;
    float DragDistance = 0;
    public void OnBeginDrag(PointerEventData eventData)
    {
        RaycastHit hit;
        Ray ray = MainCamera.ScreenPointToRay(eventData.position);
        Vector3 tempPosition = Vector3.zero;

        if (Physics.Raycast(ray, out hit)) tempPosition = hit.point;

        SelectedPosition = tempPosition;
            
        FirstDrag = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragDistance = Vector2.Distance(eventData.position,FirstDrag);

        if(DragDistance > 20)
        {
            float direction = eventData.position.x - FirstDrag.x;

            if(direction < 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            SelectedPosition.x = 0;
            SelectedPosition.y = 0; 

            GameSystem.Instance.ChangeMoverDirection(SelectedPosition,(int)direction);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
