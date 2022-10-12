using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace IcwCube
{
    
    public class IcwSettingWindow : MonoBehaviour
    {
        GameObject inputSpeed;
        GameObject inputDistance;
        GameObject inputInterval;

        GameObject sliderSpeed;
        GameObject sliderDistance;
        GameObject sliderInterval;
        bool disableEvents = false;

        private void Awake()
        {
            inputSpeed = this.transform.Find("Speed").gameObject;
            inputDistance = this.transform.Find("Distance").gameObject;
            inputInterval = this.transform.Find("Interval").gameObject;

            sliderSpeed = this.transform.Find("SliderSpeed").gameObject;
            sliderDistance = this.transform.Find("SliderDistance").gameObject;
            sliderInterval = this.transform.Find("SliderInterval").gameObject;

            inputSpeed.GetComponent<TMP_InputField>().text = IcwCubeGenerator.Instance.cubeSpeed.ToString();
            inputDistance.GetComponent<TMP_InputField>().text = IcwCubeGenerator.Instance.cubeDistance.ToString();
            inputInterval.GetComponent<TMP_InputField>().text = IcwCubeGenerator.Instance.timeToNextCube.ToString();
            SetSliders();

        }
        public void SetSliders()
        {
            if (disableEvents) return;
            disableEvents = true;
            float value = 3.0f;
            float.TryParse(inputSpeed.GetComponent<TMP_InputField>().text, out value);
            sliderSpeed.GetComponent<Slider>().value = value;

            float.TryParse(inputDistance.GetComponent<TMP_InputField>().text, out value);
            sliderDistance.GetComponent<Slider>().value = value;

            float.TryParse(inputInterval.GetComponent<TMP_InputField>().text, out value);
            sliderInterval.GetComponent<Slider>().value = value;
            disableEvents = false;
        }

        public void SetInputs()
        {
            if (disableEvents) return;
            disableEvents = true;
            inputSpeed.GetComponent<TMP_InputField>().text = sliderSpeed.GetComponent<Slider>().value.ToString();
            inputDistance.GetComponent<TMP_InputField>().text = sliderDistance.GetComponent<Slider>().value.ToString();
            inputInterval.GetComponent<TMP_InputField>().text = sliderInterval.GetComponent<Slider>().value.ToString();
            disableEvents = false;
        }
        public void OnSliderChage()
        {
            SetInputs();
        }

        public void OnEndEdit(GameObject input)
        {
            float minVal = 3.0f, maxVal = 4.0f;
            if (input.name.Contains("Speed")) { minVal = 2.0f; maxVal = 5.0f; }
            if (input.name.Contains("Distance")) { minVal = 3.0f; maxVal = 4.0f; }
            if (input.name.Contains("Interval")) { minVal = 3.0f; maxVal = 5.0f; }
            float val;
            float.TryParse(input.GetComponent<TMP_InputField>().text, out val);
            if (val > maxVal || val < minVal) input.GetComponent<TMP_InputField>().text = "3";
            SetSliders();
        }

        void CloseWindow()
        {
            Destroy(this.gameObject);
            IcwCubeGenerator.Instance.generatorEnabled = true;
        }

        public void SetGeneratorValues()
        {
            float value = 3.0f;
            float.TryParse(inputSpeed.GetComponent<TMP_InputField>().text, out value);
            IcwCubeGenerator.Instance.cubeSpeed = value;

            float.TryParse(inputDistance.GetComponent<TMP_InputField>().text, out value);
            IcwCubeGenerator.Instance.cubeDistance = value;

            float.TryParse(inputInterval.GetComponent<TMP_InputField>().text, out value);
            IcwCubeGenerator.Instance.timeToNextCube = value;
        }

        public void OnOkClick()
        {
            SetGeneratorValues();
            CloseWindow();
        }

        public void OnCancelClick()
        {
            CloseWindow();
        }

    }
}
