using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApplication.Model;

namespace TheApplication.Controller
{
    public class SaveController
    {

        List<RankedSEDocument> _RankedSEDocuments;
        int _TopicID;

        //TODO: Pass correct TopicID
        public SaveController(int TopicID, List<RankedSEDocument> RankedSEDocuments)
        {
            this._TopicID = TopicID;
            this._RankedSEDocuments = RankedSEDocuments;
        }

        public void SetSavePath(string SavePath)
        {
            Properties.Settings.Default["SavePath"] = SavePath;
            Properties.Settings.Default.Save();
        }

        public string GetSavePath()
        {
            return (string)Properties.Settings.Default["SavePath"];
        }


        public void SetFileName(string FileName)
        {
            Properties.Settings.Default["SaveFileName"] = FileName;
            Properties.Settings.Default.Save();
        }

        public string GetFileName()
        {
            return (string)Properties.Settings.Default["SaveFileName"];
        }

        public void SaveDocument()
        {

            string FileContent = string.Empty;

            foreach(RankedSEDocument Document in _RankedSEDocuments)
            {
                //TODO: Add studentnumber and groupname
                FileContent += _TopicID.ToString() + " " +
                    "Q0" + " " +
                    Document.getDocumentSaveString() + " " + 
                    "9792066" + 
                    Environment.NewLine;
            }

            //TODO: Indicate wether or not it was successfully saved
            bool SuccessfullySave = FileManipulator.SaveFile((string)Properties.Settings.Default["SavePath"], (string)Properties.Settings.Default["SaveFileName"], FileContent, true);
        }
    }
}
