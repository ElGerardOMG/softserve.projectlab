using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Product : IDisposable
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ProductType { get; set; }

    public int? ProductCategory { get; set; }

    public string? Brand { get; set; }

    public string? Family { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

    public virtual ProductCategory? ProductCategoryNavigation { get; set; }

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

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
