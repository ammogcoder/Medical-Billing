using System;
using System.Text;
using System.Xml;
using System.Collections.Generic;

namespace Parser
{
    #region SqlParser

    public class SqlParser : ParserBase
    {
        public SqlParser()
        {
        }

        public override void Parse(string text)
        {
            base.Parse(text);
            SelectList = new List<string>();
            AsList = new List<string>();
            FromList = new List<string>();
            OrderbyList = new List<string>();

            breakDownString(SelectClause, kParse.kSelect);
            breakDownString(FromClause, kParse.kFrom);
            breakDownString(OrderByClause, kParse.kOrderby);
        }

        #region Base class implementation

        #region Fields

        /// <summary>
        /// Stores the types of all the possible tags.
        /// </summary>
        Type[] fTags;
        Type[] fStringTags;

        #endregion

        #region Properties

        public bool breakAs(string str, ref string participle, ref string astext)
        {
            string lowercase = str.ToLower();
            int i = lowercase.IndexOf(" as ");
            if (i > 0)
            {
                participle = str.Substring(0, i);
                astext = str.Substring(i + 4, str.Length - participle.Length - 4/*5*/);
                participle = participle.Trim();
                astext = astext.Trim();
                astext = astext.Replace("\"","");
                return true;
            }
            lowercase = str.ToLower();
            i = lowercase.IndexOf("as");
            if (i > 0)
            {
                if (i + 2 < lowercase.Length)
                {
                    if (char.IsWhiteSpace(lowercase[i + 2]) && char.IsWhiteSpace(lowercase[i - 1]))
                    {
                        participle = str.Substring(0, i - 1);
                        astext = str.Substring(i + 3, str.Length - participle.Length - 4);
                        participle = participle.Trim();
                        astext = astext.Trim();
                        astext = astext.Replace("\"", "");
                        return true;

                    }
                }
            }
            return false;
        }

        public enum kParse
        {
            kSelect,
            kFrom,
            kOrderby
        }

        public List<string> SelectList { get; set; }
        public List<string> AsList { get; set; }
        public List<string> FromList { get; set; }
        public List<string> OrderbyList { get; set; }

        public void breakDownString(string nasty, kParse parseAs)
        {
            string part = string.Empty;
            int pos = 0;
            int bracecount = 0;
            while (pos < nasty.Length)
            {
                if (nasty[pos] == '(')
                    bracecount++;
                if (nasty[pos] == ')')
                    bracecount--;

                if (nasty[pos] == ',' && bracecount == 0)
                {
                    switch (parseAs)
                    {
                        case kParse.kSelect:
                            {
                                string participle = string.Empty;
                                string astext = string.Empty;
                                if (breakAs(part, ref participle, ref astext))
                                {
                                    SelectList.Add(participle.Trim());
                                    AsList.Add(astext.Trim());
                                }
                                else
                                {
                                    part = part.Trim();
                                    if (part.Length > 0)
                                    {
                                        SelectList.Add(part.Trim());
                                        AsList.Add(string.Empty);
                                    }

                                }
                            }
                            break;
                        case kParse.kFrom:
                            {
                                FromList.Add(part.Trim());
                            }
                            break;
                        case kParse.kOrderby:
                            {
                                OrderbyList.Add(part.Trim());
                            }
                            break;

                    }
                    part = string.Empty;
                    pos++;
                }
                else
                {
                    part += nasty[pos].ToString();
                    pos++;
                    if (pos == nasty.Length)
                    {
                        switch (parseAs)
                        {
                            case kParse.kSelect:
                                {
                                    string participle = string.Empty;
                                    string astext = string.Empty;
                                    if (breakAs(part, ref participle, ref astext))
                                    {
                                        SelectList.Add(participle.Trim());
                                        AsList.Add(astext.Trim());
                                    }
                                    else
                                    {
                                        part = part.Trim();
                                        if (part.Length > 0)
                                        {
                                            SelectList.Add(part.Trim());
                                            AsList.Add(string.Empty);
                                        }
                                    }
                                }
                                break;
                            case kParse.kFrom:
                                FromList.Add(part.Trim());
                                break;
                            case kParse.kOrderby:
                                OrderbyList.Add(part.Trim());
                                break;
                        }
                    }
                }
            }
        }

