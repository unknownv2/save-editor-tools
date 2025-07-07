using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.AdvTree;

namespace Horizon.PackageEditors.Borderlands
{
    public partial class Borderlands : EditorControl
    {
       /// <summary>
        /// Our field title ID
        /// </summary>
        //public static readonly string FID = "545407E7";

        private BorderlandsClass Borderlands_Class { get; set; }
        /// <summary>
        /// Our default constructor.
        /// </summary>
        public Borderlands()
        {
            InitializeComponent();
            TitleID = FormID.Borderlands;
            //Set our title ID
            
            //Set our maximums.
            intPlayerMoney.MaxValue = int.MaxValue;
            intPlayerXPPoints.MaxValue = int.MaxValue;
            intPlayerSkillPoints.MaxValue = int.MaxValue;
            intPlayerLevel.MaxValue = int.MaxValue;
            intSettingValue.MaxValue = int.MaxValue;
            intSettingValue.MinValue = 0;
        }

        /// <summary>
        /// Our override for the entry point for this applet. Opens the file and reads it.
        /// </summary>
        /// <returns>Returns a bool indicating if we read our file correctly.</returns>
        public override bool Entry()
        {
            //Open our file. 
            if (!this.OpenStfsFile("SaveGame.sav"))
                return false;

            //Initialize our borderlands class
            Borderlands_Class = new BorderlandsClass(IO);

            //Set our values
            intPlayerLevel.Value = (int)Borderlands_Class.Player_Struct.Level;
            intPlayerMoney.Value = (int)Borderlands_Class.Player_Struct.Money;
            intPlayerSkillPoints.Value = (int)Borderlands_Class.Player_Struct.Skill_Points;
            intPlayerXPPoints.Value = (int)Borderlands_Class.Player_Struct.XP_Points;

            //Clear our values list
            listValues.Nodes.Clear();
            //Load our values list..
            //Loop for each value
            foreach (BorderlandsClass.PlayerStructure.KeyValue KV in Borderlands_Class.Player_Struct.KeyValues)
            {
                //Create our node, give it the shortened key name.
                Node node = new Node(ShortKeyName(KV.Key));
                //Set our node's tag
                node.Tag = KV.Key;
                //Add our value
                node.Cells.Add(new Cell(KV.Value.ToString()));
                //Add our node
                listValues.Nodes.Add(node);
            }

            //Our file is read correctly.
            return true;
        }
        private string ShortKeyName(string key)
        {
            //Our part count
            int partCount = 2;
            //Split the key at each .
            List<string> tmpStr = new List<string>(key.Split('.'));
            //Remove any blank entries.
            tmpStr.Remove("");
            //If our length is <= partCount
            if (tmpStr.Count <= partCount)
                //Return our string
                return key;
            else
            {
                //Create our result string
                string result = "";
                //Otherwise let's create our shortened string.
                for (int i = tmpStr.Count - (partCount + 0); i < tmpStr.Count; i++)
                    //Add to our result
                    result += tmpStr[i] + ".";
                //Return our result.
                return result.Substring(0, result.Length - 1);
            }
        }
        public override void Save()
        {
            //Set our values
            Borderlands_Class.Player_Struct.Level = (uint)intPlayerLevel.Value;
            Borderlands_Class.Player_Struct.Money = (uint)intPlayerMoney.Value;
            Borderlands_Class.Player_Struct.Skill_Points = (uint)intPlayerSkillPoints.Value;
            Borderlands_Class.Player_Struct.XP_Points = (uint)intPlayerXPPoints.Value;
            //Use our Borderlands class to save
            Borderlands_Class.Write();
        }

        private void listValues_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If our index is valid
            if (listValues.SelectedIndex > -1)
            {
                //Set our full key name
                lblKeyName.Text = listValues.SelectedNode.Tag.ToString();
                //Set our value
                intSettingValue.Value = int.Parse(listValues.SelectedNode.Cells[1].Text);
            }
        }

        private void cmdSetValue_Click(object sender, EventArgs e)
        {
            //If our index is valid
            if (listValues.SelectedIndex > -1)
            {
                //Set our key's value
                Borderlands_Class.Player_Struct.SetKeyValue(listValues.SelectedNode.Tag.ToString(), (uint)intSettingValue.Value);
                //Set our list's text.
                listValues.SelectedNode.Cells[1].Text = intSettingValue.Value.ToString();
            }
        }

        private void cmdMaxPlayerLevel_Click(object sender, EventArgs e)
        {
            intPlayerLevel.Value = intPlayerLevel.MaxValue;
        }

        private void cmdPlayerXPPointsMax_Click(object sender, EventArgs e)
        {
            intPlayerXPPoints.Value = intPlayerXPPoints.MaxValue;
        }

        private void cmdPlayerSkillPointsMax_Click(object sender, EventArgs e)
        {
            intPlayerSkillPoints.Value = intPlayerSkillPoints.MaxValue;
        }

        private void cmdPlayerMoneyMax_Click(object sender, EventArgs e)
        {
            intPlayerMoney.Value = intPlayerMoney.MaxValue;
        }
    }
}
