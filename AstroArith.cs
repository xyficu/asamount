using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASAMount
{
	class AstroArith
	{
		public const double PI = 3.1415926535897932384626433832795;
		public const double PI360 = 2 * PI;
		public const double GtoR = PI / 180;
		public const double RtoG = 180 / PI;

		public double[,] m_matPN;

		// 地理经度, 量纲: 弧度
		private double m_vSiteLgt = 0;
		// 地理纬度, 量纲: 弧度
		private double m_vSiteLat = 0;
		// 海拔高度, 量纲: 米
		private double m_vSiteAlt = 0;

		public AstroArith()
		{
			m_vSiteLgt = 120 * GtoR;
			m_vSiteLat = 40 * GtoR;
			m_vSiteAlt = 1000;

			m_matPN = new double[3, 3];
		}

		public AstroArith(double vSiteLgt, double vSiteLat, double vSiteAlt)
		{
			m_vSiteLgt = vSiteLgt;
			m_vSiteLat = vSiteLat;
			m_vSiteAlt = vSiteAlt;

			m_matPN = new double[3, 3];
		}

		public void SetGeoSite(double vSiteLgt, double vSiteLat, double vSiteAlt)
		{
			m_vSiteLgt = vSiteLgt;
			m_vSiteLat = vSiteLat;
			m_vSiteAlt = vSiteAlt;
		}

		/*!
		 * \fn double frac(double x)
		 * \brief 计算一个实数的小数部分
		 * \param[in] 待计算实数
		 * \return
		 * 实数的小数部分
		 **/
		public double frac(double x)
		{
			return x - Math.Floor(x);
		}

		/*!
		 * \fn double ModifiedJulianDay(int nYear, int nMonth, int nDay, double vHour)
		 * \brief 计算修正儒略日
		 * \param[in] nYear 年 -- 公元
		 * \param[in] nMonth 月
		 * \param[in] nDay   日
		 * \param[in] vHour  时
		 **/
		public double ModifiedJulianDay(int nYear, int nMonth, int nDay, double vHour)
		{
			double jDay;
			long A, B, C;

			A = 10000 * nYear + 100 * nMonth + nDay;
			if (nMonth <= 2)
			{
				nMonth += 12;
				nYear--;
			}

			if (A <= 15821004.1) B = (nYear + 4716) / 4 - 1179 - 2;
			else B = nYear / 400 - nYear / 100 + nYear / 4;
			A = 365 * nYear - 679004;
			C = (long)(30.6001 * (nMonth + 1));
			jDay = A + B + C + nDay + vHour / 24;

			return jDay;
		}

		public double Epoch2JD(double epoch)
		{
			double mjd;
			int nYear, nMonth, nDay;

			nYear = (int)epoch;
			epoch = (epoch - nYear) * 12 + 1;
			nMonth = (int)epoch;
			epoch = (epoch - nMonth) * 30;
			nDay = (int)epoch;
			if (nDay == 0) nDay = 1;
			else if (nDay > 30) nDay = 30;
			mjd = ModifiedJulianDay(nYear, nMonth, nDay, 0);
			return ((mjd - 51544.5) / 36525);
		}

		public double sind(double d)
		{
			return Math.Sin(d * GtoR);
		}

		public double cosd(double d)
		{
			return Math.Cos(d * GtoR);
		}

		public double reduce(double x, double period)
		{
			return x - Math.Floor(x / period) * period;
		}

		public double reduce_angle(double x)
		{
			return reduce(x, PI360);
		}

		/*!
		 * \fn void PMatEqu(double T1, double T2)
		 * \brief 计算T1->T2岁差转换系数
		 * \param[in] T1 儒略世纪
		 * \param[in] T2 儒略世纪
		 **/
		public void PMatEqu(double T1, double T2)
		{
			double dt = T2 - T1;
			double Z, Zeta, Theta;
			const double sec = 3600;
			double c1, c2, c3;	// Z, Zeta, Theta对应的余弦项
			double s1, s2, s3;	// Z, Zeta, Theta对应的正弦项

			Zeta = ((2306.2181 + (1.39656 - 0.000139 * T1) * T1)
				+ ((0.30188 - 0.000345 * T1) + 0.017998 * dt) * dt) * dt / sec;
			Z = Zeta + ((0.7928 + 0.000411 * T1) + 0.000205 * dt) * dt * dt / sec;
			Theta = ((2004.3109 - (0.8533 + 0.000217 * T1) * T1)
				- ((0.42665 + 0.000217 * T1) + 0.041833 * dt) * dt) * dt / sec;

			c1 = cosd(Z);
			c2 = cosd(Theta);
			c3 = cosd(Zeta);
			s1 = sind(Z);
			s2 = sind(Theta);
			s3 = sind(Zeta);

			m_matPN[0, 0] = -s1 * s3 + c1 * c2 * c3;
			m_matPN[0, 1] = -s1 * c3 - c1 * c2 * s3;
			m_matPN[0, 2] = -c1 * s2;
			m_matPN[1, 0] = c1 * s3 + s1 * c2 * c3;
			m_matPN[1, 1] = c1 * c3 - s1 * c2 * s3;
			m_matPN[1, 2] = -s1 * s2;
			m_matPN[2, 0] = s2 * c3;
			m_matPN[2, 1] = -s2 * s3;
			m_matPN[2, 2] = c2;
		}

		/*!  \fn void DeltaMean2True(double JC, double X, double Y, double Z, ref double dx, ref double dy, ref double dz)
		 *   \brief 真坐标与平坐标的差值
		 *   \param[in] JC 儒略世纪
		 *   \param[in] X 笛卡尔(直角)坐标系X轴坐标
		 *   \param[in] Y 笛卡尔(直角)坐标系Y轴坐标
		 *   \param[in] Z 笛卡尔(直角)坐标系Z轴坐标
		 *   \param[out] dx X轴差值
		 *   \param[out] dy Y轴差值
		 *   \param[out] dz Z轴差值
		 **/
		public void DeltaMean2True(double JC, double X, double Y, double Z, ref double dx, ref double dy, ref double dz)
		{
			double LS;
			double D;
			double F;
			double N;
			double EPS;
			double DPSI;
			double DEPS;
			double c;
			double s;

			LS = PI360 * frac(0.993133 + 99.997306 * JC);	// 平近点角
			D = PI360 * frac(0.827362 + 1236.853087 * JC);	// 
			F = PI360 * frac(0.259089 + 1342.227826 * JC);
			N = PI360 * frac(0.347346 - 5.372447 * JC);
			EPS = 0.4090928 - 0.00022696 * JC;
			DPSI = (-17.2 * Math.Sin(N) - 1.319 * Math.Sin(2 * (F - D + N))
				- 0.227 * Math.Sin(2 * (F + N)) + 0.206 * Math.Sin(2 * N) + 0.143 * Math.Sin(LS)) / 3600 * GtoR;
			DEPS = (9.203 * Math.Cos(N) + 0.574 * Math.Cos(2 * (F - D + N))
				+ 0.098 * Math.Cos(2 * (F + N)) - 0.09 * Math.Cos(2 * N)) / 3600 * GtoR;
			c = DPSI * Math.Cos(EPS);
			s = DPSI * Math.Sin(EPS);
			dx = -(c * Y + s * Z);
			dy = (c * X - DEPS * Z);
			dz = (s * X + DEPS * Y);
		}

		/*! \fn void EqMean2True(double JC, ref double X, ref double Y, ref double Z)
		 *  \brief 从平坐标转换为真坐标
		 *  \param[in] JC 儒略世纪
		 *  \param X X轴坐标
		 *  \param Y Y轴坐标
		 *  \param Z Z轴坐标
		 **/
		public void EqMean2True(double JC, ref double X, ref double Y, ref double Z)
		{
			double dx = 0;
			double dy = 0;
			double dz = 0;

			DeltaMean2True(JC, X, Y, Z, ref dx, ref dy, ref dz);
			X += dx;
			Y += dy;
			Z += dz;
		}

		/*! \fn void Aberrat(double JC, ref double VX, ref double VY, ref double VZ)
		 *  \brief 赤道坐标中, 地球运动速度矢量
		 *  \param[in] JC 儒略世纪
		 *  \param[out] VX X轴速度, 单位：光速
		 *  \param[out] VY Y轴速度, 单位: 光速
		 *  \param[out] VZ Z轴速度, 单位: 光速
		 **/
		public void Aberrat(double JC, ref double VX, ref double VY, ref double VZ)
		{
			double l;
			double CL;

			l = PI360 * frac(0.27908 + 100.00214 * JC);
			CL = Math.Cos(l);
			VX = -0.0000994 * Math.Sin(l);
			VY = 0.0000912 * CL;
			VZ = 0.0000395 * CL;
		}

		/*! \fn void Rect2Sphere(double x, double y, double z, ref double r, ref double alpha, ref double delta)
		 *  \brief 直角坐标转换为球面坐标
		 *  \param[in]  x     X坐标
		 *  \param[in]  y     Y坐标
		 *  \param[in]  z     Z坐标
		 *  \param[out] r     距离
		 *  \param[out] alpha 基平面内角度, 量纲: 弧度
		 *  \param[out] delta 与基平面的夹角, 量纲: 弧度
		 **/
		public void Rect2Sphere(double x, double y, double z, ref double r, ref double alpha, ref double delta)
		{
			r = Math.Sqrt(x * x + y * y + z * z);
			alpha = Math.Atan2(y, x);
			delta = Math.Atan2(z, Math.Sqrt(x * x + y * y));
		}

		/*! \fn void Sphere2Rect(double r, double alpha, double delta, ref double x, ref double y, ref double z)
		 *  \brief 球面坐标转换为直角坐标
		 *  \param[in] r     距离
		 *  \param[in] alpha 基平面内角度, 量纲: 弧度
		 *  \param[in] delta 与基平面的夹角, 量纲: 弧度
		 *  \param[out] x    X坐标
		 *  \param[out] y    Y坐标
		 *  \param[out] z    Z坐标
		 **/
		public void Sphere2Rect(double r, double alpha, double delta, ref double x, ref double y, ref double z)
		{
			x = r * Math.Cos(delta) * Math.Cos(alpha);
			y = r * Math.Cos(delta) * Math.Sin(alpha);
			z = r * Math.Sin(delta);
		}

		/*!
		 * \fn void PN_Matrix(double T1, double T2)
		 * \brief 计算T1->T2历元转换矩阵
		 * \param[in] T1 儒略世纪
		 * \param[in] T2 儒略世纪
		 **/
		public void PN_Matrix(double T1, double T2)
		{
			PMatEqu(T1, T2);
			EqMean2True(T2, ref m_matPN[0, 0], ref m_matPN[1, 0], ref m_matPN[2, 0]);
			EqMean2True(T2, ref m_matPN[0, 1], ref m_matPN[1, 1], ref m_matPN[2, 1]);
			EqMean2True(T2, ref m_matPN[0, 2], ref m_matPN[1, 2], ref m_matPN[2, 2]);
		}

		/*! \fn void PError(ref double X, ref double Y, ref double Z)
		 *  \brief 计算岁差带来的偏差
		 *  \param X X轴坐标
		 *  \param Y Y轴坐标
		 *  \param Z Z轴坐标
		 **/
		public void PError(ref double X, ref double Y, ref double Z)
		{
			double U, V, W;

			U = m_matPN[0, 0] * X + m_matPN[0, 1] * Y + m_matPN[0, 2] * Z;
			V = m_matPN[1, 0] * X + m_matPN[1, 1] * Y + m_matPN[1, 2] * Z;
			W = m_matPN[2, 0] * X + m_matPN[2, 1] * Y + m_matPN[2, 2] * Z;

			X = U;
			Y = V;
			Z = W;
		}

		/*! \fn void Epoch2Actual(double T0, double ra_e, double de_e, double T1, ref double ra_a, ref double de_a)
		 *  \brief 从指定历元坐标转换为当前历元坐标
		 *  \param[in]  T0   指定儒略世纪
		 *  \param[in]  ra_a 指定历元下的赤经, 量纲: 弧度
		 *  \param[in]  de_a 指定历元下的赤纬, 量纲: 弧度
		 *  \param[in]  T1   儒略世纪
		 *  \param[out] ra_e 当前指定历元下的赤经, 量纲: 弧度
		 *  \param[out] de_e 当前指定历元下的赤纬, 量纲: 弧度
		 **/
		public void Epoch2Actual(double T0, double ra_e, double de_e, double T1, ref double ra_a, ref double de_a)
		{
			double r = 0;
			double X = 0;
			double Y = 0;
			double Z = 0;
			double VX = 0;
			double VY = 0;
			double VZ = 0;

			PN_Matrix(T0, T1);
			Aberrat(T1, ref VX, ref VY, ref VZ);
			Sphere2Rect(1.0, ra_e, de_e, ref X, ref Y, ref Z);
			PError(ref X, ref Y, ref Z);
			X += VX;
			Y += VY;
			Z += VZ;
			Rect2Sphere(X, Y, Z, ref r, ref ra_a, ref de_a);
			ra_a = reduce_angle(ra_a);
		}

		public double GetEpoch(int nYear, int nMonth, int nDay, double vHour)
		{
			return nYear + ((nMonth - 1) * 30.3 + nDay + vHour / 24) / 365;
		}

		/*! \fn void HH2HMS(double hh, ref int hour, ref int minute, ref double second)
		 *  \brief 将小时转换为时分秒
		 *  \parma[in] hh 小时数
		 *  \param[out] hour 小时
		 *  \param[out] minute 分钟
		 *  \param[out] second 秒
		 **/
		public void HH2HMS(double hh, ref int hour, ref int minute, ref double second)
		{
			hour = (int)hh;
			hh = (hh - hour) * 60;
			minute = (int)hh;
			hh = (hh - minute) * 60;
			second = hh;
		}

		/*! \fn void DD2DMS(double dd, ref int degree, ref int minute, ref double second)
		 *  \brief 将角度转换为度分秒
		 *  \param[in] dd 角度数
		 *  \param[out] degree 角度
		 *  \param[out] minute 分钟
		 *  \param[out] second 秒
		 *  \param[out] sign 符号标志
		 **/
		public void DD2DMS(double dd, ref int degree, ref int minute, ref double second, ref string sign)
		{
			if (dd < 0)
			{
				sign = "-";
				dd = -dd;
			}
			else sign = "+";
			degree = (int)dd;
			dd = (dd - degree) * 60;
			minute = (int)dd;
			second = (dd - minute) * 60;
		}

		/*!
		 * \fn bool StringHour2Double(string strHour, ref double dblHour)
		 * \brief 将字符串格式的时间转换为实数
		 * \param[in] strHour 字符串时间
		 * \param[out] dblHour 实数时间, 量纲: 小时
		 * \return
		 * 如果strHour格式合法则进行转换并返回true, 否则返回false
		 **/
		public bool StringHour2Double(string strHour, ref double dblHour)
		{
			int nLen = strHour.Length;
			string strPart = "";
			string strHH = "0";
			string strMM = "0";
			string strSS = "0";
			int part = 0; // 定位字符归属, 初始归属于小时部分
			char ch;
			int cnt = 0;
			int dotcnt = 0;	// 小数点计数

			strHour = strHour.Trim();
			for (int i = 0; i < nLen; i++) // 遍历字符串
			{
				ch = strHour[i];
				if (ch >= '0' && ch <= '9')// 是数字
				{
					if (dotcnt > 0)
					{
						strPart += ch; // 出现小数点后不再判断数据长度
						cnt++;
					}
					else
					{
						cnt++;
						if (part < 2 && cnt > 2) // 在时分区出现了连续的多于两个数字
						{
							if (part == 0) strHH = strPart;
							else if (part == 1) strMM = strPart;
							part++;
							cnt = 1;
							strPart = "";
						}
						strPart += ch;
					}
				}
				else if (ch == ' ' || ch == ':') // 是分隔符
				{
					if (dotcnt > 0) return false;	// 出现小数点后不允许再使用分隔符
					if (cnt != 0)// 分隔符前数字长度不是标准的长度, 例: 小时部分为两个字符, 分钟部分为两个字符
					{
						if (part == 0) strHH = strPart;
						else if (part == 1) strMM = strPart;
						else return false;
						// 修改监测量
						part++;
						cnt = 0;
						strPart = "";
					}
				}
				else if (ch == '.')// 判断是否小数点
				{
					strPart += ch;
					cnt++;
					if (++dotcnt > 1) return false;
				}
				else return false;
			}
			if (part == 0) strHH = strPart;
			else if (part == 1) strMM = strPart;
			else strSS = strPart;

			dblHour = Convert.ToDouble(strHH) + Convert.ToDouble(strMM) / 60.0 + Convert.ToDouble(strSS) / 3600.0;
			return dblHour <= 24.0;
		}

		/*!
		 * \fn bool StringDegree2Double(string strDegree, ref double dblDegree)
		 * \brief 将字符串格式的角度转换为实数
		 * \param[in] strDegree 字符串时间
		 * \param[out] dblDegree 实数时间, 量纲: 角度
		 * \return
		 * 如果strDegree格式合法则进行转换并返回true, 否则返回false
		 **/
		public bool StringDegree2Double(string strDegree, ref double dblDegree)
		{
			int nLen = strDegree.Length;
			string strPart = "0";
			string strDD = "0";
			string strMM = "0";
			string strSS = "0";
			int part = 0; // 定位字符归属, 初始归属于小时部分
			char ch;
			int cnt = 0;
			int dotcnt = 0;	// 小数点计数
			double sign = 1;

			strDegree = strDegree.Trim();
			for (int i = 0; i < nLen; i++) // 遍历字符串
			{
				ch = strDegree[i];
				if (ch >= '0' && ch <= '9')// 是数字
				{
					if (dotcnt > 0)
					{
						strPart += ch; // 出现小数点后不再判断数据长度
						cnt++;
					}
					else
					{
						cnt++;
						if (part < 2 && cnt > 2) // 在时分区出现了连续的多于两个数字
						{
							if (part == 0) strDD = strPart;
							else if (part == 1) strMM = strPart;
							part++;
							cnt = 0;
							strPart = "";
						}
						strPart += ch;
					}
				}
				else if (ch == ' ' || ch == ':') // 是分隔符
				{
					if (dotcnt > 0) return false;	// 出现小数点后不允许再使用分隔符
					if (cnt != 0)// 分隔符前数字长度不是标准的长度, 例: 角度部分为两个字符, 分钟部分为两个字符
					{
						if (part == 0) strDD = strPart;
						else if (part == 1) strMM = strPart;
						else return false;
						// 修改监测量
						part++;
						cnt = 0;
						strPart = "";
					}
				}
				else if (ch == '.')// 判断是否小数点
				{
					strPart += ch;
					cnt++;
					if (++dotcnt > 1) return false;
				}
				else if (ch == '+' || ch == '-')
				{
					if (ch == '-') sign = -1;
				}
				else return false;
			}
			if (part == 0) strDD = strPart;
			else if (part == 1) strMM = strPart;
			else strSS = strPart;

			dblDegree = sign * (Convert.ToDouble(strDD) + Convert.ToDouble(strMM) / 60.0 + Convert.ToDouble(strSS) / 3600.0);
			return true;
		}

		/*! \fn void Eq2AltAzi(double ha, double dec, double &azi, double &alt)
		 *  \brief 赤道坐标转换为地平坐标
		 *  \param[in]  ha  时角, 量纲: 弧度
		 *  \param[in]  dec 赤纬, 量纲: 弧度
		 *  \param[out] azi 方位角, 量纲: 弧度, 北零点
		 *  \param[out] alt 俯仰角, 量纲: 弧度
		 **/
		public void Eq2AltAzi(double ha, double dec, ref double azi, ref double alt)
		{
			double sinz, cosz;
			double sina, cosa;

			sinz = Math.Sin(m_vSiteLat) * Math.Sin(dec) + Math.Cos(m_vSiteLat) * Math.Cos(dec) * Math.Cos(ha);
			cosz = Math.Sqrt(1 - sinz * sinz);
			alt = Math.Atan2(sinz, cosz);

			sina = Math.Cos(dec) * Math.Sin(ha);
			cosa = Math.Sin(m_vSiteLat) * Math.Cos(dec) * Math.Cos(ha) - Math.Cos(m_vSiteLat) * Math.Sin(dec);
			azi = reduce_angle(Math.Atan2(sina, cosa) + PI);
		}

		/*!
		 * \fn bool IsAboveLimit(double ha, double dec)
		 * \brief 判断赤道坐标是否在水平限位高度之上. 若在之上则可观测
		 * \param[in] ha    时角, 量纲: 弧度
		 * \param[in] dec   赤纬, 量纲: 弧度
		 * \param[in] limit 高度限位, 量纲: 弧度
		 * \return
		 * 若在水平限位高度之上则返回true
		 **/
		public bool IsAboveLimit(double ha, double dec, double limit)
		{
			double azi = 0;
			double alt = 0;

			Eq2AltAzi(ha, dec, ref azi, ref alt);
			return (alt > limit);
		}
	}
}
