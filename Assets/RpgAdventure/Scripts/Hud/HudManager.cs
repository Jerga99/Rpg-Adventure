using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RpgAdventure
{
    public class HudManager : MonoBehaviour
    {
        public Slider healhSlider;

        public void SetMaxHealth(int health)
        {
            healhSlider.maxValue = health;
            SetHealth(health);
        }

        public void SetHealth(int health)
        {
            healhSlider.value = health;
        }
    }
}