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
        private enum Obis_Content { Producer_ID, Device_ID, Counter_Total, Counter_Rate1, Counter_Rate2 };

        public readonly Dictionary<Obis_Content, byte[]> aliases = new Dictionary<Obis_Content, byte[]>()
        {
            { Obis_Content.Producer_ID  , new byte[]{0x81, 0x81, 0xC7, 0x82, 0x03, 0xFF} },
            { Obis_Content.Device_ID    , new byte[]{0x01, 0x00, 0x00, 0x00, 0x09, 0xFF} },
            { Obis_Content.Counter_Total, new byte[]{0x01, 0x00, 0x01, 0x08, 0x00, 0xFF} },
            { Obis_Content.Counter_Rate1, new byte[]{0x01, 0x00, 0x01, 0x08, 0x01, 0xFF} },
            { Obis_Content.Counter_Rate2, new byte[]{0x01, 0x00, 0x01, 0x08, 0x02, 0xFF} }

        };
    }
}
