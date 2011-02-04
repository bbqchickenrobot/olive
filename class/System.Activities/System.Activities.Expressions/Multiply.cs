using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Activities;
using System.Activities.Debugger;
using System.Activities.Hosting;
using System.Activities.Statements;
using System.Activities.XamlIntegration;
using System.Windows.Markup;

namespace System.Activities.Expressions
{
	public sealed class Multiply<TLeft, TRight, TResult> : CodeActivity<TResult>
	{
		public bool Checked { get; set; }
		[RequiredArgumentAttribute]
		public InArgument<TLeft> Left { get; set; }
		[RequiredArgumentAttribute]
		public InArgument<TLeft> Right { get; set; }

		protected override TResult Execute (CodeActivityContext context)
		{
			throw new NotImplementedException ();
		}
	}
}