
using UnityEngine;

namespace IcwCube
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
