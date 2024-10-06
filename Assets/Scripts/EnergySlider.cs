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

    // Update is called once per frame
    void Update()
    {
        if (energyState != (float)shieldHandler.Energy)
        {
            slider.value = (float)shieldHandler.Energy;
            energyState = (float)shieldHandler.Energy;
        }
        else if (shieldHandler.Energy <= 0)
            slider.GetComponent<RectTransform>().gameObject.SetActive(false);
    }
}
