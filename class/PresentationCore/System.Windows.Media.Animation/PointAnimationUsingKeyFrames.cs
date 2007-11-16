
/* this file is generated by gen-animation-types.cs.  do not modify */

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;

namespace System.Windows.Media.Animation {


[ContentProperty ("KeyFrames")]
public class PointAnimationUsingKeyFrames : PointAnimationBase, IKeyFrameAnimation, IAddChild
{
	public PointAnimationUsingKeyFrames ()
	{
	}

	public PointKeyFrameCollection KeyFrames {
		get { throw new NotImplementedException (); }
		set { throw new NotImplementedException (); }
	}

	IList IKeyFrameAnimation.KeyFrames {
		get { return (IList)KeyFrames; }
		set { KeyFrames = (PointKeyFrameCollection)value; }
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void AddChild (object child)
	{
		throw new NotImplementedException ();
	}

	void IAddChild.AddChild (object child)
	{
		AddChild (child);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void AddText (string childText)
	{
		throw new NotImplementedException ();
	}

	void IAddChild.AddText (string childText)
	{
		AddText (childText);
	}

	public new PointAnimationUsingKeyFrames Clone ()
	{
		throw new NotImplementedException ();
	}

	protected override void CloneCore (Freezable sourceFreezable)
	{
		throw new NotImplementedException ();
	}

	public new PointAnimationUsingKeyFrames CloneCurrentValue ()
	{
		throw new NotImplementedException ();
	}

	protected override void CloneCurrentValueCore (Freezable sourceFreezable)
	{
		throw new NotImplementedException ();
	}

	protected override Freezable CreateInstanceCore ()
	{
		throw new NotImplementedException ();
	}

	protected override bool FreezeCore (bool isChecking)
	{
		throw new NotImplementedException ();
	}

	protected override void GetAsFrozenCore (Freezable source)
	{
		throw new NotImplementedException ();
	}

	protected override void GetCurrentValueAsFrozenCore (Freezable source)
	{
		throw new NotImplementedException ();
	}

	protected override sealed Point GetCurrentValueCore (Point defaultOriginValue, Point defaultDestinationValue, AnimationClock animationClock)
	{
		throw new NotImplementedException ();
	}

	protected override sealed Duration GetNaturalDurationCore (Clock clock)
	{
		throw new NotImplementedException ();
	}

	protected override void OnChanged ()
	{
		throw new NotImplementedException ();
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool ShouldSerializeKeyFrames()
	{
		throw new NotImplementedException ();
	}



	public bool IsAdditive {
		get { return (bool) GetValue (AnimationTimeline.IsAdditiveProperty); }
		set { SetValue (AnimationTimeline.IsAdditiveProperty, value); }
	}

	public bool IsCumulative {
		get { return (bool) GetValue (AnimationTimeline.IsCumulativeProperty); }
		set { SetValue (AnimationTimeline.IsCumulativeProperty, value); }
	}


}


}
