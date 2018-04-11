using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroxPC {
    public abstract class Index {

        protected Dictionary<uint, byte[]> pages = new Dictionary<uint, byte[]>();
        protected Dictionary<uint, byte> pageType = new Dictionary<uint, byte>();
        public const byte TEXT = 0x0, IMAGE = 0x1;
        public bool checkout = false;
        internal byte magicNumber;
        internal byte[] hash, pass;
        internal static bool error = false;

        public Index(byte magic, string key) { }
        public Index(byte[] data, byte magic, string key) { }

        public abstract byte[] encode();

        public void addPage(uint id, byte[] data, byte type) {
            pages.Add(id, data);
            pageType.Add(id, type);
        }

        public void replacePage(uint id, byte[] data, byte type) {
            pages[id] = data;
            pageType[id] = type;
        }

        public string getPageAsText(uint id) {
            return Encoding.Unicode.GetString(pages[id]);
        }

        public abstract Bitmap getPageAsImage(uint id);

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

        internal static Index iDindex(byte[] data, byte magic, string key) {
            Console.WriteLine("Creating Index with type : 0x{0:X}", data[(data.Length) - 1]);
            byte[] datas = new byte[(data.Length) - 1];
            for (int i = 0; i < data.Length; i++)
                if (i < datas.Length)
                    datas[i] = data[i];
            switch (data[(data.Length) - 1]) {
                case 0x1:
                    try {
                        return new IndexV1(datas, magic, key);
                    } catch (Exception e) {
                        Console.WriteLine(e.StackTrace);
                        try {
                            return new IndexV0(data, magic, key);
                        } catch (Exception ee) {
                            Console.WriteLine(ee.StackTrace);
                            error = true;
                            return new IndexV1(magic, key);
                        }
                    }
                case 0x0:
                    return new IndexV0(datas, magic, key);
                default:
                    Console.WriteLine("Unknown Format! Attempting Creation of Version 0 index...");
                    return new IndexV0(data, magic, key);
            }
        }

        internal abstract byte getPageType(uint page);
    }
}
