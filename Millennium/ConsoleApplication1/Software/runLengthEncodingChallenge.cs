using System;
using System.Collections.Generic;
using System.Linq;
using mlp.interviews.software.common;
using mlp.interviews.software.testInterfaces;

namespace mlp.interviews.software.test
{
    public class RunLengthEncodingChallenge : BaseClass, IChallenge
    {
        bool disposed = false;
        public byte[] Encode(byte[] original)
        {
            List<byte> dest = new List<byte>();
            byte runLength;

            for (int i = 0; i < original.Length; i++)
            {
                runLength = 1;
                while (runLength < byte.MaxValue
                    && i + 1 < original.Length
                    && original[i] == original[i + 1])
                {
                    runLength++;
                    i++;
                }
                dest.Add(runLength);
                dest.Add(original[i]);
            }
            return dest.ToArray();
        }

        public bool Winner()
        {
            var testCases = new[]
            {
                    new Tuple<byte[], byte[]>(new byte[]{0x01, 0x02, 0x03, 0x04}, new byte[]{0x01, 0x01, 0x01, 0x02, 0x01, 0x03, 0x01, 0x04}),
                    new Tuple<byte[], byte[]>(new byte[]{0x01, 0x01, 0x01, 0x01}, new byte[]{0x04, 0x01}),
                    new Tuple<byte[], byte[]>(new byte[]{0x01, 0x01, 0x02, 0x02}, new byte[]{0x02, 0x01, 0x02, 0x02})
            };

            // TODO: What limitations does your algorithm have (if any)?
            //      1. Maximum byte size is 255. 
            //      2. Property names are Item1, Item2, Item3...,Which may not be meaningful in some cases or without documentation.             
            // TODO: What do you think about the efficiency of this algorithm for encoding data?
            //      1. RLE algorithm is only efficient with files that contain lots of repititive data. Eg. Line art images contain large white and black areas are far
            //         more suitable.

            foreach (var testCase in testCases)
            {
                var encoded = Encode(testCase.Item1);
                var isCorrect = encoded.SequenceEqual(testCase.Item2);

                if (!isCorrect)
                {
                    return false;
                }
            }
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) { }
            disposed = true;

            // Call the base class implementation.
            base.Dispose(disposing);
        }

        ~RunLengthEncodingChallenge()
        {
            Dispose(false);
        }
    }
}
