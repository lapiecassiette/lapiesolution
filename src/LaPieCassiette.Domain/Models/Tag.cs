using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaPieCassiette.Domain.Models
{
  
   public class Tag
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public List<ProductTag> ProductTags { get; private set; } = new();

        private Tag() { } // EF

        public Tag(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tag name cannot be empty");

            Name = name.Trim();
        }

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tag name cannot be empty");

            Name = name.Trim();
        }
    }
}