using UnityEngine;

namespace IcwCube
{

    public class IcwCubeGenerator : MonoBehaviour
    {
        private static IcwCubeGenerator instance;
        public static IcwCubeGenerator Instance { get { return instance; } } 

        public GameObject cubeprefab;
        [System.NonSerialized] public float cubeSpeed;
        [System.NonSerialized] public float cubeDistance;
        [System.NonSerialized] public float timeToNextCube;
        [System.NonSerialized] public float currentTime;
        [System.NonSerialized] public bool generatorEnabled;
        
        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(this.gameObject); return;  }
            instance = this;
            cubeSpeed = 2.0f;
            cubeDistance = 3.0f;
            timeToNextCube = 7.0f;
            currentTime = timeToNextCube;
            generatorEnabled = false;
        }

        void Update()
        {
            if (!generatorEnabled) return;
            currentTime += Time.deltaTime;
            if (currentTime >= timeToNextCube)
            {
                currentTime = 0.0f;
                if (cubeprefab != null)
                    Instantiate(cubeprefab, this.transform);
            }
            if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
                Application.Quit();
        }
    }
}