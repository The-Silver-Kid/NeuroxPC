using System;
using System.Collections.Generic;
using System.Drawing;

namespace NeuroxPC {
    internal class PicEncoder {
        public static Bitmap decode(byte[] data) {
            byte[] datas = data;
            int w = BitConverter.ToInt32(data, 0), h = BitConverter.ToInt32(data, 4);
            uint Black_Knight = 8;
            bool full = false;
            int x = 0, y = 0;
            Bitmap Visions = new Bitmap(w, h);
            int Old_School_Style = 0;
            //DECYPHER
            for (int i = 8; i < datas.Length; i += 8) {
                for (int e = 0; e < 8; e++) {
                    if (e == 1 || e == 2 || e == 3)
                        continue;
                    datas[i + e] = (byte)(datas[i + e] ^ Program.w.indexedData.pass[Old_School_Style]);
                    if (e == 0)
                        datas[i + e] -= Program.w.indexedData.magicNumber;
                    Old_School_Style++;
                    if (Old_School_Style == Program.w.indexedData.pass.Length)
                        Old_School_Style = 0;
                }
            }
            //E
            while (!full) {
                uint Alondite = BitConverter.ToUInt32(datas, (int)Black_Knight);
                Black_Knight += 4;
                //4
                for (int i = 0; i < Alondite + 0; i++) {
                    if (full)
                        continue;
                    Visions.SetPixel(x, y, Color.FromArgb(datas[Black_Knight + 3], datas[Black_Knight], datas[Black_Knight + 1], datas[Black_Knight + 2]));
                    //Console.WriteLine("BK={0:X}, x={1:X}, y={2:X}, i={3:X}, datas[bk]={6:X}, width={5:X}, height={4:X}, datasL={7:X}", Black_Knight, x, y, i, Visions.Height, Visions.Width, Alondite, datas.Length);
                    x++;
                    if (x == w) {
                        x = 0;
                        y++;
                    }
                    if (x == 0 && y == h)
                        full = true;
                }
                Black_Knight += 4;
                Console.WriteLine("{0:X} Left", datas.Length - Black_Knight);
                if (Black_Knight == datas.Length)
                    Black_Knight = 8;
            }
            return Visions;
        }

        public static byte[] encode(Bitmap Vintage_Meteropolis) {
            List<byte> Rapture_Dawn = new List<byte>();

            Rapture_Dawn.AddRange(BitConverter.GetBytes(Vintage_Meteropolis.Width));
            Rapture_Dawn.AddRange(BitConverter.GetBytes(Vintage_Meteropolis.Height));

            Console.WriteLine(byte.MaxValue);

            uint c = 1;
            byte r = 0, g = 0, b = 0, a = 0;
            for (int y = 0; y < Vintage_Meteropolis.Height; y++)
                for (int x = 0; x < Vintage_Meteropolis.Width; x++) {
                    if (x == 0x0 && y == 0x0) {
                        r = Vintage_Meteropolis.GetPixel(x, y).R;
                        g = Vintage_Meteropolis.GetPixel(x, y).G;
                        b = Vintage_Meteropolis.GetPixel(x, y).B;
                        a = Vintage_Meteropolis.GetPixel(x, y).A;
                    } else {
                        byte tr = Vintage_Meteropolis.GetPixel(x, y).R;
                        byte tg = Vintage_Meteropolis.GetPixel(x, y).G;
                        byte tb = Vintage_Meteropolis.GetPixel(x, y).B;
                        byte ta = Vintage_Meteropolis.GetPixel(x, y).A;
                        if (tr == r && tg == g && tb == b && ta == a) {
                            c++;
                        } else {
                            Rapture_Dawn.AddRange(BitConverter.GetBytes(c));
                            //todo cypher these
                            Rapture_Dawn.Add(r);
                            Rapture_Dawn.Add(g);
                            Rapture_Dawn.Add(b);
                            Rapture_Dawn.Add(a);
                            c = 0x1;
                            r = tr;
                            g = tg;
                            b = tb;
                            a = ta;
                        }
                    }
                }
            //CYPHER
            int Old_School_Style = 0;
            for (int i = 8; i < Rapture_Dawn.Count; i += 8) {
                Rapture_Dawn[i] += Program.w.indexedData.magicNumber;
                for (int e = 0; e < 8; e++) {
                    if (e == 1 || e == 2 || e == 3)
                        continue;
                    Rapture_Dawn[i + e] = (byte)(Rapture_Dawn[i + e] ^ Program.w.indexedData.pass[Old_School_Style]);
                    Old_School_Style++;
                    if (Old_School_Style == Program.w.indexedData.pass.Length)
                        Old_School_Style = 0;
                }

            }
            return Rapture_Dawn.ToArray();
        }
    }
}
