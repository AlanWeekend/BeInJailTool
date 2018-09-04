using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARPAttack
{
    public partial class Form1 : Form
    {
        LibPcapLiveDeviceList devicesList;

        /// <summary>
        /// 发送网卡
        /// </summary>
        LibPcapLiveDevice device;

        /// <summary>
        /// 应答超时限制
        /// </summary>
        TimeSpan timeout = new TimeSpan(0, 0, 1);

        ARPTool arpTool;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 初始化获取网卡列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.LoadFile("Law.rtf");

            devicesList = LibPcapLiveDeviceList.Instance;   //获取本机所有网卡

            if (devicesList.Count < 1)
            {
                throw new Exception("没有发现本机上的网络设备");
            }

            device = devicesList[0];

            combDevice.DataSource = devicesList;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            //信息检查
            if (txtRecipientIP.Text.Equals("")) {
                MessageBox.Show("靶机IP为空！");
                return;
            }

            if (btnStart.Text.Equals("Start"))
            {
                arpTool.StartARPSpoofing(txtSenderIP.Text, txtRecipientIP.Text,txtSenderMAC.Text,txtRecipientMAC.Text );
                btnStart.Text = "Stop";
            }else
            {
                //TODO
                arpTool.StopARPSpoofing();
                btnStart.Text = "Start";
            }
        }

        private void combDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 获取相应的设备网关MAC，IP，本机MAC，IP
            device = devicesList[(sender as ComboBox).SelectedIndex];
            if (device.Interface.FriendlyName.IndexOf("VMware") ==-1) {
                arpTool = new ARPTool(device);
                arpTool.ScanStopedEvent += arpTool_ScanStopedEvent;
                arpTool.ResolvedEvent += arpTool_ResolvedEvent;
                arpTool.ResolvedTimeEvent += arpTool_ResolvedTimeEvent;
                txtLocalIP.Text = arpTool.LocalIP.ToString();
                txtLocalMAC.Text = Regex.Replace(arpTool.LocalMAC.ToString(), @"(\w{2})", "$1-").Trim('-');
                txtGatewayIP.Text = arpTool.GetwayIP.ToString();
                txtGatewayMAC.Text = Regex.Replace(arpTool.GetwayMAC.ToString(), @"(\w{2})", "$1-").Trim('-');
                txtSenderIP.Text = arpTool.GetwayIP.ToString();
                txtSenderMAC.Text = Regex.Replace(GetRandomPhysicalAddress().ToString(), @"(\w{2})", "$1-").Trim('-');
            }
            
        }

        /// <summary>
        /// 随机生成一个物理地址
        /// </summary>
        /// <returns></returns>
        private PhysicalAddress GetRandomPhysicalAddress()
        {
            Random random = new Random(Environment.TickCount);
            byte[] macBytes = new byte[] { 0x9C, 0x21, 0x6A, 0xC3, 0xB0, 0x27 };
            macBytes[5] = (byte)random.Next(255);
            Console.WriteLine(new PhysicalAddress(macBytes));
            return new PhysicalAddress(macBytes);
        }

        /// <summary>
        /// 扫描事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScan_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button.Text.Equals("Scan"))
            {
                IPAddress ip;
                if (!IPAddress.TryParse(txtRecipientIP.Text,out ip))
                {
                    MessageBox.Show("不合法的IP地址");
                    return;
                }

                button.Text = "Stop";
                arpTool.ScanLAN(txtRecipientIP.Text, txtLocalMAC.Text,txtLocalIP.Text);
            }
            else
            {
                arpTool.StopScanLan();
                button.Text = "Scan";
            }
        }

        void arpTool_ScanStopedEvent(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => { btnScan.Text = "扫描"; }));
            MessageBox.Show("扫描结束");
        }

        void arpTool_ResolvedEvent(object sender, PhysicalAddress e)
        {
            txtRecipientMAC.Text = Regex.Replace(e.ToString(), @"(\w{2})", "$1-").Trim('-');
            btnScan.Text = "Scan";
        }

        void arpTool_ResolvedTimeEvent(object sender, int e)
        {
            label6.Text = "send " + e + " time";
        }
    }
}
