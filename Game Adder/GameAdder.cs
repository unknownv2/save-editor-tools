using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Horizon.Functions;
using XboxDataBaseFile;
using XContent;
using System.Threading;
using Horizon.Forms;
using System.IO;

namespace Horizon.PackageEditors.Game_Adder
{
    public partial class GameAdder : EditorControl
    {
        public GameAdder()
        {
            InitializeComponent();
            backupOnOpen = true;
            listAchievements.Columns[2].DefaultCellStyle.WrapMode
                = listAchievements.Columns[0].DefaultCellStyle.WrapMode
                = DataGridViewTriState.True;
            Server.GameAdder.populateTitleMeta();
        }

        private ProfileFile ProfileFile;
        private TitlePlayedAdder titleAdder;
        private List<TitlePlayedRecord> TitlePlayedRecords;
        private TitleAdder tadder;
        private XContent.PEC Pec;
        private bool isInProfile(string TID)
        {
            uint uTID = uint.Parse(TID, System.Globalization.NumberStyles.HexNumber);
            foreach (TitlePlayedRecord title in TitlePlayedRecords)
                if (title.TitleId == uTID)
                    return true;
            return false;
        }

        private void populateAchievements(string TID)
        {
            listAchievements.Rows.Clear();
            Server.GameAdder.TitleTemplate title = Server.GameAdder.getTitle(TID);
            tabAchievements.Text = title.Meta.TitleName;
            foreach (Server.GameAdder.TitleAchievement ach in title.Achievements)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(listAchievements);
                row.Cells[0].Value = ach.AchievementName;
                row.Cells[1].Value = ach.Credit.ToString();
                row.Cells[2].Value = ach.LockedDescription.Length == 0 ? SettingAsString(49) : ach.LockedDescription;
                listAchievements.Rows.Add(row);
            }
        }

