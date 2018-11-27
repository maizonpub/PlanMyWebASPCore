﻿using WordPressPCL.Models;
using WordPressPCL.Utility;

namespace WordPressPCL.Client
{
    /// <summary>
    /// Client class for interaction with Categories endpoint WP REST API
    /// </summary>
    public class ItemCateringServices : CRUDOperation<ItemCategory, CategoriesQueryBuilder>
    {
        #region Init

        private new const string _methodPath = "itemcateringservices";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="HttpHelper">reference to HttpHelper class for interaction with HTTP</param>
        /// <param name="defaultPath">path to site, EX. http://demo.com/wp-json/ </param>
        public ItemCateringServices(ref HttpHelper HttpHelper, string defaultPath) : base(ref HttpHelper, defaultPath, _methodPath, true)
        {
        }

        #endregion Init
    }
}