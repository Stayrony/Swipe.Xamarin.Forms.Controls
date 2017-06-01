using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Swipe.Xamarin.Forms.Controls.Droid.Units
{
	public class ReflectedProxy<T> where T : class
	{
		private object _target;
		private readonly Dictionary<string, PropertyInfo> _cachedPropertyInfo;
		private readonly Dictionary<string, MethodInfo> _cachedMethodInfo;
		private readonly IEnumerable<PropertyInfo> _targetPropertyInfoList;
		private readonly IEnumerable<MethodInfo> _targetMethodInfoList;

		public ReflectedProxy(T target)
		{
			_target = target;

			_cachedPropertyInfo = new Dictionary<string, PropertyInfo>();
			_cachedMethodInfo = new Dictionary<string, MethodInfo>();

			TypeInfo typeInfo = typeof(T).GetTypeInfo();
			_targetPropertyInfoList = typeInfo.GetRuntimeProperties();
			_targetMethodInfoList = typeInfo.GetRuntimeMethods();
		}

		public void SetPropertyValue(object value, [CallerMemberName] string propertyName = "")
		{
			var propInfo = GetPropertyInfo(propertyName);
			propInfo?.SetValue(_target, value);
		}

		public TPropertyValue GetPropertyValue<TPropertyValue>([CallerMemberName] string propertyName = "")
		{
			var propInfo = GetPropertyInfo(propertyName);
			if (propInfo == null)
				return default(TPropertyValue);
			else
				return (TPropertyValue)propInfo.GetValue(_target);
		}

		public object Call([CallerMemberName] string methodName = "", object[] parameters = null)
		{
			if (!_cachedMethodInfo.ContainsKey(methodName))
			{
				if (_targetMethodInfoList.FirstOrDefault(mi => mi.Name == methodName) == null)
					return null;
				_cachedMethodInfo[methodName] = _targetMethodInfoList.Single(mi => mi.Name == methodName);
			}

			return _cachedMethodInfo[methodName].Invoke(_target, parameters);
		}

		#region -- Private helpers --

		private PropertyInfo GetPropertyInfo(string propertyName)
		{
			if (!_cachedPropertyInfo.ContainsKey(propertyName))
			{
				if (_targetPropertyInfoList.FirstOrDefault(pi => pi.Name == propertyName) == null)
					return null;
				_cachedPropertyInfo[propertyName] = _targetPropertyInfoList.Single(pi => pi.Name == propertyName);
			}

			return _cachedPropertyInfo[propertyName];
		}

		#endregion
	}
}
