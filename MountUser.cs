using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASCOM.DeviceInterface;
using ASCOM.DriverAccess;
using System.Timers;

namespace ASAMount
{
    class MountUser
    {

        MountParams m_mountParams;
        string m_deviceID;
        Telescope m_telescope;
        AstroArith m_astroArith;
        Timer m_timerUpdateStat;

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

        public MountUser()
        {
            m_mountParams = new MountParams();
            m_deviceID = "";
            m_astroArith = new AstroArith();

            //设置定时器获取自身状态
            m_timerUpdateStat = new Timer(100);
            m_timerUpdateStat.Elapsed += new ElapsedEventHandler(SelfUpdateStat);
            m_timerUpdateStat.AutoReset = true;
            
        }

        //连接转台
        public void ConnectDevice()
        {
            try
            {
                if (m_telescope == null || m_mountParams.connected == false)
                {
                    //从配置文件读取telescope id
                    m_deviceID = "AstrooptikServer.Telescope";
                    //tbd
                    if (m_deviceID == "")
                    {
                        m_deviceID = Telescope.Choose("AstrooptikServer.Telescope");
                        if (m_deviceID == null || m_deviceID == "")
                        {
                            m_mountParams.errMsg = "Can not find \'AstrooptikServer.Telescope\' in ASCOM driver";
                            return;
                        }
                        else
                        {
                            //保存转台名称到配置文件
                            //TBD
                        }
                    }

                    m_telescope = new Telescope(m_deviceID);
                    m_telescope.Connected = true;
                    m_mountParams.connected = true;
                    m_timerUpdateStat.Enabled = true;
                    m_mountParams.stat = TS_Stopped;
                }

                
            }
            catch (System.Exception ex)
            {
                m_mountParams.errMsg = ex.Message;
                m_mountParams.connected = false;
                m_timerUpdateStat.Enabled = false;
            }
        }

        //指向
        public void Slew(string ra, string dec)
        {
            try
            {
                if (m_telescope != null && m_mountParams.connected == true)
                {
                    double douRa = 0, douDec = 0;
                    m_astroArith.StringHour2Double(ra, ref douRa);
                    m_telescope.TargetRightAscension = douRa;
                    m_astroArith.StringDegree2Double(dec, ref douDec);
                    m_telescope.TargetDeclination = douDec;

                    //检查状态
                    if (m_telescope.AtPark)
                        m_telescope.Unpark();
                    if (m_telescope.Tracking == false)
                        m_telescope.Tracking = true;

                    //开始指向
                    if (m_telescope.CanSlewAsync)
                    {
                        m_mountParams.stat = TS_Slewing;
                        m_telescope.SlewToTargetAsync();
                    }
                }

            }
            catch (System.Exception ex)
            {
                m_mountParams.errMsg = ex.Message;
                Console.WriteLine("error message: " + ex.Message);
                //m_timerUpdateStat.Enabled = false;

            }


        }

        //急停
        public void Stop()
        {
            try
            {
                if (m_telescope != null && m_mountParams.connected == true)
                {
                    if (m_telescope.AtPark)
                    {
                        m_telescope.Unpark();
                    }
                    m_telescope.AbortSlew();
                }

            }
            catch (System.Exception ex)
            {
                m_mountParams.errMsg = ex.Message;
                Console.WriteLine("error message: " + ex.Message);
                m_timerUpdateStat.Enabled = false;
            }
        }

        //找零点
        public void FindHome()
        {
            try
            {
                if (m_telescope != null && m_mountParams.connected == true)
                {
                    if (m_telescope.CanFindHome == false) return;
                    if (m_telescope.AtPark) m_telescope.Unpark();
                    m_mountParams.stat = TS_Homing;
                    m_telescope.FindHome();

                }

            }
            catch (System.Exception ex)
            {
                m_mountParams.errMsg = ex.Message;
                Console.WriteLine("error message: " + ex.Message);
                m_timerUpdateStat.Enabled = false;
            }
        }

        //Park
        public void Park()
        {
            try
            {
                if (m_telescope != null && m_mountParams.connected == true)
                {
                    if (m_telescope.CanPark == false) return;
                    if (m_telescope.AtPark) m_telescope.Unpark();
                    m_mountParams.stat = TS_Parking;
                    m_telescope.Park();

                }

            }
            catch (System.Exception ex)
            {
                m_mountParams.errMsg = ex.Message;
                Console.WriteLine("error message: " + ex.Message);
                m_timerUpdateStat.Enabled = false;
            }
        }

