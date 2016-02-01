using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect.Common.Tests
{
	[TestFixture]
	public class InjecteeLocatorTests
	{
		//we can't test much else because it requires MonoBehaviours to be created
		[Test]
		public void Test_InjecteeLocator_Throws_Null_With_Null_Collection()
		{
			//assert
			Assert.Throws<ArgumentNullException>(() => new InjecteeLocator<MonoBehaviour>(null));
		}
	}
}
