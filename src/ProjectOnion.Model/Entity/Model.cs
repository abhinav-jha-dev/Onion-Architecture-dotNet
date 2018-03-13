﻿using ProjectOnion.Model.Interfaces;
using System;

namespace ProjectOnion.Model.Entity
{
    public class Model : IModel
    {
        private DateTime? _addedDate = null;
        private DateTime? _modifiedDate = null;
        public DateTime AddedDate
        {
            get { return this._addedDate.HasValue ? this._addedDate.Value : DateTime.Now; }
            set { this._addedDate = value; }
        }

        public DateTime ModifiedDate
        {
            get { return this._modifiedDate.HasValue ? this._modifiedDate.Value : DateTime.Now; }
            set { this._modifiedDate = value; }
        }

        public string IPAddress { get; set; }
    }
}
