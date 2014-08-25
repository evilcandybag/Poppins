using UnityEngine;
using System.Collections;

namespace Insella.AI.Poppins {
	/// <summary>
	/// Basic node class for the behavior tree implementation.
	/// </summary>
	public abstract class Node {
		
		private Status state_;
		//This just here for debugging purposes....
		public string name;
		
		public Node() {
			State = Status.READY;
		}

		
		/// <summary>
		/// The status of a node.
		/// </summary>
		public enum Status {READY,FAIL,SUCCESS,RUNNING}
		
		/// <summary>
		/// Gets the Status of the Node. Used to determine whether we are 
		/// allowed to visit a Node or not.
		/// </summary>
		protected Status State {
			get {
				return state_;
			}
			set {
				state_ = value;
			}
		}
		
		/// <summary>
		/// Visit this Node and perform its function. 
		/// </summary>
		public abstract Status Visit();
	}
}
