using System;
using System.Data;
using Oracle.DataAccess.Client;
using System.Windows.Forms;

namespace StoreDiscount
{
    public class StoreManager
    {
        private OracleDataAdapter dataAdapter;
        private OracleCommandBuilder commandBuilder;
        private DataSet dataSet;
        private string connectionString;

        public OracleDataAdapter DataAdapter { get { return dataAdapter; } set { dataAdapter = value; } }
        public OracleCommandBuilder CommandBuilder { get { return commandBuilder; } set { commandBuilder = value; } }
        public DataSet DataSet { get { return dataSet; } set { dataSet = value; } }

        // 생성자에서 연결 문자열 초기화
        public StoreManager()
        {
            connectionString = "User Id=ParkingAdmin; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe)));";
        }

        // 데이터베이스 연결 및 초기화
        public void Initialize(string commandString)
        {
            try
            {
                dataAdapter = new OracleDataAdapter(commandString, connectionString);
                commandBuilder = new OracleCommandBuilder(dataAdapter);
                dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터베이스 초기화 중 오류 발생: {ex.Message}");
            }
        }

        // INSERT, UPDATE, DELETE 명령 실행
        public void ExecuteCommand(string commandString, params OracleParameter[] parameters)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(commandString, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"명령 실행 중 오류 발생: {ex.Message}");
            }
        }
    }
}
