namespace HelperFunction.Camera
{
    public static class CameraHelper
    {
        private static UnityEngine.Camera _camera;

        /// <summary>
        /// Return either main camera or stored camera.
        /// </summary>
        public static UnityEngine.Camera Camera
        {
            get
            {
                if (_camera == null) _camera = UnityEngine.Camera.main;
                return _camera;
            }
            set => _camera = value;
        }
        
    }
}