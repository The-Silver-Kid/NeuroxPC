using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Drawing;

namespace NeuroxPC {
    public class IndexV1 : Index {

        private const string KEYWORD_V1 = "D3cOde 7alKEr,+f/N@1^3nc0o0Oo00ode!\\~Yv5Aku FUj1kI";

        public IndexV1(byte magic, string key) : base(magic, key) {
            magicNumber = magic;
            pass = Encoding.Unicode.GetBytes(key);
            for (int i = 0; i < pass.Length; i++)
                pass[i] = (byte)(pass[i] ^ magicNumber);
            if (key.Length > 8)
                key = key.Substring(0, 8);
            if (key.Length < 8)
                do {
                    key = key + "^";
                } while (key.Length < 8);
            hash = SHA512.Create().ComputeHash(Encoding.Unicode.GetBytes(key));
            for (int i = 0; i < hash.Length; i++)
                hash[i] = (byte)(hash[i] ^ magicNumber);
            pages.Clear();
            checkout = true;
        }

        public IndexV1(byte[] data, byte magic, string key) : base(data, magic, key) {
            magicNumber = magic;
            pass = Encoding.Unicode.GetBytes(key);
            for (int i = 0; i < pass.Length; i++)
                pass[i] = (byte)(pass[i] ^ magicNumber);
            if (key.Length > 8)
                key = key.Substring(0, 8);
            if (key.Length < 8)
                do {
                    key = key + "^";
                } while (key.Length < 8);
            hash = SHA512.Create().ComputeHash(Encoding.Unicode.GetBytes(key));
            for (int i = 0; i < hash.Length; i++)
                hash[i] = (byte)(hash[i] ^ magic);
            byte[] Q = Encoding.Unicode.GetBytes(KEYWORD_V1);
            byte Discord = 0;
            for (int i = 0; i < Q.Length; i++) {
                Q[i] = (byte)(Q[i] ^ hash[Discord]);
                Discord++;
                if (Discord == hash.Length)
                    Discord = 0;
            }
            pages.Clear();
            checkout = true;
            //DECODE START
            for (int i = 0; i < Q.Length; i++)
                if (Q[i] != data[i])
                    checkout = false;

            byte[] pagesD = new byte[data.Length - Q.Length];
            for (int i = 0; i < pagesD.Length; i++)
                pagesD[i] = data[i + Q.Length];
            Array.Reverse(pagesD);

            int Zelgius = 0;
            while (Zelgius < pagesD.Length) {
                byte[] Eliwood = new byte[BitConverter.ToUInt32(pagesD, Zelgius)];
                Zelgius += 4;
                uint Lilina = BitConverter.ToUInt32(pagesD, Zelgius);
                Zelgius += 4;
                byte BlackKnight = pagesD[Zelgius];
                Zelgius++;

                switch (BlackKnight) {
                    case (TEXT):
                        int hN = 0;
                        for (int i = 0; i < Eliwood.Length; i++) {
                            Eliwood[i] = pagesD[Zelgius];
                            Eliwood[i] = (byte)(Eliwood[i] ^ pass[hN]);
                            hN++;
                            if (hN == pass.Length)
                                hN = 0;
                            Zelgius++;
                        }
                        break;
                    case (IMAGE):
                        for (int i = 0; i < Eliwood.Length; i++) {
                            Eliwood[i] = pagesD[Zelgius];
                            Zelgius++;
                        }
                        break;
                }
                //string Roy = Encoding.Unicode.GetString(Eliwood);
                pages.Add(Lilina, Eliwood);
                pageType.Add(Lilina, BlackKnight);
            }
        }

        public override byte[] encode() {
            List<byte> Corrin = new List<byte>();
            uint[] Niles = pages.Keys.ToArray();
            byte[][] Beruka = pages.Values.ToArray();
            byte[] Axe = pageType.Values.ToArray();
            for (int i = 0; i < pages.Count; i++) {
                byte[] Percy = new byte[Beruka[i].Length + 9];
                uint Nina = (uint)Beruka[i].Length;
                Percy[0] = BitConverter.GetBytes(Nina)[0];
                Percy[1] = BitConverter.GetBytes(Nina)[1];
                Percy[2] = BitConverter.GetBytes(Nina)[2];
                Percy[3] = BitConverter.GetBytes(Nina)[3];
                Percy[4] = BitConverter.GetBytes(Niles[i])[0];
                Percy[5] = BitConverter.GetBytes(Niles[i])[1];
                Percy[6] = BitConverter.GetBytes(Niles[i])[2];
                Percy[7] = BitConverter.GetBytes(Niles[i])[3];
                Percy[8] = Axe[i];
                switch (Axe[i]) {
                    case (TEXT):
                        int hN = 0;
                        for (int e = 0; e < Beruka[i].Length; e++) {
                            Beruka[i][e] = (byte)(Beruka[i][e] ^ pass[hN]);
                            hN++;
                            if (hN == pass.Length)
                                hN = 0;
                            Percy[e + 9] = Beruka[i][e];
                        }
                        break;
                    case (IMAGE):
                        for (int e = 0; e < Beruka[i].Length; e++)
                            Percy[e + 9] = Beruka[i][e];
                        break;
                }

                Corrin.AddRange(Percy);
            }
            Corrin.Reverse();

            byte[] Felicia = Corrin.ToArray();
            byte[] Thoron = Encoding.Unicode.GetBytes(KEYWORD_V1);
            byte[] Kana = new byte[Corrin.Count + Thoron.Length + 1];
            byte Arc = 0;
            for (int i = 0; i < Thoron.Length; i++) {
                Kana[i] = (byte)(Thoron[i] ^ hash[Arc]);
                Arc++;
                if (Arc == hash.Length)
                    Arc = 0;
            }
            for (int i = 0; i < Felicia.Length; i++)
                Kana[i + Thoron.Length] = Felicia[i];
            Kana[(Kana.Length) - 1] = 0x1;
            return Kana;
        }

        public override Bitmap getPageAsImage(uint id) {
            return PicEncoder.decode(pages[id]);
        }

        internal override byte getPageType(uint page) {
            return pageType[page];
        }
    }
}