        private void populateAwards(string TID)
        {
            listAwards.Rows.Clear();
            Server.GameAdder.TitleTemplate title = Server.GameAdder.getTitle(TID);
            if (title.Awards.Count > 0)
            {
                foreach (Server.GameAdder.TitleAward aw in title.Awards)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(listAwards);
                    row.Cells[0].Value = aw.Name;
                    row.Cells[1].Value = aw.UnawardedText.Length == 0 ? SettingAsString(82) : aw.UnawardedText;
                    listAwards.Rows.Add(row);
                }
                tabAwards.Visible = true;
            }
            else
                tabAwards.Visible = false;
        }

        private bool isInQueue(string TID)
        {
            for (int x = 0; x < listQueue.Rows.Count; x++)
                if ((string)listQueue.Rows[x].Tag == TID)
                    return true;
            return false;
        }

        private bool isInList(string TID)
        {
            for (int x = 0; x < listTitles.Rows.Count; x++)
                if ((string)listTitles.Rows[x].Cells[1].Value == TID)
                    return true;
            return false;
        }

        private void populateTitleList()
        {
            listTitles.Rows.Clear();
            txtSearch.Text = String.Empty;
            foreach (KeyValuePair<string, Server.GameAdder.TitleTemplate> title in Server.GameAdder.Titles)
                if ((!title.Value.Meta.Flagged || !cmdFilter.Checked) && !isInQueue(title.Value.Meta.TID) && !isInProfile(title.Value.Meta.TID))
                    listTitles.Rows.Add(Server.GameAdder.getGridRow(title.Value, ref listTitles));
            listTitles.Sort(listTitles.Columns[0], ListSortDirection.Ascending);
        }

        /*private void populateQueueList()
        {
            listQueue.Rows.Clear();
            foreach (KeyValuePair<string, Server.GameAdder.TitleTemplate> title in Server.GameAdder.Titles)
                if (!isInList(title.Value.Meta.TID) && !isInProfile(title.Value.Meta.TID))
                    listQueue.Rows.Add(Server.GameAdder.getPimpedOutGridRow(title.Value, ref listQueue));
            listQueue.Sort(listQueue.Columns[0], ListSortDirection.Ascending);
            updateQueueTab();
        }*/

        private void updateQueueTab()
        {
            tabQueue.Text = SettingAsString(18);
            if (listQueue.Rows.Count > 0)
                tabQueue.Text += String.Format(SettingAsString(29), listQueue.Rows.Count);
            rbPackageEditor.Refresh();
        }

        public override bool Entry()
        {
            Text = SettingAsString(196) + (Account == null ? "Unknown" : Account.Info.GamerTag);
            cancelText = SettingAsString(103);

            this.ProfileFile = new ProfileFile(this.Package, 0xfffe07d1);
            this.ProfileFile.Read();

            populateTitleRecords();

            titleAdder = new TitlePlayedAdder(ProfileFile);
            if ((this.Package.StfsContentPackage.GetDirectoryEntryIndex(SettingAsString(45)) != SettingAsInt(8)))
                Pec = new XContent.PEC(this.Package.StfsContentPackage.GetEndianIO(SettingAsString(45)));
            tadder = new TitleAdder(this.Package.StfsContentPackage, Pec);
            populateTitleList();

            cmdFilter.Enabled = true;

            return true;
        }

        private void populateTitleRecords()
        {
            TitlePlayedRecords = new List<TitlePlayedRecord>();
            List<DataFileRecord> TitleRecords = ProfileFile.DataFile.FindDataEntries(Namespace.TITLES);
            for (int x = 0; x < TitleRecords.Count; x++)
                TitlePlayedRecords.Add(new TitlePlayedRecord(ProfileFile.DataFile.SeekToRecord(TitleRecords[x].Entry)));
        }

        private void cmdGoQueue_Click(object sender, EventArgs e)
        {
            tabQueue.Select();
        }

        private void cmdGoList_Click(object sender, EventArgs e)
        {
            tabMain.Select();
        }

        private string cancelText;
        private bool cancelAdd = false;
        private void cmdRemove_Click(object sender, EventArgs e)
        {
            if (cmdRemove.Text == cancelText)
            {
                cancelAdd = true;
                cmdRemove.Enabled = false;
                if (WorkerRunning)
                    cmdRemove.Text = "Cancelling...";
            }
            else if (listQueue.SelectedRows.Count > 0)
            {
                List<int> selectedIndexes = new List<int>();
                for (int x = 0; x < listQueue.SelectedRows.Count; x++)
                    selectedIndexes.Add(listQueue.SelectedRows[x].Index);
                selectedIndexes.Sort(intCompare);
                foreach (int index in selectedIndexes)
                    listQueue.Rows.RemoveAt(index);
                populateTitleList();
                updateQueueTab();
            }
        }

        private int intCompare(int one, int two)
        {
            if (two > one)
                return 1;
            if (one > two)
                return -1;
            return 0;
        }

        protected internal override void EditorControl_FormClosing(object s, FormClosingEventArgs e)
        {
            if (WorkerRunning && downloadThread != null && downloadThread.IsAlive)
                downloadThread.Abort();
            base.EditorControl_FormClosing(s, e);
        }

        private Thread downloadThread;
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (listTitles.SelectedRows.Count > 0)
            {
                cmdAdd.Enabled = listTitles.Enabled = false;
                cmdAdd.Text = SettingAsString(158);
                int loops = (int)Math.Ceiling((decimal)listTitles.SelectedRows.Count / SettingAsInt(181));
                int amountLeft = listTitles.SelectedRows.Count;
                int f = 0;
                downloadThread = new Thread((ThreadStart)delegate
                {
                    bool errorThrown = true;
                    try
                    {
                        for (int x = 0; x < loops; x++)
                        {
                            List<string> tids = new List<string>();
                            int toDo = (amountLeft >= SettingAsInt(181)) ? SettingAsInt(181) : amountLeft;
                            for (int i = f; i < toDo; i++)
                                tids.Add((string)listTitles.SelectedRows[i].Tag);
                            if (Server.GameAdder.reqAchievementInfo(tids.ToArray()))
                            {
                                if (listTitles.Rows.Count == 0)
                                    return;
                                Main.mainForm.Invoke((MethodInvoker)delegate
                                {
                                    for (int i = 0; i < toDo; i++)
                                        listTitles.Rows.RemoveAt(listTitles.SelectedRows[f].Index);
                                    addRowsToQueue(tids);
                                });
                            }
                            else
                                f += toDo;
                            amountLeft -= toDo;
                        }
                        errorThrown = false;
                    }
                    catch (FileNotFoundException)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        UI.errorBox("An error has occured while adding these games!\n\nDetails:\n" + ex.Message);
                    }
                    Main.mainForm.Invoke((MethodInvoker)delegate
                    {
                        if (!errorThrown && f > 0)
                            UI.messageBox(SettingAsString(146) + f.ToString() + SettingAsString(244)
                                + (f == SettingAsInt(151) ? String.Empty : SettingAsString(106)) + SettingAsString(243),
                                SettingAsString(72), MessageBoxIcon.Error, MessageBoxButtons.OK);
                        txtSearch.Text = String.Empty;
                        cmdAdd.Text = SettingAsString(117);
                        cmdAdd.Enabled = listTitles.Enabled = true;
                    });
                });
                downloadThread.Start();
            }
        }

        private void addRowsToQueue(List<string> titleIds)
        {
            foreach (string tid in titleIds)
                foreach (KeyValuePair<string, Server.GameAdder.TitleTemplate> title in Server.GameAdder.Titles)
                    if (title.Key == tid)
                        listQueue.Rows.Add(Server.GameAdder.getPimpedOutGridRow(title.Value, ref listQueue));
            if (!WorkerRunning)
                listQueue.Sort(listQueue.Columns[1], ListSortDirection.Ascending);
            updateQueueTab();
        }

        private void addTitleToProfile(string TID)
        {
            Server.GameAdder.TitleTemplate title = Server.GameAdder.getTitle(TID);
            if (this.Pec == null && title.Awards.Count > 0)
            {
                this.Pec = new PEC();
                this.Pec.Create(this.Package.StfsContentPackage, this.Package.Header.Metadata.Creator);
                this.Pec = new PEC(this.Package.StfsContentPackage.GetEndianIO(SettingAsString(45)));

                tadder = new TitleAdder(this.Package.StfsContentPackage, this.Pec);
            }
            titleAdder.AddTitleToListCache(title.Meta);
            tadder.AddTitle(uint.Parse(title.Meta.TID, System.Globalization.NumberStyles.HexNumber), title);
        }

        private void cmdAddToProfile_Click(object sender, EventArgs e)
        {
            if (Meta.IsFatx && FormHandle.isDeviceWorkerThreadRunning(Meta.DeviceIndex))
            {
                FatxHandle.showOperationRunningMessage();
                return;
            }
            string oldText = cmdRemove.Text;
            cmdRemove.Text = cancelText;
            WorkerThread = new Thread((ThreadStart)delegate
            {
#if !INT2
                    try
                    {
#endif
                if (listQueue.Rows.Count > 0)
                {
                    Main.mainForm.Invoke((MethodInvoker)delegate
                    {
                        cmdAddToProfile.Enabled = false;
                        cmdAddToProfile.Text = SettingAsString(10);
                    });
                    while (listQueue.Rows.Count > 0)
                    {
                        addTitleToProfile((string)listQueue.Rows[0].Tag);
                        Main.mainForm.Invoke((MethodInvoker)delegate
                        {
                            listQueue.Rows.RemoveAt(0);
                            updateQueueTab();
                        });
                        if (cancelAdd)
                        {
                            cancelAdd = false;
                            Main.mainForm.Invoke((MethodInvoker)delegate { cmdRemove.Enabled = true; });
                            break;
                        }
                    }
                    if (this.Pec != null)
                    {
                        this.Pec.Flush();
                        this.Pec.Save();
                    }

                    this.Package.Flush();

                    titleAdder.FlushTitleList();

                    Main.mainForm.Invoke((MethodInvoker)delegate
                    {
                        populateTitleRecords();
                        cmdAddToProfile.Text = SettingAsString(237);
                        cmdAddToProfile.Enabled = true;
                    });
                }
#if !INT2
                    }
                    catch (Exception ex)
                    {
                        saveErrorRebuild(ex);
                    }
#endif
                Main.mainForm.Invoke((MethodInvoker)delegate { cmdRemove.Text = oldText; });
            });
            WorkerThread.Start();
        }

        private void listQueue_SelectionChanged(object sender, EventArgs e)
        {
            if (listQueue.SelectedRows.Count > 0)
            {
                tabAchievements.Visible = true;
                populateAchievements((string)listQueue.SelectedRows[0].Tag);
                populateAwards((string)listQueue.SelectedRows[0].Tag);
            }
            else
                tabAwards.Visible = tabAchievements.Visible = false;
            rbPackageEditor.Refresh();
        }

        private List<int> searchIndex = new List<int>();
        private int searchIndexTop;
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchIndex = new List<int>();
            if (txtSearch.Text.Length != 0)
            {
                string searchText = txtSearch.Text.ToLower();
                for (int x = 0; x < listTitles.Rows.Count; x++)
                    if (((string)listTitles.Rows[x].Cells[0].Value).ToLower().Contains(searchText))
                        searchIndex.Add(x);
            }
            if (searchIndex.Count != 0)
                listTitles.FirstDisplayedScrollingRowIndex = searchIndex[searchIndexTop = 0];
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmdNext_Click(null, null);
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length != 0 && searchIndex.Count != 0)
                listTitles.FirstDisplayedScrollingRowIndex = searchIndex[searchIndexTop++ % searchIndex.Count];
        }

        private bool haltPopulate = false;
        private static bool messageShown = false;
        private void cmdFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (!haltPopulate && cmdFilter.Checked)
                populateTitleList();
            else if (!cmdFilter.Checked)
            {
                if (messageShown)
                    populateTitleList();
                else
                {
                    if (UI.messageBox("Disabling the title filter will allow you to add titles that are not normally found in stores or on the Xbox LIVE marketplace.\n\nAre you sure you want to disable the filter?",
                        "Disable Title Filter", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
                    {
                        messageShown = true;
                        populateTitleList();
                    }
                    else
                    {
                        haltPopulate = true;
                        cmdFilter.Checked = true;
                        haltPopulate = false;
                    }
                }
            }
        }

        private void listTitles_SelectionChanged(object sender, EventArgs e)
        {
            if (listTitles.SelectedRows.Count == 1)
            {
                Clipboard.Clear();
                Clipboard.SetText((string)listTitles.SelectedRows[0].Cells[0].Value);
            }
        }
    }
}
