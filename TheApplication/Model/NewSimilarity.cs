using Lucene.Net.Search;
using System;
using FieldInvertState = Lucene.Net.Index.FieldInvertState;

namespace TheApplication
{
    public class NewSimilarity : DefaultSimilarity
    {

        /// <summary>Implemented as <c>sqrt(freq)</c>. </summary>
        /// decreased precision when applied
        //public override float Tf(float freq)
        //{
        //    return (float)System.Math.Sqrt(freq);
        //    return 1;
        //}

        //best for Inf0 result when applied
        /// <summary>
        /// Implemented as <code>overlap / maxOverlap</code>. </summary>
        public override float Coord(int overlap, int maxOverlap)
        {
            return overlap / (float)maxOverlap;
            //return 1;
        }

        /// <summary>
        /// Implemented as <code>1/sqrt(sumOfSquaredWeights)</code>. </summary>
        public override float QueryNorm(float sumOfSquaredWeights)
        {
            return (float)(1.0 / System.Math.Sqrt(sumOfSquaredWeights));
        }
        
        /// <summary>
        /// Encodes a normalization factor for storage in an index.
        /// <p>
        /// The encoding uses a three-bit mantissa, a five-bit exponent, and the
        /// zero-exponent point at 15, thus representing values from around 7x10^9 to
        /// 2x10^-9 with about one significant decimal digit of accuracy. Zero is also
        /// represented. Negative numbers are rounded up to zero. Values too large to
        /// represent are rounded down to the largest representable value. Positive
        /// values too small to represent are rounded up to the smallest positive
        /// representable value.
        /// </summary>
        /// <seealso cref= org.apache.lucene.document.Field#setBoost(float) </seealso>
        /// <seealso cref= org.apache.lucene.util.SmallFloat </seealso>
        //public override long EncodeNormValue(float f)
        //{
        //    return BitConverter.DoubleToInt64Bits(f);
        //}

                /// <summary>
        /// Decodes the norm value, assuming it is a single byte.
        /// </summary>
        /// <seealso cref= #encodeNormValue(float) </seealso>
        //public override float DecodeNormValue(long norm)
        //{
        //    return (float) BitConverter.Int64BitsToDouble(norm);
        //}
        
        /// <summary>
        /// Implemented as
        ///  <code>state.getBoost()*lengthNorm(numTerms)</code>, where
        ///  <code>numTerms</code> is <seealso cref="FieldInvertState#getLength()"/> if {@link
        ///  #setDiscountOverlaps} is false, else it's {@link
        ///  org.apache.lucene.index.FieldInvertState#getLength()} - {@link
        ///  org.apache.lucene.index.FieldInvertState#getNumOverlap()}.
        /// 
        ///  @lucene.experimental 
        /// </summary>
        public override float ComputeNorm(string field, FieldInvertState state)
        {
            int numTerms;
            if (discountOverlaps)
            {
                numTerms = state.Length - state.NumOverlap;
            }
            else
            {
                numTerms = state.Length;
            }
            return state.Boost * ((float)(1.0 / Math.Sqrt(numTerms)));
        }

                /// <summary>
        /// Implemented as <code>sqrt(freq)</code>. </summary>
        public override float Tf(float freq)
        {
            float k = 1.2F;
            return ((k + 1) * freq) / (k + freq);

            //return ((k + 1) * freq) / (k * (1.0 - b + b * L) + freq);
            //return (float)Math.Sqrt(freq);
            //if (freq > 0)
            //{
            //    return 1.0f;
            //}
            //else
            //{
            //    return 0.0f;
            //}
        }

        /// <summary>
        /// Implemented as <code>1 / (distance + 1)</code>. 
        /// </summary>
        public override float SloppyFreq(int distance)
        {
            return 1.0f / (distance + 1);
        }

        /// <summary>
        /// The default implementation returns <code>1</code>
        /// </summary>
        //public override float ScorePayload(int doc, int start, int end, BytesRef payload)
        //{
        //    return 1;
        //}

        /// <summary>
        /// Implemented as <code>log(numDocs/(docFreq+1)) + 1</code>. 
        /// </summary>
        public override float Idf(int docFreq, int numDocs)
        {
            return (float)(Math.Log(numDocs / (double)(docFreq + 1)) + 1.0);

        }

                /// <summary>
        /// True if overlap tokens (tokens with a position of increment of zero) are
        /// discounted from the document's length.
        /// </summary>
        protected internal bool discountOverlaps = true;

        /// <summary>
        /// Determines whether overlap tokens (Tokens with
        ///  0 position increment) are ignored when computing
        ///  norm.  By default this is true, meaning overlap
        ///  tokens do not count when computing norms.
        /// 
        ///  @lucene.experimental
        /// </summary>
        ///  <seealso cref= #computeNorm </seealso>
        public virtual bool DiscountOverlaps
        {
            set { discountOverlaps = value; }
            get { return discountOverlaps; }
        }

