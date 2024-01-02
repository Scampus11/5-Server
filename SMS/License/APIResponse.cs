using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS
{
    /// <summary>
    /// this class use for web API
    /// </summary>
    public sealed class APIResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the StatusCode value.
        /// </summary>
        [NotMapped]
        public int? Status { get; set; }

        /// <summary>
        /// Gets or sets the Message value.
        /// </summary>
        [NotMapped]
        public string Message { get; set; }

        #endregion
    }

    /// <summary>
    /// this class use for web API
    /// </summary>
    public sealed class APIRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Token value.
        /// </summary>
        [NotMapped]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the Data value.
        /// </summary>
        [NotMapped]
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the Data value.
        /// </summary>
        [NotMapped]
        public int? NextPageIndex { get; set; }

        #endregion
    }

    public sealed class APIResponseData
    {
        #region Properties

        /// <summary>
        /// Gets or sets the StatusCode value.
        /// </summary>
        [NotMapped]
        public int? Status { get; set; }

        /// <summary>
        /// Gets or sets the Message value.
        /// </summary>
        [NotMapped]
        public string Message { get; set; }

        public object Data { get; set; }

        #endregion
    }
}
