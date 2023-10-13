using System.Collections.Generic;
using HelperFunction.Camera;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HelperFunction.UI
{
    public static class UIHelper
    {
        private static PointerEventData _eventData;
        private static List<RaycastResult> _raycastResults;

        /// <summary>
        /// Check if the mouse is over UI element or not.
        /// </summary>
        /// <returns> true / false </returns>
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
        /// <summary>
        /// Convert Canvas transform to word coordinates.
        /// </summary>
        /// <param name="element"> the canvas element(RectTransform). </param>
        /// <returns> vector2 => word coordinate of the given element. </returns>
        public static Vector2 CanvasElement(RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, CameraHelper.Camera, out var result);
            return result;
        }
    }
}