        public override string ToString()
        {
            return "DefaultSimilarity";
        }
        //public override float idf(long docFreq,
        //long numDocs)
        //{
        //    return (float)System.Math.Sqrt(numDocs);
        //}
    }

}

//internal class PreciseDefaultSimilarity : TFIDFSimilarity
//    {
//        /// <summary>
//        /// Sole constructor: parameter-free </summary>
//        public PreciseDefaultSimilarity()
//        {
//        }

//        /// <summary>
//        /// Implemented as <code>overlap / maxOverlap</code>. </summary>
//        public override float Coord(int overlap, int maxOverlap)
//        {
//            return overlap / (float)maxOverlap;
//        }

//        /// <summary>
//        /// Implemented as <code>1/sqrt(sumOfSquaredWeights)</code>. </summary>
//        public override float QueryNorm(float sumOfSquaredWeights)
//        {
//            return (float)(1.0 / Math.Sqrt(sumOfSquaredWeights));
//        }

//        /// <summary>
//        /// Encodes a normalization factor for storage in an index.
//        /// <p>
//        /// The encoding uses a three-bit mantissa, a five-bit exponent, and the
//        /// zero-exponent point at 15, thus representing values from around 7x10^9 to
//        /// 2x10^-9 with about one significant decimal digit of accuracy. Zero is also
//        /// represented. Negative numbers are rounded up to zero. Values too large to
//        /// represent are rounded down to the largest representable value. Positive
//        /// values too small to represent are rounded up to the smallest positive
//        /// representable value.
//        /// </summary>
//        /// <seealso cref= org.apache.lucene.document.Field#setBoost(float) </seealso>
//        /// <seealso cref= org.apache.lucene.util.SmallFloat </seealso>
//        public override long EncodeNormValue(float f)
//        {
//            return BitConverter.DoubleToInt64Bits(f);
//        }

//        /// <summary>
//        /// Decodes the norm value, assuming it is a single byte.
//        /// </summary>
//        /// <seealso cref= #encodeNormValue(float) </seealso>
//        public override float DecodeNormValue(long norm)
//        {
//            return (float) BitConverter.Int64BitsToDouble(norm);
//        }

//        /// <summary>
//        /// Implemented as
//        ///  <code>state.getBoost()*lengthNorm(numTerms)</code>, where
//        ///  <code>numTerms</code> is <seealso cref="FieldInvertState#getLength()"/> if {@link
//        ///  #setDiscountOverlaps} is false, else it's {@link
//        ///  org.apache.lucene.index.FieldInvertState#getLength()} - {@link
//        ///  org.apache.lucene.index.FieldInvertState#getNumOverlap()}.
//        /// 
//        ///  @lucene.experimental 
//        /// </summary>
//        public override float LengthNorm(FieldInvertState state)
//        {
//            int numTerms;
//            if (discountOverlaps)
//            {
//                numTerms = state.Length - state.NumOverlap;
//            }
//            else
//            {
//                numTerms = state.Length;
//            }
//            return state.Boost * ((float)(1.0 / Math.Sqrt(numTerms)));
//        }

//        /// <summary>
//        /// Implemented as <code>sqrt(freq)</code>. </summary>
//        public override float Tf(float freq)
//        {
//            return (float)Math.Sqrt(freq);
//        }

//        /// <summary>
//        /// Implemented as <code>1 / (distance + 1)</code>. 
//        /// </summary>
//        public override float SloppyFreq(int distance)
//        {
//            return 1.0f / (distance + 1);
//        }

//        /// <summary>
//        /// The default implementation returns <code>1</code>
//        /// </summary>
//        public override float ScorePayload(int doc, int start, int end, BytesRef payload)
//        {
//            return 1;
//        }

//        /// <summary>
//        /// Implemented as <code>log(numDocs/(docFreq+1)) + 1</code>. 
//        /// </summary>
//        public override float Idf(long docFreq, long numDocs)
//        {
//            return (float)(Math.Log(numDocs / (double)(docFreq + 1)) + 1.0);
//        }

//        /// <summary>
//        /// True if overlap tokens (tokens with a position of increment of zero) are
//        /// discounted from the document's length.
//        /// </summary>
//        protected internal bool discountOverlaps = true;

//        /// <summary>
//        /// Determines whether overlap tokens (Tokens with
//        ///  0 position increment) are ignored when computing
//        ///  norm.  By default this is true, meaning overlap
//        ///  tokens do not count when computing norms.
//        /// 
//        ///  @lucene.experimental
//        /// </summary>
//        ///  <seealso cref= #computeNorm </seealso>
//        public virtual bool DiscountOverlaps
//        {
//            set { discountOverlaps = value; }
//            get { return discountOverlaps; }
//        }

//        public override string ToString()
//        {
//            return "DefaultSimilarity";
//        }
//    }
