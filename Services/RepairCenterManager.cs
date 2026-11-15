using System;
using System.Collections.Generic;
using RepairCenter.Enums;
using RepairCenter.Models;

namespace RepairCenter.Services
{
    // هذا الصف مسؤول عن إدارة قائمة الأجهزة وعمليات المركز.
    public class RepairCenterManager
    {
        private List<Device> devices;

        public RepairCenterManager()
        {
            devices = new List<Device>();
            LoadSampleDevices();
        }

   
        private void LoadSampleDevices()
        {
            Television television = new Television(
                "Television Samsung", "TV-1001", DateTime.Today.AddDays(-3), 100000m, 55);
            television.ChangeStatus(MaintenanceStatus.InProgress);
            AddDevice(television);

            Microwave microwave = new Microwave(
                "Microwave LG", "MW-2001", DateTime.Today.AddDays(-2), 25000m, 800);
            microwave.ChangeStatus(MaintenanceStatus.Repaired);
            AddDevice(microwave);

            Fan fan = new Fan(
                "Fan Tornado", "FN-3001", DateTime.Today.AddDays(-1), 10000m, 3);
            AddDevice(fan);
        }

        private void AddDevice(Device device)
        {
            // الاشتراك بالحدث حتى يصلنا إشعار عند تغيير حالة الجهاز.
            device.StatusChanged += OnDeviceStatusChanged;
            devices.Add(device);
        }

        public void DisplayAllDevices()
        {
            Console.WriteLine("\n==========All devices==========");

            foreach (Device device in devices)
            {
              
                device.DisplayInfo();
                Console.WriteLine("----------------------------------");
            }
        }

        public Device FindBySerialNumber(string serialNumber)
        {
            foreach (Device device in devices)
            {
                if (device.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase))
                    return device;
            }

            return null;
        }

        public bool UpdateDeviceStatus(string serialNumber, MaintenanceStatus newStatus)
        {
            Device device = FindBySerialNumber(serialNumber);

            if (device == null)
                return false;

            device.ChangeStatus(newStatus);
            return true;
        }

        public void DisplayStatistics()
        {
            decimal totalCost = 0;

            foreach (Device device in devices)
            {
                // كل صنف ينفذ حساب التكلفة الخاص به من خلال Override.
                totalCost += device.CalculateRepairCost();
            }

            Console.WriteLine("\n========== Center Statistics ==========");
            Console.WriteLine("Number of devices: " + Device.TotalDevices);
            Console.WriteLine("Number of devices that were repaired: " + Device.RepairedDevicesCount);
            Console.WriteLine("Total repair costs: " + totalCost);
        }

        private void OnDeviceStatusChanged(Device device,
            MaintenanceStatus oldStatus, MaintenanceStatus newStatus)
        {
            Console.WriteLine("\n[Alert] The device status has been changed: " + device.Name);
            Console.WriteLine("Previous status: " + GetStatusText(oldStatus));
            Console.WriteLine("The new status: " + GetStatusText(newStatus));

            if (newStatus == MaintenanceStatus.Repaired)
                Console.WriteLine("The device has been fixed.");
        }

        private string GetStatusText(MaintenanceStatus status)
        {
            switch (status)
            {
                case MaintenanceStatus.NotStarted:
                    return "did not start";
                case MaintenanceStatus.InProgress:
                    return "Under repair";
                case MaintenanceStatus.Repaired:
                    return "Fixed";
                default:
                    return "Unknown";
            }
        }
    }
}
