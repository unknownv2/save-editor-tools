using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Horizon.PackageEditors;
using Horizon.Forms;
using Horizon.Library.Systems.FATX;
using XContent;
using Horizon.PackageEditors.Package_Manager;

namespace Horizon
{
    internal static class FormHandle
    {
        internal static void unloadFatxForms(int x)
        {
            List<int> removeKeys = new List<int>();
            foreach (KeyValuePair<int, FormConfig> fc in Forms)
                if (fc.Value.DeviceIndex == x)
                    removeKeys.Add(fc.Key);
            for (int i = 0; i < removeKeys.Count; i++)
                Forms[removeKeys[i]].KillLastForm();
        }

        internal static bool isDeviceWorkerThreadRunning(int x)
        {
            foreach (KeyValuePair<int, FormConfig> fc in Forms)
                if (fc.Value.DeviceIndex == x && fc.Value.ActiveForm != null
                    && fc.Value.ActiveForm.WorkerRunning)
                {

                    return true;
                }
            return false;
        }

        internal static FormConfig isFatxFileLoaded(int devIndex, string fatx)
        {
            foreach (KeyValuePair<int, FormConfig> fc in Forms)
                if (fc.Value.FatxPath == fatx && fc.Value.DeviceIndex == devIndex)
                    return fc.Value;
            return null;
        }

        internal static void restrictEditor(string FID)
        {
            List<int> removeKeys = new List<int>();
            foreach (KeyValuePair<int, FormConfig> fc in Forms)
                if (fc.Value.Meta.ID == FID)
                    removeKeys.Add(fc.Key);
            for (int x = 0; x < removeKeys.Count; x++)
                Forms[removeKeys[x]].KillLastForm();
        }

        internal static void initializeNewPackageManager(XContentPackage package)
        {
            int x = createNewFormConfig(Horizon.FormConfig.getFormMetaIndex(FormID.PackageManager), -1, null, false);
            Forms[x].Package = package;
            Forms[x].FileName = package.PackageFilePath ?? String.Empty;
            tempRefGlass = false;
            new PackageManager().initiateForm(x);
        }

        internal static int createNewFormConfig(byte mI, int fatxDevice, string fatxPath, bool cachePartition)
        {
            FormConfig nC = new FormConfig(mI, fatxDevice, fatxPath);
            if (nC.IsFatx)
            {
                if (FatxHandle.Devices[nC.DeviceIndex].IsFat32)
                {
                    nC.Package.LoadPackage(FatxHandle.Devices[nC.DeviceIndex].Fat32Drive.Name + nC.FatxPath);
                }
                else
                {
                    nC.Package.LoadPackage(cachePartition
                        ? FatxHandle.Devices[fatxDevice].CacheHandle.LoadFileToIO((fatxPath))
                        : FatxHandle.Devices[fatxDevice].Handle.LoadFileToIO((fatxPath)));
                }
            }
                
            Forms.Add(_lastIndex++, nC);
            return _lastIndex - 1;
        }

        private static List<string> rebuiltPackages = new List<string>();

        internal static bool tempRefGlass;
        internal static Dictionary<int, FormConfig> Forms = new Dictionary<int, FormConfig>();
        private static int _lastIndex;
        internal class FormConfig
        {
            internal FormConfig(byte mI, int deviceIndex, string fatxPath)
            {
                MetaIndex = mI;
                DeviceIndex = deviceIndex;
                FatxPath = fatxPath;
            }

            internal Horizon.FormConfig.FormMeta Meta
            {
                get { return Horizon.FormConfig.formList[MetaIndex]; }
            }

            internal EditorControl ActiveForm
            {
                get { return _activeForm; }
                set { _activeForm = value; }
            }

            internal bool HasToRebuild
            {
                get
                {
                    return !(rebuiltPackages.Contains(FileName) || IsFatx
                        || Meta.ID == FormID.ThemeCreator
                        || (Meta.ID == FormID.GamerPictureManager
                        && Package.Header.Metadata.ContentType != ContentTypes.Profile)
                        || !Package.IsLoaded || Package.Header.Metadata.VolumeType == XContentVolumeType.SVOD_Volume
                        || Package.StfsContentPackage.VolumeExtension.ReadOnly);
                }
                set
                {
                    rebuiltPackages.Add(FileName);
                }
            }

            internal int DeviceIndex;
            internal string FatxPath;
            internal bool IsFatx { get { return FatxPath != null; } }

            internal byte MetaIndex;
            private EditorControl _activeForm;
            internal string FileName = String.Empty;
            internal XContentPackage Package = new XContentPackage();

            internal void KillLastForm()
            {
                if (_activeForm != null)
                    _activeForm.Close();
            }

            internal void KillLastForm(EditorControl currentControl)
            {
                KillLastForm();
                _activeForm = currentControl;
            }
        }
    }
}
