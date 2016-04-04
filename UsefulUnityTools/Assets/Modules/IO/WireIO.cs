using UnityEngine;
using System.Collections.Generic;

namespace WireIO
{
	/// <summary>
	/// The base class used by all inputs and outputs.
	/// </summary>
	public class BaseIO
	{
		public BaseIO( string name, object value )
		{
			mName = name;
			mDefaultValue = value;
			mValue = value;
		}

		// The name of the io object.
		public string mName;

		// These are generally for performance, the defaults work rather well.
		// Store the default value.
		public bool mStoreDefault = false;

		// Store the last value. disable to save memory.
		public bool mStoreLastValue = true;

		/// <summary>
		/// Gets or sets the last value.
		/// Generally this should be avoided and only used internally.
		/// </summary>
		/// <value>The mlast value.</value>
		public object LastValue;
		public object mLastValue
		{
			get
			{
				// Return the value.
				return LastValue;
			}
			set
			{
				// Store the specified value.
				LastValue = value;
			}
		}

		private object Value;
		protected object mValue
		{
			get
			{
				return Value;
			}
			set
			{
				// If we can store the last value update it before setting the new value.
				if(mStoreLastValue)
				{
					mLastValue = Value;
				}

				// Since this could potentially be the first time we've set the value, we must set the default.
				if(mStoreDefault)
				{
					mDefaultValue = Value;
				}

				// Update the mValue.
				Value = value;
			}
		}

		/// <summary>
		/// Gets or sets the m default value.
		/// </summary>
		/// <value>The m default value.</value>
		private object DefaultValue;
		protected object mDefaultValue
		{
			get
			{
				return DefaultValue;
			}
			set
			{
				// Store the specified value.
				DefaultValue = value;
			}
		}

		/// <summary>
		/// Reset this instance to default value.
		/// </summary>
		public void Reset()
		{
			// Check this has been enabled.
			if( mStoreDefault )
			{
				// Assign the default value.
				mValue = mDefaultValue;
			}
		}
	}
}
