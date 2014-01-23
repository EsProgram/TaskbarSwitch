using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;



namespace タスクバーの表示_非表示切り替え
{
	public partial class Form1 : Form
	{
		//WINAPI関数を使うためにdllファイルをインポートする属性
		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr FindWindow(string lpClassName, IntPtr lpWindowName);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		private const int SW_HIDE = 0;
		private const int SW_NORMAL = 1;

		//WINAPI関数で使用する構造体を宣言するには、構造体配置をWINAPIのものに一致させる必要があり、それは以下の属性で実現できる
		[StructLayout(LayoutKind.Sequential)]
		struct APPBARDATA
		{
			public int cbSize;
			public IntPtr hwnd;
			public uint uCallbackMessage;
			public uint uEdge;
			public Rectangle rc;
			public int lParam;
		};

		private const int ABM_SETSTATE = 10;
		private const int ABS_AUTOHIDE = 1;
		private const int ABS_ALWAYSONTOP = 2;

		[DllImport("shell32.dll")]
		static extern int SHAppBarMessage(int msg, ref APPBARDATA pbd);

		public Form1()
		{
			InitializeComponent();

			// コントロールボックスを非表示
			//this.ControlBox = false;

			// 最大表示
			//this.WindowState = FormWindowState.Maximized;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			// タスクバーを常に表示
			APPBARDATA abd = new APPBARDATA();
			abd.cbSize = Marshal.SizeOf(abd);
			abd.lParam = ABS_ALWAYSONTOP;
			SHAppBarMessage(ABM_SETSTATE, ref abd);

			// タスクバーを表示
			ShowWindow(FindWindow("Shell_TrayWnd", IntPtr.Zero), SW_NORMAL);

			// フォームを閉じる
			//this.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			//タスクバーを自動的に隠す
			APPBARDATA abd = new APPBARDATA();
			abd.cbSize = Marshal.SizeOf(abd);
			abd.lParam = ABS_AUTOHIDE;
			SHAppBarMessage(ABM_SETSTATE, ref abd);

			// タスクバーを非表示
			ShowWindow(FindWindow("Shell_TrayWnd", IntPtr.Zero), SW_HIDE);
			
			// フォームを閉じる
			//this.Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
