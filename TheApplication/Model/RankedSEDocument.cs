using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheApplication.Model;

namespace TheApplication.Model
{
    public class RankedSEDocument : SEDocument
    {

        public RankedSEDocument(string ID, string Title, string Author, string Bibliographic, string Abstract, int Rank, float RelevanceScore) : base(ID, Title, Author, Bibliographic, Abstract)
        {
            this.Rank = Rank;
            this.RelevanceScore = RelevanceScore;
        }

        public RankedSEDocument(string ID, int Rank, float RelevanceScore)
        {
            this.ID = ID;
            this.Rank = Rank;
            this.RelevanceScore = RelevanceScore;
        }

        public RankedSEDocument(string SEDocumentText) : base(SEDocumentText)
        {
        }

        public string getAbstractFirstLine()
        {
            return (this.Abstract.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))[0];
        }

        public string getDocumentSaveString()
        {
            return ID + " " + (Rank.ToString()) + " "  + RelevanceScore.ToString() + " ";
        }

        public int Rank { get; set; }

        public float RelevanceScore { get; set; }
    }
}
