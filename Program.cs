    using System;
using System.Text;
using RepairCenter.Enums;
using RepairCenter.Services;

namespace RepairCenter
{
    class Program
    {
        static void Main(string[] args)
        {
           

            RepairCenterManager manager = new RepairCenterManager();
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n====================================");
                Console.WriteLine("Device Maintenance Center Management System");
                Console.WriteLine("====================================");
                Console.Write("Choose the operation:");
                Console.WriteLine("\n====================================");
                Console.WriteLine("1.Show all devices");
                Console.WriteLine("2.Check device status");
                Console.WriteLine("3.Show center statistics");
                Console.WriteLine("4.Exit");
               

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.DisplayAllDevices();
                        break;

                    case "2":
                        UpdateDeviceStatus(manager);
                        break;

                    case "3":
                        manager.DisplayStatistics();
                        break;

                    case "4":
                        isRunning = false;
                        Console.WriteLine("Thanks for using the program.");
                        break;

                    default:
                        Console.WriteLine("Incorrect choice, try again.");
                        break;
                }
            }
        }

        private static void UpdateDeviceStatus(RepairCenterManager manager)
        {
            Console.Write("Enter the device's serial number: ");
            string serialNumber = Console.ReadLine();

            if (manager.FindBySerialNumber(serialNumber) == null)
            {
                Console.WriteLine("No device was found with this number.");
                return;
            }
            Console.Write("Your Choose");
            Console.WriteLine("Choose the new status:");
            Console.WriteLine("1.did not start");
            Console.WriteLine("2.Under repair");
            Console.WriteLine("3.Fixed");
           

            string statusChoice = Console.ReadLine();
            MaintenanceStatus newStatus;

            switch (statusChoice)
            {
                case "1":
                    newStatus = MaintenanceStatus.NotStarted;
                    break;
                case "2":
                    newStatus = MaintenanceStatus.InProgress;
                    break;
                case "3":
                    newStatus = MaintenanceStatus.Repaired;
                    break;
                default:
                    Console.WriteLine("The selected status is incorrect.");
                    return;
            }

            bool updated = manager.UpdateDeviceStatus(serialNumber, newStatus);

            if (updated)
                Console.WriteLine("The device status has been updated successfully.");
            else
                Console.WriteLine("Unable to update the device status.");
        }
    }
}
