
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {


public class SplineDecimalKeyFrame : DecimalKeyFrame
{

	public static readonly DependencyProperty KeySplineProperty; // XXX initialize

	Decimal value;
	KeyTime keyTime;

	public SplineDecimalKeyFrame ()
	{
	}

	public SplineDecimalKeyFrame (Decimal value)
	{
		this.value = value;
		// XX keytime?
	}

	public SplineDecimalKeyFrame (Decimal value, KeyTime keyTime)
	{
		this.value = value;
		this.keyTime = keyTime;
	}

	public SplineDecimalKeyFrame (Decimal value, KeyTime keyTime, KeySpline keySpline)
	{
		this.value = value;
		this.keyTime = keyTime;
		KeySpline = keySpline;
	}

	public KeySpline KeySpline {
		get { return (KeySpline)GetValue (KeySplineProperty); }
		set { SetValue (KeySplineProperty, value); }
	}

	protected override Freezable CreateInstanceCore ()
	{
		throw new NotImplementedException ();
	}

	protected override Decimal InterpolateValueCore (Decimal baseValue, double keyFrameProgress)
	{
		double splineProgress = KeySpline.GetSplineProgress (keyFrameProgress);

		return (Decimal)((double)baseValue + ((double)value - (double)baseValue) * splineProgress);
	}
}


}
