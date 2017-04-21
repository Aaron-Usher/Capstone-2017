﻿using System.Collections.Generic;
using DataObjects;

namespace MVCPresentationLayer.Models
{
    /// <summary>
    /// Created by Michael Takrama
    /// 04/07/2017
    /// Update by Skyler Hiscock
    /// 04/08/2017
    /// Composite Data Object View Model for Product/Index
    /// </summary>

    public class ProductsListViewModel
    {
        public IEnumerable<BrowseProductViewModel> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public string SearchPhrase { get; set; }

        public string CurrentCategory { get; set; }

        public IEnumerable<string> Categories { get; set; }

    }

    public class NavMenuViewModel
    {
        public IEnumerable<string> Categories { get; set; }
        public string SearchPhrase { get; set; }
        public string SelectedCategory { get; set; }
    }
}