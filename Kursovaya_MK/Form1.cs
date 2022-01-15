using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace Kursovaya_MK
{
    public partial class Form1 : Form
    {
        string dataIN;
        SerialPort serialPort1 = new SerialPort();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Подключить")
            {
                try
                {
                    serialPort1.PortName = cbPorts.Text;
                    serialPort1.BaudRate = Convert.ToInt32(tbBaudRate.Text);
                    serialPort1.Open();
                    Thread.Sleep(1000);

                    cbPorts.Enabled = false;
                    
                    btnConnect.Text = "Отключить";

                    dataIN = serialPort1.ReadExisting();
                   
                    char[] dataArr = dataIN.ToCharArray();
                    char i = Convert.ToChar(tbSlave.Text);
                    if (i == dataArr[dataArr.Length - 5])
                     {
                    
                    
                        tbTemperature.Text = $"{dataArr[dataArr.Length - 4]}{dataArr[dataArr.Length - 3]}";
                        tbHumidity.Text = $"{dataArr[dataArr.Length - 2]}{dataArr[dataArr.Length - 1]}";
                  }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                serialPort1.Close();
                cbPorts.Enabled = true;
                
                btnConnect.Text = "Подключить";
            }     
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataIN = serialPort1.ReadExisting();
           
            char[] dataArr = dataIN.ToCharArray();
            char i = Convert.ToChar(tbSlave.Text);
            if (i == dataArr[dataArr.Length - 5])
            {
                tbTemperature.Text = $"{dataArr[dataArr.Length - 4]}{dataArr[dataArr.Length - 3]}";
                tbHumidity.Text = $"{dataArr[dataArr.Length - 2]}{dataArr[dataArr.Length - 1]}";
            }
            else
               MessageBox.Show("Slave недоступен.", "Ошибка");
        }

        private void btnPortList_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cbPorts.Text = "";
            cbPorts.Items.Clear();
            if (ports.Length != 0)
            {
                cbPorts.Items.AddRange(ports);
                cbPorts.SelectedIndex = 0;
            }
            else
                MessageBox.Show("Нет доступных портов.","Ошибка");
        }
    }
}
