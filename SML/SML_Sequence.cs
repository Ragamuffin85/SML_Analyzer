using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SML_Analyzer 
{
    /*
     typedef struct {
	    unsigned char *buffer;
	    size_t buffer_len;
	    int cursor;
	    int error;
	    char *error_msg;
    } sml_buffer;
     */
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SML_Buffer {
        public IntPtr buffer;
	    public int buffer_len;
        public int cursor;
        public int error;
        public IntPtr error_msg;
    }

    class SML_Sequence
    {
        public static readonly byte[] _escapeSeq = { 0x1b, 0x1b, 0x1b, 0x1b };
        public static readonly byte[] _startSeq  = { 0x01, 0x01, 0x01, 0x01 };


        public byte[] _sequence
        {
            get;
            private set;
        }

        private SML_Sequence()
        { 
        }

        public SML_Sequence(byte[] sequence)
        {
            //OK
            byte[] subArr = new byte[sequence.Length - 16];
            Array.Copy(sequence, 8, subArr, 0, sequence.Length - 16);
            SML_File file = new SML_File(subArr);
            file.Print();
           
            this._sequence = sequence;
            this.Parse();
        }

        private void Parse()
        {
        }
    }
}
