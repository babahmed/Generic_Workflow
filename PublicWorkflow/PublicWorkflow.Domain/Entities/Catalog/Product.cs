﻿using AspNetCoreHero.Abstractions.Domain;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}