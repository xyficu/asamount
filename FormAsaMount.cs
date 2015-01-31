using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ASAMount
{
    public partial class FormAsaMount : Form
    {

        MountUser mountUser;

        public FormAsaMount()
        {
            InitializeComponent();
            mountUser = new MountUser();
            timerMntUpdateStat.Interval = 100;
            timerMntUpdateStat.Enabled = true;
            
        }

        private void connectMountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mountUser.ConnectDevice();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
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
            mountUser.Park();
        }

        private void timerMntUpdateStat_Tick(object sender, EventArgs e)
        {
            //赤经、赤纬、方位角、高度角
            string curRa = "", curDec = "", curAz = "", curAlt = "";
            string curDate = "", curUT = "", curST = "";
            bool isTracking = false, isMoving = false, isHomed = false, isParked = false;
            mountUser.GetAllStat(ref curRa, ref curDec, ref curAz, ref curAlt,
                                ref isTracking, ref isMoving, ref isHomed, ref isParked,
                                ref curDate, ref curUT, ref curST);
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
        }


    }
}
