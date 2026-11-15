using System;
using RepairCenter.Helpers;

namespace RepairCenter.Models
{
    public class Fan : Device
    {
        public int SpeedLevels { get; private set; }

        public override string DeviceType
        {
            get { return "Fan"; }
        }

        public Fan(string name, string serialNumber, DateTime receivedDate,
            decimal partsCost, int speedLevels)
            : base(name, serialNumber, receivedDate, partsCost)
        {
            if (!ValidationHelper.IsValidPositiveNumber(speedLevels))
                throw new ArgumentException("The number of fan speeds must be greater than zero.");

            SpeedLevels = speedLevels;
        }

        public override decimal CalculateRepairCost()
        {
            // أجرة صيانة المروحة الافتراضية 20000.
            return PartsCost + 20000m;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("Number of speeds: " + SpeedLevels);
        }
    }
}
