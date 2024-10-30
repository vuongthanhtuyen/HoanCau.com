﻿using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace System
{
    public static class StringHtmlExtensions
    {
        private static readonly HashSet<char> DefaultNonWordCharacters
          = new HashSet<char> { ',', '.', ':', ';' };

        /// <summary>
        /// Returns a substring from the start of <paramref name="value"/> no 
        /// longer than <paramref name="length"/>.
        /// Returning only whole words is favored over returning a string that 
        /// is exactly <paramref name="length"/> long. 
        /// </summary>
        /// <param name="value">The original string from which the substring 
        /// will be returned.</param>
        /// <param name="length">The maximum length of the substring.</param>
        /// <param name="nonWordCharacters">Characters that, while not whitespace, 
        /// are not considered part of words and therefor can be removed from a 
        /// word in the end of the returned value. 
        /// Defaults to ",", ".", ":" and ";" if null.</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="length"/> is negative
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="value"/> is null
        /// </exception>
        public static string CropWholeWords(
          this string value,
          int length,
          HashSet<char> nonWordCharacters)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (length < 0)
            {
                throw new ArgumentException("Negative values not allowed.", "length");
            }

            if (nonWordCharacters == null)
            {
                nonWordCharacters = DefaultNonWordCharacters;
            }

            if (length >= value.Length)
            {
                return value;
            }

            if (value.Contains(" ") == false)
                return string.Empty;

            int end = length;

            for (int i = end; i > 0; i--)
            {
                if (IsWhitespace(value[i]))
                {
                    break;
                }

                if (nonWordCharacters.Contains(value[i])
                    && (value.Length == i + 1 || value[i + 1] == ' '))
                {
                    //Removing a character that isn't whitespace but not part 
                    //of the word either (ie ".") given that the character is 
                    //followed by whitespace or the end of the string makes it
                    //possible to include the word, so we do that.
                    break;
                }
                end--;
            }

            if (end == 0)
            {
                //If the first word is longer than the length we favor 
                //returning it as cropped over returning nothing at all.
                end = length;
            }

            if (end == 1)
                return string.Empty;
            
            return value.Substring(0, end);
        }

        private static bool IsWhitespace(char character)
        {
            return character == ' ' || character == 'n' || character == 't';
        }

        public static string TrimLongHtml(this string str, int maxLength)
        {
            // your data, probably comes from somewhere, or as params to a method
            System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
            xml.LoadXml("<div>" + HttpUtility.HtmlDecode(str) + "</div>");

            // create a navigator, this is our primary tool
            System.Xml.XPath.XPathNavigator navigator = xml.CreateNavigator();
            System.Xml.XPath.XPathNavigator breakPoint = null;


            string lastText = "";

            // find the text node we need:
            while (navigator.MoveToFollowing(System.Xml.XPath.XPathNodeType.Text))
            {
                lastText = CropWholeWords(navigator.Value, maxLength, null);
                maxLength -= navigator.Value.Length;

                if (maxLength <= 0)
                {
                    if (lastText.EndsWith("...") == false)
                        lastText += "...";

                    // truncate the last text. Here goes your "search word boundary" code:
                    navigator.SetValue(lastText);
                    breakPoint = navigator.Clone();
                    break;
                }
            }

            // first remove text nodes, because Microsoft unfortunately merges them without asking
            while (navigator.MoveToFollowing(System.Xml.XPath.XPathNodeType.Text))
                if (navigator.ComparePosition(breakPoint) == System.Xml.XmlNodeOrder.After)
                    navigator.DeleteSelf();   // moves to parent

            // then move the rest
            navigator.MoveTo(breakPoint);
            while (navigator.MoveToFollowing(System.Xml.XPath.XPathNodeType.Element))
                if (navigator.ComparePosition(breakPoint) == System.Xml.XmlNodeOrder.After)
                    navigator.DeleteSelf();   // moves to parent

            // then remove *all* empty nodes to clean up (not necessary): 
            // TODO, add empty elements like <br />, <img /> as exclusion
            /*
            navigator.MoveToRoot();
            while (navigator.MoveToFollowing(System.Xml.XPath.XPathNodeType.Element))
                while (!navigator.HasChildren && (navigator.Value ?? "").Trim() == "")
                    navigator.DeleteSelf();  // moves to parent
            */
            navigator.MoveToRoot();

            return (navigator.InnerXml.Substring(5, navigator.InnerXml.Length - 11));
        }

    }
}