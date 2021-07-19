using System;
using System.Collections.Generic;
using System.Text;

namespace QoLApp.Models
{
    class ConversionValues
    {
        //List<string> ImperialWeights = new List<string>{ "lbs", "ounces" };
        List<string> MetricWeights = new List<string> { "kg", "hg", "dag", "g", "dg", "cg", "mg" };
        List<string> MetricLengths = new List<string> { "km", "hm", "dam", "meters", "dm", "cm", "mm" };
        List<string> MetricVolumes = new List<string> { "kl", "hl", "dal", "liters", "dl", "cl", "ml" };
        List<string> WeightTypes = new List<string> { "lbs", "ounces", "g" };
        List<string> LengthTypes = new List<string> { "inches", "feet", "miles", "meters" };
        List<string> VolumeTypes = new List<string> { "fl oz", "cups", "gallons", "liters" };

        private string _convType;
        private double _multiplier;
        private double _firstValue;
        private double _secondValue;
        private string _firstType;
        private string _secondType;
        private int _firstIndex;
        private int _secondIndex;
        private int _allIndex;

        public int AllIndex
        {
            get { return _allIndex; }
            set { _allIndex = value; }
        }

        public int SecondIndex
        {
            get { return _secondIndex; }
            set { _secondIndex = value; }
        }
        public int FirstIndex
        {
            get { return _firstIndex; }
            set { _firstIndex = value; }
        }
        public string SecondType
        {
            get { return _secondType; }
            set { _secondType = value; }
        }
        public string FirstType
        {
            get { return _firstType; }
            set { _firstType = value; }
        }
        public double SecondValue
        {
            get { return _secondValue; }
            set { _secondValue = value; }
        }
        public double FirstValue
        {
            get { return _firstValue; }
            set { _firstValue = value; }
        }
        public double Multiplier
        {
            get { return _multiplier; }
            set { _multiplier = value; }
        }
        public string ConversionType
        {
            get { return _convType; }
            set { _convType = value; }
        }


        // this class will hold conversion values
        public ConversionValues(string firstType, string secondType, string conversionType) // should be the types (like kg, inches, etc.)
        {
            // assign to properties
            FirstType = firstType;
            SecondType = secondType;
            ConversionType = conversionType;
            int i;
            List<string> MetricList;
            List<string> AllTypes;

            if (conversionType == "Weight")
            {
                MetricList = MetricWeights;
                AllTypes = WeightTypes;
                i = 2;
            }
            else if (conversionType == "Length")
            {
                MetricList = MetricLengths;
                AllTypes = LengthTypes;
                i = 3;
            }
            else
            {
                MetricList = MetricVolumes;
                AllTypes = VolumeTypes;
                i = 3;
            }

            // if the first type is metric
            if (MetricList.Contains(firstType))
            {
                FirstIndex = MetricList.IndexOf(firstType); // g is 3, kg is 0
                // assuming both types are metric (10 multiplier)
                if (MetricList.Contains(secondType))
                {
                    SecondIndex = MetricList.IndexOf(secondType); // g is 3, kg is 0
                    // going from g to g
                    if (FirstIndex == SecondIndex)
                    {
                        Multiplier = 1;
                        SetIndexes(0, 0);
                    }
                    // if going from large to small units (kg to g)
                    else if (FirstIndex < SecondIndex) // 0kg 3g
                    {
                        Multiplier = Math.Pow(10, (SecondIndex - FirstIndex));
                        SetIndexes(0, 0);
                    }
                    // if going from small to large units (g to kg)
                    else // 3g 0kg
                    {
                        Multiplier = Math.Pow(10, (SecondIndex - FirstIndex));
                        SetIndexes(0, 0);
                    }
                }
                // assuming only the first type is metric
                else
                {
                    SecondIndex = AllTypes.IndexOf(secondType);
                    Multiplier = Math.Pow(10, (3 - FirstIndex));
                    // instead of 2, this should be... 3 for the length conversions
                    SetIndexes(i, SecondIndex);
                }
            }
            // if the first type isn't metric
            else
            {
                FirstIndex = AllTypes.IndexOf(firstType);
                // but the second type is metric
                if (MetricList.Contains(secondType))
                {
                    Multiplier = Math.Pow(10, (MetricList.IndexOf(secondType) - 3));
                    SetIndexes(FirstIndex, i);
                }
                // if neither types are metric
                else
                {
                    Multiplier = 1;
                    SetIndexes(FirstIndex, AllTypes.IndexOf(secondType));
                }
            }
        }

        private void SetIndexes(int firstIndex, int secondIndex)
        {
            FirstIndex = firstIndex;
            SecondIndex = secondIndex;
        }
    }
}
