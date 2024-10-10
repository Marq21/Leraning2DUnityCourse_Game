using UnityEngine;
using UnityEngine.UI;

public class EnergySlider : MonoBehaviour

{
    [SerializeField] Slider slider;
    [SerializeField] ShieldHandler shieldHandler;
    private float energyState;

    private void Awake()
    {
        energyState = (float)shieldHandler.Energy;
    }

    void Start()
    {

    }

    void Update()
    {
        if (energyState != (float)shieldHandler.Energy)
        {
            slider.value = (float)shieldHandler.Energy;
            energyState = (float)shieldHandler.Energy;
        }
        else if (shieldHandler.Energy <= 0)
        {
            slider.GetComponent<RectTransform>().gameObject.SetActive(false);
            slider.value = (float)shieldHandler.Energy;
        }

        // Case for looting energy on lvl. energyState in that case will be lower than shieldHandler.Energy anywhere
        if (energyState < (float)shieldHandler.Energy)
        {
            slider.GetComponent<RectTransform>().gameObject.SetActive(true);
            slider.value = (float)shieldHandler.Energy;
        }
    }
}