        public string ReBuild(bool reparse)
        {
            string sql = "SELECT \r\n\t";

            for (int i = 0; i < SelectList.Count; i++)
            {
                sql += SelectList[i];
                if (AsList[i] != null && !string.IsNullOrEmpty(AsList[i]))
                {
                    sql += " \tAS " + "\"" + AsList[i] + "\"";
                }
                if (i < SelectList.Count - 1)
                    sql += ",\r\n\t";
            }
            sql += "\r\n";

            sql += " FROM " + FromClause.TrimEnd() + "\r\n";
#if WHAT_WRONG
            if(string.IsNullOrEmpty( WhereClause ) == false)
                sql += " WHERE " + WhereClause.Trim() + "\r\n";

            string groupby = GroupByClause.Trim().ToString();
            if (!string.IsNullOrEmpty(groupby))
                sql += " GROUP BY " + groupby.TrimEnd() + "\r\n";

            string orderby = OrderByClause.Trim().ToString();
            if (!string.IsNullOrEmpty(orderby))
                sql += " ORDER BY " + orderby.TrimEnd() + "\r\n";
            if (reparse)
                Parse(sql);
            return sql.TrimEnd();
#else
            sql += " WHERE " + WhereClause.TrimEnd() + "\r\n";
            string groupby = GroupByClause.ToString();
            if (!string.IsNullOrEmpty(groupby))
                sql += " GROUP BY " + groupby.TrimEnd() + "\r\n";
            string orderby = OrderByClause.ToString();
            if (!string.IsNullOrEmpty( orderby))
                sql += " ORDER BY " + orderby.TrimEnd() + "\r\n";
            if (reparse)
                Parse(sql);
            return sql.TrimEnd();
#endif
        }

        /// <summary>
        /// Indicates whether the white space is a non-valueable character.
        /// </summary>
        protected override bool IsSkipWhiteSpace
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Returns the list of all the available tags.
        /// </summary>
        protected override Type[] Tags
        {
            get
            {
                if (fTags == null)
                {
                    fTags = new Type[] { 
						typeof(SelectTag),
						typeof(FromTag),
						typeof(WhereTag),
						typeof(OrderByTag),
						typeof(BracesTag), //"(" ")"
						typeof(StringLiteralTag),
						typeof(ForUpdateTag),
						typeof(StartWith),// START_WITH START WITH
						typeof(GroupByTag),
						typeof(QuotedIdentifierTag)
					};
                }

                return fTags;
            }
        }

        /// <summary>
        /// Returns the list of all the available String tags.
        /// </summary>
        protected override Type[] StringTags
        {
            get
            {
                if (fStringTags == null)
                {
                    fStringTags = new Type[] { 
						typeof(AsTag),
						typeof(CommaTag)
					};
                }

                return fStringTags;
            }
        }

        #endregion

        #endregion

        #region Common

        #region Methods

        /// <summary>
        /// Returns the xml node which corresponds to the From tag.
        /// If this node does not exist, this method generates an
        /// exception.
        /// </summary>
        private XmlNode GetFromTagXmlNode(XmlDocument document)
        {
            //   "ParsedDocument/Tag[@Type='FROM']"
            string xpath = string.Format(@"{0}/{1}[@{2}='{3}']",
                /* "ParsedDocument" */cRootXmlNodeName,
                /* "Tag" */cTagXmlNodeName,
                /* ""Type" */cTagTypeXmlAttributeName,
                FromTag.cTagName);
            XmlNode myFromNode = ParsedDocument.SelectSingleNode(xpath);
            if (myFromNode == null)
                throw new Exception(ToText(document));

            return myFromNode;
        }

        private XmlNode GetGroupByTagXmlNode(XmlDocument document)
        {
            //   "ParsedDocument/Tag[@Type='GroupBy']"
            string xpath = string.Format(@"{0}/{1}[@{2}='{3}']",
                /* "ParsedDocument" */cRootXmlNodeName,
                /* "Tag" */cTagXmlNodeName,
                /* ""Type" */cTagTypeXmlAttributeName,
                GroupByTag.cTagName);
            XmlNode myGroupByNode = ParsedDocument.SelectSingleNode(xpath);
            //if (myGroupByNode == null)
            //    throw new Exception(ToText(document));

            return myGroupByNode;
        }
        /// <summary>
        /// Returns the xml node which corresponds to the For Update tag.	
        /// </summary>
        private XmlNode GetForUpdateTagXmlNode()
        {
            XmlNode myForUpdateNode = ParsedDocument.SelectSingleNode(string.Format(@"{0}/{1}[@{2}='{3}']", cRootXmlNodeName, cTagXmlNodeName, cTagTypeXmlAttributeName, ForUpdateTag.cTagName));

            return myForUpdateNode;
        }

        /// <summary>
        /// Checks whether there is a tag in the text at the specified position, and returns its tag.
        /// </summary>
        internal new Type IsTag(string text, int position)
        {
            return base.IsTag(text, position);
        }

