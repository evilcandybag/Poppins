using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Insella.AI.Poppins
{
	/// <summary>
	/// Selector that selects nodes based on a priority value. 
	/// </summary>
	public class PrioritySelector : Selector
	{
		
		private HashSet<PriorityNode> children_;
		
		public PrioritySelector () : base()
		{
			children_ = new HashSet<PriorityNode>();
		}
		
		/// <summary>
		/// Visit the Selector's children in priority order. 
		/// SUCCEESS on the first succeeding child.
		/// If there is a child RUNNING, visit that Node. 
		/// </summary>
		/// <returns>
		/// SUCCESS if one child succeeds. 
		/// FAIL if all children fail. 
		/// RUNNING if there is a child that is not finished yet.
		/// </returns>
		public override Status Visit() {
			if (State == Status.RUNNING) {
				return VisitRunning();
			} else {
				var children = children_.OrderByDescending(x => x.Prio);
				foreach (PriorityNode pc in children)  {
					Node child = pc.Child;
					switch (child.Visit()) {
					case Status.READY:
						throw new InvalidOperationException("A node should never return READY when visited!"); 
					case Status.RUNNING:
						State = Status.RUNNING;
						runningNode = child;
						return State;	
					case Status.SUCCESS:
						State = Status.READY;
						return Status.SUCCESS;
					}
				}
				State = Status.READY;
				return Status.FAIL;
			}
		}
		
		/// <summary>
		/// Add a new child to the Selector, creating a PriorityNode.
		/// Take care to always save the return value as that is the only way to change its priority!
		/// </summary>
		/// <param name='child'>Child.</param>
		/// <param name="prio">The assigned priority</param>
		/// <returns>A reference to the PriorityNode.</returns>
		public PriorityNode AddChild(Node child, double prio = 1.0) {
			PriorityNode pn = new PriorityNode(child,prio);
			children_.Add(pn);
			return pn;
		}
		
		/// <summary>Removes the specified child.</summary>
		/// <param name='child'>Node to remove.</param>
		/// <returns>The removed child Node.</returns>
		public Node RemoveChild(Node child) {
			PriorityNode n = children_.First(x => x.Child == child);
			children_.Remove(n);
			return n.Child;
		}
		
		/// <summary>Give the number of child nodes.</summary>
		/// <returns>The number of children.</returns>
		public int NoChildren(){
			return children_.Count;
		}
		
	}
}

