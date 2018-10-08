# 牢底坐穿工具箱<br>

#### 项目介绍<br>
坦白从宽，回家过年。据抗从严，牢底做穿。<br>
免责声明：本项目源代码及程序仅用于研究及学习网络安全防御用，用户使用本工具所造成的所有后果，由用户承担全部法律及连带责任！作者不承担任何法律及连带责任。<br>
[《中华人民共和国网络安全法》](http://www.cac.gov.cn/2016-11/07/c_1119867116.htm)于2017年6月1日起正式实行！

#### 软件架构<br>
只是简单的WinForm而已。<br>

1.牢底坐穿底层实现引用SharpPcap、PocketDotNet。<br>
2.牢底坐穿刀只是简单的发送POST请求然后爬取页面

#### 安装教程

绿色免安装，一键简单快速坐牢。<br>

#### 各种攻击方式的原理、实现及防御详解

1. [牢底坐穿ARP](https://www.yongshen.me/?p=929)
2. 牢底坐穿TCP TODO
3. [牢底坐穿刀](https://www.yongshen.me/?p=1199)

#### 参与贡献

只有我

#### 更新日志

v0.0.5<br>

修复未安装WinPcap程序抛异常。<br>

v 0.0.4<br>
1.修复ARP欺骗不能获取到靶机MAC的小BUG<br>
2.修复牢底坐穿刀执行cd ..切换路径命令后路径不换行的小BUG<br>
<br><br>
v 0.0.3<br>
1.修复牢底坐穿ARP中的Npcap Loopback Adapter、Microsoft KM-TEST Loopback Adapter等虚拟网卡异常<br>
2.新增牢底坐穿刀工具 --只做了PHP的POST请求的简易一句话木马和Windows10的CMD命令<br>
<br><br>
v 0.0.2<br>
1.优化使用，靶机MAC地址自动获取.<br>
2.整合UI和操作类。<br>
<br><br>
v 0.0.1<br>
实现基本的ARP欺骗<br>