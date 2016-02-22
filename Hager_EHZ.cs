using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace SML_Analyzer
{
    class Hager_EHZ
    { 
        private SerialPort _serial = null;
        private SML_Sequence _lastSeq = null;

        public Hager_EHZ() : this("/dev/ttyUSB0")
        {
        }

        public Hager_EHZ(string devicePort)
        {
            if (_serial != null)
                if (_serial.IsOpen)
                    _serial.Close();

            _serial = new SerialPort(devicePort, 9600, Parity.None, 8, StopBits.One);
            _serial.Handshake = Handshake.None;
            _serial.ReadTimeout = _serial.WriteTimeout = 5000;

            _serial.Open();
        }

        private byte[] WaitForStartSeq()
        {
            List<byte> pattern = new List<byte>(SML_Sequence._escapeSeq);
            pattern.AddRange(SML_Sequence._startSeq);
            byte[] startPattern = pattern.ToArray();

            Queue<byte> ringBuffer = new Queue<byte>(startPattern.Length);

            while (1 == 1)
            {
                ringBuffer.Enqueue((byte)_serial.ReadByte());
                byte[] content = ringBuffer.ToArray();
                if (content.Length == startPattern.Length)
                {
                    for (int x = 0; x < startPattern.Length; x++)
                    {
                        if (startPattern[x] != content[x])
                        {
                            ringBuffer.Dequeue();
                            break;
                        }
                        if (x == startPattern.Length - 1)
                        {
                            return content;
                        }
                    }
                }
            }
        }

        private byte[] WaitForEndSeq()
        {
            List<byte> pattern = new List<byte>(SML_Sequence._escapeSeq);
            pattern.Add(0x1A);
            byte[] endPattern = pattern.ToArray();

            List<byte> receivedValues = new List<byte>();
            Queue<byte> ringBuffer = new Queue<byte>(endPattern.Length);

            byte receivedByte = 0;

            while (1 == 1)
            {
                receivedByte = (byte)_serial.ReadByte();
                ringBuffer.Enqueue(receivedByte);
                receivedValues.Add(receivedByte);
                byte[] content = ringBuffer.ToArray();

                if (content.Length == endPattern.Length)
                {
                    for (int x = 0; x < endPattern.Length; x++)
                    {
                        if (endPattern[x] != content[x])
                        {
                            ringBuffer.Dequeue();
                            break;
                        }
                        if (x == endPattern.Length - 1)
                        {
                            for (int checksum = 0; checksum < 3; checksum++)
                                receivedValues.Add((byte)_serial.ReadByte());
                            return receivedValues.ToArray();
                        }
                    }
                }
            }

            
        }

        public SML_Sequence ReadSequence()
        {
            List<byte> sequence = new List<byte>();
            sequence.AddRange(WaitForStartSeq());
            sequence.AddRange(WaitForEndSeq());

            this._lastSeq = new SML_Sequence(sequence.ToArray());
            return this._lastSeq;
        }

        public void Print()
        {
            byte[] retVal = this._lastSeq._sequence;
            /*foreach (byte c in retVal)
            {
                Console.Write("0x{0:X2} ", ((int)c));
            }*/

            int val = 0;
            val |= retVal[158] << 24;
            val |= retVal[159] << 16;
            val |= retVal[160] << 8;
            val |= retVal[161];
            Console.WriteLine();

            Console.WriteLine("Verbrauch: {0:F1} Wh", val/10.0);
        }
    }
}
