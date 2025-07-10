using System;
using System.Collections.Generic;
using DevComponents.DotNetBar;
using XContent;
using Horizon.Properties;
using DevComponents.AdvTree;
using System.Drawing;
using Horizon.Functions;
using System.Windows.Forms;
using Horizon.PackageEditors.Package_Manager;
using System.IO;
using Horizon.Library.Systems.FATX;
using System.Management;
using System.Threading;
using System.Globalization;
using Horizon.PackageEditors;
using System.Collections.Specialized;
using System.Linq;

namespace Horizon
{
    internal static class FatxHandle
    {
        private static Horizon.Forms.Main Main;
        internal static void unloadHandle()
        {
            for (int x = 0; x < Devices.Count; x++)
                FormHandle.unloadFatxForms(x);
            deviceEventThread.Abort();
            for (int x = 0; x < Devices.Count; x++)
                if (Devices[x].WorkerBusy)
                    Devices[x].Worker.Abort();
        }

        private static Dictionary<uint, string> TitleIDCache;
        private static void addTitleIDToCache(uint tid, string titleName)
        {
            if (TitleIDCache.ContainsKey(tid))
            {
                if (TitleIDCache[tid] == unknownGame && titleName.Length != 0)
                    TitleIDCache[tid] = titleName;
            }
            else
                TitleIDCache.Add(tid, titleName.Length == 0 ? unknownGame : titleName);
        }

        private static Thread deviceEventThread;
        internal static void initializeHandle()
        {
            Main = Horizon.Forms.Main.mainForm;
            TitleIDCache = new Dictionary<uint, string>();
            if (Settings.Default.TitleIDCache == null || Settings.Default.TitleIDCache.Count == 0)
                populateDefaultCacheItems();
            else
            {
                uint titleId;
                foreach (string title in Settings.Default.TitleIDCache)
                    if (uint.TryParse(title.Substring(0, 8), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out titleId))
                        TitleIDCache.Add(titleId, title.Substring(8));
            }
            Main.cmdFatxMod.PopupSide = ePopupSide.Bottom;
            Main.listFatx.AfterNodeSelect += new AdvTreeNodeEventHandler(listFatx_AfterNodeSelect);
            Main.listFatx.BeforeExpand += new AdvTreeNodeCancelEventHandler(listFatx_BeforeExpand);
            Main.listFatx.AfterExpand += new AdvTreeNodeEventHandler(listFatx_AfterExpand);
            Main.listFatx.AfterCollapse += new AdvTreeNodeEventHandler(listFatx_AfterCollapse);
            Main.listFatx.NodeMouseDown += new TreeNodeMouseEventHandler(listFatx_NodeMouseDown);
            Main.listFatx.NodeDoubleClick += new TreeNodeMouseEventHandler(listFatx_NodeDoubleClick);
            Main.listFatx.BeforeCellEdit += new CellEditEventHandler(listFatx_BeforeCellEdit);
            Main.listFatx.AfterCellEdit += new CellEditEventHandler(listFatx_AfterCellEdit);
            Main.listFatx.MouseMove += new MouseEventHandler(listFatx_MouseMove);
            Main.listFatx.DragDrop += new DragEventHandler(listFatx_DragDrop);
            Main.listFatx.DragEnter += new DragEventHandler(listFatx_DragEnter);
            Main.listFatx.DragOver += new DragEventHandler(listFatx_DragOver);
            Main.listFatx.KeyDown += new KeyEventHandler(listFatx_KeyDown);
            Main.cmdFatxMod.Click += new EventHandler(cmdFatxMod_Click);
            Main.cmdFatxGear.Click += new EventHandler(FormConfig.formOpenClick);
            Main.cmdFatxExtract.Click += new EventHandler(cmdFatxExtract_Click);
            Main.cmdFatxInject.Click += new EventHandler(cmdFatxInject_Click);
            Main.cmdFatxToolExtract.Click += new EventHandler(cmdFatxExtract_Click);
            Main.cmdFatxToolInject.Click += new EventHandler(cmdFatxInject_Click);
            Main.cmdFatxToolDelete.Click += new EventHandler(cmdFatxToolDelete_Click);
            Main.cmdFatxToolRenameDevice.Click += new EventHandler(cmdFatxToolRenameDevice_Click);
            Main.cmdFatxToolClearTitleCache.Click += new EventHandler(cmdFatxToolClearTitleCache_Click);
            Main.cmdFatxToolCollapseAll.Click += new EventHandler(cmdFatxToolCollapseAll_Click);
            Main.cmdFatxCopyFile.DropDownOpening += new EventHandler(cmdFatxCopyFile_DropDownOpening);
            Main.cmdFatxCopyFile.DropDownClosed += new EventHandler(cmdFatxCopyFile_DropDownClosed);
            #if INT2
            Main.cmdLoadDump.Click += new EventHandler(cmdLoadDump_Click);
            Main.cmdLoadUsbDump.Click += new EventHandler(cmdLoadUsbDump_Click);
            Main.cmdFatxUnload.Click += new EventHandler(cmdFatxUnload_Click);
            #endif
            deviceEventThread = new Thread((ThreadStart)deviceEventWatcher);
            deviceEventThread.Start();
        }

        private static void populateDefaultCacheItems()
        {
            TitleIDCache.Add(dashTitleId, "Xbox 360 Dashboard");
            TitleIDCache.Add(0x454108d4, "Left 4 Dead 2");
            TitleIDCache.Add(0x5553083b, "Assassin's Creed II");
            TitleIDCache.Add(0x454108ce, "Mass Effect 2");
            TitleIDCache.Add(0x454108f3, "FIFA 11");
            TitleIDCache.Add(0x4d5307f1, "Fable II");
        }

