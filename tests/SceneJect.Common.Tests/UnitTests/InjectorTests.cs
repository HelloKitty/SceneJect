using Moq;
using NUnit.Framework;
using System;
using System.Collections;
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

		[Test]
		public static void Test_Injector_Sets_Values()
		{
			//arrange
			Mock<IResolver> resolver = new Mock<IResolver>(MockBehavior.Strict);
			TestClass testInstance = new TestClass();

			//set it up to throw on any resolve
			resolver.Setup(x => x.Resolve(It.IsAny<Type>()))
				.Returns(new List<int>() { 1, 2, 3 });

			Injector injector = new Injector(testInstance, resolver.Object);

			//assert
			Assert.DoesNotThrow(() => injector.Inject());
			Assert.NotNull(testInstance); //make sure it didn't null this somehow lol. Can't without ref
			Assert.NotNull(testInstance.test);

			for (int i = 1; i < 4; i++)
			{
				Assert.IsTrue(testInstance.test.Contains(i));
				Assert.IsTrue(testInstance.test2.Contains(i));
			}
				
		}

		public class TestClass
		{
			[Inject]
			public IList<int> test;

			[Inject]
			public IList<int> test2;
		}
	}
}
