// RegExpConstructor.cs
//
// Authors:
//   Olivier Dufour <olivier.duff@gmail.com>
//
// Copyright (C) 2008 Olivier Dufour
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//

using System;
using Microsoft.Scripting;
using Microsoft.Scripting.Runtime;

namespace Microsoft.JScript.Runtime.Types {

	[Serializable]
	public class RegExpConstructor : FunctionObject {

		public RegExpConstructor (CodeContext context)
			: base (context, null, null, false)
		{
		}

		public static object call (CodeContext context, params object [] args)
		{
			throw new NotImplementedException ();
		}

		public static new object construct (CodeContext context, object self, params object [] args)
		{
			throw new NotImplementedException ();
		}

		public static object Construct (CodeContext context, string pattern, bool ignoreCase, bool global, bool multiline)
		{
			throw new NotImplementedException ();
		}

		public static RegExpObject CreateInstance (CodeContext context, params object [] args)
		{
			throw new NotImplementedException ();
		}

		public static RegExpObject Invoke (CodeContext context, params object [] args)
		{
			throw new NotImplementedException ();
		}

		public override void SetItem (SymbolId name, object value)
		{
			base.SetItem (name, value);
		}

		public override bool TryGetItem (SymbolId name, out object value)
		{
			return base.TryGetItem (name, out value);
		}

		public object index {
			get { throw new NotImplementedException (); }
		}

		public object input {
			get { throw new NotImplementedException (); }
			set { throw new NotImplementedException (); }
		}

		public object lastIndex {
			get { throw new NotImplementedException (); }
		}

		public object lastMatch {
			get { throw new NotImplementedException (); }
		}

		public object lastParen {
			get { throw new NotImplementedException (); }
		}

		public object leftContext {
			get { throw new NotImplementedException (); }
		}

		public object rightContext {
			get { throw new NotImplementedException (); }
		}
	}
}
