using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;
    
    public void UpdateBar(int health)
    {
        fillImage.fillAmount = health / 100f;
    }
}
