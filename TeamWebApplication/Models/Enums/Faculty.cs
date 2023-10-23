using System.ComponentModel.DataAnnotations;

namespace TeamWebApplication.Models.Enums
{
	public enum Faculty
	{
		[Display(Name = "Mathematics and Informatics")]
		MathematicsAndInformatics,
		[Display(Name = "Chemistry and Geosciences")]
		ChemistryAndGeosciences,
		Physics,
		Filology
	}
}
