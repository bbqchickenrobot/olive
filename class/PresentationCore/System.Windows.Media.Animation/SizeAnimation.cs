
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {


public class SizeAnimation : SizeAnimationBase
{
	public static readonly DependencyProperty ByProperty; /* XXX initialize */
	public static readonly DependencyProperty FromProperty; /* XXX initialize */
	public static readonly DependencyProperty ToProperty; /* XXX initialize */

	public SizeAnimation ()
	{
	}

	public SizeAnimation (Size toValue, Duration duration)
	{
	}

	public SizeAnimation (Size toValue, Duration duration, FillBehavior fillBehavior)
	{
	}

	public SizeAnimation (Size fromValue, Size toValue, Duration duration)
	{
	}

	public SizeAnimation (Size fromValue, Size tovalue, Duration duration, FillBehavior fillBehavior)
	{
	}

	public Size? By {
		get { return (Size?) GetValue (ByProperty); }
		set { SetValue (ByProperty, value); }
	}

	public Size? From {
		get { return (Size?) GetValue (FromProperty); }
		set { SetValue (FromProperty, value); }
	}

	public Size? To {
		get { return (Size?) GetValue (ToProperty); }
		set { SetValue (ToProperty, value); }
	}

	public bool IsAdditive {
		get { return (bool) GetValue (AnimationTimeline.IsAdditiveProperty); }
		set { SetValue (AnimationTimeline.IsAdditiveProperty, value); }
	}

	public bool IsCumulative {
		get { return (bool) GetValue (AnimationTimeline.IsCumulativeProperty); }
		set { SetValue (AnimationTimeline.IsCumulativeProperty, value); }
	}

	public new SizeAnimation Clone ()
	{
		throw new NotImplementedException ();
	}

	protected override Freezable CreateInstanceCore ()
	{
		throw new NotImplementedException ();
	}

	protected override Size GetCurrentValueCore (Size defaultOriginValue, Size defaultDestinationValue, AnimationClock animationClock)
	{
		throw new NotImplementedException ();
	}
}


}
