
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {


public abstract class DoubleKeyFrame : Freezable, IKeyFrame
{
	public static readonly DependencyProperty KeyTimeProperty; /* XXX initialize */
	public static readonly DependencyProperty ValueProperty; /* XXX initialize */

	protected DoubleKeyFrame ()
	{
	}

	protected DoubleKeyFrame (double value)
	{
	}

	protected DoubleKeyFrame (double value, KeyTime keyTime)
	{
	}

	public KeyTime KeyTime {
		get { return (KeyTime)GetValue (KeyTimeProperty); }
		set { SetValue (KeyTimeProperty, value); }
	}

	public double Value {
		get { return (double)GetValue (ValueProperty); }
		set { SetValue (ValueProperty, value); }
	}

	object IKeyFrame.Value {
		get { return Value; }
		set { Value = (double)value; }
	}

	public double InterpolateValue (double baseValue, double keyFrameProgress)
	{
		throw new NotImplementedException ();
	}

	protected abstract double InterpolateValueCore (double baseValue, double keyFrameProgress);
}


}
