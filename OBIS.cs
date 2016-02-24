using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SML_Analyzer
{
    /**
     Datentelegramm nach FNN Lastenheft EDL:
OBIS-Kennzahl Inhalt
81 81 C7 82 03 FF Hersteller-Identifikation 
01 00 00 00 09 FF Geräteeinzelidentifikation 
01 00 01 08 00 FF Zählerstand Totalregister 
01 00 01 08 01 FF Zählerstand Tarif 1 
01 00 01 08 02 FF Zählerstand Tarif 2 
Statusinformation
01 00 0F 07 00 FF aktuelle Wirkleistung 
01 00 15 07 00 FF: Wirkleistung L1
01 00 29 07 00 FF: Wirkleistung L2 
01 00 3D 07 00 FF: Wirkleistung L3
01 00 01 11 00 FF (nur rückseitige Schnittstelle) letzter signierter 
Total-Zählerstand 
81 81 C7 82 05 FF öffentlicher Schlüssel 
Zusatztelegramm (optional):
OBIS-Kennzahl Inhalt
01 00 60 32 00 02: Aktuelle Chiptemperatur 
01 00 60 32 00 03: Minimale Chiptemperatur 
01 00 60 32 00 04: Maximale Chiptemperatur 
01 00 60 32 00 05: Gemittelte Chiptemperatur 
01 00 60 32 03 03: Spannungsminimum 
01 00 60 32 03 04: Spannungsmaximum 
01 00 1F 07 00 FF: Strom L1 
01 00 20 07 00 FF: Spannung L1 
01 00 33 07 00 FF: Strom L2 
01 00 34 07 00 FF: Spannung L2 
01 00 47 07 00 FF: Strom L3 
01 00 48 07 00 FF: Spannung L3
     */

    
    class OBIS
    {
        public enum Obis_Content { Producer_ID, Device_ID, Counter_Total, Counter_Rate1, Counter_Rate2,
                                    Effective_Power, Power_L1, Power_L2, Power_L3, Counter_Total_Back,
                                    Public_Key, 
                                    Temp_Current, Temp_Min, Temp_Max, Temp_Mean,Voltage_Min, Voltage_Max, 
                                    Current_L1, Voltage_L1, Current_L2, Voltage_L2, Current_L3, Voltage_L3 };

        public static readonly Dictionary<Obis_Content, byte[]> aliases = new Dictionary<Obis_Content, byte[]>()
        {
            { Obis_Content.Producer_ID  ,   new byte[]{0x81, 0x81, 0xC7, 0x82, 0x03, 0xFF} },
            { Obis_Content.Device_ID    ,   new byte[]{0x01, 0x00, 0x00, 0x00, 0x09, 0xFF} },
            { Obis_Content.Counter_Total,   new byte[]{0x01, 0x00, 0x01, 0x08, 0x00, 0xFF} },
            { Obis_Content.Counter_Rate1,   new byte[]{0x01, 0x00, 0x01, 0x08, 0x01, 0xFF} },
            { Obis_Content.Counter_Rate2,   new byte[]{0x01, 0x00, 0x01, 0x08, 0x02, 0xFF} },
            //Statusinformation
            { Obis_Content.Effective_Power, new byte[]{0x01, 0x00, 0x0F, 0x07, 0x00, 0xFF} },
            { Obis_Content.Power_L1,        new byte[]{0x01, 0x00, 0x15, 0x07, 0x00, 0xFF} },
            { Obis_Content.Power_L2,        new byte[]{0x01, 0x00, 0x29, 0x07, 0x00, 0xFF} },
            { Obis_Content.Power_L3,        new byte[]{0x01, 0x00, 0x3D, 0x07, 0x00, 0xFF} },
            { Obis_Content.Counter_Total_Back, new byte[]{0x01, 0x00, 0x01, 0x11, 0x00, 0xFF}},
            { Obis_Content.Public_Key,      new byte[]{0x81, 0x81, 0xC7, 0x82, 0x05, 0xFF} },
            { Obis_Content.Temp_Current,    new byte[]{0x01, 0x00, 0x60, 0x32, 0x00, 0x02} },
            { Obis_Content.Temp_Min,        new byte[]{0x01, 0x00, 0x60, 0x32, 0x00, 0x03} },
            { Obis_Content.Temp_Max,        new byte[]{0x01, 0x00, 0x60, 0x32, 0x00, 0x04} },
            { Obis_Content.Temp_Mean,       new byte[]{0x01, 0x00, 0x60, 0x32, 0x00, 0x05} },
            { Obis_Content.Voltage_Min,     new byte[]{0x01, 0x00, 0x60, 0x32, 0x03, 0x03} },
            { Obis_Content.Voltage_Max,     new byte[]{0x01, 0x00, 0x60, 0x32, 0x03, 0x04} },
            { Obis_Content.Current_L1,      new byte[]{0x01, 0x00, 0x1F, 0x07, 0x00, 0xFF} },
            { Obis_Content.Voltage_L1,      new byte[]{0x01, 0x00, 0x20, 0x07, 0x00, 0xFF} },
            { Obis_Content.Current_L2,      new byte[]{0x01, 0x00, 0x33, 0x07, 0x00, 0xFF} },
            { Obis_Content.Voltage_L2,      new byte[]{0x01, 0x00, 0x34, 0x07, 0x00, 0xFF} },
            { Obis_Content.Current_L3,      new byte[]{0x01, 0x00, 0x47, 0x07, 0x00, 0xFF} },
            { Obis_Content.Voltage_L3,      new byte[]{0x01, 0x00, 0x48, 0x07, 0x00, 0xFF} }
        };
    }
}
