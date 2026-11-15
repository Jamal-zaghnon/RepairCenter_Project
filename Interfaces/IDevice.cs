namespace RepairCenter.Interfaces
{
    // ﺍﻟﻌﻤﻠﻴﺎﺕ ﺍﻷﺳﺎﺳﻴﺔ ﺍﻟﻤﺸﺘﺮﻛﺔ ﺑﻴﻦ ﺟﻤﻴﻊ ﺍﻷﺟﻬﺰﺓ
    public interface IDevice
    {
        void DisplayInfo();
        decimal CalculateRepairCost();
    }
}
