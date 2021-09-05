using Aqua.SEUIF97;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

namespace Aqua.Models
{
    public class Water : ObservableObject
    {
        // 水的属性数量（不包含自定义属性）
        private const int n = 30;

        public Water()
        {
            _propDic = new Dictionary<string, string>();
            for (int i = 0; i < n + 1; i++)
            {
                string key = Enum.GetName(typeof(AquaProps), i);
                string value = "";
                if (_propDic != null && !_propDic.ContainsKey(key))
                    _propDic.Add(key, value);
            }

        }
        #region 属性


        private Dictionary<string, string> _propDic;

        public Dictionary<string, string> PropDic
        {
            get { return _propDic; }
            set
            {
                _propDic = value;
                this.RaisePropertyChanged("PropDic");
            }
        }


        #endregion

        #region 方法
        public void CalcProps(PropInfo inputProp1, PropInfo inputProp2)
        {
            double dbl1;
            double dbl2;

            if (string.IsNullOrEmpty(inputProp1.Value) || string.IsNullOrEmpty(inputProp2.Value))
            {
                for (int i = 0; i < n+1; i++)
                {
                    string key = Enum.GetName(typeof(AquaProps), i);
                    this.PropDic[key] = "";
                }
                this.RaisePropertyChanged("PropDic");
            }
            else if (double.TryParse(inputProp1.Value, out dbl1) && double.TryParse(inputProp2.Value, out dbl2))
            {
                for (int i = 0; i < n; i++)
                {
                    string key = Enum.GetName(typeof(AquaProps), i);
                    string strvalue;
                    double dblValue;
                    if (inputProp1.NameEN == "Pressure")
                    {
                        if (inputProp2.NameEN == "VaporFraction")
                            dblValue = IF97.PX(dbl1, dbl2, i);
                        else if (inputProp2.NameEN == "Temperature")
                            dblValue = IF97.PT(dbl1, dbl2, i);
                        else if (inputProp2.NameEN == "SpecificVolume")
                            dblValue = IF97.PV(dbl1, dbl2, i);
                        else if (inputProp2.NameEN == "Enthalpy")
                            dblValue = IF97.PH(dbl1, dbl2, i);
                        else if (inputProp2.NameEN == "Entropy")
                            dblValue = IF97.PS(dbl1, dbl2, i);
                        else
                            throw new FormatException();
                    }
                    else if (inputProp1.NameEN == "Temperature")
                    {
                        if (inputProp2.NameEN == "VaporFraction")
                            dblValue = IF97.TX(dbl1, dbl2, i);
                        else if (inputProp2.NameEN == "SpecificVolume")
                            dblValue = IF97.TV(dbl1, dbl2, i);
                        else if (inputProp2.NameEN == "Enthalpy")
                            dblValue = IF97.TH(dbl1, dbl2, i);
                        else if (inputProp2.NameEN == "Entropy")
                            dblValue = IF97.TS(dbl1, dbl2, i);
                        else
                            throw new FormatException();
                    }
                    else if (inputProp1.NameEN == "Enthalpy")
                    {
                        if (inputProp2.NameEN == "Entropy")
                            dblValue = IF97.HS(dbl1, dbl2, i);
                        else
                            throw new FormatException();
                    }
                    else
                        throw new FormatException();

                    if (dblValue == -1)
                        strvalue = "N/A";
                    // 动力粘度的单位换算为cP
                    else if (i == 24)
                    {
                        strvalue = (dblValue * 1000).ToString();
                    }
                    // 运动粘度的单位换算为cSt
                    else if (i == 25)
                    {
                        strvalue = (dblValue * 1000000).ToString();
                    }
                    else
                        strvalue = dblValue.ToString();

                    this.PropDic[key] = strvalue;
                }

                // 计算绝热指数
                double cp;
                double cv;
                if (double.TryParse(this.PropDic["IsobaricHeatCapacity"], out cp) && double.TryParse(this.PropDic["IsochoricHeatCapacity"], out cv))
                {
                    this.PropDic["AdiabaticExponent"] = (cp / cv).ToString();
                }
                else
                {
                    this.PropDic["AdiabaticExponent"] = "N/A";
                }

                this.RaisePropertyChanged("PropDic");
            }
        }


        //public void PT(double p, double t)
        //{
        //    for (int i = 0; i < n; i++)
        //    {
        //        string key = Enum.GetName(typeof(AquaProps), i);
        //        string value;
        //        double dbl = IF97.PT(p, t, i);
        //        if (dbl == -1)
        //            value = "N/A";
        //        else
        //            value = dbl.ToString();

        //        this.PropDic[key] = value;
        //    }
        //    this.RaisePropertyChanged("PropDic");
        //}

        #endregion

        enum AquaProps
        {
            Pressure = 0,
            Temperature = 1,
            Density = 2,
            SpecificVolume = 3,
            Enthalpy = 4,
            Entropy = 5,
            Exergy = 6,
            InternalEnergy = 7,
            IsobaricHeatCapacity = 8,
            IsochoricHeatCapacity = 9,
            Sound = 10,
            IsentropicExponent = 11,
            Helmholtz = 12,
            Gibbs = 13,
            CompressibilityFactor = 14,
            VaporFraction = 15,
            Region = 16,
            ExpansionCoefficient = 17,
            IsothermalCompressibility = 18,
            dVdT = 19,
            dVdP = 20,
            dPdT = 21,
            IsothermalJ_TCoefficient = 22,
            J_TCoefficient = 23,
            DynamicViscosity = 24,
            KinematicViscosity = 25,
            ThermalConductivity = 26,
            ThermalDiffusivity = 27,
            Prandtl = 28,
            SurfaceTension = 29,
            AdiabaticExponent = 30,
        }
    }
}
