using System;
using System.Collections.Generic;
using System.Linq;
using ICities;
using UnityEngine;

namespace DifficultyOptions
{

    public class DifficultySettings : SerializableDataExtensionBase
    {
        public static VariableReference<int> _maintenanceCostMod = new VariableReference<int>(100);
        public static VariableReference<int> _constructionCostMod = new VariableReference<int>(100);
        public static VariableReference<int> _refundCostMod = new VariableReference<int>(75);
        public static VariableReference<int> _relocationCostMod = new VariableReference<int>(20);

        public void setMaintenanceCost(VariableReference<int> reference) { _maintenanceCostMod = reference; }
        public int getMaintenanceCost() { return _maintenanceCostMod.Value; }

        public void setConstructionCost(VariableReference<int> reference) { _constructionCostMod = reference; }
        public int getConstructionCost() { return _constructionCostMod.Value; }

        public void setRefundCost(VariableReference<int> reference) { _refundCostMod = reference; }
        public int getRefundCost() { return _refundCostMod.Value; }

        public void setRelocationCost(VariableReference<int> reference) { _relocationCostMod = reference; }
        public int getRelocationCost() { return _relocationCostMod.Value; }

        public void setDefaultValues()
        {
            _maintenanceCostMod.Value = 100;
            _constructionCostMod.Value = 100;
            _refundCostMod.Value = 75;
            _relocationCostMod.Value = 20;
        }

        public byte[] serialize()
        {
            IEnumerable<byte> serializeVersion = BitConverter.GetBytes(1);
            IEnumerable<byte> maintenanceCostData = BitConverter.GetBytes(_maintenanceCostMod.Value);
            IEnumerable<byte> constructionCostData = BitConverter.GetBytes(_constructionCostMod.Value);
            IEnumerable<byte> refundCostData = BitConverter.GetBytes(_refundCostMod.Value);
            IEnumerable<byte> relocationCostData = BitConverter.GetBytes(_relocationCostMod.Value);

            IEnumerable<byte> serialized = serializeVersion.Concat(maintenanceCostData).Concat(constructionCostData).Concat(refundCostData).Concat(relocationCostData);
            return serialized.ToArray<byte>();
        }

        public void deserialize(byte[] data)
        {
            int serializeVersion = BitConverter.ToInt32(data, 0);
            switch (serializeVersion)
            {
                case 1:
                    {
                        _maintenanceCostMod.Value = BitConverter.ToInt32(data, 4);
                        _constructionCostMod.Value = BitConverter.ToInt32(data, 8);
                        _refundCostMod.Value = BitConverter.ToInt32(data, 12);
                        _relocationCostMod.Value = BitConverter.ToInt32(data, 16);
                        break;
                    }
                default:
                    {
                        setDefaultValues();
                        break;
                    }
            }
        }

        public override void OnSaveData()
        {
            serializableDataManager.SaveData("ZarineDifficultyMod", serialize());
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "saving: " + _maintenanceCostMod.Value + " - " + _constructionCostMod.Value + " - " + _refundCostMod.Value + " - " + _relocationCostMod.Value);
        }

        public override void OnLoadData()
        {
            byte[] data = serializableDataManager.LoadData("ZarineDifficultyMod");
            if (data == null) {
                setDefaultValues();
                return;
            }

            deserialize(data);
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "loading: " + _maintenanceCostMod.Value + " - " + _constructionCostMod.Value + " - " + _refundCostMod.Value + " - " + _relocationCostMod.Value);
        }
    }


}
