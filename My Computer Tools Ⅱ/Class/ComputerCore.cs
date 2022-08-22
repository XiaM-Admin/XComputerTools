using OpenHardwareMonitor.Hardware;
using System;

namespace My_Computer_Tools_Ⅱ.Class
{

    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }

        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware)
                subHardware.Accept(this);
        }

        public void VisitSensor(ISensor sensor) { }

        public void VisitParameter(IParameter parameter) { }
    }

    public class ComputerCore
    {
        private UpdateVisitor updateVisitor = new UpdateVisitor();
        private Computer computer = new Computer();

        public ComputerCore()
        {
            computer.Open();
            computer.CPUEnabled = true;
            computer.GPUEnabled = true;
            computer.Accept(updateVisitor);
        }

        /// <summary>
        /// 获取CPU温度
        /// </summary>
        /// <returns></returns>
        public float GetCpuTemp()
        {
            computer.Accept(updateVisitor);
            float num = 0;
            float cc = 0;
            float avg = 0;
            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                //查找硬件类型为CPU
                if (computer.Hardware[i].HardwareType == HardwareType.CPU)
                {
                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {
                        //找到温度传感器
                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                        {
                            if (computer.Hardware[i].Sensors[j].Value is null)
                                continue;

                            num += (float)computer.Hardware[i].Sensors[j].Value;
                            if((float)computer.Hardware[i].Sensors[j].Value!=0)
                                cc++;
                            avg = num / cc;
                        }
                    }
                }
            }
            return avg;
        }

        /// <summary>
        /// 获取GPU温度
        /// </summary>
        /// <param name="isNva">是不是英伟达GPU</param>
        /// <returns></returns>
        public float GetGpuTemp(bool isNva = true)
        {
            computer.Accept(updateVisitor);
            HardwareType type = isNva ? HardwareType.GpuNvidia : HardwareType.GpuAti;

            for (int i = 0; i < computer.Hardware.Length; i++)
            {
                //查找硬件类型为CPU
                if (computer.Hardware[i].HardwareType == type)
                {
                    for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
                    {
                        //找到温度传感器
                        if (computer.Hardware[i].Sensors[j].SensorType == SensorType.Temperature)
                        {
                            if (computer.Hardware[i].Sensors[j].Value is null)
                                continue;

                            return (float)computer.Hardware[i].Sensors[j].Value;
                        }
                    }
                }
            }
            return 0;
        }

    }

    /// <summary>
    /// test
    /// </summary>
    internal sealed class CpuTemperatureReader : IDisposable
    {
        private readonly Computer _computer;

        public CpuTemperatureReader()
        {
            _computer = new Computer { 
                CPUEnabled = true,
                GPUEnabled = true
            };
            _computer.Open();
        }

        public void GetTemperaturesInCelsius()
        {
            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update(); //use hardware.Name to get CPU model
                foreach (var sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Temperature && sensor.Value.HasValue)
                    {
                        Console.WriteLine("{0}, Value={1}, Min Value={2}, Max Value={3}",
                            sensor.Name, sensor.Value.Value, sensor.Min.Value, sensor.Max.Value);
                    }
                }
            }
        }
        public void Dispose()
        {
            try
            {
                _computer.Close();
            }
            catch (Exception)
            {
                //ignore closing errors
            }
        }
    }

}
