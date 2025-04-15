using API.Utils.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class BaseClass : IBaseClass, IDisposable
    {
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public bool? IsActive { get; set; }

        [NotMapped]
        bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
