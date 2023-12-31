﻿using ECommerce.Models.Domain;

namespace ECommerce.Models.DTOs
{
    public class ProductItemCardDto
    {
        public Guid ProductItemId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public long QtyInStock { get; set; }
        public decimal Price { get; set; }
        public string? ProductItemImage { get; set; }
        public string ProductItemName { get; set; } = null!;
        public string ProductItemDescription { get; set; } = null!;
        public decimal Rating { get; set; }
        public int NumberOfRatings { get; set; }
        public int NumberOfReviews { get; set; }
        public Dictionary<string, string> Specifications { get; set; } = null!;
        public List<SellerDetailsDto>? Sellers { get; set; }
    }
}
