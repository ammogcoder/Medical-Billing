using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    #region AsTag

    [TagType(AsTag.cTagName)]
    [MatchAsTag]
    internal class AsTag : SimpleOneWordTag
    {
        #region Consts

        /// <summary>
        /// The name of the tag (its identifier).
        /// </summary>
        public const string cTagName = "AS";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the tag (its identifier and sql text)
        /// </summary>
        protected override string Name
        {
            get
            {
                return cTagName;
            }
        }

        #endregion
    }

    #endregion

    #region MatchAsTagAttribute

    internal class MatchAsTagAttribute : MatchSimpleOneWordTagAttribute
    {
        #region Properties

        /// <summary>
        /// Gets the name of the tag (its identifier and sql text)
        /// </summary>
        protected override string Name
        {
            get
            {
                return AsTag.cTagName;
            }
        }

        #endregion
    }

    #endregion
}
