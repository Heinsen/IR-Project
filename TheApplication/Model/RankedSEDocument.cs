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
            this.Description = CreateDescription();
        }

        public RankedSEDocument(string SEDocumentText) : base(SEDocumentText)
        {
        }

        private string CreateDescription()
        {
            return Author + Environment.NewLine + getAbstractFirstLine();
        } 

        public string getAbstractFirstLine()
        {
            return (this.Abstract.Split(new string[] { "." }, StringSplitOptions.None))[0];
        }

        public string getDocumentSaveString()
        {
            return ID + " " + (Rank.ToString()) + " "  + RelevanceScore.ToString() + " ";
        }

        public int Rank { get; set; }

        public float RelevanceScore { get; set; }

        public string Description { get; set; }
    }
}