        private static void listFatx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && Main.listFatx.SelectedNode != null
                && Main.listFatx.SelectedNode.Tag != null && Main.cmdFatxToolDelete.Enabled)
                cmdFatxToolDelete_Click(null, null);
        }

        private static void listFatx_AfterCellEdit(object sender, CellEditEventArgs e)
        {
            if (e.Cell == null)
                e.Cancel = true;
            else
            {
                FatxNode nodeTag = (FatxNode)e.Cell.Tag;
                nodeTag.Device.SetName(e.NewText);
                setDeviceCellOneText(e.Cell, nodeTag);
                e.NewText = e.Cell.Text;
                Main.tabFatxDrive.Text = nodeTag.Device.Name;
                Main.rbFatx.Refresh();
            }
        }

        private static void listFatx_BeforeCellEdit(object sender, CellEditEventArgs e)
        {
            if (e.Cell.Tag == null)
                e.Cancel = true;
            else
            {
                FatxNode nodeTag = (FatxNode)e.Cell.Tag;
                if (isDeviceWorkerAvailable(nodeTag.DeviceIndex) && nodeTag.NodeType == NodeTypes.Device)
                    e.Cell.Text = nodeTag.Device.Name;
                else
                    e.Cancel = true;
            }
        }

        private static string smartRenameFatxEntry(XContentPackage package)
        {
            switch (package.Header.Metadata.ContentType)
            {
                case ContentTypes.Profile:
                    return package.Header.Metadata.Creator.ToString("X16");
                case ContentTypes.GamerPicture:
                case ContentTypes.ThematicSkin:
                    if (package.Header.SignatureType != XContentSignatureType.CONSOLE_SIGNED)
                        goto createHash;
                    break;
                case ContentTypes.Installer:
                        return String.Format("tu{0:X8}_{1:X8}", package.Header.Metadata.ExecutionId.Version, 0);
                case ContentTypes.InstalledXbox360Title:
                case ContentTypes.Xbox360Title:
                    byte[] svodHashName = new byte[17];
                    package.IO.Stream.Position = 0x32c;
                    package.IO.Stream.Read(svodHashName, 0, 16);
                    svodHashName[16] = (byte)(package.Header.Metadata.ExecutionId.TitleId >> 24);
                    return svodHashName.ToHexString().ToUpper();
                case ContentTypes.GameTrailer:
                case ContentTypes.Arcade:
                case ContentTypes.Marketplace:
                case ContentTypes.Video:
            createHash:
                    byte[] contentHashName = new byte[21];
                    package.IO.Stream.Position = 0x32c;
                    package.IO.Stream.Read(contentHashName, 0, 20);
                    contentHashName[20] = (byte)(package.Header.Metadata.ExecutionId.TitleId >> 24);
                    return contentHashName.ToHexString().ToUpper();
                case ContentTypes.AvatarAsset:
                    return package.Header.Metadata.AvatarAssetData.AssetId.ToHexString().ToUpper();
            }
            return null;
        }

        #if INT2
        private static void cmdFatxUnload_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < Devices.Count; x++)
            {
                FormHandle.unloadFatxForms(x);
                Devices[x].Drive.CloseDisk();
            }
            Main.listFatx.Nodes.Clear();
            Devices.Clear();
        }

        private static void cmdLoadUsbDump_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                List<string> filePaths = new List<string>();
                for (byte x = 0; x < 19; x++)
                {
                    string currentPath = FatxDeviceControl.createLocalLogicalPath(fbd.SelectedPath, x);
                    if (File.Exists(currentPath))
                        filePaths.Add(currentPath);
                    else
                        break;
                }
                if (filePaths.Count > 0)
                {
                    EndianIO IO = new MultiFileIO(filePaths, EndianType.BigEndian, true);
                    Devices.Add(new DeviceInfo(new FatxDeviceControl.DriveBase(IO, FatxDeviceControl.DiskType.LogicalDrive, IO.Length)));
                    Main.listFatx.Nodes.Add(buildFatxNode(new FatxNode(Devices.Count - 1, NodeTypes.Device), Devices[Devices.Count - 1].DeviceImage));
                }
            }
        }

        private static void cmdLoadDump_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Devices.Add(new DeviceInfo(new FatxDeviceControl.DriveBase(new EndianIO(ofd.FileName, EndianType.BigEndian), FatxDeviceControl.DiskType.PhysicalDrive, 0)));
                Main.listFatx.Nodes.Add(buildFatxNode(new FatxNode(Devices.Count - 1, NodeTypes.Device), Devices[Devices.Count - 1].DeviceImage));
            }
        }
        #endif

        private static void cmdFatxCopyFile_DropDownOpening(object sender, EventArgs e)
        {
            for (int x = 0; x < Main.listFatx.Nodes.Count; x++)
            {
                int devIndex = ((FatxNode)Main.listFatx.Nodes[x].Tag).DeviceIndex;
                if (devIndex != (int)Main.tabFatxDrive.Tag)
                {
                    ToolStripMenuItem newCopyItem = new ToolStripMenuItem();
                    newCopyItem.Tag = devIndex;
                    newCopyItem.Text = Devices[devIndex].Name;
                    newCopyItem.Image = Devices[devIndex].DeviceImage;
                    newCopyItem.Click += new EventHandler(cmdFatxCopyFile_Click);
                    Main.cmdFatxCopyFile.DropDownItems.Add(newCopyItem);
                }
            }
            if (Main.cmdFatxCopyFile.DropDownItems.Count == 1)
            {
                Main.cmdFatxNoDevicesFound.Text = "No Devices Found";
                Main.cmdFatxNoDevicesFound.Visible = true;
            }
            else
            {
                Main.cmdFatxNoDevicesFound.Text = String.Empty;
                Main.cmdFatxNoDevicesFound.Visible = false;
            }
        }

        private static void cmdFatxCopyFile_DropDownClosed(object sender, EventArgs e)
        {
            Main.cmdFatxNoDevicesFound.Visible = true;
            while (Main.cmdFatxCopyFile.DropDownItems.Count > 1)
                Main.cmdFatxCopyFile.DropDownItems.RemoveAt(1);
        }

        private static void deviceEventWatcher()
        {
            scanForNewDevices(true);
            if (Environment.OSVersion.Version.Major < 6)
                while (true)
                {
                    Thread.Sleep(5000);
                    removeDevices();
                    scanForNewDevices(false);
                }
            else
            {
                ManagementEventWatcher watcher = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent"));
                watcher.EventArrived += new EventArrivedEventHandler(deviceWatcherTrigger);
                watcher.Start();
                while (!Program.shuttingDown)
                    Thread.Sleep(10000);
            }
        }

        private static bool scanForNewDevices(bool changeUI)
        {
            deviceTriggerOperating = true;
            updateProgressBar(ProgressBarStyle.Marquee, 0, 0);
            List<string> loadedNames = new List<string>();
            for (int x = 0; x < Main.listFatx.Nodes.Count; x++)
                loadedNames.Add(((FatxNode)Main.listFatx.Nodes[x].Tag).Device.Drive.Name);
            FatxDeviceControl.DriveBase[] driveBases = FatxDeviceControl.getFatxDevices(loadedNames);
            for (int x = 0; x < driveBases.Length; x++)
            {
                DeviceInfo deviceInfo = new DeviceInfo(driveBases[x]);
                if (deviceInfo.Name == null)
                    UI.errorBox("An error has occured while loading one of your devices!");
                else if (deviceInfo.Drive.Mounted)
                {
                    Devices.Add(deviceInfo);
                    Main.listFatx.Nodes.Add(buildFatxNode(new FatxNode(Devices.Count - 1, NodeTypes.Device), Devices[Devices.Count - 1].DeviceImage));
                    if (Main.listFatx.Nodes.Count == 1)
                        Main.listFatx.SelectedIndex = 0;
                    refreshDeviceCounter();
                }
            }
            if (changeUI)
            {
                if (Main.listFatx.Nodes.Count == 1)
                    Main.listFatx.Invoke((MethodInvoker)delegate
                    {
                        Main.listFatx.SelectedIndex = 0;
                        Main.fatxMenuLine.Visible
                            = Main.cmdFatxToolRenameDevice.Visible
                            = Main.cmdFatxToolClearTitleCache.Visible
                            = true;
                        Main.listFatx.Nodes[0].Expand();
                    });
                if (Main.listFatx.Nodes.Count > 0)
                {
                    if (Program.doneLoading)
                        Main.exFatx.Invoke((MethodInvoker)delegate { Main.FatxPanelExpanded = true; });
                    else
                        openOnOpen = true;
                }
                refreshDeviceCounter();
            }
            deviceTriggerOperating = false;
            multipleOperationsRunning = numberOfDevicesBusy() > 1;
            updateProgressBar(ProgressBarStyle.Blocks, 0, 0);
            return driveBases.Length > 0;
        }

        internal static bool openOnOpen = false;
        private static void removeDevices()
        {
            for (int x = 0; x < Main.listFatx.Nodes.Count; x++)
            {
                int devIndex = ((FatxNode)Main.listFatx.Nodes[x].Tag).DeviceIndex;
                if (!Devices[devIndex].Drive.Mounted)
                {
                    bool wasCurrentDevice = false;
                    Main.Invoke((MethodInvoker)delegate
                    {
                        for (int i = 0; i < Main.listFatx.Nodes.Count; i++)
                            if (Main.listFatx.Nodes[i].Tag != null && ((FatxNode)Main.listFatx.Nodes[i].Tag).DeviceIndex == devIndex)
                            {
                                if ((int)Main.tabFatxDrive.Tag == devIndex)
                                    wasCurrentDevice = true;
                                Main.listFatx.Nodes.Remove(Main.listFatx.Nodes[i]);
                            }
                        for (int i = 1; i < Main.cmdFatxCopyFile.DropDownItems.Count; i++)
                            if ((int)Main.cmdFatxCopyFile.DropDownItems[i].Tag == devIndex)
                                Main.cmdFatxCopyFile.DropDownItems.Remove(Main.cmdFatxCopyFile.DropDownItems[i]);
                        Main.cmdFatxNoDevicesFound.Visible = Main.cmdFatxCopyFile.DropDownItems.Count == 1;
                        Main.Invoke((MethodInvoker)delegate
                        {
                            if (Main.listFatx.Nodes.Count == 0)
                            {
                                Main.tabFatxDrive.Tag = null;
                                Main.tabFatxDrive.Text = "Connect a Device";
                                Main.cmdFatxMod.Image = Resources.QuestionMarkWide;
                                baseButtonReset(Main.cmdFatxGear.Enabled
                                    = Main.cmdFatxToolInject.Enabled
                                    = Main.cmdFatxInject.Enabled
                                    = Main.cmdFatxMod.Enabled
                                    = Main.cmdFatxToolCollapseAll.Visible
                                    = Main.cmdFatxToolClearTitleCache.Visible
                                    = Main.cmdFatxToolRenameDevice.Visible
                                    = Main.fatxMenuLine.Visible
                                    = false);
                            }
                            else if (Main.listFatx.Nodes.Count == 1)
                            {
                                Main.listFatx.SelectedIndex = 0;
                                Main.listFatx.Nodes[0].Expand();
                            }
                            else if (wasCurrentDevice)
                                Main.listFatx.SelectedIndex = 0;
                        });
                        FormHandle.unloadFatxForms(devIndex);
                        Devices[devIndex].Handle = null;
                        Devices[devIndex].CacheHandle = null;
                    });
                    x--;
                }
            }
            refreshDeviceCounter();
        }

        private static void refreshDeviceCounter()
        {
            if (Main.IsHandleCreated)
            {
                try
                {
                    Main.Invoke((MethodInvoker)delegate
                    {
                        Main.cmdFatxDevicesLoaded.Text = String.Format("{0} Device{1} Loaded", Main.listFatx.Nodes.Count, Main.listFatx.Nodes.Count == 1 ? String.Empty : "s");
                        Main.rbFatx.Refresh();
                    });
                }
                catch
                {

                }
            }
        }

        private static long lastInterval = 0;
        private static bool deviceTriggerOperating;
        private static void deviceWatcherTrigger(object sender, EventArrivedEventArgs e)
        {
            ushort triggerType = (ushort)e.NewEvent.Properties["EventType"].Value;
            if ((triggerType == 2 || triggerType == 3) && DateTime.Now.ToFileTime() > lastInterval + 100000)
            {
                if (triggerType == 2)
                    for (int x = 0; x < 5; x++)
                    {
                        if (scanForNewDevices(true))
                            break;
                        Thread.Sleep(10);
                    }
                else
                    removeDevices();
                lastInterval = DateTime.Now.ToFileTime();
            }
            deviceTriggerOperating = false;
        }

        private static void listFatx_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
        }

        private static void listFatx_DragOver(object sender, DragEventArgs e)
        {
            if (((int)e.AllowedEffect & (int)DragDropEffects.Copy) == (int)DragDropEffects.Copy)
            {
                Node hoverNode = Main.listFatx.GetNodeAt(Main.listFatx.PointToClient(new Point(e.X, e.Y)).Y);
                if (hoverNode != null)
                {
                    while (hoverNode.Parent != null)
                        hoverNode = hoverNode.Parent;
                    Main.listFatx.SelectedNode = hoverNode;
                }
            }
        }

        private static void listFatx_DragDrop(object sender, DragEventArgs e)
        {
            if (((int)e.AllowedEffect & (int)DragDropEffects.Copy) == (int)DragDropEffects.Copy && Main.listFatx.SelectedNode != null)
                injectPackages(((FatxNode)Main.listFatx.SelectedNode.Tag).DeviceIndex, (string[])e.Data.GetData(DataFormats.FileDrop));
        }

        internal static ButtonItem[] createDeviceButtonItems()
        {
            return createDeviceButtonItems(-1);
        }

        internal static ButtonItem[] createDeviceButtonItems(int exclude)
        {
            var ct = Main.listFatx.Nodes.Count;

            var deviceItems = new List<ButtonItem>(ct);

            for (var x = 0; x < ct; x++)
            {
                var nodeTag = (FatxNode)Main.listFatx.Nodes[x].Tag;

                if (nodeTag.DeviceIndex == exclude)
                    continue;

                var i = new ButtonItem("cmdUseDevice" + x);
                i.Text = nodeTag.Device.Name;
                i.Image = nodeTag.Device.DeviceImage;
                i.Tag = nodeTag.DeviceIndex;
                i.CanCustomize = false;
                deviceItems.Add(i);
            }

            if (deviceItems.Count != 0)
                return deviceItems.ToArray();

            return new[]
            {
                new ButtonItem
                {
                    Enabled = false,
                    CanCustomize = false,
                    Text = "No Devices Found",
                    Image = Resources.Info
                }
            };
        }

        internal static bool isDeviceClosed(int x)
        {
            for (int i = 0; i < Main.listFatx.Nodes.Count; i++)
                if (((FatxNode)Main.listFatx.Nodes[i].Tag).DeviceIndex == x)
                    return false;
            UI.errorBox("This device has been disconnected!");
            return true;
        }

        public static Node[] traverseDevice(FatxNode nodeTag, bool publicOnly, bool indexing)
        {
            return traverseDevice(nodeTag, publicOnly, indexing, false, 0);
        }

        private static Node[] traverseFat32Device(FatxNode nodeTag, bool publicOnly, bool indexing, bool lockId, uint lockIdValue)
        {
            var listNodes = new List<Node>();
            var usedTitleIds = new List<uint>();
            var rootDir = nodeTag.Device.Fat32Drive.Name;
            var contentDir = new DirectoryInfo(rootDir + "Content");

            if (!contentDir.Exists)
                return listNodes.ToArray();

            foreach (var profileDir in contentDir.GetDirectories())
            {
                ulong profileId;
                if (!ulong.TryParse(profileDir.Name, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out profileId) || (profileId != 0 && publicOnly))
                    continue;

                foreach (var titleDir in profileDir.GetDirectories())
                {
                    uint titleId;
                    if (!uint.TryParse(titleDir.Name, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out titleId) || (lockId && titleId != lockIdValue))
                        continue;

                    foreach (var typeDir in titleDir.GetDirectories())
                    {
                        if (indexing && TitleIDCache.ContainsKey(titleId) && TitleIDCache[titleId] != unknownGame)
                            break;

                        int typeValue;
                        if (!int.TryParse(typeDir.Name, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out typeValue) 
                            || getGeneralContentType((ContentTypes)typeValue) != nodeTag.GeneralContentType)
                            continue;

                        var files = typeDir.GetFiles();
                        var currentFile = 1;
                        foreach (var file in files)
                        {
                            try
                            {
                                var packageIo = new EndianIO(File.OpenRead(file.FullName), EndianType.BigEndian);
                                if (indexing)
                                {
                                    packageIo.Open();
                                    if (packageIo.Stream.Length >= 0x1711)
                                    {
                                        packageIo.Stream.Position = 0x1691;
                                        var titleName = packageIo.In.ReadUnicodeString(64);
                                        packageIo.Close();
                                        if (titleName.Length == 0 && currentFile != files.Length)
                                            continue;
                                        addTitleIDToCache(titleId, titleName);
                                        break;
                                    }
                                    packageIo.Close();
                                }
                                else
                                {
                                    var package = new XContentPackage();
                                    if (package.LoadPackage(packageIo, false))
                                    {
                                        if (package.Header.Metadata.ContentType == (ContentTypes)typeValue
                                            && package.Header.Metadata.Creator == profileId)
                                        {
                                            var newNode = new FatxNode(nodeTag.DeviceIndex, NodeTypes.Package);
                                            newNode.NodeInfo.ContentType = (ContentTypes)typeValue;
                                            fillPackageNode(newNode, package);
                                            newNode.NodeInfo.Fat32FileInfo = file;
                                            newNode.NodeInfo.ProfileID = profileId;
                                            newNode.NodeInfo.TitleID = titleId;
                                            listNodes.Add(buildFatxNode(newNode, imageFromPackage(package)));
                                        }
                                        package.CloseIO(true);
                                    }
                                }
                            }
                            catch { }

                            currentFile++;
                        }
                    }

                    if (indexing && TitleIDCache.ContainsKey(titleId) && !usedTitleIds.Contains(titleId))
                    {
                        var newNode = new FatxNode(nodeTag.DeviceIndex, NodeTypes.Game);
                        usedTitleIds.Add(newNode.NodeInfo.TitleID = titleId);
                        listNodes.Add(buildFatxNode(newNode, getGameNodeImage(titleId)));
                    }
                }
            }

            return listNodes.ToArray();
        }

        private static readonly string unknownGame = "Unknown Game";
        private static Node[] traverseDevice(FatxNode nodeTag, bool publicOnly, bool indexing, bool lockId, uint lockIdValue)
        {
            if (nodeTag.Device.IsFat32)
                return traverseFat32Device(nodeTag, publicOnly, indexing, lockId, lockIdValue);
            List<Node> listNodes = new List<Node>();
            List<uint> usedTitleIds = new List<uint>();
            List<FatxDirectoryEntry> profileEntries = nodeTag.Device.Handle.GetNestedDirectoryEntries(buildDirectoryPath());
            if (profileEntries != null)
                foreach (FatxDirectoryEntry profile in profileEntries)
                {
                    ulong profileId;
                    if (profile.IsDirectory && ulong.TryParse(profile.Filename, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out profileId) && (profileId == 0 || !publicOnly))
                    {
                        List<FatxDirectoryEntry> titleEntries = nodeTag.Device.Handle.GetNestedDirectoryEntries(buildDirectoryPath(profileId));
                        if (titleEntries != null)
                            foreach (FatxDirectoryEntry title in titleEntries)
                            {
                                uint titleId;
                                if (title.IsDirectory && uint.TryParse(title.Filename, NumberStyles.HexNumber,
                                    CultureInfo.InvariantCulture, out titleId) && (!lockId || titleId == lockIdValue))
                                {
                                    List<FatxDirectoryEntry> typeEntries = nodeTag.Device.Handle.GetNestedDirectoryEntries(buildDirectoryPath(profileId, titleId));
                                    if (typeEntries != null)
                                    {
                                        if (!indexing || !TitleIDCache.ContainsKey(titleId) || TitleIDCache[titleId] == unknownGame)
                                            foreach (FatxDirectoryEntry type in typeEntries)
                                            {
                                                if (indexing && TitleIDCache.ContainsKey(titleId) && TitleIDCache[titleId] != unknownGame)
                                                    break;
                                                int typeValue;
                                                if (type.IsDirectory && int.TryParse(type.Filename, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out typeValue)
                                                    && getGeneralContentType((ContentTypes)typeValue) == nodeTag.GeneralContentType)
                                                {
                                                    List<FatxDirectoryEntry> fileEntries = nodeTag.Device.Handle.GetNestedDirectoryEntries(buildDirectoryPath(profileId, titleId, (ContentTypes)typeValue));
                                                    if (fileEntries != null)
                                                    {
                                                        int currentEntry = 1;
                                                        foreach (FatxDirectoryEntry entry in fileEntries)
                                                        {
                                                            if (!entry.IsDirectory)
                                                            {
                                                                try
                                                                {
                                                                    EndianIO packageIo = nodeTag.Device.Handle.LoadFileToIO(buildDirectoryPath(profileId, titleId, (ContentTypes)typeValue, entry.Filename));
                                                                    if (indexing)
                                                                    {
                                                                        packageIo.Open();
                                                                        if (packageIo.Stream.Length >= 0x1711)
                                                                        {
                                                                            packageIo.Stream.Position = 0x1691;
                                                                            string titleName = packageIo.In.ReadUnicodeString(64);
                                                                            packageIo.Close();
                                                                            if (titleName.Length == 0 && currentEntry != fileEntries.Count)
                                                                                continue;
                                                                            addTitleIDToCache(titleId, titleName);
                                                                            break;
                                                                        }
                                                                        packageIo.Close();
                                                                    }
                                                                    else
                                                                    {
                                                                        XContentPackage package = new XContentPackage();
                                                                        if (package.LoadPackage(packageIo, false))
                                                                        {
                                                                            if (package.Header.Metadata.ContentType == (ContentTypes)typeValue
                                                                                && package.Header.Metadata.Creator == profileId)
                                                                            {
                                                                                FatxNode newNode = new FatxNode(nodeTag.DeviceIndex, NodeTypes.Package);
                                                                                newNode.NodeInfo.ContentType = (ContentTypes)typeValue;
                                                                                fillPackageNode(newNode, package);
                                                                                newNode.NodeInfo.Entry = entry;
                                                                                newNode.NodeInfo.ProfileID = profileId;
                                                                                newNode.NodeInfo.TitleID = titleId;
                                                                                listNodes.Add(buildFatxNode(newNode, imageFromPackage(package)));
                                                                            }
                                                                            package.CloseIO(true);
                                                                        }
                                                                    }
                                                                }
                                                                catch { }
                                                            }
                                                            currentEntry++;
                                                        }
                                                    }
                                                }
                                            }
                                        if (indexing && TitleIDCache.ContainsKey(titleId) && !usedTitleIds.Contains(titleId))
                                        {
                                            FatxNode newNode = new FatxNode(nodeTag.DeviceIndex, NodeTypes.Game);
                                            usedTitleIds.Add(newNode.NodeInfo.TitleID = titleId);
                                            listNodes.Add(buildFatxNode(newNode, getGameNodeImage(titleId)));
                                        }
                                    }
                                }
                            }
                    }
                }
            return listNodes.ToArray();
        }

        private static void fillPackageNode(FatxNode newNode, XContentPackage package)
        {
            newNode.NodeInfo.Version = package.Header.Metadata.ExecutionId.Version;
            newNode.NodeInfo.Svod = package.Header.Metadata.VolumeType == XContentVolumeType.SVOD_Volume;
            if (newNode.NodeInfo.Svod)
            {
                newNode.NodeInfo.ReadOnly = true;
                newNode.NodeInfo.NumberOfDataFiles = package.Header.Metadata.DataFiles;
                newNode.NodeInfo.DataFilesSize = package.Header.Metadata.DataFilesSize;
            }
            else
                newNode.NodeInfo.ReadOnly = package.StfsContentPackage.VolumeExtension.ReadOnly;
            string newDisplayName = package.Header.Metadata.DisplayName.Replace(((char)0x1c).ToString(), String.Empty).Replace(((char)0x1d).ToString(), String.Empty);
            if (newDisplayName.Length == 0)
                newNode.NodeInfo.DisplayName = "Content";
            else
                newNode.NodeInfo.DisplayName = newDisplayName;
        }

        private static Image getGameNodeImage(uint titleId)
        {
            string sTitleId = titleId.ToString("X8");
            if (sTitleId == FormID.Crysis2Profile)
                return Resources.GreenDot;
            return FormConfig.formList.FindIndex(curForm => curForm.ID == sTitleId && curForm.Type == FormConfig.FormType.Game_Modder)
                == -1 ? Resources.GrayDot : Resources.GreenDot;
        }

        private static void listFatx_AfterExpand(object sender, AdvTreeNodeEventArgs e)
        {
            if (Main.cmdDock.Checked)
                for (int x = 0; x < 8; x++)
                    if (Main.exFatx.Expanded && Main.cmdFatxExpand.Enabled
                        && Main.colTreeEntry.Width.AutoSizeWidth + Main.colTreeInfo.Width.AutoSizeWidth > Main.listFatx.Size.Width - 25)
                        Main.expandContract(true);
        }

        private static void listFatx_AfterCollapse(object sender, AdvTreeNodeEventArgs e)
        {
            if (Main.cmdDock.Checked)
                for (int x = 0; x < 8; x++)
                    if (Main.exFatx.Expanded && Main.cmdFatxContract.Enabled
                        && Main.colTreeEntry.Width.AutoSizeWidth + Main.colTreeInfo.Width.AutoSizeWidth < Main.listFatx.Size.Width - 46)
                        Main.expandContract(false);
        }

        private static Node createProfileNode(int devIndex, ulong profileId, ProfileInfo pI)
        {
            FatxNode newTag = new FatxNode(devIndex, NodeTypes.Package);
            newTag.NodeInfo.ReadOnly = false;
            newTag.NodeInfo.ProfileID = profileId;
            newTag.NodeInfo.TitleID = dashTitleId;
            newTag.NodeInfo.Entry = pI.Entry;
            newTag.NodeInfo.Fat32FileInfo = pI.Fat32FileInfo;
            newTag.NodeInfo.DisplayName = pI.Gamertag;
            newTag.NodeInfo.ContentType = ContentTypes.Profile;
            return buildFatxNode(newTag, pI.Gamerpic);
        }

        /*private static Node createStfsEntryNode(XContentPackage package, StfsDirectoryEntry entry)
        {
            if (!entry.IsEntryBound)
                return null;
            Node entryNode = new Node(entry.FileName);
            entryNode.Tag = package;
            if (entry.IsDirectory)
            {
                entryNode.ExpandVisibility = eNodeExpandVisibility.Visible;
                entryNode.Image = Resources.FolderClosed;
                entryNode.ImageExpanded = Resources.FolderOpen;
            }
            else
            {
                entryNode.ExpandVisibility = eNodeExpandVisibility.Hidden;
                entryNode.Image = Resources.FileIcon;
            }
            entryNode.Cells.Add(new Cell(makeGrayText("Size: " + Global.getFormatFromBytes(entry.FileBounds.Filesize))));
            return entryNode;
        }*/

        internal static void listFatx_BeforeExpand(object sender, AdvTreeNodeCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
#if !INT2
                try
                {
#endif
                    FatxNode nodeTag = (FatxNode)e.Node.Tag;
                    if (isDeviceWorkerAvailable(nodeTag.DeviceIndex))
                    {
                        if (nodeTag.NodeType == NodeTypes.Package)
                        {
                            /*string curPath = buildDirectoryPath(nodeTag);
                            XContentPackage package = new XContentPackage();
                            if (package.LoadPackage(nodeTag.Device.Handle.LoadFileToIO(curPath), false))
                            {
                                List<Node> entryNodes = new List<Node>();
                                for (int x = 0; x < package.StfsContentPackage.DirectoryEntries.Count; x++)
                                {
                                    Node entryNode = createStfsEntryNode(package, package.StfsContentPackage.DirectoryEntries[x]);
                                    if (entryNode != null)
                                        entryNodes.Add(entryNode);
                                }
                                e.Node.Nodes.AddRange(entryNodes.ToArray());
                                e.Node.Nodes.Sort();
                            }*/
                        }
                        else
                        {
                            if (nodeTag.NodeType == NodeTypes.ContentFolder)
                                switch (nodeTag.GeneralContentType)
                                {
                                    case GeneralContentTypes.Gamer_Profiles:
                                        foreach (KeyValuePair<ulong, ProfileInfo> pI in nodeTag.Device.ProfileCache)
                                            if (pI.Value.Entry != null || pI.Value.Fat32FileInfo != null)
                                                e.Node.Nodes.Add(createProfileNode(nodeTag.DeviceIndex, pI.Key, pI.Value));
                                        break;
                                    case GeneralContentTypes.Games:
                                        e.Node.Nodes.AddRange(traverseDevice(nodeTag, false, true));
                                        saveTitleIdCache();
                                        break;
                                    case GeneralContentTypes.Title_Updates:
                                        if (nodeTag.Device.IsFat32)
                                        {
                                            var cacheDir = new DirectoryInfo(nodeTag.Device.Fat32Drive.Name + "Cache");

                                            if (cacheDir.Exists)
                                            {
                                                var cacheNodes = new List<Node>();
                                                foreach (var cacheFile in cacheDir.GetFiles().Where(f => 
                                                    f.Name.Length == 38
                                                    && f.Name.Substring(0, 3) == "TU_"
                                                    && f.Name[10] == '_'
                                                    && f.Name[24] == '.'))
                                                {
                                                    var package = new XContentPackage();
                                                    if (package.LoadPackage(new EndianIO(new FileStream(cacheFile.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite), EndianType.BigEndian)))
                                                    {
                                                        var newNode = new FatxNode(nodeTag.DeviceIndex, NodeTypes.Package);
                                                        newNode.NodeInfo.ContentType = ContentTypes.Installer;
                                                        fillPackageNode(newNode, package);
                                                        newNode.NodeInfo.Cache = true;
                                                        newNode.NodeInfo.CachePartition = false;
                                                        newNode.NodeInfo.Version = package.Header.Metadata.ExecutionId.Version;
                                                        newNode.NodeInfo.Fat32FileInfo = cacheFile;
                                                        newNode.NodeInfo.ProfileID = package.Header.Metadata.Creator;
                                                        newNode.NodeInfo.TitleID = TitleControl.GetProperTitleID(package);
                                                        cacheNodes.Add(buildFatxNode(newNode, imageFromPackage(package)));
                                                    }
                                                    package.CloseIO(true);
                                                }

                                                e.Node.Nodes.AddRange(cacheNodes.ToArray());
                                            }

                                            e.Node.Nodes.AddRange(traverseDevice(nodeTag, true, false));
                                        }
                                        else
                                        {
                                            nodeTag.Device.MountCachePartition();
                                            FatxDevice contentPartition = nodeTag.Device.Handle;
                                            for (int x = 0; x < 2; x++)
                                            {
                                                if (x == 1)
                                                {
                                                    if (nodeTag.Device.CacheHandle == null)
                                                        break;
                                                    nodeTag.Device.Handle = nodeTag.Device.CacheHandle;
                                                }
                                                List<FatxDirectoryEntry> cacheEntries = nodeTag.Device.Handle.GetNestedDirectoryEntries(cacheFolder);
                                                if (cacheEntries != null)
                                                {
                                                    List<Node> cacheNodes = new List<Node>();
                                                    cacheEntries = nodeTag.Device.Handle.GetNestedDirectoryEntries(cacheFolder).FindAll(
                                                        cacheEntry => cacheEntry.Filename.Length == 38
                                                            && cacheEntry.Filename.Substring(0, 3) == "TU_"
                                                            && cacheEntry.Filename[10] == '_'
                                                            && cacheEntry.Filename[24] == '.');
                                                    foreach (FatxDirectoryEntry cacheEntry in cacheEntries)
                                                    {
                                                        XContentPackage package = new XContentPackage();
                                                        if (package.LoadPackage(nodeTag.Device.Handle.LoadFileToIO(String.Format(@"{0}\{1}", cacheFolder, cacheEntry.Filename))))
                                                        {
                                                            FatxNode newNode = new FatxNode(nodeTag.DeviceIndex, NodeTypes.Package);
                                                            newNode.NodeInfo.ContentType = ContentTypes.Installer;
                                                            fillPackageNode(newNode, package);
                                                            newNode.NodeInfo.Cache = true;
                                                            newNode.NodeInfo.CachePartition = x == 1;
                                                            newNode.NodeInfo.Version = package.Header.Metadata.ExecutionId.Version;
                                                            newNode.NodeInfo.Entry = cacheEntry;
                                                            newNode.NodeInfo.ProfileID = package.Header.Metadata.Creator;
                                                            newNode.NodeInfo.TitleID = TitleControl.GetProperTitleID(package);
                                                            cacheNodes.Add(buildFatxNode(newNode, imageFromPackage(package)));
                                                        }
                                                        package.CloseIO(true);
                                                    }
                                                    e.Node.Nodes.AddRange(cacheNodes.ToArray());
                                                }
                                                e.Node.Nodes.AddRange(traverseDevice(nodeTag, true, false));
                                            }
                                            nodeTag.Device.Handle = contentPartition;
                                        }
                                        break;
                                    case GeneralContentTypes.Gamer_Pictures:
                                    case GeneralContentTypes.Themes:
                                        e.Node.Nodes.AddRange(traverseDevice(nodeTag, false, false));
                                        break;
                                    default:
                                        e.Node.Nodes.AddRange(traverseDevice(nodeTag, true, false));
                                        break;
                                }
                            else if (nodeTag.NodeType == NodeTypes.Game)
                                e.Node.Nodes.AddRange(traverseDevice(nodeTag, false, false, true, nodeTag.NodeInfo.TitleID));
                            if (e.Node.Nodes.Count == 0)
                                e.Node.Nodes.Add(NoContentNode);
                            else
                                e.Node.Nodes.Sort();
                        }
                    }
                    else
                        e.Cancel = true;
#if !INT2
                }
                catch
                {
                    e.Node.Nodes.Add(new Node("Failed to Load Content") { Selectable = false, ExpandVisibility = eNodeExpandVisibility.Hidden });
                }
#endif
            }
        }

        private static void saveTitleIdCache()
        {
            StringCollection titles = new StringCollection();
            foreach (KeyValuePair<uint, string> id in TitleIDCache)
                titles.Add(id.Key.ToString("X8") + id.Value);
            Settings.Default.TitleIDCache = titles;
            Settings.Default.Save();
        }

        private static void cmdFatxMod_Click(object sender, EventArgs e)
        {
            if (((ButtonX)sender).Tag != null)
                FormConfig.formOpenClick(sender, e);
        }

        private static bool baseButtonReset(bool enable)
        {
            return Main.cmdFatxExtract.Enabled
                = Main.cmdFatxToolDelete.Enabled
                = Main.cmdFatxToolExtract.Enabled
                = Main.cmdFatxCopyFile.Enabled
                = enable;
        }

        #region Drag Drop Extraction
        private static FatxNode dragItem;
        private static List<FileSystemWatcher> dragWatcherDrives;
        internal static string dragTempFileFilter = "__Horizon_FATX_*.tmp";
        private static string dragTempFileName = String.Empty;

        private static void dragWatcherDriveTrigger(object sender, FileSystemEventArgs e)
        {
            if (dragItem == null)
                isDraggingNode = false;
            else
            {
                deleteTempFile(e.FullPath);
                if (isDeviceWorkerAvailable(dragItem.DeviceIndex))
                {
                    string fileName = e.FullPath.Substring(0, e.FullPath.LastIndexOf('\\')) + @"\" 
                        + (dragItem.NodeInfo.Fat32FileInfo != null ? dragItem.NodeInfo.Fat32FileInfo.Name : dragItem.NodeInfo.Entry.Filename);
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                    multipleOperationsRunning = numberOfDevicesBusy() > 0;
                    dragItem.Device.Worker = new Thread(new ParameterizedThreadStart(workerThread_Extract));
                    dragItem.Device.Worker.Priority = ThreadPriority.Highest;
                    dragItem.Device.Worker.Start(new DeviceOperationExtractionParameters()
                    {
                        FileName = fileName,
                        nodeTag = dragItem
                    });
                }
            }
        }

        private static void deleteTempFile(string path)
        {
            int x = 50;
            while (--x > 0 && File.Exists(path))
            {
                try { File.Delete(path); }
                catch { }
                Thread.Sleep(100);
            }
        }

        private static void clearDragData()
        {
            deleteTempFile(dragTempFileName);
            dragTempFileName = String.Empty;
            isDraggingNode = false;
            if (dragWatcherDrives != null)
            {
                for (int x = 0; x < dragWatcherDrives.Count; x++)
                    dragWatcherDrives[x].Dispose();
                dragWatcherDrives.Clear();
                dragWatcherDrives = null;
            }
        }

        private static bool isDraggingNode = false;
        private static void listFatx_NodeMouseDown(object sender, TreeNodeMouseEventArgs e)
        {
            clearDragData();
            if (e.Button == MouseButtons.Left)
            {
                FatxNode nodeTag = (FatxNode)e.Node.Tag;
                if (isDraggingNode = (nodeTag != null && nodeTag.NodeType == NodeTypes.Package))
                    dragItem = nodeTag;
            }
        }

        private static void listFatx_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingNode)
            {
                if (e.Button == MouseButtons.Left)
                    try
                    {
                        dragTempFileName = Path.GetTempPath() + dragTempFileFilter.Replace("*", Global.generateCaptchaText());
                        new FileStream(dragTempFileName, FileMode.Create).Close();
                        new FileInfo(dragTempFileName).Attributes
                            = FileAttributes.Hidden
                            | FileAttributes.System
                            | FileAttributes.Temporary
                            | FileAttributes.NotContentIndexed;
                        if (dragWatcherDrives == null)
                        {
                            dragWatcherDrives = new List<FileSystemWatcher>();
                            foreach (DriveInfo drive in DriveInfo.GetDrives())
                                if (drive.IsReady)
                                {
                                    FileSystemWatcher newWatcher = new FileSystemWatcher();
                                    newWatcher.Filter = dragTempFileFilter;
                                    newWatcher.NotifyFilter = NotifyFilters.FileName;
                                    newWatcher.Created += new FileSystemEventHandler(dragWatcherDriveTrigger);
                                    newWatcher.Path = drive.Name;
                                    newWatcher.EnableRaisingEvents = newWatcher.IncludeSubdirectories = true;
                                    dragWatcherDrives.Add(newWatcher);
                                }
                        }
                        Main.DoDragDrop(new DataObject(DataFormats.FileDrop, new string[] { dragTempFileName }), DragDropEffects.Move);
                    }
                    catch { }
                clearDragData();
            }
        }
        #endregion

        private static void listFatx_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            if (e.Node != null)
            {
                FatxNode nodeTag = (FatxNode)e.Node.Tag;
                Main.tabFatxDrive.Tag = nodeTag.DeviceIndex;
                Main.tabFatxDrive.Text = nodeTag.Device.Name;
                Main.Invoke((MethodInvoker)delegate
                {
                    Main.cmdFatxToolInject.Enabled = Main.cmdFatxInject.Enabled = true;
                    Main.rbFatx.Refresh();
                });
                Main.fatxMenuLine.Visible = (nodeTag.NodeType == NodeTypes.Device
                    || (nodeTag.NodeType == NodeTypes.ContentFolder
                    && nodeTag.GeneralContentType == GeneralContentTypes.Games));
                Main.cmdFatxToolRenameDevice.Visible
                    = Main.cmdFatxToolClearTitleCache.Visible
                    = nodeTag.NodeType == NodeTypes.Device;
                Main.cmdFatxToolCollapseAll.Visible = (nodeTag.NodeType == NodeTypes.ContentFolder
                    && nodeTag.GeneralContentType == GeneralContentTypes.Games);
                if (Main.cmdFatxGear.Enabled = baseButtonReset(nodeTag.NodeType == NodeTypes.Package
                    && (nodeTag.NodeInfo.ContentType != ContentTypes.Profile
                    || isDeviceWorkerAvailable(nodeTag.DeviceIndex))))
                {
                    if (Main.cmdFatxMod.AutoExpandOnClick = nodeTag.NodeInfo.ContentType == ContentTypes.Profile)
                    {
                        string curPath = buildDirectoryPath(nodeTag);
                        XContentPackage package = new XContentPackage();

                        using (var io = nodeTag.Device.IsFat32 
                            ? new EndianIO(new FileStream(nodeTag.Device.Fat32Drive.Name + curPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite), EndianType.BigEndian) 
                            : nodeTag.Device.Handle.LoadFileToIO(curPath))
                        if (package.LoadPackage(io, false))
                        {
                            Main.cmdFatxMod.Image = Resources.Profile_Thumb;
                            Main.cmdFatxMod.Enabled = true;
                            Main.cmdFatxMod.Tag = null;
                            Main.galFatx.SubItems.Clear();
                            List<FormConfig.FormMeta> formList = FormConfig.formList.FindAll(meta =>
                                meta.Type == FormConfig.FormType.Profile_Modder || (meta.Type == FormConfig.FormType.GPD_Modder
                                && package.StfsContentPackage.GetDirectoryEntryIndex(TitleControl.GetProperTitleIDForGpd(meta.ID) + ".gpd") != -1)
                                || meta.ID == FormID.GamerPictureManager);
                            foreach (FormConfig.FormMeta meta in formList)
                            {
                                ButtonItem bI = new ButtonItem();
                                if (meta.ID == FormID.GamerPictureManager)
                                    bI.Text = "Change Gamer Picture";
                                else
                                    bI.Text = meta.FullName;
                                bI.Image = meta.Thumbnail;
                                bI.ImagePaddingVertical = 9;
                                bI.ImagePosition = eImagePosition.Top;
                                bI.Shape = new RoundRectangleShapeDescriptor();
                                bI.ColorTable = eButtonColor.OrangeWithBackground;
                                bI.Click += new EventHandler(FormConfig.formOpenClick);
                                FormConfig.ButtonMeta buttonHandle = new FormConfig.ButtonMeta();
                                buttonHandle.FatxPath = curPath;
                                buttonHandle.DeviceIndex = nodeTag.DeviceIndex;
                                buttonHandle.FormMetaIndex = FormConfig.getFormMetaIndex(meta.ID);
                                buttonHandle.CachePartition = nodeTag.NodeInfo.CachePartition;
                                bI.Tag = buttonHandle;
                                Main.galFatx.SubItems.Add(bI);
                            }
                        }
                        else
                        {
                            Main.cmdFatxMod.Image = Resources.QuestionMarkWide;
                            Main.cmdFatxGear.Enabled = Main.cmdFatxMod.Enabled = false;
                        }
                    }
                    else
                    {
                        FormConfig.ButtonMeta buttonHandle = new FormConfig.ButtonMeta();
                        buttonHandle.CachePartition = nodeTag.NodeInfo.CachePartition;
                        buttonHandle.DeviceIndex = nodeTag.DeviceIndex;
                        buttonHandle.FatxPath = buildDirectoryPath(nodeTag);
                        if (nodeTag.NodeInfo.ContentType == ContentTypes.ThematicSkin)
                        {
                            buttonHandle.FormMetaIndex = FormConfig.getFormMetaIndex(FormID.ThemeCreator);
                            Main.cmdFatxMod.Image = Resources.Theme_Thumb;
                            Main.cmdFatxMod.Enabled = true;
                        }
                        else if (nodeTag.NodeInfo.ContentType == ContentTypes.GamerPicture)
                        {
                            buttonHandle.FormMetaIndex = FormConfig.getFormMetaIndex(FormID.GamerPictureManager);
                            Main.cmdFatxMod.Image = Resources.GamerPictureManager_Thumb;
                            Main.cmdFatxMod.Enabled = true;
                        }
                        else if (!nodeTag.NodeInfo.ReadOnly && nodeTag.NodeInfo.ContentType == ContentTypes.SavedGame)
                        {
                            buttonHandle.FormMetaIndex = 255;
                            if (TitleControl.IsEffected(nodeTag.NodeInfo.TitleID))
                            {
                                if (!isDeviceWorkerAvailable(nodeTag.DeviceIndex))
                                {
                                    Main.listFatx.SelectedNode = e.Node.Parent;
                                    return;
                                }
                                string curPath = buildDirectoryPath(nodeTag);

                                using (
                                    var io = nodeTag.Device.IsFat32
                                        ? new EndianIO(nodeTag.NodeInfo.Fat32FileInfo.OpenRead(), EndianType.BigEndian)
                                        : nodeTag.Device.Handle.LoadFileToIO(curPath))
                                {
                                    XContentPackage package = new XContentPackage();
                                    if (!package.LoadPackage(io, false)
                                        || !TitleControl.ValidSaveGamePackage(package, nodeTag.NodeInfo.TitleID, ref buttonHandle.FormMetaIndex))
                                    {
                                        Main.cmdFatxMod.Image = Resources.QuestionMarkWide;
                                        Main.cmdFatxMod.Enabled = false;
                                        goto done;
                                    }
                                }
                            }
                            if (buttonHandle.FormMetaIndex == 255)
                            {
                                string tempId = nodeTag.NodeInfo.TitleID.ToString("X8");
                                if (tempId == FormID.Crysis2Profile)
                                    tempId = FormID.Crysis2Save;
                                buttonHandle.FormMetaIndex = FormConfig.getFormMetaIndex(tempId);
                            }
                            Main.cmdFatxMod.Image = (Main.cmdFatxMod.Enabled = (buttonHandle.FormMetaIndex != 255
                                && FormConfig.formList[buttonHandle.FormMetaIndex].Type == FormConfig.FormType.Game_Modder))
                                ? FormConfig.formList[buttonHandle.FormMetaIndex].Thumbnail : Resources.QuestionMarkWide;
                        }
                        else
                        {
                            Main.cmdFatxMod.Image = Resources.QuestionMarkWide;
                            Main.cmdFatxMod.Enabled = false;
                        }
                    done:
                        Main.cmdFatxMod.Tag = buttonHandle;
                    }
                    FormConfig.ButtonMeta gearHandle = new FormConfig.ButtonMeta();
                    gearHandle.DeviceIndex = nodeTag.DeviceIndex;
                    gearHandle.FatxPath = buildDirectoryPath(nodeTag);
                    gearHandle.FormMetaIndex = FormConfig.getFormMetaIndex(FormID.PackageManager);
                    gearHandle.CachePartition = nodeTag.NodeInfo.CachePartition;
                    Main.cmdFatxGear.Tag = gearHandle;
                }
                else if (nodeTag.NodeInfo.ContentType == ContentTypes.Profile)
                    Main.listFatx.SelectedNode = e.Node.Parent;
                else
                {
                    Main.cmdFatxMod.Image = Resources.QuestionMarkWide;
                    Main.cmdFatxGear.Enabled = Main.cmdFatxMod.Enabled = false;
                }
            }
        }

        internal static void listFatx_NodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (Main.cmdFatxMod.Enabled && ((FatxNode)e.Node.Tag).GeneralContentType != GeneralContentTypes.Gamer_Profiles)
                cmdFatxMod_Click(Main.cmdFatxMod, null);
            else if (Main.cmdFatxGear.Enabled)
                FormConfig.formOpenClick(Main.cmdFatxGear, null);
        }

        internal static void cmdFatxCopyFile_Click(object sender, EventArgs e)
        {
            if (Main.listFatx.SelectedNodes.Count == 1)
            {
                int toDevice = (int)((ToolStripMenuItem)sender).Tag;
                FatxNode nodeTag = (FatxNode)Main.listFatx.SelectedNode.Tag;
                if (isDeviceWorkerAvailable(nodeTag.DeviceIndex) && isDeviceWorkerAvailable(toDevice))
                {
                    if (FatxDevice.FatxIsValidFatFileName(nodeTag.NodeInfo.Entry.Filename))
                    {
                        string filePath = buildDirectoryPath(nodeTag);
                        FormHandle.FormConfig pConfig = FormHandle.isFatxFileLoaded(nodeTag.DeviceIndex, filePath);
                        if (pConfig != null && pConfig.Meta.Type == FormConfig.FormType.Profile_Modder)
                            UI.messageBox(nodeTag.NodeInfo.DisplayName
                                + " is currently open in the " + pConfig.Meta.FullName + ".\n\nYou must close that editor before copying the profile!",
                                "File in Use", MessageBoxIcon.Error, MessageBoxButtons.OK);
                        else
                        {
                            DeviceOperationInjectionParameters i = new DeviceOperationInjectionParameters()
                            {
                                FromDeviceIndex = nodeTag.DeviceIndex,
                                ToDeviceIndex = toDevice,
                                CachePartition = nodeTag.NodeInfo.CachePartition
                            };
                            XContentPackage package = new XContentPackage();
                            if (package.LoadPackage(nodeTag.Device.IsFat32 
                                ? (new EndianIO(File.Open(nodeTag.Device.Fat32Drive.Name + filePath, FileMode.Open), EndianType.BigEndian)) 
                                : (i.CachePartition
                                ? nodeTag.Device.CacheHandle.LoadFileToIO(filePath)
                                : nodeTag.Device.Handle.LoadFileToIO(filePath))))
                            {
                                i.addPackage(package, nodeTag.NodeInfo.Fat32FileInfo != null ? nodeTag.NodeInfo.Fat32FileInfo.Name : nodeTag.NodeInfo.Entry.Filename);
                                if (package.Header.Metadata.ContentType == ContentTypes.SavedGame
                                    && (Devices[i.ToDeviceIndex].ProfileCache.Count != 0
                                    || ProfileManager.profileData.Count != 0)
                                    && !ProfileManager.isFavorite(package.Header.Metadata.Creator))
                                    new Forms.ProfileSelector(i).Show();
                                else
                                    injectPackages(i);
                            }
                        }
                    }
                    else
                        UI.errorBox("This package cannot be copied because its filename contains invalid characters!");
                }
            }
        }

        private struct DeviceOperationExtractionParameters
        {
            internal string FileName;
            internal FatxNode nodeTag;
        }

        private static int numberOfDevicesBusy()
        {
            int x = deviceTriggerOperating ? 1 : 0;
            foreach (Node node in Main.listFatx.Nodes)
                if (((FatxNode)node.Tag).Device.WorkerBusy)
                    x++;
            return x;
        }

        private static bool multipleOperationsRunning = false;
        private static void updateProgressBar(ProgressBarStyle style, int value, int max)
        {
            TaskbarProgressState taskState = TaskbarProgressState.NoProgress;
            if (multipleOperationsRunning || deviceTriggerOperating)
            {
                max = 100;
                value = 0;
                style = ProgressBarStyle.Marquee;
            }
            if (style == ProgressBarStyle.Marquee)
                taskState = TaskbarProgressState.Indeterminate;
            else if (max != 0)
                taskState = TaskbarProgressState.Normal;
            TaskbarManager.setProgressState(taskState);
            if (taskState == TaskbarProgressState.Normal)
                TaskbarManager.setProgressValue(value, max);
            if (Main.progressFatx.InvokeRequired)
                Main.progressFatx.Invoke((MethodInvoker)delegate
                {
                    Main.progressFatx.Maximum = max;
                    Main.progressFatx.Value = value;
                    Main.progressFatx.Style = style;
                });
            else
            {
                Main.progressFatx.Maximum = max;
                Main.progressFatx.Value = value;
                Main.progressFatx.Style = style;
            }
        }

        internal static bool isDeviceWorkerAvailable(int x)
        {
            if (x == -1)
                return true;
            if (isDeviceClosed(x))
                return false;
            if (Devices[x].WorkerBusy || Devices[x].CopyingFile || FormHandle.isDeviceWorkerThreadRunning(x))
            {
                showOperationRunningMessage();
                return false;
            }
            return true;
        }

        internal static void showOperationRunningMessage()
        {
            UI.messageBox("There is already an operation being performed on this device.\nPlease wait until that is finished.", "Active Operation", MessageBoxIcon.Warning);
        }

        internal static void cmdFatxExtract_Click(object sender, EventArgs e)
        {
            FatxNode nodeTag = (FatxNode)Main.listFatx.SelectedNode.Tag;
            if (isDeviceWorkerAvailable(nodeTag.DeviceIndex))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Extracting " + nodeTag.NodeInfo.DisplayName;
                sfd.FileName = nodeTag.NodeInfo.Fat32FileInfo != null ? nodeTag.NodeInfo.Fat32FileInfo.Name : nodeTag.NodeInfo.Entry.Filename;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    multipleOperationsRunning = numberOfDevicesBusy() > 0;
                    nodeTag.Device.Worker = new Thread(new ParameterizedThreadStart(workerThread_Extract));
                    nodeTag.Device.Worker.Priority = ThreadPriority.Highest;
                    nodeTag.Device.Worker.Start(new DeviceOperationExtractionParameters()
                    {
                        FileName = sfd.FileName,
                        nodeTag = nodeTag
                    });
                }
            }
        }

        private static string buildDataFilePath(string packagePath, int dataFile)
        {
            return String.Format(@"{0}.data\Data{1:D4}", packagePath, dataFile);
        }

        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

        private static bool hasRequiredDataFiles(int deviceIndex, string contentPath, uint amount)
        {
            var device = Devices[deviceIndex];
            if (device.IsFat32)
            {
                return hasRequiredDataFiles(device.Name + contentPath + ".data", amount);
            }
            return hasRequiredDataFiles(Devices[deviceIndex].Handle.GetNestedDirectoryEntries(contentPath + ".data"), amount);
        }

        private static bool hasRequiredDataFiles(List<FatxDirectoryEntry> dataFiles, uint amount)
        {
            if (dataFiles == null)
                return false;
            for (uint x = 0; x < amount; x++)
                if (dataFiles.FindIndex(dataFile => dataFile.Filename == String.Format("Data{0:D4}", x)) == -1)
                    return false;
            return true;
        }

        private static bool hasRequiredDataFiles(string dataDir, uint amount)
        {
            var basePath = dataDir + "\\Data";

            for (var x = 0; x < amount; x++)
            {
                if (!File.Exists(basePath + x.ToString("D4")))
                    return false;
            }

            return true;
        }

        private static void setTimeCreatedModified(string filePath, FatxDirectoryEntry entry)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    File.SetLastAccessTime(filePath, intToDateTime(entry.LastAccessTimeStamp));
                    File.SetLastWriteTime(filePath, intToDateTime(entry.LastWriteTimeStamp));
                    File.SetCreationTime(filePath, intToDateTime(entry.CreationTimeStamp));
                }
                catch
                {

                }
            }
        }

        private static void workerThread_Extract(object operationParameters)
        {
            bool extractionFailed = true;
            DeviceOperationExtractionParameters oP = (DeviceOperationExtractionParameters)operationParameters;
            string fatxPath = buildDirectoryPath(oP.nodeTag);
            FormHandle.FormConfig pConfig = FormHandle.isFatxFileLoaded(oP.nodeTag.DeviceIndex, fatxPath);
            if (pConfig != null && pConfig.Meta.Type == FormConfig.FormType.Profile_Modder)
                UI.messageBox(oP.nodeTag.NodeInfo.DisplayName
                    + " is currently open in the " + pConfig.Meta.FullName + ".\n\nYou must close that editor before extracting the profile!",
                    "File in Use", MessageBoxIcon.Error, MessageBoxButtons.OK);
            else
            {
                FileStream fs = null;
                FatxDevice contentPartition = oP.nodeTag.Device.Handle;
                if (oP.nodeTag.NodeInfo.CachePartition)
                    oP.nodeTag.Device.Handle = oP.nodeTag.Device.CacheHandle;
#if !INT2
            try
            {
#endif
                Stream IO = oP.nodeTag.Device.IsFat32 
                    ? File.OpenRead(oP.nodeTag.Device.Fat32Drive.Name + fatxPath) 
                    : (Stream)oP.nodeTag.Device.Handle.LoadFileStream(fatxPath);
                fs = new FileStream(oP.FileName, FileMode.Create, FileAccess.Write);
                if (oP.nodeTag.NodeInfo.Svod)
                {
                    byte[] buffer = new byte[oP.nodeTag.NodeInfo.Entry.FileSize];
                    fs.Write(buffer, 0, IO.Read(buffer, 0, buffer.Length));
                    fs.Close();
                    IO.Close();
                    List<FatxDirectoryEntry> dataFiles = null;
                    
                    if (!oP.nodeTag.Device.IsFat32)
                        dataFiles = oP.nodeTag.Device.Handle.GetNestedDirectoryEntries(fatxPath + ".data");

                    if ((!oP.nodeTag.Device.IsFat32 && hasRequiredDataFiles(dataFiles, oP.nodeTag.NodeInfo.NumberOfDataFiles))
                        || (oP.nodeTag.Device.IsFat32 && hasRequiredDataFiles(oP.nodeTag.Device.Fat32Drive.Name + fatxPath + ".data", oP.nodeTag.NodeInfo.NumberOfDataFiles)))
                    {
                        Directory.CreateDirectory(oP.FileName + ".data");
                        int bLength = 5326848;
                        buffer = new byte[bLength];
                        int partsInLastFile;
                        if (oP.nodeTag.Device.IsFat32)
                            partsInLastFile = (int)(new FileInfo(oP.nodeTag.Device.Fat32Drive.Name + fatxPath + String.Format(".data\\Data{0:D4}", dataFiles.Count - 1)).Length / bLength);
                        else
                            partsInLastFile = (int)(getFileEntry(String.Format("Data{0:D4}", dataFiles.Count - 1), dataFiles).FileSize / bLength);
                        int totalParts = (dataFiles.Count * 32) + partsInLastFile;
                        int currentPart = 0;
                        for (int x = 0; x < dataFiles.Count - 1; x++)
                        {
                            if (oP.nodeTag.Device.IsFat32)
                                IO = File.OpenRead(oP.nodeTag.Device.Fat32Drive.Name + buildDataFilePath(fatxPath, x));
                            else
                                IO = oP.nodeTag.Device.Handle.LoadFileStream(buildDataFilePath(fatxPath, x));
                            fs = new FileStream(buildDataFilePath(oP.FileName, x), FileMode.Create);
                            int currentMax = 32 * (x + 1);
                            while (currentPart < currentMax)
                            {
                                fs.Write(buffer, 0, IO.Read(buffer, 0, bLength));
                                updateProgressBar(ProgressBarStyle.Blocks, ++currentPart, totalParts);
                            }
                            fs.Close();
                            IO.Close();
                        }
                        int lastDataFile = dataFiles.Count - 1;
                        if (oP.nodeTag.Device.IsFat32)
                            IO = File.OpenRead(oP.nodeTag.Device.Fat32Drive.Name + buildDataFilePath(fatxPath, lastDataFile));
                        else
                            IO = oP.nodeTag.Device.Handle.LoadFileToIO(buildDataFilePath(fatxPath, lastDataFile)).Stream;
                        fs = new FileStream(buildDataFilePath(oP.FileName, lastDataFile), FileMode.Create);
                        for (int x = 0; x < partsInLastFile; x++)
                        {
                            fs.Write(buffer, 0, IO.Read(buffer, 0, bLength));
                            updateProgressBar(ProgressBarStyle.Blocks, ++currentPart, totalParts);
                        }
                        if (fs.Length != IO.Length)
                        {
                            buffer = new byte[IO.Length - fs.Length];
                            fs.Write(buffer, 0, IO.Read(buffer, 0, buffer.Length));
                        }
                        fs.Close();
                        IO.Close();
                    }
                    else
                    {
                        File.Delete(oP.FileName);
                        Main.Invoke((MethodInvoker)delegate
                        {
                            TaskbarManager.setProgressState(TaskbarProgressState.Error);
                            UI.errorBox("Your " + getContentTypeString(oP.nodeTag.NodeInfo.ContentType) + " package is missing data files!");
                        });
                        multipleOperationsRunning = numberOfDevicesBusy() > 2;
                        updateProgressBar(ProgressBarStyle.Blocks, 0, 0);
                        return;
                    }
                }
                else
                {
                    copyStream(IO, fs);
                    IO.Close();
                }

                if (!oP.nodeTag.Device.IsFat32)
                {
                    try { File.SetLastAccessTime(oP.FileName, intToDateTime(oP.nodeTag.NodeInfo.Entry.LastAccessTimeStamp)); } catch { }
                    try { File.SetLastWriteTime(oP.FileName, intToDateTime(oP.nodeTag.NodeInfo.Entry.LastWriteTimeStamp)); } catch { }
                    try { File.SetCreationTime(oP.FileName, intToDateTime(oP.nodeTag.NodeInfo.Entry.CreationTimeStamp)); } catch { }
                }
                
                extractionFailed = false;
#if !INT2
            }
            catch (FileNotFoundException)
            {
                TaskbarManager.setProgressState(TaskbarProgressState.Error);
                UI.errorBox("File extraction failed!\n\nYour device has been disconnected!");
            }
            catch (Exception ex)
            {
                TaskbarManager.setProgressState(TaskbarProgressState.Error);
                UI.errorBox("An error has occured while extracting your file!\nIf you see a developer, tell him this:\n\n" + ex.Message);
            }
#endif
                if (fs != null)
                    fs.Close();
                oP.nodeTag.Device.Handle = contentPartition;
                updateProgressBar(ProgressBarStyle.Blocks, 1, 1);
                string[] splitPath = oP.FileName.Split('\\');
                if (splitPath[splitPath.Length - 2] == "Desktop")
                    SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
                if (!extractionFailed)
                {
                    TaskbarManager.setProgressState(TaskbarProgressState.Paused);
                    UI.messageBox(String.Format("{0} was successfully extracted to\n{1}", oP.nodeTag.NodeInfo.DisplayName, oP.FileName));
                }
            }
            multipleOperationsRunning = numberOfDevicesBusy() > 2;
            updateProgressBar(ProgressBarStyle.Blocks, 0, 0);
        }

        internal static void injectPackages(int toDevice, string[] fileNames)
        {
            injectPackages(toDevice, fileNames, null);
        }

        internal static void injectPackages(int toDevice, string[] fileNames, EditorControl sender)
        {
            if (fileNames.Length == 0 || fileNames[0] == null || fileNames[0].Length == 0)
                return;
            if (isDeviceWorkerAvailable(toDevice))
            {
                var i = new DeviceOperationInjectionParameters()
                {
                    ToDeviceIndex = toDevice,
                    Sender = sender
                };
                ulong lastProfileId = 0;
                int numberOfSavedGames = 0;

                if (Devices[toDevice].IsFat32 && Devices[toDevice].Fat32Drive.Name[0].ToString().ToLower() == fileNames[0][0].ToString().ToLower())
                {
                    UI.errorBox("The source device cannot be the same as the one you wish to copy the files to.");
                    return;
                }

                foreach (string fileName in fileNames)
                {
                    string entryName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
                    if (FatxDevice.FatxIsValidFatFileName(entryName))
                    {
                        XContentPackage package = new XContentPackage();
                        if (package.LoadPackage(fileName, false))
                        {
                            if (package.Header.Metadata.ContentType == ContentTypes.SavedGame)
                            {
                                lastProfileId = package.Header.Metadata.Creator;
                                numberOfSavedGames++;
                            }
                            i.addPackage(package, entryName);
                        }
                        else
                            UI.errorBox("Invalid STFS Package!\n\n" + fileName);
                    }
                    else
                        UI.errorBox("The following file name contains invalid characters:\n" + entryName);
                }
                bool readyToInject = numberOfSavedGames == 0 || ((Devices[toDevice].ProfileCache.Count == 0
                    && ProfileManager.profileData.Count == 0));
                if (!readyToInject && numberOfSavedGames == 1 && ProfileManager.isFavorite(lastProfileId))
                    readyToInject = true;
                if (!readyToInject && ProfileManager.profileData.Count(p => p.Favorite) == 0)
                    readyToInject = true;
                if (readyToInject)
                {
                    Devices[toDevice].Worker = new Thread(new ParameterizedThreadStart(workerThread_Inject));
                    Devices[toDevice].Worker.Priority = ThreadPriority.Highest;
                    Devices[toDevice].Worker.Start(i);
                }
                else
                    new Forms.ProfileSelector(i).Show();
            }
        }

        internal static bool injectPackages(DeviceOperationInjectionParameters i)
        {
            if (isDeviceWorkerAvailable(i.ToDeviceIndex) && (i.FromDeviceIndex == -1 || isDeviceWorkerAvailable(i.FromDeviceIndex)))
            {
                if (i.FromDeviceIndex != -1)
                    Devices[i.FromDeviceIndex].CopyingFile = true;
                Devices[i.ToDeviceIndex].Worker = new Thread(new ParameterizedThreadStart(workerThread_Inject));
                Devices[i.ToDeviceIndex].Worker.Priority = ThreadPriority.Highest;
                Devices[i.ToDeviceIndex].Worker.Start(i);
                return true;
            }
            return false;
        }

        internal static void cmdFatxInject_Click(object sender, EventArgs e)
        {
            if (isDeviceWorkerAvailable((int)Main.tabFatxDrive.Tag))
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Select Packages to Inject";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                    injectPackages((int)Main.tabFatxDrive.Tag, ofd.FileNames);
            }
        }

        internal class PackageCache
        {
            internal XContentPackage Package;
            internal ulong NewProfileId;
            internal bool ChangeId = false;
            internal string DisplayName;
            internal string FileName;
        }

        internal class DeviceOperationInjectionParameters
        {
            internal EditorControl Sender;
            internal List<PackageCache> PackageData = new List<PackageCache>();
            internal int FromDeviceIndex = -1;
            internal int ToDeviceIndex;
            internal bool CachePartition;

            internal void addPackage(XContentPackage package, string fileName)
            {
                string displayName = package.Header.Metadata.DisplayName;
                if (package.Header.Metadata.ContentType == ContentTypes.Profile)
                    try
                    {
                        displayName = new XProfile.XProfileAccount(package.StfsContentPackage.ExtractFileToArray("Account")).Info.GamerTag;
                    }
                    catch { }
                PackageData.Add(new PackageCache()
                {
                    Package = package,
                    DisplayName = displayName,
                    FileName = fileName
                });
            }

            internal void HideSender()
            {
                if (Sender != null)
                    Sender.Hide();
            }

            internal void ShowSender()
            {
                if (Sender != null)
                    Sender.Show();
            }
        }

        private static void copyStream(Stream input, Stream output)
        {
            int bLength = 5326848;
            byte[] buffer = new byte[bLength];
            int numLoops = (int)(input.Length / bLength);
            for (int x = 0; x < numLoops; x++)
            {
                output.Write(buffer, 0, input.Read(buffer, 0, bLength));
                updateProgressBar(ProgressBarStyle.Blocks, x, numLoops);
            }
            if (output.Position != input.Length)
            {
                buffer = new byte[input.Length - output.Position];
                output.Write(buffer, 0, input.Read(buffer, 0, buffer.Length));
            }
            output.Close();
        }

        private static void workerThread_Inject(object operationParameters)
        {
            int overwrites = 0;
            DeviceOperationInjectionParameters i = (DeviceOperationInjectionParameters)operationParameters;
            FatxDevice contentPartition = null;
            if (i.CachePartition)
            {
                contentPartition = Devices[i.FromDeviceIndex].Handle;
                Devices[i.FromDeviceIndex].Handle = Devices[i.FromDeviceIndex].CacheHandle;
            }
            string completedTransfers = String.Empty;
            #if !INT2
            try
            {
            #endif
                foreach (PackageCache pc in i.PackageData)
                {
                    #if !INT2
                    try
                    {
                    #endif
                        if ((ulong)getFreeSpace(i.ToDeviceIndex) < (ulong)pc.Package.IO.Length + pc.Package.Header.Metadata.DataFilesSize)
                        {
                            UI.errorBox("There is not enough space on your device to inject the following package:\n" + pc.DisplayName);
                            break;
                        }
                        bool overwriteFile = false;
                        string magicName = smartRenameFatxEntry(pc.Package);
                        if (magicName != null)
                            pc.FileName = magicName;
                        uint realTitleId = TitleControl.GetProperTitleID(pc.Package);
                        string fatxPath = buildDirectoryPath(pc.ChangeId ? pc.NewProfileId : pc.Package.Header.Metadata.Creator,
                            realTitleId, pc.Package.Header.Metadata.ContentType);
                        if (pc.Package.Header.Metadata.VolumeType == XContentVolumeType.SVOD_Volume)
                        {
                            bool hasDataFiles = false;
                            if (i.FromDeviceIndex == -1)
                            {
                                string dataDir = pc.Package.PackageFilePath + ".data";
                                if (Directory.Exists(dataDir))
                                {
                                    int numFound = 0;
                                    string[] dataFiles = Directory.GetFiles(dataDir, "Data*", SearchOption.TopDirectoryOnly);
                                    for (int x = 0; x < pc.Package.Header.Metadata.DataFiles; x++)
                                        foreach (string dataFile in dataFiles)
                                            if (dataFile == String.Format(@"{0}\Data{1:D4}", dataDir, x))
                                                numFound++;
                                    hasDataFiles = numFound == pc.Package.Header.Metadata.DataFiles;
                                }
                            }
                            else
                                hasDataFiles = hasRequiredDataFiles(i.FromDeviceIndex, buildDirectoryPath(pc.Package.Header.Metadata.Creator,
                                    realTitleId, pc.Package.Header.Metadata.ContentType, pc.FileName),
                                    pc.Package.Header.Metadata.DataFiles);
                            if (!hasDataFiles)
                            {
                                if (i.FromDeviceIndex == -1)
                                    UI.errorBox("The following package is missing required data files and cannot be copied:\n\n" + pc.Package.PackageFilePath);
                                else
                                    UI.errorBox("The content that you are attempting to copy is missing required data files!");
                                continue;
                            }
                        }
                        string newFilePath = fatxPath + @"\" + pc.FileName;
                        List<FatxDirectoryEntry> entries = null;
                        if (!Devices[i.ToDeviceIndex].IsFat32)
                            entries = Devices[i.ToDeviceIndex].Handle.GetNestedDirectoryEntries(fatxPath);
                        if ((!Devices[i.ToDeviceIndex].IsFat32 && entries != null && getFileEntry(pc.FileName, entries) != null)
                            || (Devices[i.ToDeviceIndex].IsFat32 && File.Exists(Devices[i.ToDeviceIndex].Fat32Drive.Name + newFilePath)))
                        {
                            if (overwrites != -1)
                            {
                                if (UI.messageBox(pc.DisplayName + " already exists on the destination device.\n\nWould you like to overwrite it?", "Overwrite File?",
                                MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3) != DialogResult.Yes)
                                {
                                    if (i.Sender == null)
                                        pc.Package.CloseIO(true);
                                    continue;
                                }
                                if (++overwrites == 3 && UI.messageBox("Would you like to overwrite the rest of the files you are injecting?", "Overwrite All?",
                                MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    overwrites = -1;
                            }
                            if (FormHandle.isFatxFileLoaded(i.ToDeviceIndex, newFilePath) != null)
                            {
                                UI.messageBox(pc.DisplayName
                                    + " is currently open in an editor.\n\nClose the editor and inject the file again to overwrite.",
                                    "File in Use", MessageBoxIcon.Warning);
                                if (i.Sender == null)
                                    pc.Package.CloseIO(true);
                                continue;
                            }
                            if (pc.Package.Header.Metadata.VolumeType == XContentVolumeType.SVOD_Volume)
                                deleteSvodData(i.ToDeviceIndex, fatxPath + @"\" + pc.FileName + ".data");
                            overwriteFile = true;
                        }

                        Stream fs;
                        if (Devices[i.ToDeviceIndex].IsFat32)
                        {
                            Directory.CreateDirectory(
                                Path.GetDirectoryName(Devices[i.ToDeviceIndex].Fat32Drive.Name + newFilePath));
                            fs = File.Open(Devices[i.ToDeviceIndex].Fat32Drive.Name + newFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                        }
                        else
                            fs = overwriteFile
                                ? Devices[i.ToDeviceIndex].Handle.OverwriteFileFromStream(newFilePath, pc.Package.IO.Length)
                                : Devices[i.ToDeviceIndex].Handle.CreateFileStream(newFilePath, pc.Package.IO.Length);
                        pc.Package.IO.Position = 0;
                        if (pc.Package.Header.Metadata.VolumeType == XContentVolumeType.SVOD_Volume)
                        {
                            byte[] buffer = new byte[pc.Package.IO.Length];
                            fs.Write(buffer, 0, pc.Package.IO.Stream.Read(buffer, 0, buffer.Length));
                        }
                        else
                        {
                            copyStream(pc.Package.IO.Stream, fs);
                            if (pc.ChangeId)
                            {
                                XContentPackage injectedPackage = new XContentPackage();
                                if (injectedPackage.LoadPackage(new EndianIO(fs, EndianType.BigEndian, true), false))
                                {
                                    injectedPackage.Header.Metadata.Creator = pc.NewProfileId;
                                    injectedPackage.Save();
                                    injectedPackage.Flush();
                                    injectedPackage.CloseIO(true);
                                }
                                else
                                    fs.Close();
                            }
                            else
                                fs.Close();
                        }
                        if (pc.Package.Header.Metadata.VolumeType == XContentVolumeType.SVOD_Volume)
                        {
                            string fromSvodFile = buildDirectoryPath(pc.Package.Header.Metadata.Creator,
                                realTitleId, pc.Package.Header.Metadata.ContentType, pc.FileName);
                            int numberOfDataFiles = (int)pc.Package.Header.Metadata.DataFiles;
                            byte[] buffer = new byte[5326848];
                            int partsInLastFile;
                            if (i.FromDeviceIndex == -1)
                                partsInLastFile =
                                    (int)
                                        (new FileInfo(buildDataFilePath(pc.Package.PackageFilePath,
                                            numberOfDataFiles - 1)).Length / buffer.Length);
                            else
                            {
                                if (Devices[i.FromDeviceIndex].IsFat32)
                                {
                                    partsInLastFile = (int)(new FileInfo(Devices[i.FromDeviceIndex].Fat32Drive.Name + fromSvodFile +
                                                 ".data\\Data" + (numberOfDataFiles - 1).ToString("D4")).Length / buffer.Length);
                                }
                                else
                                {
                                    partsInLastFile = (int)(getFileEntry("Data" + (numberOfDataFiles - 1).ToString("D4"),
                                        Devices[i.FromDeviceIndex].Handle.GetNestedDirectoryEntries(fromSvodFile + ".data")).FileSize / buffer.Length);
                                }
                            }
                            
                            Stream IO;
                            int totalParts = (numberOfDataFiles * 32) + partsInLastFile;
                            int currentPart = 0;
                            for (int x = 0; x < numberOfDataFiles - 1; x++)
                            {
                                if (i.FromDeviceIndex == -1)
                                    IO = new FileStream(buildDataFilePath(pc.Package.PackageFilePath, x), FileMode.Open);
                                else if (Devices[i.FromDeviceIndex].IsFat32)
                                {
                                    IO = new FileStream(Devices[i.FromDeviceIndex].Fat32Drive.Name + buildDataFilePath(fromSvodFile, x), FileMode.Open);
                                }
                                else
                                {
                                    IO = Devices[i.FromDeviceIndex].Handle.LoadFileStream(buildDataFilePath(fromSvodFile, x));
                                }

                                if (Devices[i.ToDeviceIndex].IsFat32)
                                {
                                    var dname = Devices[i.ToDeviceIndex].Fat32Drive.Name + buildDataFilePath(newFilePath, x);
                                    Directory.CreateDirectory(Path.GetDirectoryName(dname));
                                    fs = new FileStream(dname, FileMode.Create);
                                    fs.SetLength(IO.Length);
                                }
                                else
                                {
                                    fs = Devices[i.ToDeviceIndex].Handle.CreateFileStream(buildDataFilePath(newFilePath, x), IO.Length);
                                }
                                
                                int currentMax = 32 * (x + 1);
                                int bLength = buffer.Length;
                                while (currentPart < currentMax)
                                {
                                    fs.Write(buffer, 0, IO.Read(buffer, 0, bLength));
                                    updateProgressBar(ProgressBarStyle.Blocks, ++currentPart, totalParts);
                                }
                                fs.Close();
                                IO.Close();
                            }
                            numberOfDataFiles--;
                            if (i.FromDeviceIndex == -1)
                                IO = new FileStream(buildDataFilePath(pc.Package.PackageFilePath, numberOfDataFiles), FileMode.Open);
                            else if (Devices[i.FromDeviceIndex].IsFat32)
                            {
                                IO = new FileStream(Devices[i.FromDeviceIndex].Fat32Drive.Name + buildDataFilePath(fromSvodFile, numberOfDataFiles), FileMode.Open);
                            }   
                            else 
                                IO = Devices[i.FromDeviceIndex].Handle.LoadFileStream(buildDataFilePath(fromSvodFile, numberOfDataFiles));

                            if (Devices[i.ToDeviceIndex].IsFat32)
                            {
                                var dname = Devices[i.ToDeviceIndex].Fat32Drive.Name + buildDataFilePath(newFilePath, numberOfDataFiles);
                                Directory.CreateDirectory(Path.GetDirectoryName(dname));
                                fs = new FileStream(dname, FileMode.Create);
                                fs.SetLength(IO.Length);
                            }
                            else
                                fs = Devices[i.ToDeviceIndex].Handle.CreateFileStream(buildDataFilePath(newFilePath, numberOfDataFiles), IO.Length);

                            for (int x = 0; x < partsInLastFile; x++)
                            {
                                fs.Write(buffer, 0, IO.Read(buffer, 0, buffer.Length));
                                updateProgressBar(ProgressBarStyle.Blocks, ++currentPart, totalParts);
                            }
                            if (fs.Position != IO.Length)
                            {
                                buffer = new byte[IO.Length - fs.Position];
                                fs.Write(buffer, 0, IO.Read(buffer, 0, buffer.Length));
                            }
                            fs.Close();
                            IO.Close();
                        }
                        if (i.CachePartition)
                            Devices[i.FromDeviceIndex].Handle = contentPartition;
                        updateFreeSpace(i.ToDeviceIndex);
                        if (overwriteFile)
                            updateNode(i.ToDeviceIndex, pc.Package, newFilePath);
                        else
                        {
                            if (Devices[i.ToDeviceIndex].IsFat32)
                            {
                                var fileinfo = new FileInfo(Devices[i.ToDeviceIndex].Fat32Drive.Name + fatxPath + "\\" + pc.FileName);
                                if (fileinfo.Exists)
                                {
                                    insertNewNode(i.ToDeviceIndex, pc.Package,
                                            pc.ChangeId ? pc.NewProfileId : pc.Package.Header.Metadata.Creator, null, fileinfo);
                                }
                            }
                            else
                            {
                                List<FatxDirectoryEntry> newEntries = Devices[i.ToDeviceIndex].Handle.GetNestedDirectoryEntries(fatxPath);
                                if (newEntries != null)
                                {
                                    FatxDirectoryEntry newEntry = getFileEntry(pc.FileName, newEntries);
                                    if (newEntry != null)
                                    {
                                        insertNewNode(i.ToDeviceIndex, pc.Package,
                                            pc.ChangeId ? pc.NewProfileId : pc.Package.Header.Metadata.Creator, newEntry, null);
                                    }

                                }
                            }
                        }
                        if (i.Sender == null)
                            pc.Package.CloseIO(true);
                        completedTransfers += "\n" + pc.DisplayName;
                    #if !INT2
                    }
                    catch (StfsException ex)
                    {
                        TaskbarManager.setProgressState(TaskbarProgressState.Error);
                        UI.errorBox("Something is wrong with the following package:\n" + pc.DisplayName
                            + "\n\nIf you see a developer, tell him this:\n" + ex.Message);
                    }
                    catch (FatxException ex)
                    {
                        TaskbarManager.setProgressState(TaskbarProgressState.Error);
                        UI.errorBox("An error has occured while injecting the following package:\n" + pc.DisplayName
                            + "\n\nIf you see a developer, tell him this:\n" + ex.Message);
                    }
                    catch
                    {
                        TaskbarManager.setProgressState(TaskbarProgressState.Error);
                        throw;
                    }
                    #endif
                }
            #if !INT2
            }
            catch (FileNotFoundException)
            {
                TaskbarManager.setProgressState(TaskbarProgressState.Error);
                UI.errorBox("An error has occured while injecting!\n\nYour device has been removed!");
            }
            catch (Exception ex)
            {
                TaskbarManager.setProgressState(TaskbarProgressState.Error);
                UI.errorBox("An error has occured while injecting!\n\nIf you see a developer, tell him this:\n" + ex.Message);
            }
            #endif
            if (i.CachePartition)
                Devices[i.FromDeviceIndex].Handle = contentPartition;
            if (completedTransfers.Length > 0)
            {
                updateProgressBar(ProgressBarStyle.Blocks, 1, 1);
                TaskbarManager.setProgressState(TaskbarProgressState.Paused);
                Main.Invoke((MethodInvoker)delegate
                {
                    UI.messageBox(String.Format("The following packages were successfully transfered to {0}:\n{1}", Devices[i.ToDeviceIndex].Name, completedTransfers), "Transfer Complete", MessageBoxIcon.Information);
                });
            }
            if (i.FromDeviceIndex != -1)
                Devices[i.FromDeviceIndex].CopyingFile = false;
            multipleOperationsRunning = numberOfDevicesBusy() > 1;
            updateProgressBar(ProgressBarStyle.Blocks, 0, 0);
            Main.Invoke((MethodInvoker)delegate { i.ShowSender(); });
        }

        private static void deleteSvodData(int deviceIndex, string svodDataPath)
        {
            var device = Devices[deviceIndex];

            if (device.IsFat32)
            {
                Directory.Delete(device.Fat32Drive.Name + svodDataPath, true);
                return;
            }

            List<FatxDirectoryEntry> dataFiles = Devices[deviceIndex].Handle.GetNestedDirectoryEntries(svodDataPath);
            if (dataFiles != null)
                dataFiles.ForEach(dataFile => Devices[deviceIndex].Handle.DeleteFile(svodDataPath + @"\" + dataFile.Filename));
        }

        internal static void cmdFatxToolDelete_Click(object sender, EventArgs e)
        {
            FatxNode nodeTag = (FatxNode)Main.listFatx.SelectedNode.Tag;
            if (isDeviceWorkerAvailable(nodeTag.DeviceIndex))
            {
                string fatxPath = buildDirectoryPath(nodeTag);
                if (FormHandle.isFatxFileLoaded(nodeTag.DeviceIndex, fatxPath) != null)
                    UI.messageBox(nodeTag.NodeInfo.DisplayName
                        + " is currently open in an editor.\n\nYou must close the editor before deleting the file.",
                        "File in Use", MessageBoxIcon.Error, MessageBoxButtons.OK);
                else if (UI.messageBox(String.Format("Are you sure you want to delete '{0}'?",
                    nodeTag.NodeInfo.DisplayName), "Delete File?", System.Windows.Forms.MessageBoxIcon.Question,
                    System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxDefaultButton.Button3)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    FatxDevice contentPartition = nodeTag.Device.Handle;
                    if (nodeTag.NodeInfo.CachePartition)
                        nodeTag.Device.Handle = nodeTag.Device.CacheHandle;
                    if (nodeTag.Device.IsFat32)
                        File.Delete(nodeTag.Device.Fat32Drive.Name + fatxPath);
                    else
                        nodeTag.Device.Handle.DeleteFile(fatxPath);
                    if (nodeTag.NodeInfo.Svod)
                        deleteSvodData(nodeTag.DeviceIndex, fatxPath + ".data");
                    if (nodeTag.GeneralContentType == GeneralContentTypes.Gamer_Profiles
                        && nodeTag.Device.ProfileCache.ContainsKey(nodeTag.NodeInfo.ProfileID))
                        nodeTag.Device.ProfileCache.Remove(nodeTag.NodeInfo.ProfileID);
                    Node deleteNode = Main.listFatx.SelectedNode;
                    Main.listFatx.SelectedNode = Main.listFatx.SelectedNode.Parent;
                    Main.listFatx.SelectedNode.Nodes.Remove(deleteNode);
                    if (Main.listFatx.SelectedNode.Nodes.Count == 0)
                    {
                        if (nodeTag.GeneralContentType == GeneralContentTypes.Games)
                        {
                            deleteNode = Main.listFatx.SelectedNode;
                            Main.listFatx.SelectedNode = Main.listFatx.SelectedNode.Parent;
                            Main.listFatx.SelectedNode.Nodes.Remove(deleteNode);
                            if (Main.listFatx.SelectedNode.Nodes.Count == 0)
                                Main.listFatx.SelectedNode.Nodes.Add(NoContentNode);
                        }
                        else
                            Main.listFatx.SelectedNode.Nodes.Add(NoContentNode);
                    }
                    nodeTag.Device.Handle = contentPartition;
                    updateFreeSpace(nodeTag.DeviceIndex);
                }
            }
        }

        internal static Node NoContentNode
        {
            get
            {
                return new Node("No Content")
                {
                    Selectable = false,
                    ExpandVisibility = eNodeExpandVisibility.Hidden
                };
            }
        }

        internal static void cmdFatxToolRenameDevice_Click(object sender, EventArgs e)
        {
            Main.listFatx.SelectedNode.BeginEdit();
        }

        internal static void cmdFatxToolClearTitleCache_Click(object sender, EventArgs e)
        {
            if (UI.messageBox("Changes may not take effect until you restart Horizon.\n\nYour devices will take a bit longer to load the next few times while Horizon rebuilds the cache.\n\nAre you sure you want to clear your title name cache?",
                "Clear Title Cache?", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
            {
                TitleIDCache.Clear();
                populateDefaultCacheItems();
                saveTitleIdCache();
                UI.messageBox("Title cache cleared.", "Cache Cleared", MessageBoxIcon.Information);
            }
        }

        internal static void cmdFatxToolCollapseAll_Click(object sender, EventArgs e)
        {
            Main.listFatx.SelectedNode.CollapseAll();
        }

        internal static Node getPackageNodeFromFat32Filename(Node currentNode, string filename)
        {
            for (int x = 0; x < currentNode.Nodes.Count; x++)
            {
                if (currentNode.Nodes[x].Nodes.Count > 0)
                {
                    Node packageNode = getPackageNodeFromFat32Filename(currentNode.Nodes[x], filename);
                    if (packageNode != null)
                        return packageNode;
                }
                else if (currentNode.Nodes[x].Tag != null)
                {
                    FatxNode fatxNode = (FatxNode)currentNode.Nodes[x].Tag;
                    if (fatxNode.NodeType == NodeTypes.Package
                        && fatxNode.NodeInfo.Fat32FileInfo.FullName.Equals(filename, StringComparison.InvariantCultureIgnoreCase))
                        return currentNode.Nodes[x];
                }
            }
            return null;
        }

        internal static Node getPackageNodeFromStartingCluster(Node currentNode, uint startingCluster)
        {
            for (int x = 0; x < currentNode.Nodes.Count; x++)
            {
                if (currentNode.Nodes[x].Nodes.Count > 0)
                {
                    Node packageNode = getPackageNodeFromStartingCluster(currentNode.Nodes[x], startingCluster);
                    if (packageNode != null)
                        return packageNode;
                }
                else if (currentNode.Nodes[x].Tag != null)
                {
                    FatxNode fatxNode = (FatxNode)currentNode.Nodes[x].Tag;
                    if (fatxNode.NodeType == NodeTypes.Package
                        && fatxNode.NodeInfo.Entry.FirstClusterNumber == startingCluster)
                        return currentNode.Nodes[x];
                }
            }
            return null;
        }

        internal static void updateNode(int devIndex, XContentPackage package, string fatxPath)
        {
            updateFreeSpace(devIndex);

            var deviceNode = getDeviceNodeFromIndex(devIndex);
            if (deviceNode == null)
                return;

            if (Devices[devIndex].IsFat32)
            {
                Node packageNode = getPackageNodeFromFat32Filename(deviceNode, Devices[devIndex].Fat32Drive.Name + fatxPath);
                if (packageNode == null)
                    return;

                FatxNode nodeTag = (FatxNode)packageNode.Tag;
                nodeTag.NodeInfo.Fat32FileInfo = new FileInfo(Devices[devIndex].Fat32Drive.Name + fatxPath);
                nodeTag.NodeInfo.ProfileID = package.Header.Metadata.Creator;
                if (package.Header.Metadata.ContentType == ContentTypes.Profile)
                {
                    addToProfileCache(package, nodeTag.NodeInfo.Fat32FileInfo, devIndex);
                    nodeTag.NodeInfo.DisplayName =
                        Devices[devIndex].ProfileCache[nodeTag.NodeInfo.ProfileID].Gamertag;
                }
                else
                {
                    if (Main.InvokeRequired)
                        Main.Invoke((MethodInvoker)delegate { fillPackageNode(nodeTag, package); });
                    else
                        fillPackageNode(nodeTag, package);
                }
                if (Main.InvokeRequired)
                    Main.Invoke(
                        (MethodInvoker)
                            delegate { fillPackageNode(packageNode, nodeTag, imageFromPackage(package)); });
                else
                    fillPackageNode(packageNode, nodeTag, imageFromPackage(package));
                if (packageNode.IsSelected && nodeTag.GeneralContentType != GeneralContentTypes.Gamer_Profiles)
                    listFatx_AfterNodeSelect(Main.listFatx, new AdvTreeNodeEventArgs(eTreeAction.Code, packageNode));
                Main.Invoke((MethodInvoker)delegate { packageNode.Parent.Nodes.Sort(); });
                return;
            }

            int lastIndex = fatxPath.LastIndexOf('\\');
            if (lastIndex != -1)
            {
                List<FatxDirectoryEntry> fatxEntries = Devices[devIndex].Handle.GetNestedDirectoryEntries(fatxPath.Substring(0, lastIndex));
                if (fatxEntries != null)
                {
                    FatxDirectoryEntry fatxEntry = getFileEntry(fatxPath.Substring(lastIndex + 1), fatxEntries);
                    if (fatxEntry != null && !fatxEntry.IsDirectory)
                    {
                        if (deviceNode != null)
                        {
                            Node packageNode = getPackageNodeFromStartingCluster(deviceNode, fatxEntry.FirstClusterNumber);
                            if (packageNode != null)
                            {
                                FatxNode nodeTag = (FatxNode)packageNode.Tag;
                                nodeTag.NodeInfo.Entry = fatxEntry;
                                nodeTag.NodeInfo.ProfileID = package.Header.Metadata.Creator;
                                if (package.Header.Metadata.ContentType == ContentTypes.Profile)
                                {
                                    addToProfileCache(package, fatxEntry, devIndex);
                                    nodeTag.NodeInfo.DisplayName = Devices[devIndex].ProfileCache[nodeTag.NodeInfo.ProfileID].Gamertag;
                                }
                                else
                                {
                                    if (Main.InvokeRequired)
                                        Main.Invoke((MethodInvoker)delegate { fillPackageNode(nodeTag, package); });
                                    else
                                        fillPackageNode(nodeTag, package);
                                }
                                if (Main.InvokeRequired)
                                    Main.Invoke((MethodInvoker)delegate { fillPackageNode(packageNode, nodeTag, imageFromPackage(package)); });
                                else
                                    fillPackageNode(packageNode, nodeTag, imageFromPackage(package));
                                if (packageNode.IsSelected && nodeTag.GeneralContentType != GeneralContentTypes.Gamer_Profiles)
                                    listFatx_AfterNodeSelect(Main.listFatx, new AdvTreeNodeEventArgs(eTreeAction.Code, packageNode));
                                Main.Invoke((MethodInvoker)delegate { packageNode.Parent.Nodes.Sort(); });
                            }
                        }
                    }
                }
            }
        }

        internal static void RenameSavedGame(int devIndex, XContentPackage package, string oldPath, string newName)
        {
            if (package.Header.Metadata.ContentType != ContentTypes.SavedGame)
                return;

            package.Flush();
            package.Save(true);
            package.CloseIO(true);

            var device = Devices[devIndex];

            if (device.IsFat32)
            {
                var deviceNod = getDeviceNodeFromIndex(devIndex);

                if (deviceNod == null)
                    return;

                File.Move(device.Fat32Drive.Name + oldPath, device.Fat32Drive.Name + newName);
                var packageNod = getPackageNodeFromFat32Filename(deviceNod, device.Fat32Drive.Name + oldPath);
                ((FatxNode)packageNod.Tag).NodeInfo.Fat32FileInfo = new FileInfo(device.Fat32Drive.Name + newName);
                return;
            }

            var lastIndex = oldPath.LastIndexOf('\\');

            if (lastIndex == -1)
                return;

            var directory = oldPath.Substring(0, lastIndex);
            var fatxEntries = Devices[devIndex].Handle.GetNestedDirectoryEntries(directory);

            if (fatxEntries == null)
                return;

            var fatxEntry = getFileEntry(oldPath.Substring(lastIndex + 1), fatxEntries);

            if (fatxEntry == null || fatxEntry.IsDirectory)
                return;

            var deviceNode = getDeviceNodeFromIndex(devIndex);

            if (deviceNode == null)
                return;

            var packageNode = getPackageNodeFromStartingCluster(deviceNode, fatxEntry.FirstClusterNumber);

            if (packageNode == null)
                return;

            var packageStr = Devices[devIndex].Handle.LoadFileStream(oldPath);
            var packageArr = packageStr.ToArray();
            packageStr.Close();

            Devices[devIndex].Handle.DeleteFile(oldPath);

            var newPath = Path.GetDirectoryName(oldPath) + "\\" + newName;

            Devices[devIndex].Handle.CreateFileFromArray(newPath, packageArr);

            fatxEntries = Devices[devIndex].Handle.GetNestedDirectoryEntries(directory);

            ((FatxNode)packageNode.Tag).NodeInfo.Entry = fatxEntries.Find(e => e.Filename == newName);
        }

        internal static Node getDeviceNodeFromIndex(int devIndex)
        {
            for (int x = 0; x < Main.listFatx.Nodes.Count; x++)
                if (devIndex == ((FatxNode)Main.listFatx.Nodes[x].Tag).DeviceIndex)
                    return Main.listFatx.Nodes[x];
            return null;
        }

        private static void addToProfileCache(XContentPackage package, FatxDirectoryEntry fatxEntry, int devIndex)
        {
            ProfileInfo pI = new ProfileInfo();
            buildProfileInfo(package, ref pI);
            pI.Entry = fatxEntry;
            Devices[devIndex].addProfileToCache(package.Header.Metadata.Creator, pI);
        }

        private static void addToProfileCache(XContentPackage package, FileInfo fat32FileInfo, int devIndex)
        {
            ProfileInfo pI = new ProfileInfo();
            buildProfileInfo(package, ref pI);
            pI.Fat32FileInfo = fat32FileInfo;
            Devices[devIndex].addProfileToCache(package.Header.Metadata.Creator, pI);
        }

        internal static void insertNewNode(int devIndex, XContentPackage package, ulong profileId, FatxDirectoryEntry fatxEntry, FileInfo fat32Info)
        {
            Node deviceNode = getDeviceNodeFromIndex(devIndex);
            if (deviceNode != null)
            {
                GeneralContentTypes generalType = getGeneralContentType(package.Header.Metadata.ContentType);
                for (int x = 0; x < deviceNode.Nodes.Count; x++)
                    if (generalType == ((FatxNode)deviceNode.Nodes[x].Tag).GeneralContentType)
                    {
                        Node generalNode = deviceNode.Nodes[x];
                        if (generalType == GeneralContentTypes.Games)
                        {
                            bool buildGameNode = true;
                            uint titleId = TitleControl.GetProperTitleID(package);
                            for (int i = 0; i < generalNode.Nodes.Count; i++)
                                if (generalNode.Nodes[i].Tag != null && ((FatxNode)generalNode.Nodes[i].Tag).NodeInfo.TitleID
                                    == titleId)
                                {
                                    buildGameNode = false;
                                    generalNode = generalNode.Nodes[i];
                                    break;
                                }
                            if (buildGameNode)
                            {
                                FatxNode nodeTag = new FatxNode(devIndex, NodeTypes.Game);
                                nodeTag.NodeInfo.TitleID = titleId;
                                addTitleIDToCache(titleId, package.Header.Metadata.TitleName);
                                if (generalNode.Nodes.Count > 0)
                                    Main.Invoke((MethodInvoker)delegate
                                    {
                                        if (!generalNode.Nodes[0].Selectable)
                                            generalNode.Nodes[0].Remove();
                                        generalNode.Nodes.Add(buildFatxNode(nodeTag, getGameNodeImage(nodeTag.NodeInfo.TitleID)));
                                        generalNode.Nodes.Sort();
                                    });
                                saveTitleIdCache();
                                break;
                            }
                        }
                        if (package.Header.Metadata.ContentType == ContentTypes.Profile)
                        {
                            if (fatxEntry != null)
                            {
                                addToProfileCache(package, fatxEntry, devIndex);
                            }
                            else
                            {
                                addToProfileCache(package, fat32Info, devIndex);
                            }
                        }
                            
                        if (generalNode.Nodes.Count > 0)
                        {
                            Main.Invoke((MethodInvoker)delegate
                            {
                                if (!generalNode.Nodes[0].Selectable)
                                    generalNode.Nodes[0].Remove();
                                if (package.Header.Metadata.ContentType == ContentTypes.Profile)
                                    generalNode.Nodes.Add(createProfileNode(devIndex, package.Header.Metadata.Creator,
                                        Devices[devIndex].ProfileCache[package.Header.Metadata.Creator]));
                                else
                                {
                                    FatxNode newNode = new FatxNode(devIndex, NodeTypes.Package);
                                    newNode.NodeInfo.ContentType = package.Header.Metadata.ContentType;
                                    fillPackageNode(newNode, package);
                                    newNode.NodeInfo.Entry = fatxEntry;
                                    newNode.NodeInfo.Fat32FileInfo = fat32Info;
                                    newNode.NodeInfo.ProfileID = profileId;
                                    newNode.NodeInfo.TitleID = TitleControl.GetProperTitleID(package);
                                    generalNode.Nodes.Add(buildFatxNode(newNode, imageFromPackage(package)));
                                }
                                generalNode.Nodes.Sort();
                            });
                        }
                        break;
                    }
            }
        }

        private static Image imageFromPackage(XContentPackage package)
        {
            Image nodeImage;
            if (package.Header.Metadata.ThumbnailSize == 0)
                nodeImage = Resources.Console;
            else
                try
                {
                    nodeImage = Image.FromStream(new MemoryStream(package.Header.Metadata.Thumbnail));
                }
                catch { nodeImage = Resources.Console; }
            return nodeImage;
        }

        private static string buildDirectoryPath(FatxNode nodeTag)
        {
            if (nodeTag.NodeInfo.Cache)
                return String.Format(@"{0}\{1}", cacheFolder, nodeTag.NodeInfo.Entry.Filename);
            return buildDirectoryPath(nodeTag.NodeInfo.ProfileID, nodeTag.NodeInfo.TitleID, nodeTag.NodeInfo.ContentType, 
                nodeTag.NodeInfo.Fat32FileInfo != null ? nodeTag.NodeInfo.Fat32FileInfo.Name : nodeTag.NodeInfo.Entry.Filename);
        }

        internal static string buildDirectoryPath(ulong profileId, uint titleId, ContentTypes contentType, string fileName)
        {
            return String.Format(@"{0}\{1}", buildDirectoryPath(profileId, titleId, contentType), fileName);
        }

        private static string buildDirectoryPath() { return "Content"; }
        private static string buildDirectoryPath(ulong profileId)
        {
            return String.Format(@"{0}\{1:X16}", buildDirectoryPath(), profileId);
        }

        private static string buildDirectoryPath(ulong profileId, uint titleId)
        {
            return String.Format(@"{0}\{1:X8}", buildDirectoryPath(profileId), titleId);
        }

        internal static string buildDirectoryPath(ulong profileId, uint titleId, ContentTypes contentType)
        {
            return String.Format(@"{0}\{1:X8}", buildDirectoryPath(profileId, titleId), (int)contentType);
        }

        internal enum NodeTypes
        {
            Device,
            ContentFolder,
            Game,
            Package
        }

        internal enum GeneralContentTypes
        {
            Games,
            Gamer_Profiles,
            Demos,
            Videos,
            Themes,
            Gamer_Pictures,
            Avatar_Items,
            Title_Updates,
            System_Items
        }

        internal class FatxNode
        {
            internal FatxNode(int deviceIndex, NodeTypes nodeType)
            {
                DeviceIndex = deviceIndex;
                NodeType = nodeType;
                if (NodeType == NodeTypes.Game)
                    GeneralContentType = GeneralContentTypes.Games;
            }

            internal FatxNode(int deviceIndex, GeneralContentTypes generalContentType)
            {
                DeviceIndex = deviceIndex;
                NodeType = NodeTypes.ContentFolder;
                GeneralContentType = generalContentType;
            }

            internal int DeviceIndex;
            internal DeviceInfo Device
            {
                get { return Devices[DeviceIndex]; }
            }
            internal NodeTypes NodeType;
            internal GeneralContentTypes GeneralContentType;
            internal ExtendedInfo NodeInfo = new ExtendedInfo();
        }

        internal static string makeGrayText(string text)
        {
            return String.Format("<font color=\"#7D7974\">{0}</font>", text);
        }

        private static long getFreeSpace(int devIndex)
        {
            var device = Devices[devIndex];

            if (device.IsFat32)
            {
                device.Fat32Drive = new DriveInfo(device.Fat32Drive.Name);
                return device.Fat32Drive.TotalFreeSpace;
            }

            return (long)Devices[devIndex].Handle.VolumeExtension.FreedClusterCount
                * Devices[devIndex].Handle.VolumeExtension.ClusterSize;
        }

        private static void updateFreeSpace(int devIndex)
        {
            Node deviceNode = getDeviceNodeFromIndex(devIndex);
            if (deviceNode != null)
            {
                if (Main.InvokeRequired)
                    Main.Invoke((MethodInvoker)delegate
                    {
                        deviceNode.Cells[1].Text = createFreeSpaceString(devIndex);
                    });
                else
                    deviceNode.Cells[1].Text = createFreeSpaceString(devIndex);
            }
        }

        private static string createFreeSpaceString(int devIndex)
        {
            return makeGrayText(Global.getFormatFromBytes(getFreeSpace(devIndex)) + " Free");
        }

        private static void setDeviceCellOneText(Cell cellOne, FatxNode nodeTag)
        {
            cellOne.Text = nodeTag.Device.Name + lineBreak;

            if (nodeTag.Device.IsFat32)
            {
                cellOne.Text += makeGrayText("USB Flash Drive");
                return;
            }

            switch (nodeTag.Device.Drive.DeviceType)
            {
                case FatxDeviceControl.FatxDeviceType.HDD:
                    cellOne.Text += makeGrayText("Hard Drive");
                    break;
                case FatxDeviceControl.FatxDeviceType.MU:
                    cellOne.Text += makeGrayText("Memory Unit");
                    break;
                case FatxDeviceControl.FatxDeviceType.USB:
                    cellOne.Text += makeGrayText("USB Flash Drive");
                    break;
            }
        }

        internal static string lineBreak = "<br></br>";
        private static Node buildFatxNode(FatxNode nodeTag, Image nodeImage)
        {
            Node node = new Node();
            node.Cells.Add(new Cell());
            node.Image = nodeImage;
            switch (nodeTag.NodeType)
            {
                case NodeTypes.ContentFolder:
                    node.ImageExpanded = Resources.FolderOpen;
                    node.ExpandVisibility = eNodeExpandVisibility.Visible;
                    node.Cells[0].Text = nodeTag.GeneralContentType.ToString().Replace("_", " ");
                    break;
                case NodeTypes.Device:
                    setDeviceCellOneText(node.Cells[0], nodeTag);
                    node.Cells[1].Text = createFreeSpaceString(nodeTag.DeviceIndex);
                    foreach (GeneralContentTypes type in Enum.GetValues(typeof(GeneralContentTypes)))
                        node.Nodes.Add(buildFatxNode(new FatxNode(nodeTag.DeviceIndex, type), Resources.FolderClosed));
                    break;
                case NodeTypes.Game:
                    node.ExpandVisibility = eNodeExpandVisibility.Visible;
                    node.Cells[0].Text = TitleIDCache[nodeTag.NodeInfo.TitleID];
                    node.Cells[1].Text = String.Format("{0} {1:X8}", makeGrayText("Title ID:"), nodeTag.NodeInfo.TitleID);
                    break;
                case NodeTypes.Package:
                    fillPackageNode(node, nodeTag, nodeImage);
                    break;
            }
            node.Tag = nodeTag;
            return node;
        }

        private static void fillPackageNode(Node fatxNode, FatxNode nodeTag, Image nodeImage)
        {
            Bitmap b = new Bitmap(32, 41);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(nodeImage, 0, 5, 32, 32);
            g.Dispose();
            fatxNode.Image = (Image)b;
            fatxNode.ExpandVisibility = eNodeExpandVisibility.Hidden;
            fatxNode.Cells[0].Text = nodeTag.NodeInfo.DisplayName + lineBreak;
            nodeTag.GeneralContentType = getGeneralContentType(nodeTag.NodeInfo.ContentType);
            if (nodeTag.NodeInfo.ContentType == ContentTypes.Profile)
                fatxNode.Cells[0].Text += makeGrayText(nodeTag.NodeInfo.ProfileID.ToString("X16"));
            else if (nodeTag.NodeInfo.ContentType == ContentTypes.Installer)
                fatxNode.Cells[0].Text += makeGrayText(String.Format("Version {0}.0", nodeTag.NodeInfo.Version));
            else
            {
                fatxNode.Cells[0].Text += makeGrayText(getContentTypeString(nodeTag.NodeInfo.ContentType));
                if (nodeTag.NodeInfo.ContentType == ContentTypes.SavedGame)
                {
                    fatxNode.Cells[0].Text += lineBreak + makeGrayText("Profile: ");
                    ProfileData pData = ProfileManager.fetchProfile(nodeTag.NodeInfo.ProfileID);
                    if (pData == null)
                        fatxNode.Cells[0].Text += "Unknown";
                    else
                        fatxNode.Cells[0].Text += pData.Gamertag;
                }
            }

            string timeFormat = "M/d/yyyy";

            if (nodeTag.NodeInfo.Fat32FileInfo != null)
            {
                var fileinfo = nodeTag.NodeInfo.Fat32FileInfo;
                fileinfo.Refresh();
                fatxNode.Cells[1].Text = makeGrayText("Size: ") +
                                         Global.getFormatFromBytes(nodeTag.NodeInfo.Svod
                                             ? (long)nodeTag.NodeInfo.DataFilesSize
                                             : fileinfo.Length);
                fatxNode.Cells[1].Text += lineBreak + makeGrayText("Created: ") +
                                          fileinfo.CreationTime.ToString(timeFormat);
                fatxNode.Cells[1].Text += lineBreak + makeGrayText("Modified: ") +
                                          fileinfo.LastWriteTime.ToString(timeFormat);
            }
            else
            {
                fatxNode.Cells[1].Text = makeGrayText("Size: ") +
                                         Global.getFormatFromBytes(nodeTag.NodeInfo.Svod
                                             ? (long)nodeTag.NodeInfo.DataFilesSize
                                             : nodeTag.NodeInfo.Entry.FileSize);
                fatxNode.Cells[1].Text += lineBreak + makeGrayText("Created: ") +
                                          intToDateTime(nodeTag.NodeInfo.Entry.CreationTimeStamp).ToString(timeFormat);
                fatxNode.Cells[1].Text += lineBreak + makeGrayText("Modified: ") +
                                          intToDateTime(nodeTag.NodeInfo.Entry.LastWriteTimeStamp).ToString(timeFormat);
            }
        }

        private static DateTime intToDateTime(int dateTime)
        {
            DateTime newTime;
            try
            {
                newTime = FatxDevice.FatxFatTimestampToTime(dateTime);
            }
            catch
            {
                newTime = DateTime.Now;
            }
            return newTime;
        }

        private static string timeStampToDate(int timeStamp)
        {
            StfsTimeStamp time = new StfsTimeStamp() { AsINT = timeStamp };
            return String.Format("{0}/{1}/{2}", time.Month, time.Day, time.Year);
        }

        internal struct ExtendedInfo
        {
            internal FatxDirectoryEntry Entry;
            internal FileInfo Fat32FileInfo;
            internal ContentTypes ContentType;
            internal string DisplayName;
            internal ulong ProfileID;
            internal bool ReadOnly;
            internal bool Svod;
            internal bool Cache;
            internal bool CachePartition;
            internal uint Version;
            internal uint NumberOfDataFiles;
            internal ulong DataFilesSize;
            internal uint TitleID;
        }

        private static GeneralContentTypes getGeneralContentType(ContentTypes contentType)
        {
            switch (contentType)
            {
                case ContentTypes.SavedGame:
                case ContentTypes.Marketplace:
                case ContentTypes.Publisher:
                case ContentTypes.XNA:
                case ContentTypes.XNACommunity:
                case ContentTypes.InstalledXbox360Title:
                case ContentTypes.XboxTitle:
                case ContentTypes.SocialTitle:
                case ContentTypes.Xbox360Title:
                case ContentTypes.XboxSavedGame:
                case ContentTypes.XboxDownload:
                case ContentTypes.GameTitle:
                case ContentTypes.Arcade:
                    return GeneralContentTypes.Games;
                case ContentTypes.Profile:
                    return GeneralContentTypes.Gamer_Profiles;
                case ContentTypes.GameDemo:
                    return GeneralContentTypes.Demos;
                case ContentTypes.ViralVideo:
                case ContentTypes.Video:
                case ContentTypes.GameTrailer:
                case ContentTypes.Movie:
                case ContentTypes.TV:
                case ContentTypes.MusicVideo:
                case ContentTypes.GameVideo:
                case ContentTypes.PodcastVideo:
                    return GeneralContentTypes.Videos;
                case ContentTypes.ThematicSkin:
                    return GeneralContentTypes.Themes;
                case ContentTypes.GamerPicture:
                    return GeneralContentTypes.Gamer_Pictures;
                case ContentTypes.AvatarAsset:
                    return GeneralContentTypes.Avatar_Items;
                case ContentTypes.Installer:
                    return GeneralContentTypes.Title_Updates;
                default:
                    return GeneralContentTypes.System_Items;
            }
        }

        private static string getContentTypeString(ContentTypes contentType)
        {
            switch (contentType)
            {
                case ContentTypes.SavedGame:
                    return "Saved Game";
                case ContentTypes.Marketplace:
                    return "Marketplace Content";
                case ContentTypes.Publisher:
                    return "Publisher Data";
                case ContentTypes.IPTVDVR:
                    return "IPTV DVR Data";
                case ContentTypes.IPTVPauseBuffer:
                    return "IPTV Pause Buffer";
                case ContentTypes.XNA:
                case ContentTypes.XNACommunity:
                    return "Indie Game";
                case ContentTypes.InstalledXbox360Title:
                    return "Installed Game";
                case ContentTypes.XboxTitle:
                    return "Original Xbox Game";
                case ContentTypes.SocialTitle:
                    return "Social Game";
                case ContentTypes.Xbox360Title:
                    return "Games on Demand";
                case ContentTypes.SystemUpdateStoragePack:
                    return "System Update Storage";
                case ContentTypes.AvatarAsset:
                    return "Avatar Item";
                case ContentTypes.Profile:
                    return "Gamer Profile";
                case ContentTypes.ThematicSkin:
                    return "Theme";
                case ContentTypes.GamerPicture:
                    return "Gamer Picture Pack";
                case ContentTypes.Cache:
                    return "Cached Data";
                case ContentTypes.StorageDownload:
                    return "Storage Download";
                case ContentTypes.XboxSavedGame:
                    return "Xbox Saved Game";
                case ContentTypes.XboxDownload:
                    return "Xbox Download";
                case ContentTypes.GameDemo:
                    return "Game Demo";
                case ContentTypes.ViralVideo:
                case ContentTypes.Video:
                    return "Video";
                case ContentTypes.GameTitle:
                    return "Game";
                case ContentTypes.Installer:
                    return "Title Update";
                case ContentTypes.GameTrailer:
                    return "Game Trailer";
                case ContentTypes.Arcade:
                    return "Xbox LIVE<br></br>Arcade Game";
                case ContentTypes.LicenseStore:
                    return "License Storage";
                case ContentTypes.Movie:
                    return "Movie";
                case ContentTypes.TV:
                    return "TV Content";
                case ContentTypes.MusicVideo:
                    return "Music Video";
                case ContentTypes.GameVideo:
                    return "Promotional Video";
                case ContentTypes.PodcastVideo:
                    return "Podcast Video";
                default:
                    return "Content";
            }
        }

        internal struct ProfileInfo
        {
            internal bool UnknownOrCorrupted;
            internal string Gamertag;
            internal Image Gamerpic;
            internal ulong XUID;
            internal FatxDirectoryEntry Entry;
            internal FileInfo Fat32FileInfo;
        }

        internal static FatxDirectoryEntry getFileEntry(string entryName, List<FatxDirectoryEntry> entries)
        {
            int x = entries.FindIndex(entry => entry.Filename == entryName);
            if (x == -1)
                return null;
            return entries[x];
        }

        private static FatxDirectoryEntry doesEntryExist(string entryName, List<FatxDirectoryEntry> entries, bool dir)
        {
            FatxDirectoryEntry entry = getFileEntry(entryName, entries);
            if (entry == null || entry.IsDirectory != dir)
                return null;
            return entry;
        }

        private static void buildProfileInfo(XContentPackage package, ref ProfileInfo pI)
        {
            try
            {
                pI.Gamerpic = imageFromPackage(package);
                XProfile.XProfileAccount acc = new XProfile.XProfileAccount(package.StfsContentPackage.ExtractFileToArray("Account"));
                pI.Gamertag = filterGamertag(acc.Info.GamerTag);
                pI.XUID = acc.Info.XuidOnline;
                if (pI.Gamertag == null)
                    throw new Exception();
                pI.UnknownOrCorrupted = false;
            }
            catch
            {
                pI.UnknownOrCorrupted = true;
                pI.Gamertag = "Corrupted Profile";
                pI.Gamerpic = Resources.QuestionMark;
            }
        }

        internal static string filterGamertag(string gamertag)
        {
            try
            {
                byte temp;
                for (int x = 0; x < gamertag.Length; x++)
                {
                    if (gamertag[x] == '^')
                    {
                        gamertag = gamertag.Remove(x, x < gamertag.Length - 1
                            && byte.TryParse(gamertag[x + 1].ToString(), out temp) ? 2 : 1);
                        x--;
                    }
                    else if (gamertag[x] < ' ' || gamertag[x] > '~'
                        || gamertag[x] == '>' || gamertag[x] == '<'
                        || gamertag[x] == '/')
                        gamertag = gamertag.Remove(x--, 1);
                }
                return gamertag;
            }
            catch
            {
                return null;
            }
        }

        private static readonly string cacheFolder = "Cache";
        private static readonly uint dashTitleId = 0xFFFE07D1;
        internal static volatile List<DeviceInfo> Devices = new List<DeviceInfo>();
        internal class DeviceInfo
        {
            internal bool IsFat32 = false;
            internal DriveInfo Fat32Drive;

            internal DeviceInfo(FatxDeviceControl.DriveBase DriveBase)
            {
                Drive = DriveBase;

                if (DriveBase.IsFat32)
                {
                    try
                    {
                        IsFat32 = true;
                        Fat32Drive = new DriveInfo(DriveBase.Name);

                        var nameFilePath = Fat32Drive.Name + nameFile;

                        if (File.Exists(nameFilePath))
                        {
                            var nameArray = File.ReadAllBytes(nameFilePath);
                            if (nameArray[0] == 0xfe && nameArray[1] == 0xff)
                                Name = System.Text.Encoding.BigEndianUnicode.GetString(nameArray);
                        }

                        Name = Name ?? EmptyDeviceName;

                        var contentFolder = new DirectoryInfo(Fat32Drive.Name + "Content");

                        if (!contentFolder.Exists)
                            return;

                        foreach (var profileFolder in contentFolder.GetDirectories().Where(d => d.Name.Length == 16))
                        {
                            ulong profileId;
                            if (
                                !ulong.TryParse(profileFolder.Name, NumberStyles.HexNumber, CultureInfo.InvariantCulture,
                                    out profileId) || profileId == 0)
                            {
                                continue;
                            }

                            var profileTypeFolder = buildDirectoryPath(profileId, dashTitleId, ContentTypes.Profile);

                            if (!Directory.Exists(Fat32Drive.Name + profileTypeFolder))
                            {
                                continue;
                            }

                            ProfileInfo pI = new ProfileInfo();
                            var profileInfo =
                                new FileInfo(Fat32Drive.Name + profileTypeFolder + "\\" + profileFolder.Name);

                            if (!profileInfo.Exists)
                            {
                                pI.Gamertag = "Unknown Profile";
                                pI.Gamerpic = Resources.QuestionMark;
                                pI.UnknownOrCorrupted = true;
                            }
                            else
                                try
                                {
                                    using (var fs = profileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                    {
                                        var package = new XContentPackage();
                                        var io = new EndianIO(fs, EndianType.BigEndian);
                                        package.LoadPackage(io, false);
                                        buildProfileInfo(package, ref pI);
                                        io.Close();
                                    }
                                    
                                }
                                catch
                                {
                                }
                            pI.Fat32FileInfo = profileInfo;
                            addProfileToCache(profileId, pI);
                        }
                    }
                    catch {}

                    return;
                }

                try
                {
                    switch (Drive.DeviceType)
                    {
                        case FatxDeviceControl.FatxDeviceType.MU:
                            Handle = new FatxDevice(Drive.IO, FATXPartitionType.NonGrowable, (long)FatxDeviceControl.MU.Storage, Drive.Length - (long)FatxDeviceControl.MU.Storage);
                            break;
                        case FatxDeviceControl.FatxDeviceType.HDD:
                            Handle = new FatxDevice(Drive.IO, FATXPartitionType.NonGrowable, (long)FatxDeviceControl.HDD.Storage, Drive.Length - (long)FatxDeviceControl.HDD.Storage);
                            break;
                        default:
                            Handle = new FatxDevice(Drive.IO, FATXPartitionType.Growable, (long)FatxDeviceControl.USB.Storage, Drive.Length - (long)FatxDeviceControl.USB.Storage);
                            break;
                    }
                    Handle.FatxMountVolume();
                    List<FatxDirectoryEntry> entries = Handle.GetRootDirectoryEntries();
                    if (doesEntryExist(nameFile, entries, false) != null)
                    {
                        byte[] nameArray = Handle.ExtractFileToArray(nameFile);
                        if (nameArray[0] == 0xfe && nameArray[1] == 0xff)
                            Name = System.Text.Encoding.BigEndianUnicode.GetString(nameArray);
                    }
                    Name = Name ?? EmptyDeviceName;
                    /*if (doesEntryExist(cacheFolder, entries, true) != null)
                        try
                        {
                            List<FatxDirectoryEntry> cacheFiles = Handle.GetNestedDirectoryEntries(cacheFolder);
                            int x = cacheFiles.FindIndex(cacheEntry => cacheEntry.FileSize % 0x34 == 0
                                && cacheEntry.Filename.Substring(0, 3) == "TN_");
                            if (x != -1)
                            {
                                EndianIO titleCache = Handle.LoadFileToIO(String.Format(@"{0}\{1}", cacheFolder, cacheFiles[x].Filename));
                                while (titleCache.Position != titleCache.Length)
                                {
                                    long nextPosition = titleCache.Position + 0x34;
                                    uint titleID = titleCache.In.ReadUInt32();
                                    addTitleIDToCache(titleID, titleCache.In.ReadUnicodeNullTermString());
                                    titleCache.Position = nextPosition;
                                }
                                titleCache.Close();
                            }
                        }
                        catch { }*/ // Title ID cache removed. More efficient without it.
                    if (doesEntryExist(buildDirectoryPath(), entries, true) != null)
                    {
                        entries = Handle.GetNestedDirectoryEntries(buildDirectoryPath());
                        if (entries != null)
                        {
                            entries = entries.FindAll(
                                entry => entry.IsDirectory && entry.Filename.Length == 16);
                            foreach (FatxDirectoryEntry entry in entries)
                            {
                                ulong profileId;
                                if (ulong.TryParse(entry.Filename, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out profileId) && profileId != 0)
                                {
                                    List<FatxDirectoryEntry> profileDirectory = Handle.GetNestedDirectoryEntries(buildDirectoryPath(profileId, dashTitleId, ContentTypes.Profile));
                                    if (profileDirectory != null)
                                    {
                                        ProfileInfo pI = new ProfileInfo();
                                        FatxDirectoryEntry profileEntry = doesEntryExist(entry.Filename, profileDirectory, false);
                                        if (profileEntry == null)
                                        {
                                            pI.Gamertag = "Unknown Profile";
                                            pI.Gamerpic = Resources.QuestionMark;
                                            pI.UnknownOrCorrupted = true;
                                        }
                                        else
                                            try
                                            {
                                                XContentPackage package = new XContentPackage();
                                                package.LoadPackage(Handle.LoadFileToIO(buildDirectoryPath(profileId, dashTitleId, ContentTypes.Profile, entry.Filename)), false);
                                                buildProfileInfo(package, ref pI);
                                                package.CloseIO(true);
                                            }
                                            catch { }
                                        pI.Entry = profileEntry;
                                        addProfileToCache(profileId, pI);
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
            }

            private string EmptyDeviceName
            {
                get
                {
                    if (IsFat32)
                        return "Flash Drive";

                    switch (Drive.DeviceType)
                    {
                        case FatxDeviceControl.FatxDeviceType.HDD:
                            return "Hard Disk Drive";
                        case FatxDeviceControl.FatxDeviceType.MU:
                            return "Memory Card";
                        default:
                            return "Flash Drive";
                    }
                }
            }

            internal void MountCachePartition()
            {
                if (IsFat32)
                    return;

                switch (Drive.DeviceType)
                {
                    case FatxDeviceControl.FatxDeviceType.USB:
                        Drive.IO.Position = (long)FatxDeviceControl.USB.StorageSystem;
                        if (Drive.IO.In.ReadUInt32() == 0x58544146)
                        {
                            CacheHandle = new FatxDevice(Drive.IO, FATXPartitionType.NonGrowable, (long)FatxDeviceControl.USB.StorageSystem, 0x4000000);
                            CacheHandle.FatxMountVolume();
                        }
                        break;
                }
            }

            internal FatxDeviceControl.DriveBase Drive;
            internal FatxDevice Handle, CacheHandle;
            internal Thread Worker;
            internal bool WorkerBusy
            {
                get
                {
                    return Worker != null && Worker.IsAlive;
                }
            }
            internal bool CopyingFile = false;
            internal string Name;
            private static readonly string nameFile = "name.txt";

            internal void SetName(string deviceName)
            {
                if (deviceName != Name)
                {
                    bool newFile = IsFat32 ? File.Exists(Fat32Drive.Name + nameFile) : (doesEntryExist(nameFile, Handle.GetRootDirectoryEntries(), false) == null);
                    if (deviceName.Length != 0)
                    {
                        byte[] head = new byte[] { 0xfe, 0xff };
                        byte[] buffer = System.Text.Encoding.BigEndianUnicode.GetBytes(deviceName);
                        Stream nameStream = IsFat32 ? File.Open(Fat32Drive.Name + nameFile, FileMode.Create, FileAccess.Write) : (newFile ? Handle.CreateFileStream(nameFile, head.Length + buffer.Length)
                            : Handle.OverwriteFileFromStream(nameFile, head.Length + buffer.Length));
                        nameStream.Write(new byte[] { 0xfe, 0xff }, 0, 2);
                        nameStream.Write(buffer, 0, buffer.Length);
                        nameStream.Close();

                        if (IsFat32)
                        {
                            var fn = Fat32Drive.Name + nameFile;
                            File.SetAttributes(fn, File.GetAttributes(fn) | FileAttributes.Hidden);
                        }
                    }
                    else if (!newFile)
                    {
                        if (IsFat32)
                            File.Delete(Fat32Drive.Name + nameFile);
                        else
                            Handle.DeleteFile(nameFile);
                    }
                }
                Name = deviceName.Length == 0 ? EmptyDeviceName : deviceName;
            }

            internal Image DeviceImage
            {
                get
                {
                    if (IsFat32)
                        return Resources.FatxUSB_24;
                    switch (Drive.DeviceType)
                    {
                        case FatxDeviceControl.FatxDeviceType.HDD:
                            return Resources.FatxHDD_24;
                        case FatxDeviceControl.FatxDeviceType.MU:
                            return Resources.FatxMU_24;
                        default:
                            return Resources.FatxUSB_24;
                    }
                }
            }

            internal Dictionary<ulong, ProfileInfo> ProfileCache = new Dictionary<ulong, ProfileInfo>();
            internal void addProfileToCache(ulong profileId, ProfileInfo profileInfo)
            {
                if (ProfileCache.ContainsKey(profileId))
                    ProfileCache.Remove(profileId);
                ProfileCache.Add(profileId, profileInfo);
                if (!profileInfo.UnknownOrCorrupted)
                    ProfileManager.addProfileToCache(profileId, profileInfo.Gamertag, profileInfo.XUID,  profileInfo.Gamerpic.ToByteArray(), true);
            }
        }
    }
}
