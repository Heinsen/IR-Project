using Lucene.Net.Documents;
using Lucene.Net.Search;
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
        string _TopicID;

        public SaveController(string TopicID, List<RankedSEDocument> RankedSEDocuments)
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


        public bool SaveDocument()
        {

            string FileContent = string.Empty;
            foreach (RankedSEDocument Doc in _RankedSEDocuments)
            {
                FileContent += _TopicID + " " +
                    "Q0" + " " +
                    Doc.getDocumentSaveString() + " " +
                    "9792066_9647279_SWORD" +
                    Environment.NewLine;
            }
            return FileManipulator.SaveFile((string)Properties.Settings.Default["SavePath"], (string)Properties.Settings.Default["SaveFileName"], FileContent, true);
        }
    }
}
