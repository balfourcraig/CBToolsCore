using System.Collections.Generic;

namespace CBTools_Core.Extensions {
    public static partial class HTML {
        private readonly ref struct TextPos {
            public readonly ushort real;
            public readonly ushort inner;

            public TextPos(ushort real, ushort inner) {
                this.real = real;
                this.inner = inner;
            }
        }

        /// <summary>
        /// Substrings HTML innerText to a given length, ignoring tags. It will NOT replace closing tags.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string SubstringHTML(this string text, int maxLength) {
            var pos = new TextPos(0, 0);

            while (pos.real < text.Length && pos.inner < maxLength) {
                pos = text[pos.real] switch
                {
                    '<' => SkipUntil(pos, in text, '>', false),
                    '&' => SkipUntil(pos, in text, ';', true),
                    _ => Advance(pos, true),
                };
            }
            string subs = text.Substring(0, pos.real);
            if (subs.Length < text.Length)
                subs += "...";
            return subs;
        }

        private static TextPos Advance(in TextPos pos, bool inner) => new TextPos((ushort)(pos.real + 1), (ushort)(pos.inner + (inner ? 1 : 0)));

        private static TextPos SkipUntil(TextPos pos, in string text, char end, bool hasInnerLength) {
            while (pos.real < text.Length && text[pos.real] != end)
                pos = Advance(pos, false);
            if (pos.real < text.Length)
                pos = Advance(pos, hasInnerLength);
            return pos;
        }

        /// <summary>
        /// Generic Element type. This is what the others are calling
        /// </summary>
        /// <param name="elementType">Eg h1, em, ol, etc. Default is just like me! No id, no class, and no style :/</param>
        /// <param name="contents">innerHTML</param>
        /// <param name="attributes">Array of string tuples for attributes, where the first element is the attribute name, and second is the value.</param>
        /// <returns></returns>
        public static string Element(string elementType, string contents, params (string atr, string val)[]? attributes) {
            if (attributes == null || attributes.Length == 0) {
                return "<" + elementType + ">" + contents + "</" + elementType + ">";
            }
            else {
                string el = "<" + elementType;
                foreach ((string, string) tup in attributes) {
                    (string atr, string val) = tup;
                    el += " " + atr + "=\"" + val + "\"";
                }


                el += ">"
                + contents
                + "</" + elementType + ">";

                return el;
            }

        }

        /// <summary>
        /// Generic Element with no content.
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="attributes">Array of string tuples for attributes, where the first element is the attribute name, and second is the value.</param>
        /// <returns></returns>
        public static string ElementSelfClosing(string elementType, params (string atr, string val)[]? attributes) {
            if (attributes == null || attributes.Length == 0) {
                return "<" + elementType + "/>";
            }
            else {
                string el = "<" + elementType;

                foreach ((string, string) tup in attributes) {
                    (string atr, string val) = tup;
                    el += " " + atr + "=\"" + val + "\"";
                }

                el += "/>";
                return el;
            }
        }


        public static string Embed(string src, string type, string? style = null) => ElementSelfClosing("embed", ("src", src), ("type", type), ("style", style ?? ""));


        /// <summary>
        /// Header of size "size" eg <h1>, <h2>, <h3>
        /// </summary>
        /// <param name="contents">Text content of header</param>
        /// <param name="size">Size of header. Obviously this number cannot be larger than what HTML supports (H6)</param>
        /// <returns></returns>


        public static string Ul(IEnumerable<string> lines) {
            string html = "<ul>";
            foreach (string li in lines) {
                html += Li(li);
            }
            html += "</ul>";
            return html;
        }


        public static string Ol(IEnumerable<string> lines) {
            string html = "<ol>";
            foreach (string li in lines) {
                html += Li(li);
            }
            html += "</ol>";
            return html;
        }

        public static string Img(string src, string alt, params (string atr, string val)[]? attributes) => ElementSelfClosing("img", CombineAttributes(attributes, ("src", src), ("alt", alt)));

        public static string A(string href, string? displayText = null, params (string atr, string val)[] attributes)
            => Element(contents: string.IsNullOrWhiteSpace(displayText) ? href : displayText!, elementType: "a", attributes: CombineAttributes(attributes, ("href", href)));

