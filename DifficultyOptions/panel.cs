using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.Collections.Generic;

namespace DifficultyOptions
{
    public class modPanel : UIPanel
    {
        public bool _panelOpen = false;
        public UILabel _label;
        public List<modSlider> _sliders = new List<modSlider>();
        public List<UIButton> _buttons = new List<UIButton>();

        public void setPanel(string Title)
        {
            var uiView = UIView.GetAView();
            this.name = Title;

            this.backgroundSprite = "MenuPanel";
            this.transformPosition = new Vector3(-uiView.GetBounds().max.x + 0.3f, uiView.GetBounds().max.y - 0.105f);
            this.height = 300;
            this.width = 600;
            this.Hide();
            this.opacity = 0.95f;

            // Title
            _label = (UILabel)this.AddUIComponent(typeof(UILabel));
            _label.textColor = new Color32(255, 255, 255, 255);
            _label.padding = new RectOffset(5, 5, 5, 5);
            _label.text = Title;
            _label.relativePosition = new Vector3(300 - (_label.width/2) , 10f);

            buttonSetter buttonSetter = new buttonSetter();
            var button = (UIButton)this.AddUIComponent(typeof(UIButton));
            buttonSetter.configureButton(button, "Prices");
            button.relativePosition = new Vector3(10f, 50f);

            /*var button2 = (UIButton)this.AddUIComponent(typeof(UIButton));
            buttonSetter.configureButton(button2, "Second Button");

            var button3 = (UIButton)this.AddUIComponent(typeof(UIButton));
            buttonSetter.configureButton(button3, "Third Button");*/

            // maintenance slider
            var maintenanceSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            maintenanceSlider.relativePosition = new Vector3(220f, 100f);
            maintenanceSlider.setSlider(this, "Maintenance cost %:", DifficultySettings._maintenanceCostMod, 0, 500);
            _sliders.Add(maintenanceSlider);

            // construction slider
            var constructionSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            constructionSlider.relativePosition = new Vector3(220f, 135f);
            constructionSlider.setSlider(this, "Construction cost %:", DifficultySettings._constructionCostMod, 0, 500);
            _sliders.Add(constructionSlider);

            // refund slider
            var refundSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            refundSlider.relativePosition = new Vector3(220f, 170f);
            refundSlider.setSlider(this, "Refund return %:", DifficultySettings._refundCostMod, 0, 100);
            _sliders.Add(refundSlider);

            // relocation slider
            var relocationSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            relocationSlider.relativePosition = new Vector3(220f, 205f);
            relocationSlider.setSlider(this, "Relocation cost %:", DifficultySettings._relocationCostMod, 0, 100);
            _sliders.Add(relocationSlider);
        }

        public void togglePanel(UIComponent component, UIMouseEventParameter eventParam)
        {
            if (_panelOpen)
            {
                _panelOpen = false;
                this.Hide();
            }
            else
            {
                _panelOpen = true;
                this.Show();
            }

        }

        public override void OnDestroy()
        {
            Object.Destroy(_label);
            _sliders.ForEach(delegate(modSlider slider)
            {
                Object.Destroy(slider);
            });
            _buttons.ForEach(delegate(UIButton button)
            {
                Object.Destroy(button);
            });
        }
    }
}
