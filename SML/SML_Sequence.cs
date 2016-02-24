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
     
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SML_Buffer {
        public IntPtr buffer;
	    public int buffer_len;
        public int cursor;
        public int error;
        public IntPtr error_msg;
    }*/

    struct SML_Period
    {
        byte unknown;
        byte unknown1;
        byte Unit;
        byte Scaler;
        int value;
        byte Signature;
    }

    class SML_Sequence
    {
        public static readonly byte[] _escapeSeq = { 0x1b, 0x1b, 0x1b, 0x1b };
        public static readonly byte[] _startSeq  = { 0x01, 0x01, 0x01, 0x01 };

        private Dictionary<OBIS.Obis_Content, SML_Period> content = new Dictionary<OBIS.Obis_Content, SML_Period>(); 

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
            byte[] subArr = new byte[sequence.Length - 16];
            int idx = -1;
           
            this._sequence = sequence;
            idx = this.FindIDX( OBIS.Obis_Content.Counter_Rate1 );
        }

        private int FindIDX(OBIS.Obis_Content value)
        {
            byte[] seq = OBIS.aliases[value];
            int len = seq.Length;
            int cnt = 0;

            for (int x = 0; x < this._sequence.Length; x++)
            {
                if (this._sequence[x] == seq[cnt++])
                {
                    if (len == cnt)
                    {
                        return ++x;
                    }
                }
                else
                {
                    cnt = 0;
                }
            }
            return -1;
        }

        private SML_Period GetContent(int idx)
        {
            SML_Period x = new SML_Period();
            return x;
        }
    }
}
