using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; 
    public Image fill;
    public Color normalColor = Color.yellow;
    public Color phase2Color = Color.red;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void ChangeToPhase2Color()
    {
        fill.color = phase2Color;
    }
}