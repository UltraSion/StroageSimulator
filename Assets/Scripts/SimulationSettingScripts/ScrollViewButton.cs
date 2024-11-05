using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SimulationSettingScripts
{
public class ScrollViewButton : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI bpName;

    public void SetName(string blueprintName) 
        => this.bpName.text = blueprintName;

    public void AddEvent(UnityAction e) 
        => button.onClick.AddListener(e);
}
}