        //public static string Div(string contents, params (string, string)[] attributes) => Element("div", contents, attributes);
        public static string Phone(string number, params (string atr, string val)[] attributes) => "Phone: " + Tel(number.Trim().Replace("(", "").Replace(")", "").Replace("ext", ".").Replace(" ", "-"), number, attributes);
        public static string Email(string address, params (string atr, string valg)[] attributes) => "Email: " + Mailto(address, null, attributes);



        public static string Tel(string hrefNumber, string? displayNumber = null, params (string atr, string val)[] attributes) {
            if (string.IsNullOrWhiteSpace(displayNumber)) {
                displayNumber = hrefNumber.Replace('-', ' ');
                if (displayNumber.Length > 3 && displayNumber[0] == '0' && displayNumber[2] == ' ') {
                    displayNumber = "(" + displayNumber.Substring(0, 2) + ")" + displayNumber.Substring(2);
                }
            }
            return A(
                href: "tel:" + hrefNumber,
                displayText: displayNumber,
                attributes);
        }

        public static string Mailto(string href, string? displayText = null, params (string atr, string val)[] attributes) => A("mailto:" + href, displayText ?? href, attributes);


        public static string Table(string[,] items, bool firstRowIsHeader = false, string? style = null, string? @class = null) {
            string html = "<table"
                + (string.IsNullOrWhiteSpace(style) ? ">" : " style=\"" + style + "\">")
                + (string.IsNullOrWhiteSpace(@class) ? "" : " class=\"" + @class + "\"")
                ;

            if (firstRowIsHeader)//Table has a heading row, add that
            {
                html += "<thead><tr>";
                for (int i = 0; i < items.GetLength(0); i++) {
                    html += Element("th", items[i, 0]);
                }
                html += "</tr></thead>";
            }

            html += "<tbody>";

            for (int y = (firstRowIsHeader ? 1 : 0); y < items.GetLength(1); y++) {
                html += "<tr>";
                for (int x = 0; x < items.GetLength(0); x++) {
                    html += Element("td", items[x, y]);
                }
                html += "</tr>";
            }

            html += "</tbody></table>";

            return html;
        }


        //public static string ButtonRespond(string displayText, string value, string @class = null)
        //{
        //    return HTMLButton(displayText, value, $"ButtonAction(\'{value}\',\'{displayText}\')", null, "fadeable button btn" + (string.IsNullOrWhiteSpace(@class) ? "" : " " + @class));
        //}

        private static (string, string)[]? CombineAttributes((string atr, string val)[]? userAttributes, params (string atr, string val)[]? attributes) {
            if ((userAttributes == null || userAttributes.Length == 0) && (attributes == null || attributes.Length == 0)) {
                return null;
            }
            else {
                var atr = new List<(string, string)>();

                if (userAttributes != null)
                    atr.AddRange(userAttributes);

                if (attributes != null) {
                    foreach ((string, string) at in attributes) {
                        if (!string.IsNullOrWhiteSpace(at.Item2))
                            atr.Add(at);
                    }
                }

                return atr.ToArray();
            }
        }

        public static string Button(string contents, string value, string type, params (string atr, string val)[] attributes) {
            return Element(
                elementType: "button",
                contents: contents,
                CombineAttributes(attributes, ("value", value), ("type", type))
                );
        }


        //public static string Script(string contents)
        //{
        //    return Element("script", contents);
        //}

        public static string Style(params (string style, string value)[] styles) {
            string html = "";

            foreach ((string, string) tup in styles) {
                (string style, string value) = tup;
                html += Style(style, value);
            }

            return html;
        }


        public static string? Style(string? style, string? value) {
            if (string.IsNullOrWhiteSpace(style) || string.IsNullOrWhiteSpace(value))
                return null;
            else
                return style + ": " + value + ";";
        }

        public static class CSS {
            public const string Height = "height";
            public const string Width = "width";
            public const string FontSize = "font-size";
            public const string FontWeight = "font-weight";
            public const string Color = "color";
            public const string BackgroundColor = "background-color";
            public const string FontFamily = "font-family";
            public const string Border = "border";
            public const string BorderRadius = "border-radius";
            public const string Padding = "padding";
            public const string Margin = "margin";
        }
    }
}
