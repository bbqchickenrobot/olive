//
// System.Windows.Controls.TextBlock.cs
//
// Authors:
//	Atsushi Enomoto  <atsushi@ximian.com>
//
// Copyright (C) 2007 Novell, Inc (http://www.novell.com)
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
using System.Windows;
using System.Windows.Media;
using Mono;

namespace System.Windows.Controls {

	public sealed class TextBlock : FrameworkElement {

		public static readonly DependencyProperty ActualHeightProperty =
			DependencyProperty.Lookup (Kind.TEXTBLOCK, "ActualHeight", typeof (double));
		public static readonly DependencyProperty ActualWidthProperty =
			DependencyProperty.Lookup (Kind.TEXTBLOCK, "ActualWidth", typeof (double));
		public static readonly DependencyProperty FontFamilyProperty =
			DependencyProperty.Lookup (Kind.TEXTBLOCK, "FontFamily", typeof (string));
		public static readonly DependencyProperty FontSizeProperty =
			DependencyProperty.Lookup (Kind.TEXTBLOCK, "FontSize", typeof (double));
		public static readonly DependencyProperty FontStretchProperty =
			DependencyProperty.Lookup (Kind.TEXTBLOCK, "FontStretch", typeof (FontStretches));
		public static readonly DependencyProperty FontStyleProperty =
			DependencyProperty.Lookup (Kind.TEXTBLOCK, "FontStyle", typeof (FontStyles));
		public static readonly DependencyProperty FontWeightProperty =
			DependencyProperty.Lookup (Kind.TEXTBLOCK, "FontWeight", typeof (int));
		public static readonly DependencyProperty ForegroundProperty =
			DependencyProperty.Lookup (Kind.TEXTBLOCK, "Foreground", typeof (Brush));
		public static readonly DependencyProperty InlinesProperty =
			DependencyProperty.Lookup (Kind.TEXTBLOCK, "Inlines", typeof (Inlines));
		public static readonly DependencyProperty TextDecorationsProperty =
			DependencyProperty.Lookup (Kind.TEXTBLOCK, "TextDecorations", typeof (TextDecorations));
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Lookup (Kind.TEXTBLOCK, "Text", typeof (string));
		public static readonly DependencyProperty TextWrappingProperty =
			DependencyProperty.Lookup (Kind.TEXTBLOCK, "TextWrapping", typeof (TextWrapping));

		public TextBlock ()  : base (NativeMethods.text_block_new ())
		{
		}

		internal TextBlock (IntPtr raw) : base (raw)
		{
		}

		public double ActualHeight {
			get { return (double) GetValue (ActualHeightProperty); }
		}

		public double ActualWidth {
			get { return (double) GetValue (ActualWidthProperty); }
		}

		public string FontFamily {
			get { return (string) GetValue (FontFamilyProperty); }
			set { SetValue (FontFamilyProperty, value); }
		}

		public double FontSize {
			get { return (double) GetValue (FontSizeProperty); }
			set { SetValue (FontSizeProperty, value); }
		}

		public FontStretches FontStretch {
			get { return (FontStretches) GetValue (FontStretchProperty); }
			set { SetValue (FontStretchProperty, value); }
		}

		public FontStyles FontStyle {
			get { return (FontStyles) GetValue (FontStyleProperty); }
			set { SetValue (FontStyleProperty, value); }
		}

		public FontWeights FontWeight {
			get { return (FontWeights) GetValue (FontWeightProperty); }
			set { SetValue (FontWeightProperty, value); }
		}

		public Brush Foreground {
			get { return (Brush) GetValue (ForegroundProperty); }
			set { SetValue (ForegroundProperty, value); }
		}

		public Inlines Inlines {
			get { return (Inlines) GetValue (InlinesProperty); }
			set { SetValue (InlinesProperty, value); }
		}

		public TextDecorations TextDecorations {
			get { return (TextDecorations) GetValue (TextDecorationsProperty); }
			set { SetValue (TextDecorationsProperty, value); }
		}

		public TextWrapping TextWrapping {
			get { return (TextWrapping) GetValue (TextWrappingProperty); }
			set { SetValue (TextWrappingProperty, value); }
		}

		public string Text {
			get { return (string) GetValue (TextProperty); }
			set { SetValue (TextProperty, value); }
		}

		public void SetFontSource (DependencyObject Downloader)
		{
			if (Downloader != null) {
				Downloader dl = (Downloader as Downloader);
				NativeMethods.text_block_set_font_source (native, dl.native);
			} else {
				NativeMethods.text_block_set_font_source (native, IntPtr.Zero);
			}
		}

		internal override Kind GetKind ()
		{
			return Kind.TEXTBLOCK;
		}
	}
}
