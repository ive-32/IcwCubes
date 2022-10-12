using IcwCubes;
using UnityEngine;

namespace IcwCube
{
    public class IcwBaseCubeClass : MonoBehaviour
    {
        private Rigidbody rb;
        private float cubeSpeed;
        private float cubeDistance;
        private float timeToLive;
        private Vector3 previouspos;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null) { Destroy(this.gameObject); return; }
            SetSpeedAndDistance();

            // куб падает с ускорением             
            // чтобы отлетел на N единиц в сторону нужно поднять его 
            // t*t / 2 + 0.5 - толщина самого куба
            float fallTime = cubeDistance / cubeSpeed;
            transform.position = new Vector3(0, -4, -(fallTime * fallTime ) / 2.0f - 0.5f);
            previouspos = transform.position;
            transform.rotation = Random.rotation;

            Vector3 vel3d;
            do
            {
                //vel = Random.insideUnitCircle.normalized * cubeSpeed;
                vel3d = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(0.7f, 1.0f), 0).normalized;
                //vel3d = new Vector3(0, 1, 0) * cubeSpeed; // для теста бросал снизу вверх
            }
            while (Mathf.Abs(vel3d.x * cubeDistance) > 1.5f);
            vel3d = vel3d * cubeSpeed;
            Vector3 applyPoint = Vector3.Reflect(new Vector3(vel3d.x, vel3d.y, 0.2f), Vector3.back);
            rb.AddForceAtPosition(vel3d, applyPoint, ForceMode.VelocityChange);
            timeToLive = 0; 
        }

        public void SetSpeedAndDistance()
        {
            cubeSpeed = IcwCubeGenerator.Instance.cubeSpeed;
            cubeDistance = IcwCubeGenerator.Instance.cubeDistance;
        }

        // Update is called once per frame
        void Update()
        {
            if (rb.angularVelocity.magnitude / Mathf.PI * 180 < 3.0f)
            //if ((transform.position -  previouspos).magnitude < 0.001f) // куб остановился
            {
                timeToLive += Time.deltaTime;
            }
            else timeToLive = 0;
            if (timeToLive>0.3f)
            {
                // Destroy cube
                
                //IcwSplashText sp = IcwCubeGenerator.Instance.gameObject.GetComponent<IcwSplashText>();
                //if (sp!= null)
                {
                    int result = 1;
                    float forwardz = Mathf.Abs(this.transform.forward.z);
                    float rightz = Mathf.Abs(this.transform.right.z);
                    float upz = Mathf.Abs(this.transform.up.z);
                    if (forwardz > rightz && forwardz > upz) // 1 or 6
                        if (this.transform.forward.z > 0) result = 6; else result = 1;
                    if (rightz > forwardz && rightz > upz) // 2 or 5
                        if (this.transform.right.z > 0) result = 5; else result = 2;
                    if (upz > rightz && upz > forwardz) // 3 or 4
                        if (this.transform.up.z > 0) result = 3; else result = 4;
                    string[] names = new string[] { "One", "Two", "Three", "Four", "Five", "Six" };
                    IcwSplashText.instance.SplashText(this.transform.position, result.ToString(), names[result-1]);
                }

                //Debug.LogWarning(this.transform.forward.ToString() + " - " + this.transform.right.ToString() + " - " + this.transform.up.ToString());
                Destroy(this.gameObject);
            }
            previouspos = transform.position; 
        }


    }
}