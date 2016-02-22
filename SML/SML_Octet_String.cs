using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SML_Analyzer
{
    /* 
     typedef struct {
        unsigned char *str;
        int len;
    } octet_string;
     */
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SML_Octet_String_Struct
    {
        public IntPtr str;                      //unsigned char *
        public int len;
    }

    public class SML_Octet_String
    {
        public string str
        {
            get;
            private set;
        }
        public int len
        {
            get;
            private set;
        }

        private SML_Octet_String() { }

        public SML_Octet_String(SML_Octet_String_Struct os)
        {
            this.len = os.len;
            Console.WriteLine("OCTET:"+this.len);
            for (int x = 0; x < this.len && x<64 ; x++)
            {
                IntPtr p = new IntPtr(os.str.ToInt64() + x);
                this.str += (char)Marshal.ReadByte(p);
            }
        }

    }
}
