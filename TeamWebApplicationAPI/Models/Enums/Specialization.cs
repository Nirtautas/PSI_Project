using System.ComponentModel.DataAnnotations;

namespace TeamWebApplicationAPI.Models.Enums
{
	public enum Specialization
	{
		[Display(Name = "Program systems")]
		ProgramSystems,
		Informatics,
		Chemistry,
		Geology,
		[Display(Name = "Quantum physics")]
		QuantumPhysics,
		[Display(Name = "Fluid physics")]
		FluidPhysics,
		[Display(Name = "English filology")]
		EnglishFilology,
		[Display(Name = "Lithuanian filology")]
		LithuanianFilology,
		None
	}
}
