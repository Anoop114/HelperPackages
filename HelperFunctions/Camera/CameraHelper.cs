namespace HelperFunction.Camera
{
    public static class CameraHelper
    {
        private static UnityEngine.Camera _camera;

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