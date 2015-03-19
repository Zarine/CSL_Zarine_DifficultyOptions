using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.Collections.Generic;

namespace DifficultyOptions
{
    public class zarineButton : UIButton
    {
        public List<modSlider> _sliders = new List<modSlider>();

        public void configure(string Title)
        {
            // Set the text to show on the button.
            text = Title;

            // Set the button dimensions.
            width = 100;
            height = 30;

            // Style the button to look like a menu button.
            normalBgSprite = "ButtonMenu";
            disabledBgSprite = "ButtonMenuDisabled";
            hoveredBgSprite = "ButtonMenuHovered";
            focusedBgSprite = "ButtonMenuFocused";
            pressedBgSprite = "ButtonMenuPressed";
            textColor = new Color32(255, 255, 255, 255);
            disabledTextColor = new Color32(7, 7, 7, 255);
            hoveredTextColor = new Color32(7, 132, 255, 255);
            focusedTextColor = new Color32(255, 255, 255, 255);
            pressedTextColor = new Color32(30, 30, 44, 255);

            // Enable button sounds.
            playAudioEvents = true;
        }

        public void add(modSlider slider)
        {
            _sliders.Add(slider);
        }

        public void hideSliders()
        {
            _sliders.ForEach(delegate(modSlider slider)
            {
                slider.hideAll();
            });
        }

        public void showSliders()
        {
            _sliders.ForEach(delegate(modSlider slider)
            {
                slider.showAll();
            });
        }

        public override void OnDestroy()
        {
            _sliders.Clear();
            base.OnDestroy();
        }


    }
}
