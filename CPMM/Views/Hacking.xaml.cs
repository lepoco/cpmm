// This Source Code Form is subject to the terms of the GNU GPL-3.0.
// If a copy of the GPL was not distributed with this file, You can obtain one at https://www.gnu.org/licenses/gpl-3.0.en.html.
// Copyright (C) 2022 Leszek Pomianowski and CPMM Contributors.
// All Rights Reserved.

#nullable enable

using CPMM.Code;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CPMM.Views
{
    /// <summary>
    /// Event send from <see cref="Hacking"/>.
    /// </summary>
    public delegate Task HackingEvent(object sender);

    /// <summary>
    /// Interaction logic for Hacking.xaml
    /// </summary>
    public partial class Hacking : Window
    {
        public struct Configuration
        {
            public bool LockUserControl { get; set; } = true;

            public bool TopMost { get; set; } = true;

            public bool ShowFullIntro { get; set; } = true;

            public Configuration()
            {
            }
        }

        private readonly string _randomChars =
            "eWÝ4ÀT¬ý´gGßÀ´ã:Æï¦>d¢&RrÂ6¡ÞßíK¦*kj¿é)ÂÛÝÎ2e>)VÎzV¤q3ìÖdøSØîÅR5£T¼¾§p$«©VWÒ½4u¨·ª$À±àDtqj@ç.Uð6Tt3ÂD¤xW,µP.ß%iýºèKwëåÍÖcãw$2t];7^7µÐfþwÜ4ò(ðÔ4{Ë£ÛWÙHè¡í¡nc©3¯ã2·y´Ç?îÖï.C(Ý¦B±Cóº)¥VpnjÆ:Íµ¦";

        private readonly Random _random = new();

        private bool _allowClosing = false;

        private WindowState _allowedWindowState = WindowState.Normal;

        private string _currentMessage = String.Empty;

        /// <summary>
        /// Hacking configuration.
        /// </summary>
        public Configuration Config { get; set; } = new();

        /// <summary>
        /// Event triggered when hacking is finished.
        /// </summary>
        public HackingEvent? OnFinished;

        public Hacking()
        {
            InitializeComponent();

            if (Config.TopMost)
                Topmost = true;

            if (Config.LockUserControl)
                LockUserInteractions();

            Run();
        }

        private async void Run()
        {
            await Task.Run(HackingMessagesLoop);

            await Finish();

            Application.Current.Dispatcher.Invoke(Close);
        }

        private async Task<bool> Finish()
        {
            if (OnFinished != null)
                await OnFinished(this);

            _allowClosing = true;

            return true;
        }

        private async Task<bool> Write(string message, bool pause = true, int time = 200)
        {
            if (pause)
                await Task.Delay(time);

            _currentMessage += message;

            await Dispatch(() =>
            {
                ConsoleTextBlock.Text = _currentMessage;
                ConsoleScrollViewer.ScrollToEnd();
            });

            return true;
        }

        private async Task<bool> Clear()
        {
            _currentMessage = String.Empty;

            await Dispatch(() =>
            {
                ConsoleTextBlock.Text = _currentMessage;
                ConsoleScrollViewer.ScrollToEnd();
            });

            return true;
        }

        private async Task<bool> DrawSpinner(int lenght = 20)
        {
            string[] loader = { "-", "\\", "|", "/", "-", "\\", "|", "/" };
            int c = 0;

            _currentMessage += "/";

            for (int i = 0; i < lenght; i++)
            {
                await Task.Delay(100);

                await Dispatch(() =>
                {
                    _currentMessage = _currentMessage.Remove(_currentMessage.Length - 1);
                    _currentMessage += loader[c];

                    ConsoleTextBlock.Text = _currentMessage;
                });

                c++;

                if (c > loader.Length - 1)
                    c = 0;
            }

            return true;
        }

        private async Task<bool> DrawBlinker(int length = 10)
        {
            _currentMessage += " ";

            for (int i = 0; i < length; i++)
            {
                await Task.Delay(300);

                await Dispatch(() =>
                {
                    if (i % 2 == 0)
                    {
                        _currentMessage = _currentMessage.Remove(_currentMessage.Length - 1);
                        _currentMessage += " ";
                    }
                    else
                    {
                        _currentMessage = _currentMessage.Remove(_currentMessage.Length - 1);
                        _currentMessage += "_";
                    }

                    ConsoleTextBlock.Text = _currentMessage;
                });
            }

            _currentMessage = _currentMessage.Remove(_currentMessage.Length - 1);

            return true;
        }

        private string GenerateRandomString(int length)
        {
            return new string(Enumerable.Repeat(_randomChars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private async Task<bool> SwapForeground(string type = "")
        {
            return await Dispatch(() =>
            {
                switch (type)
                {
                    case "red":
                        ConsoleTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 97, 91));
                        break;

                    default:
                        RootBorder.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                        break;
                }
            });
        }

        private async Task<bool> SwapBackground(string type = "")
        {
            return await Dispatch(() =>
            {
                switch (type)
                {
                    case "gradient":
                        RootBorder.Background = new LinearGradientBrush(Color.FromRgb(8, 7, 15),
                            Color.FromRgb(23, 6, 10), new Point(0.5, 0), new Point(0.5, 1));
                        break;

                    default:
                        RootBorder.Background = new SolidColorBrush(Color.FromRgb(33, 33, 33));
                        break;
                }
            });
        }

        private async Task<bool> ShowErrorWindows()
        {
            await Dispatch(async () =>
            {
                var w1 = new Window
                {
                    Title = "AarSvc - TASK FAILED SUCCESSFULLY",
                    Width = 600,
                    Height = 300,
                    Background = Brushes.Black,
                    Foreground = Brushes.Red
                };
                var w2 = new Window
                {
                    Title = "ClipSVC - TASK FAILED SUCCESSFULLY",
                    Width = 600,
                    Height = 300,
                    Background = Brushes.Black,
                    Foreground = Brushes.Red
                };
                var w3 = new Window
                {
                    Title = "CryptSvc - TASK FAILED SUCCESSFULLY",
                    Width = 600,
                    Height = 300,
                    Background = Brushes.Black,
                    Foreground = Brushes.Red
                };
                var w4 = new Window
                {
                    Title = "DevMgmtService - TASK FAILED SUCCESSFULLY",
                    Width = 600,
                    Height = 300,
                    Background = Brushes.Black,
                    Foreground = Brushes.Red
                };
                var w5 = new Window
                {
                    Title = "IKEEXT - TASK FAILED SUCCESSFULLY",
                    Width = 600,
                    Height = 300,
                    Background = Brushes.Black,
                    Foreground = Brushes.Red
                };
                var w6 = new Window
                {
                    Title = "KeyIso - TASK FAILED SUCCESSFULLY",
                    Width = 600,
                    Height = 300,
                    Background = Brushes.Black,
                    Foreground = Brushes.Red
                };

                w1.Content = new TextBlock()
                {
                    Text = "TASK FAILED SUCCESSFULLY",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 18
                };
                w2.Content = new TextBlock()
                {
                    Text = "TASK F$ILED SUCCESSFULLY\nEXPLORER.EXE NOT RESPONDING",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 18
                };
                w3.Content = new TextBlock()
                {
                    Text = "TASK F$ILE# SUCCE##FULLY\nTASK SCHEDULER CORRUPTED",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 18
                };
                w4.Content = new TextBlock()
                {
                    Text = "TASK FAILe$ SU##ESSFUL(Y~~\nMULTI LAYER ELEVATION ACCESS",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 18
                };
                w5.Content = new TextBlock()
                {
                    Text = "!A3K FAILe$ $U##ESSFUL(Y~~Y\nSHELL32.DLL COM DESTROYED",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 18
                };
                w6.Content = new TextBlock()
                {
                    Text = "!A3K FAILe$ $U##E$$FUL(Y~~Y\nMEMORY VIOLATION AT ADDRESS: 0x00000069",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 18
                };

                w1.Show();
                await Task.Delay(200);

                w2.Show();
                await Task.Delay(200);

                w3.Show();
                await Task.Delay(200);

                w4.Show();
                await Task.Delay(200);

                w5.Show();
                await Task.Delay(200);

                w6.Show();
                await Task.Delay(200);

                await Task.Delay(200);

                w1.Close();
                w2.Close();
                w3.Close();
                w4.Close();
                w5.Close();
                w6.Close();
            });

            return true;
        }

        private void LockUserInteractions()
        {
            Closing += Hacking_Closing;
            StateChanged += Hacking_StateChanged;
        }

        private void Hacking_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_allowClosing)
                e.Cancel = true;
        }

        private void Hacking_StateChanged(object? sender, EventArgs e)
        {
            switch (_allowedWindowState)
            {
                case WindowState.Normal:
                    WindowState = WindowState.Normal;
                    break;
                case WindowState.Maximized:
                    WindowState = WindowState.Maximized;
                    break;
            }
        }

        private async Task<bool> Dispatch(Action callback)
        {
            await Application.Current.Dispatcher.InvokeAsync(callback);

            return true;
        }

        private async Task<bool> ShowBlueScreenOfDeath()
        {
            return await Dispatch(() => { BlueScreenOfDeath.Visibility = Visibility.Visible; });
        }

        private async Task<bool> HackingMessagesLoop()
        {
            await Write("\n===================================================================", false);
            await Write("\n  ARASAKA INC アラサカ社", false);
            await Write("\n  NCPD Illegal Cyber Implant Detection System", false);
            await Write("\n===================================================================", false);
            await Write("\n  v. " + GH.Version + ", 2077-03-01", false);

            await Write("\n\nPermitted locations:", false);
            await Write("\nSapporo, Osaka, Kyoto, Fukuoka, Hong Kong, Taipei, Shanghai, Honolulu, Nairobi", false);

            await Write(
                "\n\nWARNING: CONFIDENTIALITY NOTICE - The information enclosed with this application are the private, confidential property of the sender, and the material is privileged communication intended solely for the individual indicated. If you are not the intended recipient, you are notified that any review, disclosure, copying, distribution, or the taking of any other action relevant to the contents of this transmission are strictly prohibited. If you have received this transmission in error, please notify us immediately at arasaka@nightcity.love",
                false);
            await Task.Delay(400);

            await Write("\n\nAuthorized by: " + Environment.UserName + " (" + Environment.MachineName + ")", false);

            for (int i = 2; i < 5; i++)
            {
                await Write(
                    "\n\n[  ] Connecting to the Arasaka INC cloud, path - netdir://RSA8192.ftps.gate.arasaka.inc/node-" +
                    i + " ");
                await DrawSpinner();
                await Write("\n[OK] Done.");
            }

            await Write(
                "\n\n[  ] Connecting to the Arasaka INC cloud, path - netdir://RSA8192.ftps.gate.arasaka.inc/node-5 ");
            await DrawSpinner(10);
            await Write("\n[!!] ERROR!");
            await Write("\nConnection to the gateway could not be established");

            await Write("\nERROR");
            await Write("\nRROR");
            await Write("\n" + GenerateRandomString(15));
            await Write("\n RO   R");

            await Dispatch(() => { WindowStyle = WindowStyle.None; });

            await Write("\n" + GenerateRandomString(40));
            await Write("\n" + GenerateRandomString(120));
            await Write("\n" + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

            await Dispatch(() => { WindowStyle = WindowStyle.ThreeDBorderWindow; });

            await Write("\n" + GenerateRandomString(75));
            await Write("\n" + GenerateRandomString(120));

            await Dispatch(() => { WindowStyle = WindowStyle.None; });

            await Write("\n" + GenerateRandomString(120));
            await Write("\n" + GenerateRandomString(120));

            await Dispatch(() => { WindowStyle = WindowStyle.SingleBorderWindow; });

            await ShowErrorWindows();
            await Task.Delay(300);

            await Write("\nR E RO   R");
            await Write("\n" + GenerateRandomString(50));
            await Write("\n\nCRITICAL SYSTEM FAILURE");
            await Write("\n" + GenerateRandomString(30));

            await SwapBackground("gradient");
            await SwapForeground("red");

            await Task.Delay(100);

            await Write("\nObject reference not set to an instance of an object frame content");
            await Write("\n\n3wiedzmin3najlepszy" + GenerateRandomString(10), false);

            await SwapBackground();
            await SwapForeground();

            await Task.Delay(100);

            await Write("\n" + GenerateRandomString(120));
            await Write("\n ***** ***");
            await Write("\nSYSTEM FAILUTRE");
            await Write("\n" + GenerateRandomString(30));

            await SwapBackground("gradient");
            await SwapForeground("red");

            await Task.Delay(100);

            await Write("\n" + GenerateRandomString(200));
            await Write("\n" + GenerateRandomString(212));
            await Write("\n\n" + GenerateRandomString(10) + "lubieplacki" + GenerateRandomString(10), false);
            await Write("\nWIN KERNEL VIOLATED, SEGMENTATION FAULT");
            await Write(
                "fatal error: in \"kernel / basic_communication\": memory access violation at address: 0x00000024");
            await Write("\n W14D0M0 KT0, W14D0M0 C0, W14D0M0 K0G0", false);
            await Write("\n'Arasaka Manager.exe' (CoreCLR: clrhost): Loaded");

            await SwapBackground();
            await SwapForeground();

            await Task.Delay(100);

            await Write("\nIRQL_NOT_GREATER_OR_EQUAL");

            await SwapBackground("gradient");
            await SwapForeground("red");

            await Task.Delay(100);

            await Write(
                "\nWarning engine/environment.ws(30): Global native function 'EnableDebugOverlayFilter' was not exported from C++ code.");
            await Write("\nGlobal native function 'DebugSetEShowFlag' was not exported from C++ code.");
            await Write("\nMEMORY VIOLATED, SEGMENTATION FAULT");
            await Write("\n" + GenerateRandomString(200));
            await Write("\n" + GenerateRandomString(212));
            await Write("\n" + "W3 H4VE A C1TY T0 BU4N");

            await SwapBackground();
            await SwapForeground();

            await Task.Delay(300);

            //Main background
            await Clear();
            await SwapBackground("gradient");
            await SwapForeground("red");

            await Dispatch(() =>
            {
                Focus();

                _allowedWindowState = WindowState.Maximized;

                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
            });

            //ASCII ART
            await Write("\n                              **             ,", true, 30);
            await Write("\n                          ****               *", true, 30);
            await Write("\n         .                             ****         *     **,", true, 30);
            await Write("\n         *                 *          ****  *      **    **", true, 30);
            await Write("\n         *              * *       *   ****  *      **     *", true, 30);
            await Write("\n         *             *         *    ******   *  *", true, 30);
            await Write("\n         *             **.       **   **********    *             **", true, 30);
            await Write("\n          *             ***   * ************* *    *  * ,          .", true, 30);
            await Write("\n                  ,*     **   **************      **    **    ,", true, 30);
            await Write("\n             *      *    **   ***************   *****  ***   **", true, 30);
            await Write("\n              ***        *** ***********************  *** ,   *", true, 30);
            await Write("\n    *    ,  *  ,**    *  ***********,***   ***** ***  *       *", true, 30);
            await Write("\n     *    ****  **  * ** ********* *** ********  *****   **   *", true, 30);
            await Write("\n      *   *****  *   **** **  * *,******* *********** * ***", true, 30);
            await Write("\n            * , *  *********** ********* * ********** *  **", true, 30);
            await Write("\n  *      ,   ******** ****.************* *******    *.* *.   ", true, 30);
            await Write("\n         ** *      ,  ************* **** *****       *  * *", true, 30);
            await Write("\n      * ***    * * **  * ******** * ********        .*,****", true, 30);
            await Write("\n       ****.  **   *     *, ** *** ,* ****   *      ** *.**", true, 30);
            await Write("\n       ****.     * *. *   **********               ** ****", true, 30);
            await Write("\n       ****  *   * ,  ,   * ********* * **      *** ***** *", true, 30);
            await Write("\n     .  ,*  *  ***.  ,***  * ** ***  *** **********  *** * **", true, 30);
            await Write("\n             **  ******   *   * ******  ***       .*** *  ** *", true, 30);
            await Write("\n        **  .           ** **   ********  ***********  ***  * *", true, 30);
            await Write("\n         *          ******.**  * ***   **    ,**** ****  ,**.,*", true, 30);
            await Write("\n                  **,******** .***** **  ****** *,   ** ,   ,*", true, 30);
            await Write("\n                    ***   , **        *** ***, *        ****", true, 30);
            await Write("\n                  ***** *** * *  *****   ******", true, 30);
            await Write("\n               *******                     *******", true, 30);
            await Write("\n          *********    *                **    .**********", true, 30);
            await Write("\n                 ***   **, ***********  *.   **", true, 30);
            await Write("\n                  *********          *********", true, 30);
            await Write("\n                        *** ******** ***", true, 30);
            await Write("\n                          *************,", true, 30);
            await Write("\n                            **********", true, 30);

            await Task.Delay(500);

            // Show some scary folders
            await Write("\n\nStart scanning the host");

            string[] dirs = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            foreach (string path in dirs)
            {
                await Write("\n" + path, false);
                await Task.Delay(20);
            }

            dirs = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
            foreach (string path in dirs)
            {
                await Write("\n" + path, false);
                await Task.Delay(20);
            }

            dirs = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            foreach (string path in dirs)
            {
                await Write("\n" + path, false);
                await Task.Delay(20);
            }

            await Task.Delay(200);

            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
                await Write("\nKILLING A PROCESS: " + p.ProcessName.ToUpper() + (!String.IsNullOrEmpty(p.MainWindowTitle) ? " (" + p.MainWindowTitle + ")" : "") + "\nAllocated memory: " + p.PrivateMemorySize64 + "\n", false, 50);

            await Task.Delay(300);

            /// Spooky wiping! ...

            await Write("\n\n[  ] Wipe: C:\\Windows\\SysWOW6\\ ");
            await DrawSpinner(10);
            await Write("\n[OK] Done.");

            await Write("\n\n[  ] Wipe: C:\\Windows\\LiveKernelReports\\WATCHDOG\\ ");
            await DrawSpinner(10);
            await Write("\n[OK] Done.");

            await Write("\n\n[  ] Wipe: C:\\Windows\\Boot\\EFI\\ ");
            await DrawSpinner(10);
            await Write("\n[OK] Done.");

            await Write(
                "\n\n[  ] Wipe: " + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.ssh ");
            await DrawSpinner(10);
            await Write("\n[OK] Done.");

            await Write("\n\n$ ARASAKA INC アラサカ社 - NCPD Illegal Cyber Implant Detection System");
            await Write("\n$ All security systems have stopped working");


            if (Config.ShowFullIntro)
                await WriteAdditionalIntro();

            _allowClosing = true;
            return true;
        }

        private async Task<bool> WriteAdditionalIntro()
        {
            await Write("\nPartition formatting in progress ...");

            string[] allDrives = System.IO.Directory.GetLogicalDrives();
            for (int i = 0; i < allDrives.Length; i++)
            {
                await Write("\n[HDD~SDD~NVME]\n " + allDrives[i], false);
                await Write("\nFORMAT TO: ZFS\n\n");

                await Task.Delay(100);
            }

            await Write("\nSUCCESS");

            await Task.Delay(2000);

            await Clear();

            await Write("\n" + GenerateRandomString(120), true, 100);
            await Write("\n" + GenerateRandomString(120), true, 100);
            await Write("\n" + GenerateRandomString(120), true, 100);
            await Write("\n" + GenerateRandomString(120), true, 100);
            await Write("\n" + GenerateRandomString(120), true, 100);
            await Write("\n" + GenerateRandomString(120), true, 100);
            await Write("\n" + GenerateRandomString(120), true, 100);

            await Write(
                "\nfatal error: in \"kernel / restricted_area\": memory access violation at address: 0x00000029");

            await Write("\n.");
            await Write("\n.");
            await Write("\n.");
            await Write("\n.");
            await Write("\n _> ");
            await DrawBlinker(6);

            await Write("Hello V.");
            await Write("\n _> ");

            await DrawBlinker(8);
            await Write("I see you've decided to become a netrunner");
            await Write("\n _> ");

            await DrawBlinker(6);

            await Write("Your device is ready, please come inside...\n\n");

            await Task.Delay(500);

            for (int i = 0; i < 100; i++)
            {
                await Write("\n" + GenerateRandomString(15), false);

                await Task.Delay(30);
            }

            await ShowBlueScreenOfDeath();

            await Task.Delay(5000);

            return true;
        }
    }
}