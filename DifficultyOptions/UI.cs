﻿using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace DifficultyOptions
{
    public class DifficultyOptionsMod : IUserMod
    {
        public string Name { get { return "Difficulty Options"; } }
        public string Description { get { return "Set your own difficulty from easier to real hardcore"; } }

        public static DifficultySettings DIFFICULTY_SETTINGS = new DifficultySettings();
    }

    public class LoadingExtension : LoadingExtensionBase
    {
        public UIButton _button;
        public modPanel _panel;

        public override void OnLevelLoaded(LoadMode mode)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "Zarine Difficulty Options Mod - Loading start");
            // Add a new button to the view.
            var uiView = UIView.GetAView();
            _button = (UIButton)uiView.AddUIComponent(typeof(UIButton));
            buttonSetter buttonSetter = new buttonSetter();
            buttonSetter.configureButton(_button, "Difficulty");

            _panel = (modPanel)uiView.AddUIComponent(typeof(modPanel));
            _panel.setPanel("Difficulty Settings");

            // Place the button.
            _button.transformPosition = new Vector3(uiView.GetBounds().max.x - 0.3f, 0.105f - uiView.GetBounds().max.y);

            // Respond to button click.
            _button.eventClick += _panel.togglePanel;

            /*var UIPanels = uiView.GetComponentsInChildren<UIPanel>();
            for (var i = 0; i < UIPanels.Length; i++ )
            {
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "UIPanels: " + UIPanels[i].name);       
            }*/

            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "Zarine Difficulty Options Mod - Loading end");
        }

        public override void OnLevelUnloading()
        {
            Object.Destroy(_panel);
            Object.Destroy(_button);
        }
    }
}