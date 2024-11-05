using Script;
using TMPro;
using UnitCodes.LifterCodes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LifterSettingScripts
{
public class LifterSettingManager : MonoBehaviour
{
    public TMP_InputField Accel;
    public TMP_InputField BrakePower;
    public TMP_InputField MaxSpeed;
    public TMP_InputField RotateSpeed;
    public TMP_InputField PackageDropSpeed;

    public TextMeshProUGUI errorWindow;
    
    public void EndSetting()
    {
        LifterSetting lifterSetting = new LifterSetting();
        
        if (!float.TryParse(Accel.text, out lifterSetting.acceleration))
        {
            Accel.text = "Please input float value only";
            return;
        }
        if (!float.TryParse(BrakePower.text, out lifterSetting.brakePower))
        {
            BrakePower.text = "Please input float value only";
            return;
        }
        if (!float.TryParse(MaxSpeed.text, out lifterSetting.maxSpeed))
        {
            MaxSpeed.text = "Please input float value only";
            return;
        }
        if (!float.TryParse(RotateSpeed.text, out lifterSetting.RotateSpeed))
        {
            RotateSpeed.text = "Please input float value only";
            return;
        }
        if (!float.TryParse(PackageDropSpeed.text, out lifterSetting.packageDorpSpeed))
        {
            PackageDropSpeed.text = "Please input float value only";
            return;
        }
        
        Hub.LifterSettings.Add(lifterSetting);
        SceneManager.LoadScene("MainScene");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}
}