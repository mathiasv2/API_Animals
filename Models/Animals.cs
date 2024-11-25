using System;
namespace FinalWorkshop.Models
{
	public class Animals
	{
		public int id { get; set; }
		public string name { get; set; }

        public Species Species { get; set; }
		public int SpeciesId { get; set; }
	}
}

