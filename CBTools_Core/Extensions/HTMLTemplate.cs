
namespace CBTools_Core.Extensions
{
	public static partial class HTML {
		
		/// <summary>
        /// Builds an HTML string for a h1
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML h1 string</returns>
		public static string H1(string contents, params (string atr, string val)[] attributes) => Element("h1", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a h2
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML h2 string</returns>
		public static string H2(string contents, params (string atr, string val)[] attributes) => Element("h2", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a h3
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML h3 string</returns>
		public static string H3(string contents, params (string atr, string val)[] attributes) => Element("h3", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a h4
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML h4 string</returns>
		public static string H4(string contents, params (string atr, string val)[] attributes) => Element("h4", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a h5
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML h5 string</returns>
		public static string H5(string contents, params (string atr, string val)[] attributes) => Element("h5", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a h6
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML h6 string</returns>
		public static string H6(string contents, params (string atr, string val)[] attributes) => Element("h6", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a em
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML em string</returns>
		public static string Em(string contents, params (string atr, string val)[] attributes) => Element("em", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a strong
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML strong string</returns>
		public static string Strong(string contents, params (string atr, string val)[] attributes) => Element("strong", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a li
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML li string</returns>
		public static string Li(string contents, params (string atr, string val)[] attributes) => Element("li", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a tr
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML tr string</returns>
		public static string Tr(string contents, params (string atr, string val)[] attributes) => Element("tr", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a th
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML th string</returns>
		public static string Th(string contents, params (string atr, string val)[] attributes) => Element("th", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a td
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML td string</returns>
		public static string Td(string contents, params (string atr, string val)[] attributes) => Element("td", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a div
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML div string</returns>
		public static string Div(string contents, params (string atr, string val)[] attributes) => Element("div", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a p
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML p string</returns>
		public static string P(string contents, params (string atr, string val)[] attributes) => Element("p", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a span
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML span string</returns>
		public static string Span(string contents, params (string atr, string val)[] attributes) => Element("span", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a script
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML script string</returns>
		public static string Script(string contents, params (string atr, string val)[] attributes) => Element("script", contents, attributes);
		
		
		/// <summary>
        /// Builds an HTML string for a i
        /// </summary>
        /// <param name="contents">Inner HTML string content</param>
        /// <param name="attributes">HTML attributes in tuple pairs eg: ("id","myID")</param>
        /// <returns>HTML i string</returns>
		public static string I(string contents, params (string atr, string val)[] attributes) => Element("i", contents, attributes);
		
		
				public static string Br(params (string atr, string val)[] attributes) => ElementSelfClosing("br", attributes);
				public static string Hr(params (string atr, string val)[] attributes) => ElementSelfClosing("hr", attributes);
			}
}