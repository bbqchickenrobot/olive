
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {


public class SplinePointKeyFrame : PointKeyFrame
{

	public static readonly DependencyProperty KeySplineProperty; // XXX initialize

	Point value;
	KeyTime keyTime;

	public SplinePointKeyFrame ()
	{
	}

	public SplinePointKeyFrame (Point value)
	{
		this.value = value;
		// XX keytime?
	}

	public SplinePointKeyFrame (Point value, KeyTime keyTime)
	{
		this.value = value;
		this.keyTime = keyTime;
	}

	public SplinePointKeyFrame (Point value, KeyTime keyTime, KeySpline keySpline)
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

	protected override Point InterpolateValueCore (Point baseValue, double keyFrameProgress)
	{
		double splineProgress = KeySpline.GetSplineProgress (keyFrameProgress);

		return new Point (baseValue.X + (value.X - baseValue.X) * splineProgress, baseValue.Y + (value.Y - baseValue.Y) * splineProgress);;
	}
}


}
