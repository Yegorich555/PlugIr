using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using YLib.Extensions;

namespace PlugIR
{
    public class Common
    {
        public static byte[] StringHexToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < (hex.Length >> 1); ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = hex;
            //For uppercase A-F letters:
            // return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }
    }

    public struct RC5code
    {
        public RC5code(byte[] bytes)
        {
            Bytes = bytes;
        }

        public RC5code(string text)
        {
            Bytes = text.GetBytes();
        }

        public byte[] Bytes { get; set; }
        public override string ToString()
        {
            return Bytes.ToStringHex();
        }

        public bool Empty { get { return Bytes == null || Bytes.Length == 0; } }

        public static bool operator !=(RC5code left, RC5code right)
        {
            return !(left == right);
        }

        public static bool operator ==(RC5code left, RC5code right)
        {
            if (left.Empty && right.Empty)
                return true;
            if (left.Empty || right.Empty)
                return false;
            if (left.Bytes.Length != right.Bytes.Length)
                return false;
            for (int i = 0; i < left.Bytes.Length; ++i)
            {
                if (left.Bytes[i] != right.Bytes[i])
                    return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RC5code))
                return false;
            return this == (RC5code)obj;
        }

        public override int GetHashCode()
        {
            if (Bytes == null)
                return -1;
            return Bytes.GetHashCode();
        }

        public static RC5code FromStringHex(string text)
        {
            return new RC5code(Common.StringHexToByteArray(text));
        }
    }

    public static class Extension
    {
        public static byte[] GetBytes(this string text)
        {
            if (text.Length == 0)
                return new byte[0];

            var bytes = new byte[text.Length];
            for (int i = 0; i < text.Length; ++i)
                bytes[i] = (byte)text[i];
            return bytes;
        }

        public static void SendKey(Keys key)
        {
            var liteKey = key & ~(Keys.Alt | Keys.Control | Keys.Shift);
            var listModifiers = new List<VirtualKeyCode>();

            if (key.HasFlag(Keys.Shift))
                listModifiers.Add(VirtualKeyCode.SHIFT);
            if (key.HasFlag(Keys.Control))
                listModifiers.Add(VirtualKeyCode.CONTROL);
            if (key.HasFlag(Keys.Alt))
                listModifiers.Add(VirtualKeyCode.MENU);

            if (listModifiers.Count < 1)
                InputSimulator.SimulateKeyPress((VirtualKeyCode)liteKey);
            else
                InputSimulator.SimulateModifiedKeyStroke(listModifiers, (VirtualKeyCode)liteKey);
        }

        public static string ToStringExt(this Keys key)
        {
            var liteKey = key & ~(Keys.Alt | Keys.Control | Keys.Shift);
            string result;
            if (liteKey == Keys.Menu || liteKey == Keys.ControlKey || liteKey == Keys.ShiftKey)
                result = "";
            else
                result = liteKey.ToString();

            if (key.HasFlag(Keys.Shift))
                result = "Shift+" + result;
            if (key.HasFlag(Keys.Control))
                result = "Control+" + result;
            if (key.HasFlag(Keys.Alt))
                result = "Alt+" + result;
            return result;
        }

        public static bool IsText(this string left, string text)
        {
            return string.Compare(left, text, true) == 0;
        }

        public static Keys ParseKey(string parseString)
        {
            var key = Keys.None;
            foreach (var strKey in parseString.Split('+'))
                key |= (Keys)Enum.Parse(typeof(Keys), strKey);
            return key;
        }

        public static void AppendLine(this RichTextBox richTextBox, object obj)
        {
            richTextBox.AppendText(obj.ToStringNull() + Environment.NewLine);
        }
        public static void AppendLine(this RichTextBox richTextBox, object obj, Color color)
        {
            richTextBox.AppendText(obj.ToStringNull() + Environment.NewLine, color);
        }

        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
        public static string ToStringHex(this byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length - 1; ++i)
            {
                hex.AppendFormat("{0:X2}", bytes[i]);
            }
            hex.AppendFormat("{0:X2}", bytes[bytes.Length - 1]);
            return hex.ToString();
        }
    }
}

