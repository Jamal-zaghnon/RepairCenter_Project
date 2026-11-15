using System;
using RepairCenter.Delegates;
using RepairCenter.Enums;
using RepairCenter.Helpers;
using RepairCenter.Interfaces;

namespace RepairCenter.Models
{
    // الصف الأب المجرد لجميع الأجهزة
    public abstract class Device : IDevice
    {
        // جميع الحقول خاصة لحماية بيانات الكائن.
        private readonly string serialNumber;
        private string name;
        private DateTime receivedDate;
        private decimal partsCost;
        private MaintenanceStatus status;

        // عدادات مشتركة بين جميع الأجهزة.
        public static int TotalDevices { get; private set; }
        public static int RepairedDevicesCount { get; private set; }

        // خصائص مشتركة بين أنواع الأجهزة.
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        // الرقم التسلسلي للقراءة فقط بعد إنشاء الجهاز.
        public string SerialNumber
        {
            get { return serialNumber; }
        }

        public DateTime ReceivedDate
        {
            get { return receivedDate; }
            private set { receivedDate = value; }
        }

        public decimal PartsCost
        {
            get { return partsCost; }
            private set { partsCost = value; }
        }

        public MaintenanceStatus Status
        {
            get { return status; }
            private set { status = value; }
        }

        public abstract string DeviceType { get; }

        // الحدث الذي يتم إطلاقه عند تغيير حالة الصيانة.
        public event StatusChangedHandler StatusChanged;

        protected Device(string name, string serialNumber, DateTime receivedDate, decimal partsCost)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Device name is required.");

            if (!ValidationHelper.IsValidSerialNumber(serialNumber))
                throw new ArgumentException("The serial number is incorrect.");

            if (!ValidationHelper.IsValidDate(receivedDate))
                throw new ArgumentException("The receipt date is incorrect.");

            if (!ValidationHelper.IsValidCost(partsCost))
                throw new ArgumentException("The cost of the parts can't be negative.");

            Name = name;
            this.serialNumber = serialNumber;
            ReceivedDate = receivedDate;
            PartsCost = partsCost;
            Status = MaintenanceStatus.NotStarted;

            TotalDevices++;
        }

        // كل جهاز يحسب تكلفة الإصلاح بطريقته الخاصة.
        public abstract decimal CalculateRepairCost();

        // عرض البيانات المشتركة للجهاز.
        public virtual void DisplayInfo()
        {
            Console.WriteLine("Device type: " + DeviceType);
            Console.WriteLine("Device Name: " + Name);
            Console.WriteLine("Serial number: " + SerialNumber);
            Console.WriteLine("Date of receipt: " + ReceivedDate.ToShortDateString());
            Console.WriteLine("Cost of parts: " + PartsCost);
            Console.WriteLine("Total cost: " + CalculateRepairCost());
            Console.WriteLine("Maintenance Status: " + GetStatusText());
        }

        // تغيير الحالة من خلال دالة حتى نتحكم بالعملية ونطلق الحدث.
        public void ChangeStatus(MaintenanceStatus newStatus)
        {
            if (Status == newStatus)
                return;

            MaintenanceStatus oldStatus = Status;

            if (oldStatus == MaintenanceStatus.Repaired)
                RepairedDevicesCount--;

            Status = newStatus;

            if (newStatus == MaintenanceStatus.Repaired)
                RepairedDevicesCount++;

            // إطلاق الحدث بعد نجاح تغيير الحالة.
            if (StatusChanged != null)
                StatusChanged(this, oldStatus, newStatus);
        }

        public string GetStatusText()
        {
            switch (Status)
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
