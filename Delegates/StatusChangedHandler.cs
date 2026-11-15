using RepairCenter.Enums;
using RepairCenter.Models;

namespace RepairCenter.Delegates
{
    // يحدد شكل الدالة التي تستقبل إشعار تغيير حالة الجهاز.
    public delegate void StatusChangedHandler(
        Device device,
        MaintenanceStatus oldStatus,
        MaintenanceStatus newStatus);
}
