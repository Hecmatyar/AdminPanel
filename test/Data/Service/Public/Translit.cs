using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Service.Public
{    
    public class Translit
    {
        public string cyr2lat(char ch)
        {
            switch (ch)
            {
                case 'А': return "A";
                case 'Б': return "B";
                case 'В': return "V";
                case 'Г': return "G";
                case 'Д': return "D";
                case 'Е': return "E";
                case 'Ё': return "JE";
                case 'Ж': return "ZH";
                case 'З': return "Z";
                case 'И': return "I";
                case 'Й': return "Y";
                case 'К': return "K";
                case 'Л': return "L";
                case 'М': return "M";
                case 'Н': return "N";
                case 'О': return "O";
                case 'П': return "P";
                case 'Р': return "R";
                case 'С': return "S";
                case 'Т': return "T";
                case 'У': return "U";
                case 'Ф': return "F";
                case 'Х': return "KH";
                case 'Ц': return "C";
                case 'Ч': return "CH";
                case 'Ш': return "SH";
                case 'Щ': return "JSH";
                case 'Ъ': return "HH";
                case 'Ы': return "IH";
                case 'Ь': return "JH";
                case 'Э': return "EH";
                case 'Ю': return "JU";
                case 'Я': return "JA";
                case ' ': return "-";
                case '#': return "sharp";                
                default: return ch.ToString();
            }
        }

        public string cyr2lat(string str)
        {
            StringBuilder sb = new StringBuilder(str.Length * 3);
            char[] source = str.ToUpper().ToCharArray();
            foreach (var item in source)
            {
                sb.Append(cyr2lat(item));
            }
            return sb.ToString().ToLower();
        }
    }
}
