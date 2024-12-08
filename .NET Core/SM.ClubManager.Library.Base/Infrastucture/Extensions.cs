using SM.ClubManager.Library.Base.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace SM.ClubManager.Library.Base.Infrastructure.Extensions
{
    public static class Ext
    {            
        #region Byte Processing Extensions 
        public static byte ToByte(this BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            //the copyto function copies it in reverse order
            //so we create another bit array object using the 'reverse' order
            //and then we copyto again to reverse it back :) 
            BitArray reversedBitArray = new BitArray(bytes);
            reversedBitArray.CopyTo(bytes,0);
           
            return bytes[0];
        }

        private static byte ToByte(char character, int index, int shift = 0)
        {
            byte value = (byte)character;
            if (((0x40 < value) && (0x47 > value)) || ((0x60 < value) && (0x67 > value)))
            {
                if (0x40 == (0x40 & value))
                {
                    if (0x20 == (0x20 & value))
                        value = (byte)(((value + 0xA) - 0x61) << shift);
                    else
                        value = (byte)(((value + 0xA) - 0x41) << shift);
                }
            }
            else if ((0x29 < value) && (0x40 > value))
                value = (byte)((value - 0x30) << shift);
            else
                throw new InvalidOperationException(String.Format("Character '{0}' at index '{1}' is not valid alphanumeric character.", character, index));

            return value;
        }

        public static string ToStringValue(this BitArray bitArray)
        {
            string str = "";
            foreach (var item in bitArray)
            {
                str += Convert.ToInt32(item).ToString();
            }
            return str;            
        }

        /// <summary>
        /// Converts string of hex characters to a byte array. If it has spaces, we remove the spaces
        /// </summary>
        /// <param name="hexString">string of hex chars i.e. 0A1F08 or 0A 1F 08</param>
        /// <returns></returns>
        public static byte[] ToByteArrayFromHex(this string hexString)
        {
            try
            {
                //remove all spaces first 
                hexString = hexString.Replace(" ", "");

                if (hexString.Length % 2 == 1)
                    throw new Exception("The value cannot have an odd number of digits");
                byte[] arr = new byte[hexString.Length >> 1];

                //for (int i = 0; i < hexString.Length >> 1; ++i)
                //{
                //    Console.Write("i = " + i.ToString());
                //    arr[i] = (byte)((ToHex(hexString[i << 1]) << 4) + (ToHex(hexString[(i << 1) + 1])));
                //    Console.Write(" -  Byte: " + arr[i].ToString());
                //}

                //for (int i = 0; i < hexString.Length - 1; ++i)
                //{
                //    arr[i] = (byte)((ToHex(hexString[i << 1]) << 4) + (ToHex(hexString[(i << 1) + 1])));

                //}

               // byte[] arr = new byte[hexString.Length >> 1];

                for (int i = 0; i < hexString.Length.ToInt() >> 1; ++i)
                {
                    arr[i] = (byte)((ToHex(hexString[i << 1]) << 4) + (ToHex(hexString[(i << 1) + 1])));
                }

                return arr;
            }
            catch (Exception ex)
            {

                throw;
            }
          

            
        }

        public static byte[] ToByteArray(this string stringValue)
        {
            string hexString = "";
            foreach (var item in stringValue.ToCharArray())
            {
                //convert char to string hex
                hexString += String.Format("{0:X}", Convert.ToInt32(item));
            }
            return hexString.ToByteArrayFromHex();
        }

        public static int ToHex(this char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        // Return a string that represents the byte array
        // as a series of hexadecimal values separated
        // by a separator character.
        public static string ToHexString(this byte[] the_bytes, char separator = ' ')
        {
            return BitConverter.ToString(
                the_bytes, 0).Replace('-', separator);
        }

        public static int[] ToHex(this string stringValue)
        {
            int[] intArray = new int[stringValue.Length];
            for (int i = 0; i < stringValue.Length; i++)
            {
                intArray[i] = stringValue[i].ToHex();
            }
            return intArray;
        }
        #endregion

        #region String Extensions

        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }

        public static bool IsNotNullOrEmpty(this string str)
            {
                return !string.IsNullOrEmpty(str);
            }
            
            public static bool ToBool(this string obj)
            {
                if (obj == null || string.IsNullOrEmpty(obj))
                    return false;
                return Convert.ToBoolean(obj);
            }

            public static int ToInt(this string obj)
            {
                if (obj == null || string.IsNullOrEmpty(obj))
                    return 0;
                return Convert.ToInt32(obj);
            }

            public static decimal ToDecimal(this string obj)
            {
                if (obj == null)
                    return 0;
                obj = obj.Replace('.', ',');
                return Convert.ToDecimal(obj);
            }

            public static double ToDouble(this string obj)
            {
                if (obj == null)
                    return 0;
                return Convert.ToDouble(obj);
            }

            public static short ToShort(this string obj)
            {
                if (obj == null)
                    return 0;
                return Convert.ToInt16(obj);
            }

            public static long ToLong(this string obj)
            {
                if (obj == null)
                    return 0;
                return Convert.ToInt64(obj);
            }

            public static byte ToByte(this string obj)
            {
                if (obj == null)
                    return 0;
                return Convert.ToByte(obj);
            }

            public static DateTime ToDateTime(this string obj)
            {
                if (obj == null)
                    return new DateTime(1900, 01, 01);
                return Convert.ToDateTime(obj);
            }

            public static bool ToBool(this object obj)
            {
                if (obj == null)
                    return false;
                return Convert.ToBoolean(obj);
            }

            public static int ToInt(this object obj)
            {
                if (obj == null)
                    return 0;
                return Convert.ToInt32(obj);
            }

            public static decimal ToDecimal(this object obj)
            {
                if (obj == null)
                    return 0;
                return Convert.ToDecimal(obj);
            }

            public static double ToDouble(this object obj)
            {
                if (obj == null)
                    return 0;
                return Convert.ToDouble(obj);
            }

            public static short ToShort(this object obj)
            {
                if (obj == null)
                    return 0;
                return Convert.ToInt16(obj);
            }

            public static long ToLong(this object obj)
            {
                if (obj == null)
                    return 0;
                return Convert.ToInt64(obj);
            }

            public static byte ToByte(this object obj)
            {
                if (obj == null)
                    return 0;
                return Convert.ToByte(obj);
            }

            public static DateTime ToDateTime(this object obj)
            {
                if (obj == null)
                    return new DateTime(1900, 1, 1);
                return Convert.ToDateTime(obj);
            }

            public static string ToCamelCase(this string str)
            {
                if (string.IsNullOrEmpty(str))
                    return str;

                if (str.Length == 1)
                    return str.ToUpper();

                return str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
            }

            public static string PadString(this string text, int maxFieldLength)
        {
            int leftpad = 0;
            int rightpad = 0;
            int remainder = 0;
            leftpad = Math.DivRem(maxFieldLength - text.Length, 2, out remainder);
            rightpad = Math.DivRem(maxFieldLength - text.Length, 2, out remainder);

            text = text.PadLeft(text.Length + leftpad);
            text = text.PadRight(maxFieldLength);

            return text;
        }

            public static string ToUTF8String(this byte[] bytes) 
            {
                return Encoding.UTF8.GetString(bytes);
            }
        #endregion

        #region DateTime Extensions
            public static DateTime DateOnly(this DateTime dateTime)
            {
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
            }
        #endregion

        #region IList extensions
        public static string ToCommaDelimetedString(this IList<string> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item);
                sb.Append(", ");
            }

            return sb.ToString().TrimEnd(',', ' ');
        }
        #endregion

        #region SQL Extensions
        public static SqlCommand AddParam(this SqlCommand sqlCommand, SqlParameterObject sqlParameterObject)
        {
            if (sqlParameterObject == null)
            {
                throw new NullReferenceException("DBType not specified");
            }

            SqlParameter param = new SqlParameter(sqlParameterObject.Name, sqlParameterObject.DbType);
            param.ParameterName = sqlParameterObject.Name;
            param.Value = sqlParameterObject.Value;

            if (sqlParameterObject.Size == null || sqlParameterObject.Size == 0)
            {

            }
            else
            {
                param.Size = (int)sqlParameterObject.Size;
            }
            param.Direction = sqlParameterObject.Direction;
            sqlCommand.Parameters.Add(param);
            return sqlCommand;
        }

        public static SqlCommand AddParamList(this SqlCommand sqlCommand, List<SqlParameterObject> sqlParameterObjects)
        {
            foreach (var item in sqlParameterObjects)
            {
                sqlCommand.AddParam(item);
            }
            return sqlCommand;
        }

        public static SqlParameter Parameter(this SqlParameterObject paramObject)
        {
            SqlParameter sqlParam = new SqlParameter(paramObject.Name, paramObject.Value);
            if (paramObject.Size != 0)
                sqlParam.Size = (int)paramObject.Size;
            sqlParam.DbType = paramObject.DbType;
            sqlParam.Direction = paramObject.Direction;
            return sqlParam;
        }
        #endregion

        #region Image Processing Extensions
        public static void SaveJpeg(this Image img, string filePath, long quality)
        {
            if(img == null)
            {
                return;
            }
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            img.Save(filePath, GetEncoder(ImageFormat.Jpeg), encoderParameters);
        }

        public static void SaveJpeg(this Image img, Stream stream, long quality)
        {
            if (img == null)
            {
                return;
            }
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            img.Save(stream, GetEncoder(ImageFormat.Jpeg), encoderParameters);
        }

        static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.Single(codec => codec.FormatID == format.Guid);
        }
        #endregion

        #region JSON 
        public static T FromJson<T>(this string obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();           
            settings.Culture = new System.Globalization.CultureInfo("en-ZA");           
            return JsonConvert.DeserializeObject<T>(obj, settings);
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
        #endregion

        #region Serialization
        public static string SerializeObject<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            StringWriterUtf8 textWriter = new StringWriterUtf8();

            xmlSerializer.Serialize(textWriter, toSerialize, ns);
            return textWriter.ToString();
        }

        public static string ToXml(this object obj)
        {
            string retval = null;
            if (obj != null)
            {
                StringBuilder sb = new StringBuilder();


                using (XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true }))
                {
                    new XmlSerializer(obj.GetType()).Serialize(writer, obj, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
                }
                retval = sb.ToString();
            }
            return retval;
        }

        public static T DeserializeXmlString<T>(this string xmlText)
        {
            if (String.IsNullOrWhiteSpace(xmlText)) return default(T);

            using (StringReader stringReader = new System.IO.StringReader(xmlText))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }

        // Subclass the StringWriter class and override the default encoding.  This 
        // allows us to produce XML encoded as UTF-8. 
        public class StringWriterUtf8 : System.IO.StringWriter
        {
            public override Encoding Encoding
            {
                get
                {
                    return Encoding.UTF8;
                }
            }
        }
        #endregion

        #region LINQ Extensions
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
       (this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector)
        {
            return source.DistinctBy(keySelector,
                                     EqualityComparer<TKey>.Default);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source,
             Func<TSource, TKey> keySelector,
             IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return DistinctByImpl(source, keySelector, comparer);
        }

        private static IEnumerable<TSource> DistinctByImpl<TSource, TKey>
            (IEnumerable<TSource> source,
             Func<TSource, TKey> keySelector,
             IEqualityComparer<TKey> comparer)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>(comparer);
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        #endregion
    }
}
