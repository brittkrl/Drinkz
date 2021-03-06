﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace B4.EE.KarlstromB.Domain.Models
{
    public class Cocktail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Preparation { get; set; }
        public string ImageUrl { get; set; }
        public int? Rating { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; } = new ObservableCollection<Ingredient>();
        public Guid UserId { get; set; }
        public bool HasIce { get; set; }
    }
}
