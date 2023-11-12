using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class ThermometerUI : UI
    {
        [SerializeField] private Image filledImage;
        
        public void SetFillAmount(float fillAmount)
        {
            filledImage.fillAmount = fillAmount;
        }

        public void SetFillAmountToZero()
        {
            SetFillAmount(0);
        }
    }
}