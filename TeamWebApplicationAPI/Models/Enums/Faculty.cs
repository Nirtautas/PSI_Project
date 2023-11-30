using System.ComponentModel.DataAnnotations;

namespace TeamWebApplicationAPI.Models.Enums
{
	public enum Faculty
	{
		[Display(Name = "Mathematics and Informatics")]
		MathematicsAndInformatics,
		[Display(Name = "Chemistry and Geosciences")]
		ChemistryAndGeosciences,
		Physics,
		Filology,
		None
	}
}
