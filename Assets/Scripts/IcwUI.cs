using UnityEngine;

namespace IcwCube
{
    public class IcwUI :MonoBehaviour
    {
        public GameObject settingsWindowPrefab;
        GameObject buttonObject;
        private void Awake()
        {
            buttonObject = transform.Find("Button").gameObject;
            if (buttonObject == null) { Destroy(this.gameObject); return; }
        }

        public void Update()
        {
            if (IcwCubeGenerator.Instance.generatorEnabled != buttonObject.activeSelf)
                buttonObject.SetActive(IcwCubeGenerator.Instance.generatorEnabled);
        }

        public void OnClick()
        {
            IcwCubeGenerator.Instance.generatorEnabled = false;
            Instantiate(settingsWindowPrefab);
        }
    }
}
