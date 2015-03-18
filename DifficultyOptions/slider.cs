using System;
using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace DifficultyOptions
{
    public class modSlider : UISlider
    {
        public UILabel _valueDisplayer;
        public UILabel _label;
        public VariableReference<int> _dataValue;

        public void setSlider(modPanel panel, string Title, VariableReference<int> reference, int min, int max)
        {
            _valueDisplayer = (UILabel)panel.AddUIComponent(typeof(UILabel));
            _label = (UILabel)panel.AddUIComponent(typeof(UILabel));
            _dataValue = reference;

            this.maxValue = max;
            this.minValue = min;
            this.stepSize = 1;
            this.scrollWheelAmount = 5;
            this.backgroundSprite = "ScrollbarTrack";
            this.orientation = UIOrientation.Horizontal;
            this.fillMode = UIFillMode.Fill;
            this.fillPadding = new RectOffset(2, 2, 2, 2);
            this.value = reference.Value;
            this.width = 200;
            // TODO: find how to see the slider filling

            _valueDisplayer.position = this.position + new Vector3(210, 0, 0);
            _valueDisplayer.textColor = new Color32(255, 255, 255, 255);
            _valueDisplayer.padding = new RectOffset(5, 5, 5, 5);

            _label.position = this.position + new Vector3(-210, 0, 0);
            _label.textColor = new Color32(255, 255, 255, 255);
            _label.padding = new RectOffset(5, 5, 5, 5);
            _label.text = Title;
        }

        protected override void OnValueChanged()
        {
            this._valueDisplayer.text = this.value.ToString();
            _dataValue.Value = (int)this.value;
        }

        public override void OnDestroy()
        {
            UnityEngine.Object.Destroy(_valueDisplayer);
            UnityEngine.Object.Destroy(_label);
        }
    }
}
