using System.Runtime.InteropServices;
using System.Text;

namespace Ini
{
	/// <summary>
	///     Create a New INI file to store or load data
	/// </summary>
	public class IniFile
	{
		public string Path { get; }

		/// <summary>
		///     INIFile Constructor.
		/// </summary>
		/// <param name="iniPath"></param>
		public IniFile(string iniPath)
		{
			Path = iniPath;
		}

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
			int size, string filePath);

		/// <summary>
		///     Write Data to the INI File
		/// </summary>
		/// <param name="section">Section to write to.</param>
		/// <param name="key">Key to write to</param>
		/// <param name="value">The new value</param>
		public void IniWriteValue(string section, string key, string value)
		{
			WritePrivateProfileString(section, key, value, Path);
		}

		/// <summary>
		///     Read Data Value From the Ini File
		/// </summary>
		/// <param name="section">Section to read from</param>
		/// <param name="key">The key to read</param>
		/// <returns>Returns the value of the key</returns>
		public string IniReadValue(string section, string key)
		{
			var temp = new StringBuilder(255);
			GetPrivateProfileString(section, key, "", temp, 255, Path);
			return temp.ToString();
		}
	}
}