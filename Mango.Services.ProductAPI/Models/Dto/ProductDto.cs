﻿using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI.Models.Dto
{
    public class ProductDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        [Range(1, 100)]
        public int Count { get; set; } = 1;
    }
}