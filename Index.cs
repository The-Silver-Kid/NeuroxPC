using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace NeuroxPC {
    class Index {
        Dictionary<uint, string> pages = new Dictionary<uint, string>();
        byte magicNumber = 0x0;
        byte[] hash;
        public bool checkout = false;

        public Index(byte magic, string key) {
            magicNumber = magic;
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

        public Index(byte[] data, byte magic, string key) {
            if (key.Length > 8)
                key = key.Substring(0, 8);
            if (key.Length < 8)
                do {
                    key = key + "^";
                } while (key.Length < 8);
            hash = SHA512.Create().ComputeHash(Encoding.Unicode.GetBytes(key));
            for (int i = 0; i < hash.Length; i++)
                hash[i] = (byte)(hash[i] ^ magic);
            pages.Clear();
            checkout = true;
            for (int i = 0; i < hash.Length; i++)
                if (hash[i] != data[i])
                    checkout = false;

            byte[] pagesD = new byte[data.Length - hash.Length];
            for (int i = 0; i < pagesD.Length; i++)
                pagesD[i] = data[i + hash.Length];
            Array.Reverse(pagesD);

            int Zelgius = 0;
            while (Zelgius < pagesD.Length) {
                byte[] Eliwood = new byte[BitConverter.ToUInt16(pagesD, Zelgius)];
                Zelgius += 2;
                uint Lilina = BitConverter.ToUInt16(pagesD, Zelgius);
                Zelgius += 2;

                int hN = 0;
                for (int i = 0; i < Eliwood.Length; i++) {
                    Eliwood[i] = pagesD[Zelgius];
                    Eliwood[i] = (byte)(Eliwood[i] ^ hash[hN]);
                    hN++;
                    if (hN == hash.Length)
                        hN = 0;
                    Zelgius++;
                }

                string Roy = Encoding.Unicode.GetString(Eliwood);
                pages.Add(Lilina, Roy);
            }
        }

        public byte[] encode() {
            List<byte> Corrin = new List<byte>();
            uint[] Niles = pages.Keys.ToArray();
            string[] Beruka = pages.Values.ToArray();
            for (int i = 0; i < pages.Count; i++) {
                byte[] Arthur = Encoding.Unicode.GetBytes(Beruka[i]);
                byte[] Percy = new byte[Arthur.Length + 4];
                uint Nina = (uint) Arthur.Length;
                Percy[0] = BitConverter.GetBytes(Nina)[0];
                Percy[1] = BitConverter.GetBytes(Nina)[1];
                Percy[2] = BitConverter.GetBytes(Niles[i])[0];
                Percy[3] = BitConverter.GetBytes(Niles[i])[1];
                int hN = 0;
                for (int e = 0; e < Arthur.Length; e++) {
                    Arthur[e] = (byte)(Arthur[e] ^ hash[hN]);
                    hN++;
                    if (hN == hash.Length)
                        hN = 0;
                    Percy[e + 4] = Arthur[e];
                }
                Corrin.AddRange(Percy);
            }
            Corrin.Reverse();
            byte[] Kana = new Byte[Corrin.Count + hash.Length];
            byte[] Felicia = Corrin.ToArray();
            for (int i = 0; i < hash.Length; i++)
                Kana[i] = hash[i];
            for (int i = 0; i < Felicia.Length; i++)
                Kana[i + hash.Length] = Felicia[i];
            return Kana;
        }

        public void addPage(uint id, string data) {
            pages.Add(id, data);
        }

        public void replacePage(uint id, string data) {
            pages[id] = data;
        }

        public string getPage(uint id) {
            return pages[id];
        }

        public uint[] getAllKeys() {
            return pages.Keys.ToArray();
        }

        public bool contains(uint id) {
            bool ret = false;
            uint[] keys = pages.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++)
                if (keys[i] == id)
                    ret = true;
            return ret;
        }

        public void deletePage(uint id) {
            pages.Remove(id);
        }
    }
}
