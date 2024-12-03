using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkingManagement
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 첫 번째 폼을 열 때 다른 폼이 끝날 때까지 대기
            var parkingStatusForm = new ParkingStatusForm();
            parkingStatusForm.FormClosed += (sender, e) => Application.Exit(); // 첫 번째 폼이 닫히면 종료

            Application.Run(parkingStatusForm); // 첫 번째 폼 실행
        }
    }
}