        #endregion

        #endregion

        #region Where Clause

        #region Methods

        /// <summary>
        /// Returns the xml node which corresponds to the Where tag.
        /// If this node does not exist, creates a new one (if needed).
        /// </summary>
        private XmlNode GetWhereTagXmlNode(bool createNew, XmlDocument document)
        {
            XmlNode myWhereNode = ParsedDocument.SelectSingleNode(string.Format(@"{0}/{1}[@{2}='{3}']", cRootXmlNodeName, cTagXmlNodeName, cTagTypeXmlAttributeName, WhereTag.cTagName));
            if (myWhereNode == null && createNew)
            {
                WhereTag myWhereTag = new WhereTag();
                myWhereTag.InitializeFromData(this, null, false);
                myWhereNode = CreateTagXmlNode(myWhereTag, document);

                XmlNode myFromNode = GetFromTagXmlNode(document);
                myFromNode.ParentNode.InsertAfter(myWhereNode, myFromNode);
            }

            return myWhereNode;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Where clause of the parsed sql.
        /// </summary>
        public string WhereClause
        {
            get
            {
                #region Get the Where xml node

                XmlNode myWhereTagXmlNode = GetWhereTagXmlNode(false, this.ParsedDocument);
                if (myWhereTagXmlNode == null)
                    return string.Empty;

                #endregion

                StringBuilder myStringBuilder = new StringBuilder();
                XmlNodesToText(myStringBuilder, myWhereTagXmlNode.ChildNodes, false);
                string str = myStringBuilder.ToString();
                return myStringBuilder.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    #region Remove the Where xml node

                    XmlNode myWhereXmlNode = GetWhereTagXmlNode(false, this.ParsedDocument);
                    if (myWhereXmlNode != null)
                        myWhereXmlNode.ParentNode.RemoveChild(myWhereXmlNode);

                    #endregion
                }
                else
                {
                    #region Modify the Where xml node

                    XmlNode myWhereXmlNode = GetWhereTagXmlNode(true, this.ParsedDocument);

                    ClearXmlNode(myWhereXmlNode);

                    TagBase myWhereTag = TagXmlNodeToTag(myWhereXmlNode, false);

                    ParseBlock(myWhereXmlNode, myWhereTag, value, 0);

                    #endregion
                }
            }
        }

        #endregion

        #endregion

        #region Order By Clause

        #region Methods

        private XmlNode GetSelectTagXmlNode(bool createNew)
        {
            string str = string.Format(@"{0}/{1}[@{2}='{3}']", cRootXmlNodeName, cTagXmlNodeName, cTagTypeXmlAttributeName, SelectTag.cTagName);
            XmlNode mySelectNode = ParsedDocument.SelectSingleNode(str);
            if (mySelectNode == null && createNew)
            {
                SelectTag mySelectTag = new SelectTag();
                mySelectTag.InitializeFromData(this, null, false);
                mySelectNode = CreateTagXmlNode(mySelectTag, this.ParsedDocument);

                XmlNode myForUpdateNode = GetForUpdateTagXmlNode();
                if (myForUpdateNode != null)
                {
                    myForUpdateNode.ParentNode.InsertBefore(mySelectNode, myForUpdateNode);
                    return mySelectNode;
                }

                XmlNode myNode = GetFromTagXmlNode(this.ParsedDocument);
                myNode.ParentNode.AppendChild(mySelectNode);
            }

            return mySelectNode;
        }


        /// <summary>
        /// Returns the xml node which corresponds to the Order By tag.
        /// If this node does not exist, creates a new one (if needed).
        /// </summary>
        private XmlNode GetOrderByTagXmlNode(bool createNew)
        {
            string str = string.Format(@"{0}/{1}[@{2}='{3}']", cRootXmlNodeName, cTagXmlNodeName, cTagTypeXmlAttributeName, OrderByTag.cTagName);
            XmlNode myOrderByNode = ParsedDocument.SelectSingleNode(string.Format(@"{0}/{1}[@{2}='{3}']", cRootXmlNodeName, cTagXmlNodeName, cTagTypeXmlAttributeName, OrderByTag.cTagName));
            if (myOrderByNode == null && createNew)
            {
                OrderByTag myOrderByTag = new OrderByTag();
                myOrderByTag.InitializeFromData(this, null, false);
                myOrderByNode = CreateTagXmlNode(myOrderByTag, this.ParsedDocument);

                XmlNode myForUpdateNode = GetForUpdateTagXmlNode();
                if (myForUpdateNode != null)
                {
                    myForUpdateNode.ParentNode.InsertBefore(myOrderByNode, myForUpdateNode);
                    return myOrderByNode;
                }

                XmlNode myFromNode = GetFromTagXmlNode(this.ParsedDocument);
                myFromNode.ParentNode.AppendChild(myOrderByNode);
            }

            return myOrderByNode;
        }

