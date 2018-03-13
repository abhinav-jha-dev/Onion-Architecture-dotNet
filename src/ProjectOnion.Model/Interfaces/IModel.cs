using System;

namespace ProjectOnion.Model.Interfaces
{
    public interface IModel
    {
        DateTime AddedDate { get; set; }
        DateTime ModifiedDate { get; set; }
        string IPAddress { get; set; }
    }
}
