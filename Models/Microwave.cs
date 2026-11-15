using System;
using RepairCenter.Helpers;

namespace RepairCenter.Models
{
    public class Microwave : Device
    {
        public int PowerWatts { get; private set; }

        public override string DeviceType
        {
            get { return "Microwave"; }
        }

        public Microwave(string name, string serialNumber, DateTime receivedDate,
            decimal partsCost, int powerWatts)
            : base(name, serialNumber, receivedDate, partsCost)
        {
            if (!ValidationHelper.IsValidPositiveNumber(powerWatts))
                throw new ArgumentException("The microwave's power must be greater than zero.");

            PowerWatts = powerWatts;
        }

        public override decimal CalculateRepairCost()
        {
            // أجرة صيانة الميكروويف الافتراضية 35000.
            return PartsCost + 35000m;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("Electric power: " + PowerWatts + " watt");
        }
    }
}
