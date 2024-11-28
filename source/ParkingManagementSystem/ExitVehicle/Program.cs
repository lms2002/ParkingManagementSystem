using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExitVehicle
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 초기 vehicleId 값을 설정 (필요에 따라 수정)
            int initialVehicleId = 0;
            Application.Run(new ExitVehicleManagement(initialVehicleId));
        }
    }
}
