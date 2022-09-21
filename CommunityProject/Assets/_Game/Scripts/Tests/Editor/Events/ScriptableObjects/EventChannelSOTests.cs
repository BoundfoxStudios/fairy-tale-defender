using System.Text.RegularExpressions;
using BoundfoxStudios.CommunityProject.Events.ScriptableObjects;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BoundfoxStudios.CommunityProject.Tests.Editor.Events.ScriptableObjects
{
	public class EventChannelSOTests
	{
		private VoidEventChannelSO _voidEventChannel;

		[SetUp]
		public void SetUp()
		{
			_voidEventChannel = ScriptableObject.CreateInstance<VoidEventChannelSO>();
			_voidEventChannel.name = "Void Event Channel Test";
		}

		[TearDown]
		public void TearDown()
		{
			_voidEventChannel = null;
		}

		[Test]
		public void RaiseDoesRaiseTheEvent()
		{
			LogAssert.Expect(LogType.Log, new Regex(@"Event.*?Void Event Channel Test raised"));

			var raised = false;

			_voidEventChannel.Raised += () => raised = true;

			_voidEventChannel.Raise();

			raised.Should().BeTrue();
		}

		private class VoidEventChannelSO : EventChannelSO { }
	}

	// ReSharper disable once InconsistentNaming
	public class EventChannelTSOTests
	{
		private ObjectEventChannelSO _objectEventChannel;

		[SetUp]
		public void SetUp()
		{
			_objectEventChannel = ScriptableObject.CreateInstance<ObjectEventChannelSO>();
			_objectEventChannel.name = "Object Event Channel Test";
		}

		[TearDown]
		public void TearDown()
		{
			_objectEventChannel = null;
		}

		[Test]
		public void RaiseDoesRaiseTheEvent()
		{
			LogAssert.Expect(LogType.Log, new Regex(@"Event.*?Object Event Channel Test raised.*?value.*?Foo=Unit Test, Bar=5"));

			var raised = false;

			_objectEventChannel.Raised += _ => raised = true;

			_objectEventChannel.Raise(new()
			{
				Bar = 5,
				Foo = "Unit Test"
			});

			raised.Should().BeTrue();
		}

		private class ObjectEventChannelSO : EventChannelSO<ObjectEventChannelSO.EventArgs>
		{
			public class EventArgs
			{
				public int Bar;
				public string Foo;

				public override string ToString()
				{
					return $"{nameof(Foo)}={Foo}, {nameof(Bar)}={Bar.ToString()}";
				}
			}
		}
	}
}
