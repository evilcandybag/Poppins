using System;

namespace Insella.AI.Poppins
{
	/// <summary>
	/// Decorator Node that visits its child iff a condition is met. 
	/// </summary>
	public class ConditionDecorator : Decorator
	{
		private Func<bool> condition;
		
		/// <summary>
		/// Construct a childless ConditionDecorator with the given condition.
		/// </summary>
		/// <param name='condition'>
		/// Condition on which to execute Child.
		/// </param>
		public ConditionDecorator(Func<bool> condition) : base(){
			this.condition = condition;
		}
		/// <summary>
		/// Construct a ConditionDecorator with the given Child and condition.
		/// </summary>
		/// <param name='child'>
		/// Child.
		/// </param>
		/// <param name='condition'>
		/// Condition on which to execute Child.
		/// </param>
		public ConditionDecorator (Node child, Func<bool> condition) : base(child) 
		{
			this.condition = condition;
		}
		
		public override Node.Status Visit() {
			if (condition()) {
				return Child.Visit();
			} else {
				return Node.Status.FAIL;
			}
		}
		
	}
}

