using System;
using RepairCenter.Helpers;

namespace RepairCenter.Models
{
    public class Television : Device
    {
        public int ScreenSize { get; private set; }

        public override string DeviceType
        {
            get { return "Television"; }
        }

        public Television(string name, string serialNumber, DateTime receivedDate,
            decimal partsCost, int screenSize)
            : base(name, serialNumber, receivedDate, partsCost)
        {
            if (!ValidationHelper.IsValidPositiveNumber(screenSize))
                throw new ArgumentException("The screen size has to be bigger than zero.");

            ScreenSize = screenSize;
        }

        public override decimal CalculateRepairCost()
        {
            // أجرة صيانة الشاشة 50000.
            return PartsCost + 50000m;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine("Screen size: " + ScreenSize + "inch");
        }
    }
}
