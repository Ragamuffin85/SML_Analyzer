using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SML_Analyzer
{
    /* 
     typedef struct {
	    u32 *tag;
	    void *data;
    } sml_message_body;
     */
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SML_Message_Body_Struct
    {
        public IntPtr tag;                      //uint32 *
        public IntPtr data;                     //void *
    }

    /*
     typedef struct {
	    octet_string *transaction_id;
	    u8 *group_id;
	    u8 *abort_on_error;
	    sml_message_body *message_body;
	    u16 *crc;
    } sml_message;
     */

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SML_Message_Struct
    {
        public IntPtr transaction_id;
        public IntPtr group_id;
        public IntPtr abort_on_error;
        public IntPtr message_body;
        public IntPtr crc;
        /* end of message */
    }

    public class SML_Message
    {
#region DATA
        public SML_Octet_String transaction_id
        {
            get;
            private set;
        }
        public byte group_id
        {
            get;
            private set;
        }
        public byte abort_on_error
        {
            get;
            private set;
        }
        public SML_Message_Body_Struct message_body
        {
            get;
            private set;
        }
        public short crc
        {
            get;
            private set;
        }
#endregion

#region CONST/DEST
        private SML_Message() { }

        public SML_Message(SML_Message_Struct msg)
        {
            string state = String.Empty;
            try
            {
                state = String.Empty;
                SML_Octet_String_Struct os = (SML_Octet_String_Struct)Marshal.PtrToStructure(msg.transaction_id, typeof(SML_Octet_String_Struct));
                this.transaction_id = new SML_Octet_String( os );
                Console.WriteLine("Transaction ID Done =>");
                this.group_id = Marshal.ReadByte( msg.group_id );
                Console.WriteLine("Group ID Done => {0:X8}", this.group_id);
                this.abort_on_error = Marshal.ReadByte( msg.abort_on_error );
                Console.WriteLine("Abort On Error Done => {0:X8}", this.abort_on_error);
                this.message_body = (SML_Message_Body_Struct)Marshal.PtrToStructure(msg.message_body, typeof(SML_Message_Body_Struct));
                Console.WriteLine("SML_Message_Body Done => ");
                this.crc = Marshal.ReadInt16(msg.crc);
                Console.WriteLine("CRC Done => {0:X8}",this.crc);
            }
            catch (Exception e)
            {
                Exception ex = new Exception("SML_Message creation error: STATE: \n"+state+"\t"+ e.Message);
                throw ex;
            }
        }
#endregion
    }
}
