using NUnit.Framework;
using Sceneject.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Autofac.Tests
{
	[TestFixture]
	public static class AutofacRegisterationStratTests
	{
		[Test]
		public static void Test_AutofacRegisterationStrat_Throws_On_Null_Builder()
		{
			//assert
			Assert.Throws<ArgumentNullException>(() => new AutofacRegisterationStrat(null));
		}
	}
}
