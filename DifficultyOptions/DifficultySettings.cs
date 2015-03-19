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

        public static VariableReference<int> _residentialPercentage = new VariableReference<int>(100);
        public static VariableReference<int> _commercialPercentage = new VariableReference<int>(100);
        public static VariableReference<int> _industrialPercentage = new VariableReference<int>(100);

        public static VariableReference<int> _residentialFlat = new VariableReference<int>(0);
        public static VariableReference<int> _commercialFlat = new VariableReference<int>(0);
        public static VariableReference<int> _industrialFlat = new VariableReference<int>(0);

        public int getMaintenanceCost() { return _maintenanceCostMod.Value; }
        public int getConstructionCost() { return _constructionCostMod.Value; }
        public int getRefundCost() { return _refundCostMod.Value; }
        public int getRelocationCost() { return _relocationCostMod.Value; }

        public int getResidentialPercentage() { return _residentialPercentage.Value; }
        public int getCommercialPercentage() { return _commercialPercentage.Value; }
        public int getIndustrialPercentage() { return _industrialPercentage.Value; }

        public int getResidentialFlat() { return _residentialFlat.Value; }
        public int getCommercialFlat() { return _commercialFlat.Value; }
        public int getIndustrialFlat() { return _industrialFlat.Value; }

        public void setDefaultValues()
        {
            _maintenanceCostMod.Value = 100;
            _constructionCostMod.Value = 100;
            _refundCostMod.Value = 75;
            _relocationCostMod.Value = 20;
            setDefaultValuesV2();
        }

        public void setDefaultValuesV2()
        {
            _residentialPercentage.Value = 100;
            _commercialPercentage.Value = 100;
            _industrialPercentage.Value = 100;
            _residentialFlat.Value = 0;
            _commercialFlat.Value = 0;
            _industrialFlat.Value = 0;
        }

        public byte[] serialize()
        {
            IEnumerable<byte> serializeVersion = BitConverter.GetBytes(2);
            IEnumerable<byte> maintenanceCostData = BitConverter.GetBytes(_maintenanceCostMod.Value);
            IEnumerable<byte> constructionCostData = BitConverter.GetBytes(_constructionCostMod.Value);
            IEnumerable<byte> refundCostData = BitConverter.GetBytes(_refundCostMod.Value);
            IEnumerable<byte> relocationCostData = BitConverter.GetBytes(_relocationCostMod.Value);

            IEnumerable<byte> residentialPercentage = BitConverter.GetBytes(_residentialPercentage.Value);
            IEnumerable<byte> commercialPercentage = BitConverter.GetBytes(_commercialPercentage.Value);
            IEnumerable<byte> industrialPercentage = BitConverter.GetBytes(_industrialPercentage.Value);

            IEnumerable<byte> residentialFlat = BitConverter.GetBytes(_residentialFlat.Value);
            IEnumerable<byte> commercialFlat = BitConverter.GetBytes(_commercialFlat.Value);
            IEnumerable<byte> industrialFlat = BitConverter.GetBytes(_industrialFlat.Value);

            IEnumerable<byte> serialized = serializeVersion.Concat(maintenanceCostData).Concat(constructionCostData).Concat(refundCostData).Concat(relocationCostData).Concat(residentialPercentage).Concat(commercialPercentage)
                .Concat(industrialPercentage).Concat(residentialFlat).Concat(commercialFlat).Concat(industrialFlat);
            return serialized.ToArray<byte>();
        }

        public void deserializeV1(byte[] data)
        {
            _maintenanceCostMod.Value = BitConverter.ToInt32(data, 4);
            _constructionCostMod.Value = BitConverter.ToInt32(data, 8);
            _refundCostMod.Value = BitConverter.ToInt32(data, 12);
            _relocationCostMod.Value = BitConverter.ToInt32(data, 16);
        }

        public void deserializeV2(byte[] data)
        {
            _residentialPercentage.Value = BitConverter.ToInt32(data, 20);
            _commercialPercentage.Value = BitConverter.ToInt32(data, 24);
            _industrialPercentage.Value = BitConverter.ToInt32(data, 28);
            _residentialFlat.Value = BitConverter.ToInt32(data, 32);
            _commercialFlat.Value = BitConverter.ToInt32(data, 36);
            _industrialFlat.Value = BitConverter.ToInt32(data, 40);
        }

        public void deserialize(byte[] data)
        {
            int serializeVersion = BitConverter.ToInt32(data, 0);
            switch (serializeVersion)
            {
                case 1:
                    {
                        deserializeV1(data);
                        setDefaultValuesV2();
                        break;
                    }
                case 2:
                    {
                        deserializeV1(data);
                        deserializeV2(data);
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
        }

        public override void OnLoadData()
        {
            byte[] data = serializableDataManager.LoadData("ZarineDifficultyMod");
            if (data == null) {
                setDefaultValues();
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "no data found for Difficulty Mod setting to default values");
                return;
            }

            deserialize(data);
        }
    }


}
