using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    #region CommaTag

    [TagType(CommaTag.cTagName)]
    [MatchCommaTag]
    internal class CommaTag : TagBase
    {
        #region Consts

        /// <summary>
        /// The name of the tag (its identifier).
        /// </summary>
        public const string cTagName = "COMMA_IDENTIFIER";

        /// <summary>
        /// The tag delimiter.
        /// </summary>
        public const string cTagDelimiter = ",";

        #endregion

        #region Methods

        #region Common

        /// <summary>
        /// Reads the tag at the specified position in the specified word and separator array.
        /// </summary>
        /// <returns>
        /// The position after the tag (at which to continue reading).
        /// </returns>
        protected override int InitializeCoreFromText(ParserBase parser, string sql, int position, TagBase parentTag)
        {
            #region Check the arguments

            ParserBase.CheckTextAndPositionArguments(sql, position);

            #endregion

            int myAfterTagStartPos = MatchStartStatic(sql, position);

            if (myAfterTagStartPos < 0)
                throw new Exception("Cannot read the Comma tag.");

            #region Read the identifier's value

            int myTagEndPos = sql.IndexOf(cTagDelimiter, myAfterTagStartPos, StringComparison.InvariantCultureIgnoreCase);
            if (myTagEndPos < 0)
                return position + 1;// sql.Length;// throw new Exception("Cannot read the Comma tag.");

            if (myAfterTagStartPos == myTagEndPos)
                Value = string.Empty;
            else
                Value = sql.Substring(myAfterTagStartPos, myTagEndPos - myAfterTagStartPos);

            #endregion

            Parser = parser;

            HasContents = true;

            return myTagEndPos + cTagDelimiter.Length;
        }

        /// <summary>
        /// Writes the start of the tag.
        /// </summary>
        public override void WriteStart(StringBuilder output)
        {
            CheckInitialized();

            #region Check the parameters

            if (output == null)
                throw new ArgumentNullException();

            #endregion

            output.Append(cTagDelimiter);
            output.Append(Value);
            output.Append(cTagDelimiter);
        }

        #endregion

        /// <summary>
        /// Returns a value indicating whether there is the tag ending at the specified position.
        /// </summary>
        /// <returns>
        /// If this value is less than zero, then there is no ending; otherwise the 
        /// position after ending is returned.
        /// </returns>
        private int MatchEnd(string sql, int position, int literalStartPos)
        {
            #region Check the arguments

            ParserBase.CheckTextAndPositionArguments(sql, position);

            #endregion

            if (string.Compare(sql, position, cTagDelimiter, 0, cTagDelimiter.Length, true) != 0)
                return -1;
            return position + 1;

#if ASDFASF
            #region Check the next and previous symbols (where there are ' symbols)
            #region Determine the number of ' symbols before

            int myNumberOfApostBefore = 0;
            for (int myCurPos = position - 1; myCurPos >= literalStartPos; myCurPos--)
            {
                if (string.Compare(sql, myCurPos, cTagDelimiter, 0, cTagDelimiter.Length, true) == 0)
                    myNumberOfApostBefore++;
                else
                    break;
            }

            #endregion

            if (!(myNumberOfApostBefore == 1 && position == literalStartPos + 1))
            {
                if (myNumberOfApostBefore % 2 == 1)
                    return -1;

            #region Check whether the next symbol is '

                if (position + 1 < sql.Length)
                {
                    if (string.Compare(sql, position + 1, cTagDelimiter, 0, cTagDelimiter.Length, true) == 0)
                        return -1;
                }

                #endregion
            }

            #endregion

            return position + cTagDelimiter.Length;
#endif
        }

        #region Static

        /// <summary>
        /// Checks whether there is the tag start at the specified position 
        /// in the specified sql.
        /// </summary>
        /// <returns>
        /// The position after the tag or -1 there is no tag start at the position.
        /// </returns>
        public static int MatchStartStatic(string sql, int position)
        {
            #region Check the arguments

            ParserBase.CheckTextAndPositionArguments(sql, position);

            #endregion

            if (string.Compare(sql, position, cTagDelimiter, 0, cTagDelimiter.Length, true) != 0)
                return -1;

            return position + cTagDelimiter.Length;
        }

        #endregion

        #endregion
    }

    #endregion

    #region MatchCommaTagAttribute

    internal class MatchCommaTagAttribute : MatchTagAttributeBase
    {
        #region Methods

        public override bool Match(string sql, int position)
        {
            return CommaTag.MatchStartStatic(sql, position) >= 0;
        }

        #endregion
    }

    #endregion
}
