using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ASAMount
{
    class MountNet
    {
        private IPAddress m_ip;
        private IPEndPoint m_ep;
        Socket m_sktDev;
        string m_errMsg;
        bool m_connected;
        MountUser m_mountUser;
        Timer m_timerHousekeep;
        public MountNet(MountUser mntUser)
        {
            m_mountUser = mntUser;
            m_ip = IPAddress.Parse("190.168.1.115");
            m_ep = new IPEndPoint(m_ip, 30001);
            m_sktDev = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.IP);
            m_errMsg = "";
            m_connected = false;

            //启动消息接收线程
            Thread thd = new Thread(new ThreadStart(ReceiveMessage));
            thd.IsBackground = true;
            thd.Start();

            //启动housekeeping线程
            m_timerHousekeep = new Timer(new TimerCallback(HouseKeeping), null, 0, 10000);
            m_timerHousekeep.Change(0, 10000);
        }

        ~MountNet()
        {
            try
            {
                //发送反注册消息
                SendMessage("RMOUNT");
                if (m_sktDev != null)
                {
                    m_sktDev.Shutdown(SocketShutdown.Both);
                    m_sktDev.Close();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Dispose error: " + ex.Message);
            }
        }

        private void HouseKeeping(object o)
        {
            //获取系统时间
            string lt = GetLocalTime();
            //每隔10s由设备端发给服务器
            SendMessage("RM,HOUSEKEEPING," + lt);
        }

        //设备端反向连接
        public void ConnectToHost()
        {
            try
            {
                while (m_connected == false)
                {
                    Console.WriteLine("try to connect to host ...");
                    m_sktDev.Connect(m_ep);
                    m_connected = true;
                    //连接成功后发送注册消息
                    SendMessage("MOUNT");

                }

            }
            catch (System.Exception ex)
            {
                Thread.Sleep(5000);
                m_errMsg = ex.Message;
                m_connected = false;
                ConnectToHost();
            }
        }


        public void SendMessage(string msg)
        {
            try
            {
                byte[] buf = Encoding.ASCII.GetBytes(msg);
                if (true == m_connected)
                {
                    m_sktDev.Send(buf);

                }
            }
            catch (System.Exception ex)
            {
                Console.Write("send message error: " + ex.Message);
            }

        }

        //接收远程指令
        public void ReceiveMessage()
        {
            try
            {
                string msg = "";
                byte[] buf = new byte[1024];
                int length = 0;
                while ((length = m_sktDev.Receive(buf)) != 0)
                {
                    msg = Encoding.ASCII.GetString(buf, 0, length);
                    Console.WriteLine("message received: {0}", msg);
                    ResolveCmds(msg);
                }
            }
            catch (System.Exception ex)
            {
                m_errMsg = ex.Message;
                Console.WriteLine("resolve message error: " + ex.Message);
                ReceiveMessage();
            }
        }

        //解析远程指令
        public void ResolveCmds(string msg)
        {
            try
            {
                string deviceType, cmd;
                string ra = "", dec = "", az = "", alt = "", date = "", ut = "", st = "", lt = "";
                int movStat = 0;
                string[] cmdList = msg.Split(',');
                string reply = "";
                deviceType = cmdList[0];
                if (cmdList.Length < 3)
                    return;
                if (deviceType != "M")
                    return;
                cmd = cmdList[1];
                lt = cmdList.Last();

                if (cmd == "SLEW")
                {
                    ra = cmdList[2];
                    dec = cmdList[3];
                    //control device
                    m_mountUser.Slew(ra, dec);
                    //send reply message
                    reply = "R" + string.Join(",", cmdList, 0, cmdList.Length - 1);
                    reply += "," + "0" + "," + lt;
                    SendMessage(reply);

                }
                else if (cmd == "STOP")
                {
                    //control device
                    m_mountUser.Stop();

                    //send reply message
                    reply = "R" + string.Join(",", cmdList, 0, cmdList.Length - 1);
                    reply += "," + "0" + "," + lt;
                    SendMessage(reply);

                }
                else if (cmd == "HOME")
                {
                    //control device
                    m_mountUser.FindHome();
                    //send reply message
                    reply = "R" + string.Join(",", cmdList, 0, cmdList.Length - 1);
                    reply += "," + "0" + "," + lt;
                    SendMessage(reply);
                }
                else if (cmd == "PARK")
                {
                    //control device
                    m_mountUser.Park();
                    //send reply message
                    reply = "R" + string.Join(",", cmdList, 0, cmdList.Length - 1);
                    reply += "," + "0" + "," + lt;
                    SendMessage(reply);
                }
                else if (cmd == "STATUS")
                {
                    //control device
                    m_mountUser.GetAllStat(ref ra, ref dec, ref az, ref alt, ref date, ref ut, ref st, ref movStat);
                    //pos = m_mountUser.GetCurPos().ToString("f3");
                    //temp = m_mountUser.GetCurTemp().ToString("f1");
                    //ismoving = (m_mountUser.GetMoveStatus() ? 1 : 0).ToString();
                    //send reply message
                    reply = "R" + string.Join(",", cmdList, 0, cmdList.Length - 1);
                    reply += "," + ra;
                    reply += "," + dec;
                    reply += "," + az;
                    reply += "," + alt;
                    reply += "," + date;
                    reply += "," + ut;
                    reply += "," + st;
                    reply += "," + movStat;
                    reply += "," + lt;
                    SendMessage(reply);
                }
                Console.WriteLine("send: {0}", reply);

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("cmds error: {0}", ex.Message);
            }
        }

        //获取local time
        private string GetLocalTime()
        {
            return DateTime.Now.Year.ToString() +
                        DateTime.Now.Month.ToString("d2") +
                        DateTime.Now.Day.ToString("d2") +
                        "T" +
                        DateTime.Now.Hour.ToString("d2") +
                        DateTime.Now.Minute.ToString("d2") +
                        DateTime.Now.Second.ToString("d2") +
                        "." +
                        DateTime.Now.Millisecond.ToString("d3");
        }
    }
}