        #endregion

        public void WriteParsedXmlToRootC()
        {

            //XmlWriter xw = new 
            if (ParsedDocument != null)
                ParsedDocument.Save("c:\\ParsedDoc.xml");
        }

        #region Properties

        /// <summary>
        /// private XmlNode GetSelectTagXmlNode(bool createNew)
        /// </summary>
        public string SelectClause
        {
            get
            {
                //XmlDocument asdf = new XmlDocument();
#if ASDFASDF
        XmlNodeList lst = ParsedDocument.GetElementsByTagName("SELECT");
        string alltext = ToText();
        StringBuilder output = new StringBuilder();
        foreach (XmlNode node in lst)
        {
            XmlNodeToText(output, node);
            string str = output.ToString();
        
        }
#endif
#if noneednow
                XmlNodeList xNodelst = ParsedDocument.SelectNodes("ParsedDocument/*");
                StringBuilder output = new StringBuilder();
                foreach (XmlNode xNode in xNodelst)
                {
                    if (xNode.Name == cTagXmlNodeName)
                    {
                        XmlNode part = xNode.SelectSingleNode("@Type");
                        if (part.Value == FromTag.cTagName)
                            break;
                    }
                    XmlNodeToText(output, xNode);
                }
                string done = output.ToString();
                done = done.Remove( done.IndexOf(SelectTag.cTagName), SelectTag.cTagName.Length + 1 );
                XmlNode mySelectNode = ParsedDocument;
                ///////////////////////////////
#endif
                XmlNode mySelectTagXmlNode = GetSelectTagXmlNode(false);
                if (mySelectTagXmlNode == null)
                    return string.Empty;


                StringBuilder myStringBuilder = new StringBuilder();
                XmlNodesToText(myStringBuilder, mySelectTagXmlNode.ChildNodes, false);
                return myStringBuilder.ToString();

            }
        }

        /// <summary>
        /// </summary>
        public string FromClause
        {
            get
            {
                XmlNode myFromTagXmlNode = GetFromTagXmlNode(this.ParsedDocument);
                if (myFromTagXmlNode == null)
                    return string.Empty;


                StringBuilder myStringBuilder = new StringBuilder();
                XmlNodesToText(myStringBuilder, myFromTagXmlNode.ChildNodes, false);
                return myStringBuilder.ToString();

            }
        }

        /// <summary>
        /// </summary>
        public string GroupByClause
        {
            get
            {
                XmlNode myGroupByTagXmlNode = GetGroupByTagXmlNode(this.ParsedDocument);
                if (myGroupByTagXmlNode == null)
                    return string.Empty;


                StringBuilder myStringBuilder = new StringBuilder();
                XmlNodesToText(myStringBuilder, myGroupByTagXmlNode.ChildNodes, false);
                return myStringBuilder.ToString();

            }
        }

        /// <summary>
        /// Gets or sets the Order By clause of the parsed sql.
        /// </summary>
        public string OrderByClause
        {
            get
            {

                string getOrderby;
                try
                {
                    if (!this.IsDocumentInit)
                        return string.Empty;

                    XmlNode myOrderByTagXmlNode = GetOrderByTagXmlNode(false);
                    if (myOrderByTagXmlNode == null)
                        return string.Empty;

                    StringBuilder myStringBuilder = new StringBuilder();
                    XmlNodesToText(myStringBuilder, myOrderByTagXmlNode.ChildNodes, false);
                    getOrderby = myStringBuilder.ToString();
                }
                catch  
                {
                    throw;
                }
                return getOrderby.ToString();

            }
            set
            {
                if (!this.IsDocumentInit)
                    return;
                if (string.IsNullOrEmpty(value))
                {
                    XmlNode myOrderByXmlNode = GetOrderByTagXmlNode(false);
                    if (myOrderByXmlNode != null)
                        myOrderByXmlNode.ParentNode.RemoveChild(myOrderByXmlNode);
                }
                else
                {
                    XmlNode myOrderByXmlNode = GetOrderByTagXmlNode(true);
                    ClearXmlNode(myOrderByXmlNode);
                    TagBase myOrderByTag = TagXmlNodeToTag(myOrderByXmlNode, false);
                    ParseBlock(myOrderByXmlNode, myOrderByTag, value, 0);
                }
            }
        }

        #endregion

        #endregion
    }

    #endregion
}
