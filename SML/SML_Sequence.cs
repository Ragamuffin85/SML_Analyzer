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

    public struct SML_Period
    {
        public byte unknown;
        public byte unknown1;
        public byte Unit;
        public sbyte Scaler;
        public int Value;
        public byte Signature;
    }

    class SML_Sequence
    {
        public static readonly byte[] _escapeSeq = { 0x1b, 0x1b, 0x1b, 0x1b };
        public static readonly byte[] _startSeq  = { 0x01, 0x01, 0x01, 0x01 };

        public Dictionary<OBIS.Obis_Content, SML_Period> _content
        {
            get;
            private set;
        }

        public byte[] _sequence
        {
            get;
            private set;
        }

        private SML_Sequence()
        {
            this._content = new Dictionary<OBIS.Obis_Content, SML_Period>();
        }

        public SML_Sequence(byte[] sequence) : this()
        {
            byte[] subArr = new byte[sequence.Length - 16];
            int idx = -1;
           
            this._sequence = sequence;

            OBIS.Obis_Content[] OC = (OBIS.Obis_Content[])(Enum.GetValues(typeof( OBIS.Obis_Content)));

            foreach (OBIS.Obis_Content c in OC)
            {
                try
                {
                    idx = -1;
                    idx = this.FindIDX(c);
                    _content.Add(c, GetPeriod(idx));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Parsing Error: " + c.ToString()+" MESSAGE: "+ e.Message);
                }
            }
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

        private SML_Period GetPeriod(int idx)
        {
            SML_Period x = new SML_Period();
            x.unknown   = (byte)ReadKey(ref idx);
            x.unknown1  = (byte)ReadKey(ref idx);
            x.Unit      = (byte)ReadKey(ref idx);
            x.Scaler    = (sbyte)ReadKey(ref idx);
            x.Value     = ReadKey(ref idx);
            x.Signature = (byte)ReadKey(ref idx);
            return x;
        }

        private int ReadKey(ref int idx)
        {
            int val = 0;
            switch( this._sequence[idx] )
            {
                case 0x55:
                    val |= this._sequence[++idx] << 24;
                    val |= this._sequence[++idx] << 16;
                    val |= this._sequence[++idx] << 8;
                    val |= this._sequence[++idx];
                    break;
                case 0x52: //Fallthrough
                case 0x62:
                    val |= this._sequence[++idx];
                    break;
                case 0x77: //End of Period
                    break;
                case 0x01: // OPTIONAL: SKIPP
                default:
                    val = -1;
                    break;
            }

            //Next Valueidentifier index
            idx++;
            return val;
        }
    }
}
