using System;
using Microsoft.Extensions.Logging;
using Xbim.Common;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Xbim.Common.Enumerations;
using Xbim.Common.ExpressValidation;
using Xbim.Ifc4.Interfaces;
// ReSharper disable once CheckNamespace
// ReSharper disable InconsistentNaming
namespace Xbim.Ifc4.ConstructionMgmtDomain
{
	public partial class IfcLaborResourceType : IExpressValidatable
	{
		public enum IfcLaborResourceTypeClause
		{
			CorrectPredefinedType,
		}

		/// <summary>
		/// Tests the express where-clause specified in param 'clause'
		/// </summary>
		/// <param name="clause">The express clause to test</param>
		/// <returns>true if the clause is satisfied.</returns>
		public bool ValidateClause(IfcLaborResourceTypeClause clause) {
			var retVal = false;
			try
			{
				switch (clause)
				{
					case IfcLaborResourceTypeClause.CorrectPredefinedType:
						retVal = (PredefinedType != IfcLaborResourceTypeEnum.USERDEFINED) || ((PredefinedType == IfcLaborResourceTypeEnum.USERDEFINED) && Functions.EXISTS(this/* as IfcTypeResource*/.ResourceType));
						break;
				}
			} catch (Exception ) {
				/*var log = ApplicationLogging.CreateLogger<Xbim.Ifc4.ConstructionMgmtDomain.IfcLaborResourceType>();
				log.LogError(string.Format("Exception thrown evaluating where-clause 'IfcLaborResourceType.{0}' for #{1}.", clause,EntityLabel), ex);*/
			}
			return retVal;
		}

		public override IEnumerable<ValidationResult> Validate()
		{
			foreach (var value in base.Validate())
			{
				yield return value;
			}
			if (!ValidateClause(IfcLaborResourceTypeClause.CorrectPredefinedType))
				yield return new ValidationResult() { Item = this, IssueSource = "IfcLaborResourceType.CorrectPredefinedType", IssueType = ValidationFlags.EntityWhereClauses };
		}
	}
}
