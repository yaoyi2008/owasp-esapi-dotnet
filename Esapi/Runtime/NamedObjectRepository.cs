﻿using System;
using System.Collections.Generic;
using Owasp.Esapi.Interfaces;

namespace Owasp.Esapi.Runtime
{
    /// <summary>
    /// Object repository
    /// </summary>
    /// <typeparam id="TObject"></typeparam>
    internal class NamedObjectRepository<TObject> : IObjectRepository<string, TObject>
        where TObject : class
    {
        private Dictionary<string, TObject> _entries;

        public NamedObjectRepository()
        {
            _entries = new Dictionary<string, TObject>();
        }

        public NamedObjectRepository(IDictionary<string, TObject> entries)
        {
            _entries = (entries != null ?
                            new Dictionary<string, TObject>(entries) :
                            new Dictionary<string, TObject>());
        }

        /// <summary>
        /// Get entries
        /// </summary>
        protected Dictionary<string, TObject> Entries
        {
            get { return _entries; }
        }

        #region IObjectRepository<TName, TObject> Members
        /// <summary>
        /// Register object
        /// </summary>
        /// <param id="id"></param>
        /// <param id="value"></param>
        /// <returns></returns>
        public IObjectRepository<string, TObject> Register(string id, TObject value)
        {
            if (string.IsNullOrEmpty(id)) {
                throw new ArgumentException("Invalid id", "id");
            }
            if (value == null) {
                throw new ArgumentNullException("value");
            }
            _entries[id] = value;
            return this;
        }
        /// <summary>
        /// Revoke object
        /// </summary>
        /// <param id="id"></param>
        /// <returns></returns>
        public IObjectRepository<string, TObject> Revoke(string id)
        {
            _entries.Remove(id);
            return this;
        }
        /// <summary>
        /// Lookup object
        /// </summary>
        /// <param id="id"></param>
        /// <param id="value"></param>
        /// <returns></returns>
        public bool Lookup(string id, out TObject value)
        {
            return _entries.TryGetValue(id, out value);
        }
        /// <summary>
        /// Get count
        /// </summary>
        public int Count
        {
            get { return _entries.Count; }
        }
        /// <summary>
        /// Get keys
        /// </summary>
        public ICollection<string> Ids
        {
            get { return _entries.Keys; }
        }
        /// <summary>
        /// Get objects
        /// </summary>
        public ICollection<TObject> Objects
        {
            get { return _entries.Values; }
        }
        /// <summary>
        /// Get object
        /// </summary>
        /// <param id="id"></param>
        /// <returns></returns>
        public TObject this[string id]
        {
            get { return _entries[id]; }
        }

        #endregion

    }
}
