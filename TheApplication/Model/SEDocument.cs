using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TheApplication
{
    public class SEDocument
    {
        public SEDocument(string ID, string Title, string Author, string Bibliographic, string Abstract)
        {
            this.ID = ID;
            this.Title = Title;
            this.Author = Author;
            this.Bibliographic = Bibliographic;
            this.Abstract = Abstract;
        }

        public SEDocument(string SEDocumentText)
        {
            SEDocumentText = SEDocumentText.Replace("\n", " ");
            int docIdIndex = SEDocumentText.IndexOf(".I");
            int titleIndex = SEDocumentText.IndexOf(".T");
            int authorIndex = SEDocumentText.IndexOf(".A");
            int bibIndex = SEDocumentText.IndexOf(".B");
            int absIndex = SEDocumentText.IndexOf(".W");

            this.ID = SEDocumentText.Substring(docIdIndex + 2, titleIndex - docIdIndex - 2).Trim();
            this.Title = SEDocumentText.Substring(titleIndex + 2, authorIndex - titleIndex - 2).Trim();
            this.Author = SEDocumentText.Substring(authorIndex + 2, bibIndex - authorIndex - 2).Trim();
            this.Bibliographic = SEDocumentText.Substring(bibIndex + 2, absIndex - bibIndex - 2).Trim();
            this.Abstract = SEDocumentText.Substring(absIndex + 2, SEDocumentText.Length - absIndex - 2).Trim();

            //Remove Repeated Title from Abstract
            this.Abstract = this.Abstract.Remove(0, this.Title.Count()).Trim();
        }
        public string ID { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Bibliographic { get; set; }

        public string Abstract { get; set; }  
    }
}
