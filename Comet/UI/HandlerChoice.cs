using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace Comet.UI
{
    public partial class HandlerChoice : Form
    {
        public HandlerChoice()
        {
            InitializeComponent();
            Text += Preferences.SelectedDrive.Name;
            Icon = Properties.Resources._1;
            // Check if we are running as administrator, if yes, give the elevation button a shield
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                    BtnElevate.Visible = false;
                else
                    NativeMethods.SendMessage(BtnElevate.Handle, 0x160C, 0, 1);
            }
            ImageList il = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                // Get proper sizes for small icon lists on different DPIs, 49 = SM_CXSMICON, 50 = SM_CYSMICON
                ImageSize = new Size(NativeMethods.GetSystemMetrics(49), NativeMethods.GetSystemMetrics(50))
            };
            il.Images.Add(GetIconFromLib("imageres.dll", 2));
            Dictionary<string, int> IconListIndexForHint = new Dictionary<string, int>();
            long totalSpaceUsed = 0;
            for (int i = 0; i < Preferences.CleanupHandlers.Count; i++)
            {
                CleanupHandler oHandler = Preferences.CleanupHandlers[i];
                totalSpaceUsed += oHandler.BytesUsed;
                if (oHandler.IconHint != null)
                {
                    // Reuse already loaded icon
                    if (IconListIndexForHint.ContainsKey((string)oHandler.IconHint))
                    {
                        oHandler.IconHint = IconListIndexForHint[(string)oHandler.IconHint];
                    }
                    else
                    {
                        // Get icon from PE file, comma separates filename and ID
                        string[] splitHint = ((string)oHandler.IconHint).TrimStart('@').Split(',');
                        Icon obtIcon = GetIconFromLib(splitHint[0], int.Parse(splitHint[1]));
                        if (obtIcon != null)
                        {
                            il.Images.Add(obtIcon);
                            IconListIndexForHint[(string)oHandler.IconHint] = il.Images.Count - 1;
                            oHandler.IconHint = il.Images.Count - 1;
                        }
                        else
                        {
                            // Otherwise use placeholder icon
                            IconListIndexForHint[(string)oHandler.IconHint] = 0;
                            oHandler.IconHint = 0;
                        }
                    }
                }
                else
                    oHandler.IconHint = 0;

            }
            LblIntro.Text = string.Format(LblIntro.Text, NiceSize(totalSpaceUsed), Preferences.SelectedDrive.Name);
            LvwHandlers.SmallImageList = il;
            PtbDrive.Image = GetIconOfPath(Preferences.SelectedDrive.Letter, false).ToBitmap();
        }

        private void HandlerChoice_Load(object sender, EventArgs e)
        {
            Api.ReinstateHandlers(Preferences.CleanupHandlers, Preferences.SelectedDrive.Letter);
            for (int i = 0; i < Preferences.CleanupHandlers.Count; i++)
            {
                CleanupHandler oHandler = Preferences.CleanupHandlers[i];
                LvwHandlers.Items.Add(new ListViewItem(new[] { oHandler.DisplayName, NiceSize(oHandler.BytesUsed) }) { ImageIndex = (int)oHandler.IconHint, Tag = i, Checked = oHandler.StateFlag });
            }
            CalculateSelectedSavings();
            // Select first loaded handler so its description gets shown
            LvwHandlers.SelectedIndices.Add(0);
        }

        private void BtnElevate_Click(object sender, EventArgs e)
        {
            ProcessStartInfo proc = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = Application.ExecutablePath,
                Verb = "runas"
            };
            try { Process.Start(proc); } catch { return; }
            Close();
        }

        private void LvwHandlers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LvwHandlers.SelectedItems.Count > 0)
            {
                CleanupHandler oHandler = Preferences.CleanupHandlers[(int)LvwHandlers.SelectedItems[0].Tag];
                LblDesc.Text = oHandler.Description;
                if ((oHandler.Flags & HandlerFlags.HasSettings) == HandlerFlags.HasSettings)
                {
                    if (oHandler.ButtonText != null)
                        BtnAdvanced.Text = oHandler.ButtonText;
                    BtnAdvanced.Visible = true;
                }
                else
                    BtnAdvanced.Visible = false;
            }
        }

        private void LvwHandlers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CalculateSelectedSavings();
        }

        private void BtnAdvanced_Click(object sender, EventArgs e)
        {
            Preferences.CleanupHandlers[(int)LvwHandlers.SelectedItems[0].Tag].Instance.ShowProperties(Handle);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Api.DeactivateHandlers(Preferences.CleanupHandlers);
            Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult dlgRes = MessageBox.Show("Are you sure you want to permanently delete these files?", "Managed Disk Cleanup", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dlgRes == DialogResult.Yes)
            {
                int removedCount = 0;
                for (int i = 0; i < LvwHandlers.Items.Count; i++)
                {
                    CleanupHandler cHandler = Preferences.CleanupHandlers[(int)LvwHandlers.Items[i].Tag - removedCount];
                    if (!BtnElevate.Visible)
                    {
                        cHandler.StateFlag = LvwHandlers.Items[i].Checked;
                        Api.UpdateHandlerStateFlag(cHandler);
                    }
                    // Get rid of handlers that we won't need early on
                    if (!LvwHandlers.Items[i].Checked)
                    {
                        try { cHandler.Instance.Deactivate(out uint dummy); } catch { }
                        Marshal.FinalReleaseComObject(cHandler.Instance);
                        Preferences.CleanupHandlers.Remove(cHandler);
                        removedCount++;
                    }
                }
                Api.DeactivateHandlers(Preferences.CleanupHandlers);
                Preferences.ProcessPurge = true;
                Close();
            }
        }

        private void ScaleListViewColumns(ListView listview, SizeF factor)
        {
            foreach (ColumnHeader column in listview.Columns)
            {
                column.Width = (int)Math.Round(column.Width * factor.Width);
            }
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            base.ScaleControl(factor, specified);
            ScaleListViewColumns(LvwHandlers, factor);
        }

        private void CalculateSelectedSavings()
        {
            Preferences.CurrentSelectionSavings = 0;
            for (int i = 0; i < LvwHandlers.Items.Count; i++)
            {
                if (LvwHandlers.Items[i].Checked)
                    Preferences.CurrentSelectionSavings += Preferences.CleanupHandlers[(int)LvwHandlers.Items[i].Tag].BytesUsed;
            }
            LblSavingsNum.Text = NiceSize(Preferences.CurrentSelectionSavings);
        }

        private string NiceSize(long bytes)
        {
            string[] norm = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            int count = norm.Length - 1;
            decimal size = bytes;
            int x = 0;

            while (size >= 1000 && x < count)
            {
                size /= 1024;
                x++;
            }

            return string.Format($"{Math.Round(size, 2)} {norm[x]}", MidpointRounding.AwayFromZero);
        }

        private Icon GetIconFromLib(string file, int number)
        {
            NativeMethods.ExtractIconEx(file, number, out IntPtr bigIcon, out IntPtr smallIcon, 1);
            if (smallIcon != IntPtr.Zero)
            {
                NativeMethods.DestroyIcon(bigIcon);
                return Icon.FromHandle(smallIcon).AsDisposableIcon();
            }
            else if (bigIcon != IntPtr.Zero)
                return Icon.FromHandle(bigIcon).AsDisposableIcon();
            else
                return null;

        }

        private Icon GetIconOfPath(string strPath, bool bSmall)
        {
            SHFILEINFO info = new SHFILEINFO(true);
            //int cbFileInfo = Marshal.SizeOf(info);
            int cbFileInfo = 688 + IntPtr.Size;
            int flags = 0x110;
            if (bSmall)
                flags |= 1;

            NativeMethods.SHGetFileInfo(strPath, 256, out info, (uint)cbFileInfo, flags);
            return Icon.FromHandle(info.hIcon).AsDisposableIcon();
        }
    }
}
