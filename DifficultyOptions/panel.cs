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
        public List<zarineButton> _buttons = new List<zarineButton>();

        public void setPanel(string Title)
        {
            var uiView = UIView.GetAView();
            this.name = Title;

            this.backgroundSprite = "MenuPanel";
            this.transformPosition = new Vector3(-uiView.GetBounds().max.x + 0.3f, uiView.GetBounds().max.y - 0.105f);
            this.height = 400;
            this.width = 600;
            this.Hide();
            this.opacity = 0.95f;

            // Title
            _label = (UILabel)this.AddUIComponent(typeof(UILabel));
            _label.textColor = new Color32(255, 255, 255, 255);
            _label.padding = new RectOffset(5, 5, 5, 5);
            _label.text = Title;
            _label.relativePosition = new Vector3(300 - (_label.width/2) , 10f);

            // PRICES
            zarineButton priceButton = (zarineButton)this.AddUIComponent(typeof(zarineButton));
            priceButton.configure("Prices");
            priceButton.relativePosition = new Vector3(10f, 50f);
            priceButton.eventClick += toggleTab;

            // maintenance slider
            var maintenanceSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            maintenanceSlider.relativePosition = new Vector3(220f, 100f);
            maintenanceSlider.setSlider(this, "Maintenance cost %:", DifficultySettings._maintenanceCostMod, 0, 500, priceButton);
            _sliders.Add(maintenanceSlider);

            // construction slider
            var constructionSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            constructionSlider.relativePosition = new Vector3(220f, 135f);
            constructionSlider.setSlider(this, "Construction cost %:", DifficultySettings._constructionCostMod, 0, 500, priceButton);
            _sliders.Add(constructionSlider);

            // refund slider
            var refundSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            refundSlider.relativePosition = new Vector3(220f, 170f);
            refundSlider.setSlider(this, "Refund return %:", DifficultySettings._refundCostMod, 0, 100, priceButton);
            _sliders.Add(refundSlider);

            // relocation slider
            var relocationSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            relocationSlider.relativePosition = new Vector3(220f, 205f);
            relocationSlider.setSlider(this, "Relocation cost %:", DifficultySettings._relocationCostMod, 0, 100, priceButton);
            _sliders.Add(relocationSlider);

            // DEMANDS
            zarineButton demandButton = (zarineButton)this.AddUIComponent(typeof(zarineButton));
            demandButton.configure("Demands");
            demandButton.relativePosition = new Vector3(120f, 50f);
            demandButton.eventClick += toggleTab;

            // residential % slider
            var residentialPercentageSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            residentialPercentageSlider.relativePosition = new Vector3(220f, 100f);
            residentialPercentageSlider.setSlider(this, "Residential %:", DifficultySettings._residentialPercentage, 0, 300, demandButton);
            _sliders.Add(residentialPercentageSlider);

            // commercial % slider
            var commercialPercentageSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            commercialPercentageSlider.relativePosition = new Vector3(220f, 135f);
            commercialPercentageSlider.setSlider(this, "Commercial %:", DifficultySettings._commercialPercentage, 0, 300, demandButton);
            _sliders.Add(commercialPercentageSlider);

            // industrial % slider
            var industrialPercentageSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            industrialPercentageSlider.relativePosition = new Vector3(220f, 170f);
            industrialPercentageSlider.setSlider(this, "Industrial %:", DifficultySettings._industrialPercentage, 0, 300, demandButton);
            _sliders.Add(industrialPercentageSlider);

            // residential flat slider
            var residentialFlatSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            residentialFlatSlider.relativePosition = new Vector3(220f, 205f);
            residentialFlatSlider.setSlider(this, "Residential flat:", DifficultySettings._residentialFlat, -100, 100, demandButton);
            _sliders.Add(residentialFlatSlider);

            // commercial flat slider
            var commercialFlatSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            commercialFlatSlider.relativePosition = new Vector3(220f, 240f);
            commercialFlatSlider.setSlider(this, "Commercial flat:", DifficultySettings._commercialFlat, -100, 100, demandButton);
            _sliders.Add(commercialFlatSlider);

            // industrial flat slider
            var industrialFlatSlider = (modSlider)this.AddUIComponent(typeof(modSlider));
            industrialFlatSlider.relativePosition = new Vector3(220f, 275f);
            industrialFlatSlider.setSlider(this, "Industrial flat:", DifficultySettings._industrialFlat, -100, 100, demandButton);
            _sliders.Add(industrialFlatSlider);

            demandButton.hideSliders();
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

        public void toggleTab(UIComponent component, UIMouseEventParameter eventParam)
        {
            // Hide all
            _sliders.ForEach(delegate(modSlider slider)
            {
                slider.hideAll();
            });

            // Show only required
            ((zarineButton)component).showSliders();
        }

        public override void OnDestroy()
        {
            Object.Destroy(_label);
            _sliders.ForEach(delegate(modSlider slider)
            {
                Object.Destroy(slider);
            });
            _buttons.ForEach(delegate(zarineButton button)
            {
                Object.Destroy(button);
            });
            base.OnDestroy();
        }
    }
}
