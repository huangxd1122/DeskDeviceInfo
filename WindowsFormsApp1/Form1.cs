﻿using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace DeskDeviceInfo
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;

            Load += Form1_Load;
            //FormClosing += Form1_FormClosing;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_APPWINDOW = 0x40000;
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                //cp.ExStyle &= (~WS_EX_APPWINDOW);    // 不显示在TaskBar
                cp.ExStyle |= WS_EX_TOOLWINDOW;      // 不显示在Alt-Tab
                return cp;
            }
        }

        /// <summary>
        /// 为当前程序创建后台启动文件到 开机启动文件夹
        /// </summary>
        static void CreateStartup()
        {
            try
            {

                RegistryKey RKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");

                // 如果没有ComputerCtrl关键字，会自动创建，如果有就更新键值
                object regobj = RKey.GetValue("DeskDeviceInfopro");
                if (regobj == null)
                {
                    string execPath = Application.ExecutablePath;

                    if (File.Exists(execPath) == true)
                    {
                        RKey.SetValue("DeskDeviceInfopro", execPath, RegistryValueKind.String);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                string c = Environment.GetEnvironmentVariable("computername");
                label1.Text = c;
                label2.Text = getIPAddress();

                this.TransparencyKey = Color.White;

                Rectangle rec = Screen.GetWorkingArea(this);

                int SW = rec.Width;

                Location = new Point(SW - Width, 50);

                CreateStartup();
            }
            catch (Exception)
            {

            }
        }

        private static string getIPAddress()
        {
            System.Net.IPAddress addr;    // 获得本机局域网IP地址   
            addr = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
            return addr.ToString();
        }


    }
}