        double m_vOldRA = 0;
        double m_vOldDEC = 0;
        int m_nTrackRepeat = 0;
        //自刷新状态
        public void SelfUpdateStat(object o, ElapsedEventArgs e)
        {
            try
            {
                if (m_telescope != null && m_mountParams.connected == true)
                {
                    int hour = 0, minute = 0, degree = 0;
                    double second = 0.0;
                    string sign = "";
                    double ra = m_telescope.RightAscension;
                    double dec = m_telescope.Declination;
                    double az = m_telescope.Azimuth;
                    double alt = m_telescope.Altitude;
                    
                    //获取RA，DEC，方位角，高度角
                    m_astroArith.HH2HMS(ra, ref hour, ref minute, ref second);
                    m_mountParams.ra = string.Format("{0:00}:{1:00}:{2:00.00}", hour, minute, second);
                    m_astroArith.DD2DMS(dec, ref degree, ref minute, ref second, ref sign);
                    m_mountParams.dec = sign + string.Format("{0:00}:{1:00}:{2:00.0}", degree, minute, second);
                    m_astroArith.DD2DMS(az, ref degree, ref minute, ref second, ref sign);
                    m_mountParams.az = sign + string.Format("{0:00}:{1:00}:{2:00.0}", degree, minute, second);
                    m_astroArith.DD2DMS(alt, ref degree, ref minute, ref second, ref sign);
                    m_mountParams.alt = sign + string.Format("{0:00}:{1:00}:{2:00.0}", degree, minute, second);

                    //从望远镜获取恒星时
                    double st = m_telescope.SiderealTime;
                    m_astroArith.HH2HMS(st, ref hour, ref minute, ref second);
                    m_mountParams.st = string.Format("{0:00}:{1:00}:{2:00.00}", hour, minute, second);

                    //从本机获取世界时
                    DateTime utc = DateTime.Now.ToUniversalTime();
                    m_mountParams.date = string.Format("{0}-{1:00}-{2:00}", utc.Year, utc.Month, utc.Day);
                    m_mountParams.ut = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", utc.Hour, utc.Minute, utc.Second, utc.Millisecond / 10);

                    switch (m_mountParams.stat)
                    {
                        case TS_Parked:
                            if (m_telescope.Slewing)
                            {
                                m_mountParams.stat = TS_Slewing;
                            }
                            else if (m_telescope.Tracking)
                            {
                                if (Math.Abs(ra - m_vOldRA) < 1 && Math.Abs(dec - m_vOldDEC) < 1)
                                    m_nTrackRepeat++;
                                else
                                {
                                    m_vOldRA = ra;
                                    m_vOldDEC = dec;
                                    m_nTrackRepeat = 0;
                                }
                                if (m_nTrackRepeat > 5)
                                {
                                    m_mountParams.stat = TS_Tracking;
                                }
                            }
                            break;
                        case TS_Homing:
                            if (m_telescope.AtHome)
                            {
                                m_mountParams.stat = TS_Homed;
                            }
                            break;
                        case TS_Parking:
                            if (m_telescope.AtPark)
                            {
                                m_mountParams.stat = TS_Parked;
                            }
                            break;
                        case TS_Slewing:
                            if (!m_telescope.Slewing)
                            {
                                if (Math.Abs(ra - m_vOldRA) < 1 && Math.Abs(dec - m_vOldDEC) < 1)
                                    m_nTrackRepeat++;
                                else
                                {
                                    m_vOldRA = ra;
                                    m_vOldDEC = dec;
                                    m_nTrackRepeat = 0;
                                }
                                if (m_nTrackRepeat > 10)
                                {
                                    m_mountParams.stat = TS_Tracking;
                                }
                            }
                            break;
                        default:
                            break;
                    }

                }

            }
            catch (System.Exception ex)
            {
                m_mountParams.errMsg = ex.Message;
                Console.WriteLine("error message: " + ex.Message);
                m_mountParams.connected = false;
                m_timerUpdateStat.Enabled = false;
            }
        }


        public void GetAllStat(ref string curRA, ref string curDEC, ref string curAz, ref string curAlt,
            ref string curDate, ref string curUT, ref string curST, ref int curStat)
        {
            //赤经、赤纬、方位、高度
            curRA = m_mountParams.ra;
            curDEC = m_mountParams.dec;
            curAz = m_mountParams.az;
            curAlt = m_mountParams.alt;

            //日期、世界时、恒星时
            curDate = m_mountParams.date;
            curUT = m_mountParams.ut;
            curST = m_mountParams.st;

            //获取移动状态
            curStat = m_mountParams.stat;
        }

        public bool GetLink()
        {
            try
            {
                if (m_telescope != null)
                    return m_mountParams.connected;
                else
                    return false;

            }
            catch (System.Exception ex)
            {
                m_telescope = null;
                m_mountParams.errMsg = ex.Message;
                Console.WriteLine("error message: " + ex.Message);
                return false;
            }
        }
    }

}