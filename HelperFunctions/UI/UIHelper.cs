using System.Collections.Generic;
using HelperFunctions.Camera;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HelperFunction.HelperFunctions.UI
{
    public static class UIHelper
    {
        private static PointerEventData _eventData;
        private static List<RaycastResult> _raycastResults;

        public static bool IsOverUI()
        {
            _eventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            _raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventData,_raycastResults);
            return _raycastResults.Count > 0;
        }
    }

    public static class WorldPointToCanvas
    {
        public static Vector2 CanvasElement(RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, CameraHelper.Camera, out var result);
            return result;
        }
    }
}