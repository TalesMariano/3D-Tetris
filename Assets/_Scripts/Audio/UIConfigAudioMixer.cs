//https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIConfigAudioMixer : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider sliderMaster;
    public Slider sliderBGM;
    public Slider sliderSFX;

    private void OnEnable()
    {
        UpdateSlider();
    }


    public void SetVolumeMaster(float value)
    {
        audioMixer.SetFloat("Master Volume", Mathf.Log10(value) * 20);
    }

    public void SetVolumeBGM(float value)
    {
        audioMixer.SetFloat("BGM Volume", Mathf.Log10(value) * 20);
    }

    public void SetVolumeSFX(float value)
    {
        audioMixer.SetFloat("SFX Volume", Mathf.Log10(value) * 20);
    }

    [ContextMenu("UpdateSlider")]
    void UpdateSlider()
    {
        float vMaster;
        audioMixer.GetFloat("Master Volume", out vMaster);
        sliderMaster.value = Mathf.Pow(10,vMaster/20);

        float vSFX;
        audioMixer.GetFloat("SFX Volume", out vSFX);
        sliderSFX.value = Mathf.Pow(10, vSFX / 20);

        float vBGM;
        audioMixer.GetFloat("BGM Volume", out vBGM);
        sliderBGM.value = Mathf.Pow(10, vBGM / 20);
    }
}
