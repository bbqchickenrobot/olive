
/* this file is generated by gen-collection-types.cs.  do not modify */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace System.Windows.Media {


	public class GeneralTransformCollection : Animatable, ICollection<GeneralTransform>, IList<GeneralTransform>, ICollection, IList
	{
		public struct Enumerator : IEnumerator<GeneralTransform>, IEnumerator
		{
			public void Reset()
			{
				throw new NotImplementedException (); 
			}

			public bool MoveNext()
			{
				throw new NotImplementedException (); 
			}

			public GeneralTransform Current {
				get { throw new NotImplementedException (); }
			}

			object IEnumerator.Current {
				get { return Current; }
			}

			void IDisposable.Dispose()
			{
			}
		}

		public GeneralTransformCollection ()
		{
		}

		public GeneralTransformCollection (IEnumerable<GeneralTransform> values)
		{
		}

		public GeneralTransformCollection (int length)
		{
		}

		public GeneralTransformCollection Clone ()
		{
			throw new NotImplementedException ();
		}

		public GeneralTransformCollection CloneCurrentValue ()
		{
			throw new NotImplementedException ();
		}

		protected override Freezable CreateInstanceCore ()
		{
			throw new NotImplementedException ();
		}

		protected override void GetCurrentValueAsFrozenCore (Freezable sourceFreezable)
		{
			throw new NotImplementedException ();
		}

		protected override void CloneCurrentValueCore (Freezable sourceFreezable)
		{
			throw new NotImplementedException ();
		}

		protected override void CloneCore (Freezable sourceFreezable)
		{
			throw new NotImplementedException ();
		}

		public bool Contains (GeneralTransform value)
		{
			throw new NotImplementedException ();
		}

		public bool Remove (GeneralTransform value)
		{
			throw new NotImplementedException ();
		}

		public int IndexOf (GeneralTransform value)
		{
			throw new NotImplementedException ();
		}

		public void Add (GeneralTransform value)
		{
			throw new NotImplementedException ();
		}

		public void Clear ()
		{
			throw new NotImplementedException ();
		}

		public void CopyTo (GeneralTransform[] array, int offset)
		{
			throw new NotImplementedException ();
		}

		public void Insert (int index, GeneralTransform value)
		{
			throw new NotImplementedException ();
		}

		public void RemoveAt (int index)
		{
			throw new NotImplementedException ();
		}

		public int Count {
			get { throw new NotImplementedException (); }
		}

		public GeneralTransform this[int index] {
			get { throw new NotImplementedException (); }
			set { throw new NotImplementedException (); }
		}

		public static GeneralTransformCollection Parse (string str)
		{
			throw new NotImplementedException ();
		}

		bool ICollection<GeneralTransform>.IsReadOnly {
			get { return false; }
		}

		IEnumerator<GeneralTransform> IEnumerable<GeneralTransform>.GetEnumerator()
		{
			throw new NotImplementedException ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			throw new NotImplementedException ();
		}

		bool ICollection.IsSynchronized {
			get { return false; }
		}

		object ICollection.SyncRoot {
			get { return this; }
		}

		void ICollection.CopyTo(Array array, int offset)
		{
			CopyTo ((GeneralTransform[]) array, offset);
		}

		bool IList.IsFixedSize {
			get { return false; }
		}

		bool IList.IsReadOnly {
			get { return false; }
		}

		object IList.this[int index] {
			get { return this[index]; }
			set { this[index] = (GeneralTransform)value; }
		}

		int IList.Add (object value)
		{
			Add ((GeneralTransform)value);
			return Count;
		}

		bool IList.Contains (object value)
		{
			return Contains ((GeneralTransform)value);
		}

		int IList.IndexOf (object value)
		{
			return IndexOf ((GeneralTransform)value);
		}

		void IList.Insert (int index, object value)
		{
			Insert (index, (GeneralTransform)value);
		}

		void IList.Remove (object value)
		{
			Remove ((GeneralTransform)value);
		}


		protected override bool FreezeCore (bool isChecking)
		{{
			throw new NotImplementedException ();
		}}
	}
}