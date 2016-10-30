using Lucene.Net.Search;

namespace TheApplication
{
    public class NewSimilarity : DefaultSimilarity
    {
        /// <summary>
        /// Implemented as <code>sqrt(freq)</code>.
        /// </summary>
        public override float Tf(float freq)
        {
            float k = 1.2F;
            return ((k + 1) * freq) / (k + freq);
        }

        /// <summary>
        /// True if overlap tokens (tokens with a position of increment of zero) are
        /// discounted from the document's length.
        /// </summary>
        protected internal bool discountOverlaps = true;

        public override string ToString()
        {
            return "DefaultSimilarity";
        }
    }

}