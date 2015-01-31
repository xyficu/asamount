namespace ASAMount
{
    partial class FormAsaMount
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectMountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelMountRA = new System.Windows.Forms.Label();
            this.labelMountDEC = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.labelMountAz = new System.Windows.Forms.Label();
            this.labelMountAlt = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelMountST = new System.Windows.Forms.Label();
            this.labelMountStat = new System.Windows.Forms.Label();
            this.labelMountUT = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.labelMountDate = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxTargetDEC = new System.Windows.Forms.TextBox();
            this.textBoxTargetRA = new System.Windows.Forms.TextBox();
            this.buttonMountPark = new System.Windows.Forms.Button();
            this.buttonMountHome = new System.Windows.Forms.Button();
            this.buttonMountStop = new System.Windows.Forms.Button();
            this.buttonMountSlew = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelMountDrvStat = new System.Windows.Forms.Label();
            this.timerMntUpdateStat = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(601, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectMountToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // connectMountToolStripMenuItem
            // 
            this.connectMountToolStripMenuItem.Name = "connectMountToolStripMenuItem";
            this.connectMountToolStripMenuItem.Size = new System.Drawing.Size(184, 24);
            this.connectMountToolStripMenuItem.Text = "Connect Mount";
            this.connectMountToolStripMenuItem.Click += new System.EventHandler(this.connectMountToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 24);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(17, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Status:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(49, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "RA:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(41, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "DEC:";
            // 
            // labelMountRA
            // 
            this.labelMountRA.AutoSize = true;
            this.labelMountRA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMountRA.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMountRA.Location = new System.Drawing.Point(86, 57);
            this.labelMountRA.Name = "labelMountRA";
            this.labelMountRA.Size = new System.Drawing.Size(98, 18);
            this.labelMountRA.TabIndex = 4;
            this.labelMountRA.Text = "00:00:00.00";
            // 
            // labelMountDEC
            // 
            this.labelMountDEC.AutoSize = true;
            this.labelMountDEC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMountDEC.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMountDEC.Location = new System.Drawing.Point(86, 86);
            this.labelMountDEC.Name = "labelMountDEC";
            this.labelMountDEC.Size = new System.Drawing.Size(98, 18);
            this.labelMountDEC.TabIndex = 5;
            this.labelMountDEC.Text = "+00:00:00.0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(213, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Az:";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label111.Location = new System.Drawing.Point(205, 86);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(40, 16);
            this.label111.TabIndex = 7;
            this.label111.Text = "Alt:";
            // 
            // labelMountAz
            // 
            this.labelMountAz.AutoSize = true;
            this.labelMountAz.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMountAz.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMountAz.Location = new System.Drawing.Point(252, 57);
            this.labelMountAz.Name = "labelMountAz";
            this.labelMountAz.Size = new System.Drawing.Size(98, 18);
            this.labelMountAz.TabIndex = 8;
            this.labelMountAz.Text = "000:00:00.0";
            // 
            // labelMountAlt
            // 
            this.labelMountAlt.AutoSize = true;
            this.labelMountAlt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMountAlt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMountAlt.Location = new System.Drawing.Point(252, 86);
            this.labelMountAlt.Name = "labelMountAlt";
            this.labelMountAlt.Size = new System.Drawing.Size(90, 18);
            this.labelMountAlt.TabIndex = 9;
            this.labelMountAlt.Text = "00:00:00.0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelMountST);
            this.groupBox1.Controls.Add(this.labelMountStat);
            this.groupBox1.Controls.Add(this.labelMountUT);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.labelMountDate);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.labelMountRA);
            this.groupBox1.Controls.Add(this.labelMountAlt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelMountAz);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label111);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.labelMountDEC);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(553, 127);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mount Information";
            // 
            // labelMountST
            // 
            this.labelMountST.AutoSize = true;
            this.labelMountST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMountST.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMountST.Location = new System.Drawing.Point(427, 86);
            this.labelMountST.Name = "labelMountST";
            this.labelMountST.Size = new System.Drawing.Size(90, 18);
            this.labelMountST.TabIndex = 16;
            this.labelMountST.Text = "00:00:00.0";
            // 
            // labelMountStat
            // 
            this.labelMountStat.AutoSize = true;
            this.labelMountStat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMountStat.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMountStat.Location = new System.Drawing.Point(86, 28);
            this.labelMountStat.Name = "labelMountStat";
            this.labelMountStat.Size = new System.Drawing.Size(66, 18);
            this.labelMountStat.TabIndex = 15;
            this.labelMountStat.Text = "Stopped";
            // 
            // labelMountUT
            // 
            this.labelMountUT.AutoSize = true;
            this.labelMountUT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMountUT.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMountUT.Location = new System.Drawing.Point(427, 57);
            this.labelMountUT.Name = "labelMountUT";
            this.labelMountUT.Size = new System.Drawing.Size(90, 18);
            this.labelMountUT.TabIndex = 14;
            this.labelMountUT.Text = "00:00:00.0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(389, 86);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 16);
            this.label17.TabIndex = 13;
            this.label17.Text = "ST:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(389, 57);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 16);
            this.label16.TabIndex = 12;
            this.label16.Text = "UT:";
            // 
            // labelMountDate
            // 
            this.labelMountDate.AutoSize = true;
            this.labelMountDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMountDate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMountDate.Location = new System.Drawing.Point(427, 28);
            this.labelMountDate.Name = "labelMountDate";
            this.labelMountDate.Size = new System.Drawing.Size(90, 18);
            this.labelMountDate.TabIndex = 11;
            this.labelMountDate.Text = "2015-01-30";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(373, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 16);
            this.label14.TabIndex = 10;
            this.label14.Text = "Date:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxTargetDEC);
            this.groupBox2.Controls.Add(this.textBoxTargetRA);
            this.groupBox2.Controls.Add(this.buttonMountPark);
            this.groupBox2.Controls.Add(this.buttonMountHome);
            this.groupBox2.Controls.Add(this.buttonMountStop);
            this.groupBox2.Controls.Add(this.buttonMountSlew);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(12, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(553, 209);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control";
            // 
            // textBoxTargetDEC
            // 
            this.textBoxTargetDEC.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxTargetDEC.Location = new System.Drawing.Point(84, 63);
            this.textBoxTargetDEC.Name = "textBoxTargetDEC";
            this.textBoxTargetDEC.Size = new System.Drawing.Size(154, 26);
            this.textBoxTargetDEC.TabIndex = 9;
            this.textBoxTargetDEC.Text = "+00:00:00.0";
            // 
            // textBoxTargetRA
            // 
            this.textBoxTargetRA.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxTargetRA.Location = new System.Drawing.Point(84, 28);
            this.textBoxTargetRA.Name = "textBoxTargetRA";
            this.textBoxTargetRA.Size = new System.Drawing.Size(154, 26);
            this.textBoxTargetRA.TabIndex = 8;
            this.textBoxTargetRA.Text = "00:00:00.00";
            // 
            // buttonMountPark
            // 
            this.buttonMountPark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMountPark.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonMountPark.Location = new System.Drawing.Point(357, 64);
            this.buttonMountPark.Name = "buttonMountPark";
            this.buttonMountPark.Size = new System.Drawing.Size(85, 31);
            this.buttonMountPark.TabIndex = 7;
            this.buttonMountPark.Text = "Park";
            this.buttonMountPark.UseVisualStyleBackColor = true;
            this.buttonMountPark.Click += new System.EventHandler(this.buttonMountPark_Click);
            // 
            // buttonMountHome
            // 
            this.buttonMountHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMountHome.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonMountHome.Location = new System.Drawing.Point(357, 22);
            this.buttonMountHome.Name = "buttonMountHome";
            this.buttonMountHome.Size = new System.Drawing.Size(85, 31);
            this.buttonMountHome.TabIndex = 6;
            this.buttonMountHome.Text = "Home";
            this.buttonMountHome.UseVisualStyleBackColor = true;
            this.buttonMountHome.Click += new System.EventHandler(this.buttonMountHome_Click);
            // 
            // buttonMountStop
            // 
            this.buttonMountStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMountStop.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonMountStop.ForeColor = System.Drawing.Color.Red;
            this.buttonMountStop.Location = new System.Drawing.Point(264, 64);
            this.buttonMountStop.Name = "buttonMountStop";
            this.buttonMountStop.Size = new System.Drawing.Size(85, 31);
            this.buttonMountStop.TabIndex = 5;
            this.buttonMountStop.Text = "Stop";
            this.buttonMountStop.UseVisualStyleBackColor = true;
            this.buttonMountStop.Click += new System.EventHandler(this.buttonMountStop_Click);
            // 
            // buttonMountSlew
            // 
            this.buttonMountSlew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMountSlew.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonMountSlew.ForeColor = System.Drawing.Color.LimeGreen;
            this.buttonMountSlew.Location = new System.Drawing.Point(264, 22);
            this.buttonMountSlew.Name = "buttonMountSlew";
            this.buttonMountSlew.Size = new System.Drawing.Size(85, 31);
            this.buttonMountSlew.TabIndex = 4;
            this.buttonMountSlew.Text = "Slew";
            this.buttonMountSlew.UseVisualStyleBackColor = true;
            this.buttonMountSlew.Click += new System.EventHandler(this.buttonMountSlew_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(30, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 16);
            this.label11.TabIndex = 1;
            this.label11.Text = "DEC:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(38, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "RA:";
            // 
            // labelMountDrvStat
            // 
            this.labelMountDrvStat.AutoSize = true;
            this.labelMountDrvStat.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMountDrvStat.Location = new System.Drawing.Point(32, 410);
            this.labelMountDrvStat.Name = "labelMountDrvStat";
            this.labelMountDrvStat.Size = new System.Drawing.Size(217, 14);
            this.labelMountDrvStat.TabIndex = 12;
            this.labelMountDrvStat.Text = "Mount driver is not connected.";
            // 
            // timerMntUpdateStat
            // 
            this.timerMntUpdateStat.Tick += new System.EventHandler(this.timerMntUpdateStat_Tick);
            // 
            // FormAsaMount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(601, 445);
            this.Controls.Add(this.labelMountDrvStat);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormAsaMount";
            this.Text = "ASA Mount Controller";
            this.Load += new System.EventHandler(this.FormAsaMount_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectMountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelMountRA;
        private System.Windows.Forms.Label labelMountDEC;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.Label labelMountAz;
        private System.Windows.Forms.Label labelMountAlt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonMountPark;
        private System.Windows.Forms.Button buttonMountHome;
        private System.Windows.Forms.Button buttonMountStop;
        private System.Windows.Forms.Button buttonMountSlew;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelMountST;
        private System.Windows.Forms.Label labelMountStat;
        private System.Windows.Forms.Label labelMountUT;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label labelMountDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxTargetDEC;
        private System.Windows.Forms.TextBox textBoxTargetRA;
        private System.Windows.Forms.Label labelMountDrvStat;
        private System.Windows.Forms.Timer timerMntUpdateStat;
    }
}

