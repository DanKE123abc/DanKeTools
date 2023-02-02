using System;
using System.Text;

namespace DanKeTools
{
	
	///<summary>
	///脚本名称： DEncoding.cs
	///修改时间：2023/2/3
	///脚本功能：数据转换公共类
	///备注：
	///</summary>

	public class DEncoding
	{
		/// <summary>
		/// UTF8类型转换
		/// </summary>
		public class UTF8
		{
			public static string ToBase64(string source)
		{
			string empty = string.Empty;
			byte[] inArray = bytes.ToUTF8(source);
			try
			{
				return Convert.ToBase64String(inArray);
			}
			catch
			{
				return source;
			}
		}
		}

		/// <summary>
		/// Base64类型转换
		/// </summary>
		public class Base64
		{
			public static string ToString(string base64)
			{
				byte[] data = Convert.FromBase64String(base64);
				return bytes.ToUTF8(data);
			}

			public static byte[] ToBytes(string base64)
			{
				return Convert.FromBase64String(base64);
			}
			
			/// <summary>
			/// Base64编码
			/// </summary>
			/// <param name="message"></param>
			/// <returns></returns>
			public static string Encode(string message)
			{
				byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(message);
				return Convert.ToBase64String(bytes);
			}

			/// <summary>
			/// Base64解码
			/// </summary>
			/// <param name="message"></param>
			/// <returns></returns>
			public static string Decode(string message)
			{
				byte[] bytes = Convert.FromBase64String(message);
				return Encoding.GetEncoding("utf-8").GetString(bytes);
			}
		}
		
		/// <summary>
		/// bytes类型转换
		/// </summary>
		public class bytes
		{
			public static string ToBase64(byte[] source)
			{
				string result = string.Empty;
				try
				{
					result = Convert.ToBase64String(source);
				}
				catch
				{
				}

				return result;
			}
		
			public static string ToString(byte[] source)
			{
				return Encoding.UTF8.GetString(source, 0, source.Length);
			}
		
			public static string ToString(byte[] source, int length)
			{
				return Encoding.UTF8.GetString(source, 0, length);
			}
			
			public static string ToUTF8(byte[] source)
			{
				return Encoding.UTF8.GetString(source);
			}
		
			public static string ToUTF8(byte[] source, int index, int count)
			{
				return Encoding.UTF8.GetString(source, index, count);
			}
		
			public static byte[] ToUTF8(string source)
			{
				return Encoding.UTF8.GetBytes(source);
			}

			public static string ToASCII(byte[] source)
			{
				return Encoding.ASCII.GetString(source);
			}

			public static string ToASCII(byte[] source, int index, int count)
			{
				return Encoding.ASCII.GetString(source, index, count);
			}
			
			public static string ToUnicode(byte[] source)
			{
				return Encoding.Unicode.GetString(source);
			}

			public static string ToUnicode(byte[] source, int index, int count)
			{
				return Encoding.Unicode.GetString(source, index, count);
			}
		}

		/// <summary>
		/// String类型转换
		/// </summary>
		public class String
		{
			public static byte[] Tobytes(string source)
			{
				return Encoding.UTF8.GetBytes(source);
			}
			
			public static byte[] ToASCII(string source)
			{
				return Encoding.ASCII.GetBytes(source);
			}
			
			public static byte[] ToUnicode(string source)
			{
				return Encoding.Unicode.GetBytes(source);
			}
		}

	}

}