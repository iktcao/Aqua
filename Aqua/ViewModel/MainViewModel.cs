using Aqua.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Aqua.ViewModel
{
    public class MainViewModel : ViewModelBase, IDataErrorInfo
    {
        private ObservableCollection<PropInfo> _inputPropList;

        #region 构造函数        
        public MainViewModel()
        {
            myWater = new Water();
            InitialVM();
        }

        #endregion

        #region 属性
        private Water myWater;
        public Water MyWater
        {
            get { return myWater; }
            set
            {
                myWater = value;
                this.RaisePropertyChanged("MyWater");
            }
        }

        // 第一个输入参数信息
        private PropInfo _inputProp1;
        public PropInfo InputProp1
        {
            get { return _inputProp1; }
            set
            {
                _inputProp1 = value;
                this.RaisePropertyChanged("InputProp1");
            }
        }

        // 第二个输入参数信息
        private PropInfo _inputProp2;
        public PropInfo InputProp2
        {
            get { return _inputProp2; }
            set
            {
                _inputProp2 = value;
                this.RaisePropertyChanged("InputProp2");
            }
        }

        // 第一个输入参数值
        public string InputValue1
        {
            get { return InputProp1.Value; }
            set
            {
                InputProp1.Value = value;
                this.Calc();
                this.RaisePropertyChanged("InputValue1");
            }
        }

        // 第二个输入参数值
        public string InputValue2
        {
            get { return InputProp2.Value; }
            set
            {
                InputProp2.Value = value;
                this.Calc();
                this.RaisePropertyChanged("InputValue2");
            }
        }

        // 第一组输入参数列表
        private ObservableCollection<PropInfo> _inputPropList1;
        public ObservableCollection<PropInfo> InputPropList1
        {
            get { return _inputPropList1; }
            set
            {
                _inputPropList1 = value;
                this.RaisePropertyChanged("InputPropList1");
            }
        }

        // 第二组输入参数列表
        private ObservableCollection<PropInfo> _inputPropList2;
        public ObservableCollection<PropInfo> InputPropList2
        {
            get { return _inputPropList2; }
            set
            {
                _inputPropList1 = value;
                this.RaisePropertyChanged("InputPropList2");
            }
        }
        #endregion

        #region 命令
        // 响应输入第一组输入参数列表的切换
        private RelayCommand _input1SelectCommand;
        public RelayCommand Input1SelectCommand
        {
            get
            {
                if (_input1SelectCommand == null)
                    _input1SelectCommand = new RelayCommand(() => ExcuteSelectCommand());
                return _input1SelectCommand;
            }
            set { _input1SelectCommand = value; }
        }

        private void ExcuteSelectCommand()
        {
            if (!string.IsNullOrEmpty(InputProp1.NameEN))
            {
                if (InputProp1.NameEN == "Pressure")
                {
                    InputPropList2.Clear();
                    InputPropList2.Add(_inputPropList[1]);
                    InputPropList2.Add(_inputPropList[2]);
                    InputPropList2.Add(_inputPropList[3]);
                    InputPropList2.Add(_inputPropList[4]);
                    InputPropList2.Add(_inputPropList[5]);
                    InputProp2 = InputPropList2[0];
                    InputValue1 = "";
                    InputValue2 = "";
                }

                else if (InputProp1.NameEN == "Temperature")
                {
                    InputPropList2.Clear();
                    InputPropList2.Add(_inputPropList[2]);
                    InputPropList2.Add(_inputPropList[3]);
                    InputPropList2.Add(_inputPropList[4]);
                    InputPropList2.Add(_inputPropList[5]);
                    InputProp2 = InputPropList2[0];
                    InputValue1 = "";
                    InputValue2 = "";
                }

                else if (InputProp1.NameEN == "Enthalpy")
                {
                    InputPropList2.Clear();
                    InputPropList2.Add(_inputPropList[5]);
                    InputProp2 = InputPropList2[0];
                    InputValue1 = "";
                    InputValue2 = "";
                }
            }
        }

        // 响应输入第二组输入参数列表的切换
        private RelayCommand _input2SelectCommand;
        public RelayCommand Input2SelectCommand
        {
            get
            {
                if (_input2SelectCommand == null)
                    _input2SelectCommand = new RelayCommand(() => ExcuteInput2SelectCommand());
                return _input2SelectCommand;
            }
            set { _input2SelectCommand = value; }
        }

        private void ExcuteInput2SelectCommand()
        {
            if (InputProp2 != null)
                InputValue2 = "";
        }
        #endregion

        #region 方法
        // 初始化ViewModel的方法
        private void InitialVM()
        {
            _inputPropList = new ObservableCollection<PropInfo>()
            {
                new PropInfo() { NameCN = "压力",NameEN = "Pressure",Unit = "MPa(a)"},
                new PropInfo() { NameCN = "温度",NameEN = "Temperature",Unit = "°C"},
                new PropInfo() { NameCN = "气相分率",NameEN = "VaporFraction",Unit = ""},
                new PropInfo() { NameCN = "比容",NameEN = "SpecificVolume",Unit = "m³/kg"},
                new PropInfo() { NameCN = "焓",NameEN = "Enthalpy",Unit = "kJ/kg"},
                new PropInfo() { NameCN = "熵",NameEN = "Entropy",Unit = "kJ/(kg·K)"},
            };

            _inputPropList1 = new ObservableCollection<PropInfo>();
            _inputPropList1.Add(_inputPropList[0]);
            _inputPropList1.Add(_inputPropList[1]);
            _inputPropList1.Add(_inputPropList[4]);

            _inputPropList2 = new ObservableCollection<PropInfo>();
            _inputPropList2.Add(_inputPropList[2]);
            _inputPropList2.Add(_inputPropList[1]);
            _inputPropList2.Add(_inputPropList[3]);
            _inputPropList2.Add(_inputPropList[4]);
            _inputPropList2.Add(_inputPropList[5]);

            _inputProp1 = _inputPropList1[0];
            _inputProp2 = _inputPropList2[0];
        }

        // 计算属性值的方法
        private void Calc()
        {
            this.MyWater.CalcProps(InputProp1, InputProp2);
        }
        #endregion

        #region 数据验证
        // IDataErrorInfo继承方法:用于指示整个对象的错误
        public string Error { get => null; }

        // IDataErrorInfo继承方法:用于指示单个属性级别的错误
        public String this[string columnName]
        {
            get
            {
                var columnProp = this.GetType().GetProperty(columnName).GetValue(this);
                if (columnProp == null)
                {
                    return "请输入参数";
                }
                else if (string.IsNullOrEmpty(columnProp.ToString()))
                {
                    return "请输入参数";
                }
                else
                {
                    double dbl;
                    if (double.TryParse(columnProp.ToString(), out dbl))
                    {
                        if (columnName == "InputValue1")
                        {
                            if (InputProp1.NameEN == "Pressure")
                            {
                                if (dbl < 0 || dbl > 100)
                                    return "需介于0~100之间";
                            }
                            else if (InputProp1.NameEN == "Temperature")
                            {
                                if (dbl < 0 || dbl > 2000)
                                    return "需介于0~2000之间";
                            }
                        }
                        else if (columnName == "InputValue2")
                        {
                            if (InputProp2.NameEN == "VaporFraction")
                            {
                                if (dbl < 0 || dbl > 1)
                                    return "需介于0-1之间";
                            }
                            else if (InputProp2.NameEN == "Temperature")
                            {
                                double dblPressure;
                                if (InputProp1.NameEN == "Pressure" && double.TryParse(InputValue1, out dblPressure))
                                {
                                    if (dblPressure > 0 && dblPressure <= 10)
                                    {
                                        if (dbl < 0 || dbl > 2000)
                                            return "需介于0~2000之间";
                                    }
                                    else if (dblPressure > 10 && dblPressure <= 100)
                                    {
                                        if (dbl < 0 || dbl > 800)
                                            return "需介于0~800之间";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string str1 = columnProp.ToString() + "0";
                        if (!double.TryParse(str1, out dbl))
                            return "输入格式有误";
                    }
                }
                return null;
            }
        }
        #endregion
    }
}
