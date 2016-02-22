using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SML_Analyzer
{
    /* 
     typedef struct {
    sml_message **messages;
    short messages_len;
    sml_buffer *buf;
    } sml_file;
     */
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SML_File_struct
    {
        public IntPtr messages;                 //sml_message **
        public short message_len;
        public IntPtr buf;                      //sml_buffer *
    }

    public class SML_File
    {
#region IMPORT
#if WINDOWS
        public const string __IMPORT = "libsml.dll";
#else
        public const string __IMPORT = "libsml.so.1";
#endif

        [DllImport(__IMPORT, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr sml_file_parse(byte[] buffer, int len);

        [DllImport(__IMPORT, CallingConvention = CallingConvention.Cdecl)]
        private static extern void sml_file_print(IntPtr file);

        [DllImport(__IMPORT, CallingConvention = CallingConvention.Cdecl)]
        private static extern void sml_file_free(IntPtr file);

#endregion

#region DATA
        private IntPtr fileHndl = new IntPtr();
        private SML_File_struct file;

        public SML_Message_Struct[] messages
        {
            get;
            private set;
        }

        public short message_len
        {
            get;
            private set;
        }

        public SML_Buffer buf
        {
            get;
            private set;
        }

#endregion

#region CONST/DEST
        private SML_File() { }

        public SML_File(byte[] sequence)
        {
            try
            {
                fileHndl = sml_file_parse(sequence, sequence.Length);
                file = (SML_File_struct)Marshal.PtrToStructure(fileHndl, typeof(SML_File_struct));

                this.buf = (SML_Buffer)Marshal.PtrToStructure(file.buf, typeof(SML_Buffer));
                this.message_len = file.message_len;

                int structSize = Marshal.SizeOf(typeof(SML_Message_Struct));
                SML_Message[] messages = new SML_Message[this.message_len];

                for (int i = 0; i < this.message_len; i++)
                {
                    IntPtr p = new IntPtr((file.messages.ToInt64() + i * structSize));
                    SML_Message_Struct msg = (SML_Message_Struct)Marshal.PtrToStructure(p, typeof(SML_Message_Struct));
                    messages[i] = new SML_Message(msg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SML_File creation error: "+ e.Message);
                //throw e;
            }
        }

        ~SML_File()
        {
            if( IntPtr.Zero != fileHndl )
            {
                sml_file_free(fileHndl);
            }
        }
#endregion

        public void Print()
        {
            
        }
            
    }
}
