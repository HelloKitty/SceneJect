using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Common.Tests
{
	[TestFixture]
	public static class InjectorTests
	{
		[Test]
		public static void Test_Injector_Ctor_With_Null_Values()
		{
			//assert
			Assert.Throws<ArgumentNullException>(() => new Injector(null, Mock.Of<IResolver>()));
			Assert.Throws<ArgumentNullException>(() => new Injector(Mock.Of<IResolver>(), null));
		}

		[Test]
		public static void Test_Injector_Throws_When_Resolver_Throws()
		{
			//arrange
			Mock<IResolver> resolver = new Mock<IResolver>(MockBehavior.Strict);

			//set it up to throw on any resolve
			resolver.Setup(x => x.Resolve(It.IsAny<Type>()))
				.Throws<Exception>();

			Injector injector = new Injector(new TestClass(), resolver.Object);

			//assert
			Assert.Throws<InvalidOperationException>(() => injector.Inject());
        }

		public class TestClass
		{
			[Inject]
			public IDisposable test;
		}
	}
}
