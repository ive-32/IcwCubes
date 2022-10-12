using IcwCube;
using UnityEngine;

namespace IcwCubes
{
    public class IcwWelcomeForm : MonoBehaviour
    {
        public void OnButtonClick() 
        {
            Destroy(this.gameObject);
            IcwCubeGenerator.Instance.generatorEnabled = true;
        }
    }
}
