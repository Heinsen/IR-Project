using System;

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

        public RankedSEDocument(string ID, int Rank, float RelevanceScore)
        {
            this.ID = ID;
            this.Rank = Rank;
            this.RelevanceScore = RelevanceScore;
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
