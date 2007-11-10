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
// Copyright (c) 2007 Novell, Inc. (http://www.novell.com)
//
// Authors:
//	Chris Toshok (toshok@novell.com)
//

using System;
using System.Security;

namespace System.Windows {

	public interface IDataObject
	{
		[SecurityCritical]
		object GetData (string format);
		[SecurityCritical]
		object GetData (Type format);
		[SecurityCritical]
		object GetData (string format, bool autoConvert);

		[SecurityCritical]
		bool GetDataPresent (string format);
		[SecurityCritical]
		bool GetDataPresent (Type format);
		[SecurityCritical]
		bool GetDataPresent (string format, bool autoConvert);

		[SecurityCritical]
		string[] GetFormats ();
		[SecurityCritical]
		string[] GetFormats (bool autoConvert);

		[SecurityCritical]
		void SetData (object data);
		[SecurityCritical]
		void SetData (string format, object data);
		[SecurityCritical]
		void SetData (Type format, object data);
		[SecurityCritical]
		void SetData (string format, object data, bool autoConvert);
	}

}
