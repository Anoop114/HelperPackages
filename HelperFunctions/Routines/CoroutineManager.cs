using System.Collections;
using UnityEngine;

namespace HelperFunction.Routines
{
    public class CoroutineManager : MonoBehaviour
    {
        public static CoroutineManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        /// <summary>
        /// start coroutine by pass the IEnumerator  
        /// </summary>
        /// <param name="routineFun"> function that is going to run. </param>
        public void StartRoutine(IEnumerator routineFun) => StartCoroutine(routineFun);

        /// <summary>
        /// start coroutine by pass the IEnumerator
        /// </summary>
        /// <param name="routineFun"> the function that need to stop. </param>
        public void StopRoutine(IEnumerator routineFun) => StopCoroutine(routineFun);

    }
}