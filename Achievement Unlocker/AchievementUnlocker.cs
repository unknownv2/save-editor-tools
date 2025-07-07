using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XboxDataBaseFile;
using System.IO;
using Horizon.Properties;
using DevComponents.DotNetBar.Controls;
using Horizon.Functions;
using System.Threading;
using Horizon.Forms;

namespace Horizon.PackageEditors.Achievement_Unlocker
{
    public partial class AchievementUnlocker : EditorControl
    {
        public AchievementUnlocker()
        {
            InitializeComponent();
            backupOnOpen = true;
            listAchievements.Columns[2].DefaultCellStyle.WrapMode
                = listAchievements.Columns[1].DefaultCellStyle.WrapMode
                = DataGridViewTriState.True;
        }

        private ProfileFile Profile;
        private const string notSet = "Not Set";
        public override bool Entry()
        {
            Profile = new ProfileFile(Package, 0xfffe07d1);
            Profile.Read();
            tabMain.Text = Account == null ? "Unknown" : Account.Info.GamerTag;
            SettingRecord rec = new SettingRecord();
            lblMetaGamerZone.Text = "Gamerzone: ";
            lblMetaMotto.Text = "Motto: ";
            lblMetaName.Text = "Name: ";
            lblMetaLocation.Text = "Location: ";
            lblMetaBio.Text = "Bio: ";
            if (rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_ZONE)))
                switch ((XProfileGamercardZoneOptions)rec.nData)
                {
                    case XProfileGamercardZoneOptions.XPROFILE_GAMERCARD_ZONE_XBOX_1:
                        lblMetaGamerZone.Text += "Xbox";
                        break;
                    case XProfileGamercardZoneOptions.XPROFILE_GAMERCARD_ZONE_RR:
                        lblMetaGamerZone.Text += "Recreation";
                        break;
                    case XProfileGamercardZoneOptions.XPROFILE_GAMERCARD_ZONE_FAMILY:
                        lblMetaGamerZone.Text += "Family";
                        break;
                    case XProfileGamercardZoneOptions.XPROFILE_GAMERCARD_ZONE_UNDERGROUND:
                        lblMetaGamerZone.Text += "Underground";
                        break;
                    case XProfileGamercardZoneOptions.XPROFILE_GAMERCARD_ZONE_PRO:
                        lblMetaGamerZone.Text += "Pro";
                        break;
                }
            else
                lblMetaGamerZone.Text += notSet;
            lblMetaMotto.Text += rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_MOTTO))
                ? UnicodeEncoding.BigEndianUnicode.GetString(rec.varData) : notSet;
            lblMetaName.Text += rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_USER_NAME))
                ? UnicodeEncoding.BigEndianUnicode.GetString(rec.varData) : notSet;
            lblMetaLocation.Text += rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_USER_LOCATION))
                ? UnicodeEncoding.BigEndianUnicode.GetString(rec.varData) : notSet;
            lblMetaBio.Text += rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_USER_BIO))
                ? UnicodeEncoding.BigEndianUnicode.GetString(rec.varData) : notSet;
            lblProfileID.Text = "Profile ID: " + Package.Header.Metadata.Creator.ToString("X");
            forAch = SettingAsString(247);
            forGs = SettingAsString(52);
            noDLC = SettingAsString(18);
            secretAchievement = SettingAsString(111);
            unlockAllDisplayed = SettingAsString(184);
            populateTitleRecords();
            updateProfileProgressText();
            if (TitlePlayedRecords.Count == 0)
            {
                cmdUnlockAllAchievements.Visible = false;
                UI.messageBox(this, "This profile has not played any titles!", "No Titles", MessageBoxIcon.Information);
            }
            else
            {
                cmdUnlockAllAchievements.Visible = true;
                listGames.Items[0].Selected = true;
            }
            rbPackageEditor.Refresh();
            return true;
        }

        private static string forAch;
        private static string forGs;
        private static string noDLC;
        public override void Save()
        {
            Profile.IO.Close();
            Profile.IO.Open();
        }

        public override void revertForm()
        {
            isBusy = true;
            tabMain.Select();
            tabGame.Visible
                = tabAchievement.Visible
                = false;
            listAchievements.Rows.Clear();
            listGames.Items.Clear();
            currentGame = SettingAsInt(190);
            achTracker = null;
            totalPossible = new uint[SettingAsInt(146)];
            TitlePlayedRecords = new List<TitlePlayedRecord>();
            isBusy = false;
        }

        private void updateProfileProgressText()
        {
            pProfileAchievements.Maximum = (int)totalPossible[0];
            pProfileGamerscore.Maximum = (int)totalPossible[1];
            SettingRecord rec = new SettingRecord();
            pProfileAchievements.Value = rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_ACHIEVEMENTS_EARNED))
                ? rec.nData : 0;
            pProfileAchievements.Text = String.Format(forAch, pProfileAchievements.Value, pProfileAchievements.Maximum, null);
            pProfileGamerscore.Value = rec.Read(Profile.SettingsTracker.ReadSetting(XProfileIds.XPROFILE_GAMERCARD_CRED))
                ? rec.nData : 0;
            pProfileGamerscore.Text = String.Format(forGs, pProfileGamerscore.Value, pProfileGamerscore.Maximum, null);
        }
        
        private void updateGameProgress()
        {
            updateGameRow(currentGame);
            pGameAchievements.Maximum = (int)TitlePlayedRecords[currentGame].AchievementsPossible;
            pGameAchievements.Value = (int)TitlePlayedRecords[currentGame].AchievementsEarned;
            pGameAchievements.Text = String.Format(forAch, pGameAchievements.Value, pGameAchievements.Maximum,
                (achTracker.Achievements.Count == (int)TitlePlayedRecords[currentGame].AchievementsPossible ? String.Empty : noDLC));
            pGameGamerscore.Maximum = (int)TitlePlayedRecords[currentGame].CredPossible;
            pGameGamerscore.Value = (int)TitlePlayedRecords[currentGame].CredEarned;
            pGameGamerscore.Text = String.Format(forGs, pGameGamerscore.Value, pGameGamerscore.Maximum,
                (achTracker.Achievements.Count == (int)TitlePlayedRecords[currentGame].AchievementsPossible ? String.Empty : noDLC));
        }

        private uint[] totalPossible;
        private List<TitlePlayedRecord> TitlePlayedRecords;
        private void populateTitleRecords()
        {
            prePopulate();
            List<DataFileRecord> TitleRecords = Profile.DataFile.FindDataEntries(Namespace.TITLES);
            for (int x = 0; x < TitleRecords.Count; x++)
                if (TitleRecords[x].Id.Id != 0)
                {
                    TitlePlayedRecord tpr = new TitlePlayedRecord(Profile.DataFile.SeekToRecord(TitleRecords[x].Entry));
                    if (Package.StfsContentPackage.GetDirectoryEntryIndex(ProfileFile.FormatTitleIDToFilename(tpr.TitleId)) != -1
                        && tpr.CredPossible != 0 && tpr.AchievementsPossible != 0)
                    {
                        TitlePlayedRecords.Add(tpr);
                        totalPossible[0] += tpr.AchievementsPossible;
                        totalPossible[1] += tpr.CredPossible;
                        checkRowAdd(TitlePlayedRecords.Count + SettingAsInt(190));
                    }
                }
            postPopulate();
        }

        private void checkRowAdd(int x)
        {
            if (TitlePlayedRecords[x].TitleName.ToLower().Contains(txtGameSearch.Text.ToLower())
                || TitlePlayedRecords[x].TitleId.ToString("x").Contains(txtGameSearch.Text.ToLower()))
                listGames.Items.Add(getGameRow(x));
        }

        private void txtGameSearch_TextChanged(object sender, EventArgs e)
        {
            if (Package.IsLoaded)
            {
                prePopulate();
                for (int x = 0; x < TitlePlayedRecords.Count; x++)
                    checkRowAdd(x);
                postPopulate();
                if (listGames.Items.Count == 0)
                {
                    currentGame = -1;
                    isBusy = true;
                    listAchievements.Rows.Clear();
                    isBusy = false;
                    tabGame.Visible = tabAchievement.Visible = false;
                    tabMain.Select();
                    rbPackageEditor.Refresh();
                }
            }
        }

        private bool isBusy = false;
        private void prePopulate()
        {
            isBusy = true;
            listGames.Items.Clear();
            listGames.BeginUpdate();
            listGames.TileSize = new Size(SettingAsInt(20), SettingAsInt(9));
            listGames.LargeImageList = new ImageList();
            listGames.LargeImageList.ImageSize = new Size(SettingAsInt(87), SettingAsInt(87));
            listGames.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
        }

        private void postPopulate()
        {
            listGames.EndUpdate();
            if (listGames.Items.Count > 0)
                listGames.Items[0].Selected = true;
            else
                tabMain.Select();
            isBusy = false;
        }

        private Image getGameTile(DataFile dataFile)
        {
            try
            {
                return Image.FromStream(new MemoryStream(
                    dataFile.ReadRecord(new DataFileId()
                    {
                        Namespace = Namespace.IMAGES,
                        Id = (ulong)SettingAsLong(32)
                    })));
            }
            catch { return Resources.QuestionMark; }
        }

        private ListViewItem getGameRow(int x)
        {
            DataFile dataFile = new DataFile(Package.StfsContentPackage.GetEndianIO(ProfileFile.FormatTitleIDToFilename(TitlePlayedRecords[x].TitleId)));
            dataFile.Read();
            listGames.LargeImageList.Images.Add(getGameTile(dataFile));
            ListViewItem Game = new ListViewItem(TitlePlayedRecords[x].TitleName, listGames.Items.Count);
            Game.SubItems[0].Tag = x;
            Game.SubItems.Add(TitlePlayedRecords[x].TitleId.ToString("X"));
            Game.SubItems.Add(String.Format(SettingAsString(128), TitlePlayedRecords[x].CredEarned.ToString(), TitlePlayedRecords[x].CredPossible.ToString()));
            return Game;
        }

        private void updateGameRow(int x)
        {
            for (int i = 0; i < listGames.Items.Count; i++)
                if ((int)listGames.Items[i].SubItems[0].Tag == x)
                    listGames.Items[i].SubItems[2].Text = String.Format(SettingAsString(128), TitlePlayedRecords[x].CredEarned.ToString(), TitlePlayedRecords[x].CredPossible.ToString());
        }

        public override void Initialize()
        {
            int shiftBy = SettingAsInt(124);
            if (Program.glassEnabled)
            {
                listGames.Location = new Point(listGames.Location.X - shiftBy, listGames.Location.Y - 1);
                listAchievements.Location = new Point(listAchievements.Location.X - shiftBy, listAchievements.Location.Y - 1);
                gpGameSearch.Location = new Point(gpGameSearch.Location.X - shiftBy, gpGameSearch.Location.Y - 1);
                gpAchievementSearch.Location = new Point(gpAchievementSearch.Location.X - shiftBy, gpAchievementSearch.Location.Y - 1);
                movePackageManagerIcon(DevComponents.DotNetBar.eItemAlignment.Near);
            }
        }

        private int currentGame;
        private static string unlockAllDisplayed;
        private void listGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listGames.SelectedIndices.Count != 0)
            {
                isBusy = true;
                if (titleModified)
                {
                    Package.Flush();
                    titleModified = false;
                }
                DataFile dataFile = new DataFile(Package.StfsContentPackage.GetEndianIO(ProfileFile.FormatTitleIDToFilename(TitlePlayedRecords[(int)listGames.SelectedItems[0].SubItems[0].Tag].TitleId)));
                try { dataFile.Read(); }
                catch
                {
                    UI.messageBox(this, "Invalid GPD detected. You will not be able to unlock achievements for this game until you recover your profile!", "Invalid GPD", MessageBoxIcon.Error);
                    return;
                }
                currentGame = (int)listGames.SelectedItems[0].SubItems[0].Tag;
                pbGame.Image = getGameTile(dataFile);
                achTracker = new AchievementTracker(Profile, dataFile, TitlePlayedRecords[currentGame]);
                achTracker.Read();
                lblGame.Text = "<b>Title Name:</b> " + achTracker.TitleRecord.TitleName;
                lblTitleID.Text = "<b>Title ID:</b> " + achTracker.TitleRecord.TitleId.ToString("X");
                updateLastPlayed(achTracker.TitleRecord.LastLoaded, achTracker.TitleRecord.LastLoadedAsLong);
                cmdUnlockAll.Text = unlockAllDisplayed;
                tabGame.Text = achTracker.TitleRecord.TitleName;
                updateGameProgress();
                tabGame.Visible = true;
                tabGame.Select();
                rbPackageEditor.Refresh();
                populateAchievementList();
                if (listAchievements.Rows.Count > 0)
                    loadAchievementRow(0);
                isBusy = false;
                tabGame.Select();
                rbPackageEditor.Refresh();
            }
        }

        private AchievementTracker achTracker;
        private void populateAchievementList(object sender, EventArgs e) { if (Package.IsLoaded) populateAchievementList(); }
        private void populateAchievementList()
        {
            listAchievements.Rows.Clear();
            if (currentGame != -1)
            {
                for (int x = new int(); x < achTracker.Achievements.Count; x++)
                    if ((achTracker.Achievements[x].Label.ToLower().Contains(txtAchievementSearch.Text.ToLower())
                        || achTracker.Achievements[x].Description.ToLower().Contains(txtAchievementSearch.Text.ToLower())
                        || achTracker.Achievements[x].Unachieved.ToLower().Contains(txtAchievementSearch.Text.ToLower()))
                        && ((ckAchievementLocked.Checked && !achTracker.Achievements[x].AchievementEarned)
                            || (ckAchievementUnlockedOffline.Checked && achTracker.Achievements[x].AchievementEarned && !achTracker.Achievements[x].AchievementEarnedOnline)
                            || (ckAchievementUnlockedOnline.Checked && achTracker.Achievements[x].AchievementEarnedOnline)))
                        listAchievements.Rows.Add(getAchievementRow(x));
                if (!(tabAchievement.Visible = listAchievements.Rows.Count != 0))
                    tabGame.Select();
                rbPackageEditor.Refresh();
            }
            //listAchievements.Sort(listAchievements.Columns[1], ListSortDirection.Ascending);
        }

        private Image getAchievementTile(int x)
        {
            Image achTile = achTracker.GetAchievementTile(achTracker.Achievements[x]);
            return achTile == null ? (achTracker.Achievements[x].AchievementEarned
                ? Resources.EarnedAchievement : Resources.Unearned) : achTile;
        }

        private DataGridViewRow getAchievementRow(int x)
        {
            DataGridViewRow Ach = new DataGridViewRow();
            Ach.CreateCells(listAchievements);
            Ach.Height = SettingAsInt(9);
            Ach.Tag = x;
            Ach.Cells[0].Value = getAchievementTile(x);
            Ach.Cells[1].Value = achTracker.Achievements[x].Label.Replace(Environment.NewLine, String.Empty)
                + Environment.NewLine
                + (achTracker.Achievements[x].AchievementEarned ? (SettingAsString(118) + (achTracker.Achievements[x].AchievementEarnedOnline
                    ? SettingAsString(227) : SettingAsString(21))) : SettingAsString(135))
                + Environment.NewLine
                + achTracker.Achievements[x].cred.ToString() + SettingAsString(54);
            if (achTracker.Achievements[x].AchievementEarned)
                Ach.Cells[2].Value = achTracker.Achievements[x].Description;
            else
                if (achTracker.Achievements[x].AchievementShowUnachieved)
                    Ach.Cells[2].Value = achTracker.Achievements[x].Unachieved;
                else
                    Ach.Cells[2].Value = secretAchievement;
            Ach.Cells[2].Value = ((string)Ach.Cells[2].Value).Replace(Environment.NewLine, " ");
            return Ach;
        }

        private int currentAchievement;
        private void listAchievements_SelectionChanged(object sender, EventArgs e)
        {
            if (!isBusy)
            {
                if (listAchievements.SelectedRows.Count > 1)
                {
                    cmdUnlockAll.Text = SettingAsString(84);
                    tabGame.Select();
                }
                else
                {
                    cmdUnlockAll.Text = unlockAllDisplayed;
                    tabAchievement.Select();
                }
                if (listAchievements.Rows.Count > 0)
                    if (listAchievements.SelectedRows.Count == 0)
                        listAchievements.Rows[0].Selected = true;
                    else
                        loadAchievementRow(listAchievements.SelectedRows[0].Index);
            }
        }

        private static string secretAchievement;
        private void loadAchievementRow(int row)
        {
            currentAchievement = (int)listAchievements.Rows[row].Tag;
            DataGridViewRow Row = getAchievementRow(currentAchievement);
            pbAchievement.Image = (Image)(listAchievements.Rows[row].Cells[0].Value = Row.Cells[0].Value);
            listAchievements.Rows[row].Cells[1].Value = Row.Cells[1].Value;
            listAchievements.Rows[row].Cells[2].Value = Row.Cells[2].Value;
            lblLockedDescription.Text = "<b>Locked Description:</b> "
                + (achTracker.Achievements[currentAchievement].AchievementShowUnachieved ? achTracker.Achievements[currentAchievement].Unachieved : secretAchievement);
            lblUnlockedDescription.Text = "<b>Unlocked Description:</b> " + achTracker.Achievements[currentAchievement].Description;
            dateUnlocked.Enabled = ckUnlockedOffline.Checked = ckUnlockedOnline.Checked = false;
            if (achTracker.Achievements[currentAchievement].AchievementEarnedOnline)
                ckUnlockedOnline.Checked = true;
            else if (achTracker.Achievements[currentAchievement].AchievementEarned)
                ckUnlockedOffline.Checked = true;
            else
                dateUnlocked.Value = DateTime.Now;
            lblGamerscore.Text = String.Format("<b>{0}</b>", achTracker.Achievements[currentAchievement].cred);
            dateUnlocked.Tag = achTracker.Achievements[currentAchievement].dateTimeAchieved.ToFileTimeUtc();
            panelAchievement.Enabled = !achTracker.Achievements[currentAchievement].AchievementEarned;
            tabAchievement.Text = achTracker.Achievements[currentAchievement].Label;
            rbPackageEditor.Refresh();
        }

        private void ckUnlocked_CheckedChanged(object sender, EventArgs e)
        {
            if (dateUnlocked.Enabled = ckUnlockedOnline.Checked)
                dateUnlocked.Value = DateTime.Now;
        }

        private void cmdClearGameSearch_Click(object sender, EventArgs e)
        {
            txtGameSearch.Text = String.Empty;
        }

        private void cmdClearAchievementSearch_Click(object sender, EventArgs e)
        {
            txtAchievementSearch.Text = String.Empty;
        }

        private void cmdExtractGPD_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Game Progress Data|*.gpd";
            sfd.FileName = ProfileFile.FormatTitleIDToFilename(TitlePlayedRecords[currentGame].TitleId);
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(sfd.FileName, Package.StfsContentPackage.ExtractFileToArray(ProfileFile.FormatTitleIDToFilename(TitlePlayedRecords[currentGame].TitleId)));
                UI.messageBox(this, "GPD extracted successfully!", "Extracted", MessageBoxIcon.Information);
            }
        }

        private bool titleModified = false;
        private void unlockAchievement(int row, bool earnedOnline)
        {
            int x = (int)listAchievements.Rows[row].Tag;
            if (!achTracker.Achievements[x].AchievementEarned)
            {
                var dt = dateUnlocked.Value.AddMilliseconds(Global.random.Next(0, 60000));
                achTracker.UnlockAchievement(achTracker.Achievements[x], earnedOnline, dt);
                titleModified = true;
                if (WorkerRunning)
                {
                    Main.mainForm.Invoke((MethodInvoker)delegate
                    {
                        updateGameProgress();
                    });
                }
                else
                {
                    loadAchievementRow(row);
                    updateProfileProgressText();
                    updateGameProgress();
                }
            }
        }

        private void cmdUnlockAll_Click(object sender, EventArgs e)
        {
            if (listAchievements.SelectedRows.Count > 1)
                for (int x = 0; x < listAchievements.SelectedRows.Count; x++)
                    unlockAchievement(listAchievements.SelectedRows[x].Index, false);
            else
                for (int x = 0; x < listAchievements.Rows.Count; x++)
                {
                    if (cancelUnlock)
                        break;
                    unlockAchievement(x, false);
                }
        }

        private void cmdSetAchievement_Click(object sender, EventArgs e)
        {
            try
            {
                if (ckUnlockedOffline.Checked || ckUnlockedOnline.Checked)
                {
                    unlockAchievement(listAchievements.SelectedRows[0].Index, ckUnlockedOnline.Checked);
                    if (ckUnlockedOnline.Checked)
                        updateLastPlayed(TitlePlayedRecords[currentGame].LastLoaded, TitlePlayedRecords[currentGame].LastLoadedAsLong);
                }
            }
            catch (Exception ex)
            {
                saveErrorRebuild(ex);
            }
        }
        private void updateLastPlayed(DateTime lastPlayed, long asLong)
        {
            lblLastPlayed.Text = "<b>Last Played:</b> " + (asLong == 0 ? SettingAsString(15) : lastPlayed.ToString());
        }

        private void pbProfile_Mouse(object sender, EventArgs e)
        {
            lblMetaBio.Visible = !lblMetaBio.Visible;
        }

        private void enableControls(bool enable)
        {
            gpGameSearch.Enabled
                = gpAchievementSearch.Enabled
                = cmdUnlockAll.Enabled
                = cmdExtractGPD.Enabled
                = cmdSetAchievement.Enabled
                = listGames.Enabled
                = listAchievements.Enabled
                = enable;
            rbPackageEditor.Refresh();
        }

        private bool cancelUnlock = false;
        private static readonly string cancel = "Cancel";
        private void cmdUnlockAllAchievements_Click(object sender, EventArgs e)
        {
            if (cmdUnlockAllAchievements.Text.Length == cancel.Length)
                cancelUnlock = true;
            else if (TitlePlayedRecords.Count > 0 && UI.messageBox(this, SettingAsString(200),
                SettingAsString(99), MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                if (Meta.IsFatx && FormHandle.isDeviceWorkerThreadRunning(Meta.DeviceIndex))
                {
                    FatxHandle.showOperationRunningMessage();
                    return;
                }
                enableControls(false);
                cmdUnlockAllAchievements.Text = cancel;
                txtGameSearch.Text = txtAchievementSearch.Text = String.Empty;
                WorkerThread = new Thread((ThreadStart)delegate
                {
                    bool errorThrown = true;
                    try
                    {
                        for (int x = 0; x < listGames.Items.Count; x++)
                        {
                            Main.mainForm.Invoke((MethodInvoker)delegate { listGames.Items[x].Selected = true; });
                            cmdUnlockAll_Click(cmdUnlockAll, null);
                            if (cancelUnlock)
                                break;
                        }
                        errorThrown = false;
                    }
                    catch (FileNotFoundException)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        UI.errorBox("An error has occured while unlocking your achievements!\n\nDetails:\n" + ex.Message);
                    }
                    Main.mainForm.Invoke((MethodInvoker)delegate
                    {
                        updateProfileProgressText();
                        for (int x = 0; x < listAchievements.Rows.Count; x++)
                            loadAchievementRow(x);
                        loadAchievementRow(0);
                        if (!errorThrown && !cancelUnlock)
                            UI.messageBox(this, SettingAsString(62), SettingAsString(94), MessageBoxIcon.Information);
                        cancelUnlock = false;
                        cmdUnlockAllAchievements.Text = "Unlock Everything";
                        enableControls(true);
                    });
                });
                WorkerThread.Start();
            }
        }
    }
}
