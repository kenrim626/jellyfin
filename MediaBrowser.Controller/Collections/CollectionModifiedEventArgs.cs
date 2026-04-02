#pragma warning disable CS1591

using System;
using System.Collections.Generic;
using MediaBrowser.Controller.Entities;

namespace MediaBrowser.Controller.Collections
{
    public class CollectionModifiedEventArgs : EventArgs
    {
        public CollectionModifiedEventArgs(Folder collection, IReadOnlyCollection<BaseItem> itemsChanged)
        {
            Collection = collection;
            ItemsChanged = itemsChanged;
        }

        /// <summary>
        /// Gets or sets the collection.
        /// </summary>
        /// <value>The collection.</value>
        public Folder Collection { get; set; }

        /// <summary>
        /// Gets or sets the items changed.
        /// </summary>
        /// <value>The items changed.</value>
        public IReadOnlyCollection<BaseItem> ItemsChanged { get; set; }
    }
}
