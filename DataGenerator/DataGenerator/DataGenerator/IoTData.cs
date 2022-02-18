using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class IoTData
    {
        static Random rnd = new Random();
        string DeviceName = "";
        string DeviceType = "";
        string UOM1 = "";
        string UOM2 = "";
        decimal UOM1Value = 0;
        decimal UOM2Value = 0;
        DateTime TimeStamp;

        public IoTData(string dName, string dType)
        {
            this.DeviceName = dName;
            this.DeviceType = dType;
            if (dType == "GPS")
            {
                GenerateGPSData();
            }
            else if (dType == "H2O")
            {
                GenerateWaterData();
            }
            else if (dType == "GAS")
            {
                GenerateGasData();
            }
            else if (dType == "ELECTRIC")
            {
                GenerateElectricData();
            }
        }

        public override string ToString()
        {
            return DeviceName + "," + DeviceType + "," + TimeStamp + "," + UOM1 + "," + UOM1Value + "," 
                + UOM2 + "," + UOM2Value + Environment.NewLine;
        }

        private void GenerateElectricData()
        {
            UOM1 = "kWH"; // Kilowatt Hours
            UOM2 = "KVA"; // Demand Value
            UOM1Value = Decimal.Parse(rnd.Next(0, 100).ToString() + "." + rnd.Next(0, 1000000000).ToString());
            UOM2Value = Decimal.Parse(rnd.Next(0, 10).ToString() + "." + rnd.Next(0, 1000000000).ToString());
            TimeStamp = DateTime.Now;
        }

        private void GenerateGasData()
        {
            UOM1 = "CF"; //Cubic Feet
            UOM2 = "PSI"; //Pressure
            UOM1Value = Decimal.Parse(rnd.Next(0, 1000).ToString() + "." + rnd.Next(0, 1000000000).ToString());
            UOM2Value = Decimal.Parse(rnd.Next(0, 2).ToString() + "." + rnd.Next(0, 1000000000).ToString());
            TimeStamp = DateTime.Now;
        }

        private void GenerateWaterData()
        {
            UOM1 = "CM"; //Cubic Meters
            UOM2 = "TEMPCelsius";
            UOM1Value = Decimal.Parse(rnd.Next(0, 10).ToString() + "." + rnd.Next(0,1000000000).ToString());
            UOM2Value = Decimal.Parse(rnd.Next(0, 30).ToString() + "." + rnd.Next(0, 1000000000).ToString());
            TimeStamp = DateTime.Now;
        }

        private void GenerateGPSData()
        {
            UOM1 = "Latitude";
            UOM2 = "Longitude";
            UOM1Value = Decimal.Parse(rnd.Next(49, 52).ToString() + "." + rnd.Next(0, 1000000000).ToString());
            UOM2Value = Decimal.Parse(rnd.Next(-111, -109).ToString() + "." + rnd.Next(0, 1000000000).ToString());
            TimeStamp = DateTime.Now;
        }
    }
}
