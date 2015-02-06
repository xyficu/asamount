using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ASAMount
{
    public partial class FormAsaMount : Form
    {

        // 自定义望远镜状态字
        /** 望远镜状态字
         * 0  : 初始状态, 断开连接
         * 1  : 静止
         * 2  : 搜索零点中
         * 3  : 搜索零点成功
         * 4  : 指向中
         * 5  : 跟踪中
         * 6  : 轴控制中
         * 7  : 复位中
         * 8  : 复位
         **/
        private const int TS_NONE = 0;
        private const int TS_Stopped = 1;
        private const int TS_Homing = 2;
        private const int TS_Homed = 3;
        private const int TS_Slewing = 4;
        private const int TS_Tracking = 5;
        private const int TS_Moving = 6;
        private const int TS_Parking = 7;
        private const int TS_Parked = 8;

        MountUser mountUser;
        MountNet mountNet;
        Thread mntNetThd;

        public FormAsaMount()
        {
            InitializeComponent();

            mountUser = new MountUser();
            timerMntUpdateStat.Interval = 100;
            timerMntUpdateStat.Enabled = true;

            //connect to host
            mountNet = new MountNet(mountUser);
            mntNetThd = new Thread(new ThreadStart(mountNet.ConnectToHost));
            mntNetThd.IsBackground = true;
            mntNetThd.Start();

        }

        ~FormAsaMount()
        {
            
        }

        private void connectMountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mountUser.ConnectDevice();
        }

        private void buttonMountSlew_Click(object sender, EventArgs e)
        {
            mountUser.Slew(textBoxTargetRA.Text, textBoxTargetDEC.Text);
        }

        private void buttonMountHome_Click(object sender, EventArgs e)
        {
            mountUser.FindHome();
        }

        private void buttonMountStop_Click(object sender, EventArgs e)
        {
            mountUser.Stop();
        }

        private void buttonMountPark_Click(object sender, EventArgs e)
        {
            labelMountStat.Text = "Parking...";
            mountUser.Park();
        }

        private void timerMntUpdateStat_Tick(object sender, EventArgs e)
        {
            //赤经、赤纬、方位角、高度角
            string curRa = "", curDec = "", curAz = "", curAlt = "";
            string curDate = "", curUT = "", curST = "";
            int stat = 0;
            mountUser.GetAllStat(ref curRa, ref curDec, ref curAz, ref curAlt,
                                ref curDate, ref curUT, ref curST, ref stat);
            labelMountRA.Text = curRa;
            labelMountDEC.Text = curDec;
            labelMountAz.Text = curAz;
            labelMountAlt.Text = curAlt;

            labelMountDate.Text = curDate;
            labelMountUT.Text = curUT;
            labelMountST.Text = curST;
            
            //连接状态
            switch (mountUser.GetLink())
            {
                case true:
                    labelMountDrvStat.Text = "Mount driver is connected.";
                    labelMountDrvStat.ForeColor = Color.Green;
                    break;
                case false:
                default:
                    labelMountDrvStat.Text = "Mount driver is not connected.";
                    labelMountDrvStat.ForeColor = Color.Red;
                    break;
            }

            //移动状态
            switch (stat)
            {
                case TS_Stopped:
                    labelMountStat.Text = "Stopped";
                    break;
                case TS_Homing:
                    labelMountStat.Text = "Homing...";
                    break;
                case TS_Homed:
                    labelMountStat.Text = "Homed";
                    break;
                case TS_Slewing:
                    labelMountStat.Text = "Slewing...";
                    break;
                case TS_Tracking:
                    labelMountStat.Text = "Tracking...";
                    break;
                case TS_Parking:
                    labelMountStat.Text = "Parking...";
                    break;
                case TS_Parked:
                    labelMountStat.Text = "Parked";
                    break;
                default:
                    break;
            }
        }

        private void FormAsaMount_Load(object sender, EventArgs e)
        {
            mountUser.ConnectDevice();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mountUser.Disconnect();
            this.Close();
        }

        private void FormAsaMount_FormClosing(object sender, FormClosingEventArgs e)
        {

            mountUser.Disconnect();

        }


    }
}
