using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Core;

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
			Assert.Throws<ArgumentNullException>(() => new Injector(Mock.Of<IComponentContext>(), null));
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
		public static void Test_Injector_DoesntSetWhenOnlyIfNullIsUsedWhenNotNull_Values()
		{
			//arrange
			ContainerBuilder builder = new ContainerBuilder();
			object testObj = new object();
			builder.RegisterInstance(testObj)
				.As<System.Object>()
				.SingleInstance();

			IComponentContext resolveComponent = builder.Build();

			TestWithOnlyIfNull testInstance = new TestWithOnlyIfNull();

			Injector injector = new Injector(testInstance, new AutoFacToIResolverAdapter(resolveComponent));

			//assert
			Assert.DoesNotThrow(() => injector.Inject());
			Assert.AreSame(testObj, testInstance.TestObj, "Was null first time.");

			//Now we have to make sure with a new container that we can't set it to a different object
			ContainerBuilder builder2 = new ContainerBuilder();
			object testObj2 = new object();
			builder2.RegisterInstance(testObj2)
				.As<System.Object>()
				.SingleInstance();

			IComponentContext resolveComponent2 = builder2.Build();

			Injector injector2 = new Injector(testInstance, new AutoFacToIResolverAdapter(resolveComponent2));

			//assert
			Assert.DoesNotThrow(() => injector2.Inject());
			Assert.AreSame(testObj, testInstance.TestObj);
			Assert.AreNotSame(testObj2, testInstance.TestObj);
		}

		[Injectee]
		public class TestWithOnlyIfNull
		{
			[InjectOnlyIfNull]
			[Inject]
			public object TestObj { get; }

			/// <inheritdoc />
			public TestWithOnlyIfNull(object testObj)
			{
				TestObj = testObj;
			}

			public TestWithOnlyIfNull()
			{
				
			}
		}

		[Test]
		public static void Test_Injector_Sets_Values()
		{
			//arrange
			ContainerBuilder builder = new ContainerBuilder();
			builder.RegisterInstance(new List<int>() { 1, 2, 3 })
				.AsSelf()
				.AsImplementedInterfaces()
				.SingleInstance();

			IComponentContext resolveComponent = builder.Build();

			TestClass testInstance = new TestClass();

			Injector injector = new Injector(testInstance, new AutoFacToIResolverAdapter(resolveComponent));

			//assert
			Assert.DoesNotThrow(() => injector.Inject());
			Assert.NotNull(testInstance); //make sure it didn't null this somehow lol. Can't without ref
			Assert.NotNull(testInstance.test);
			Assert.NotNull(testInstance.test2);
			Assert.NotNull(testInstance.test3);
			Assert.NotNull(testInstance.test4);
			Assert.NotNull(testInstance.test5);

			for (int i = 1; i < 4; i++)
			{
				Assert.IsTrue(testInstance.test.Contains(i));
				Assert.IsTrue(testInstance.test2.Contains(i));
				Assert.IsTrue(testInstance.test3.Contains(i));
				Assert.IsTrue(testInstance.test4.Contains(i));
				Assert.IsTrue(testInstance.test5.Contains(i));
			}		
		}

		[Injectee]
		public class TestClass
		{
			[Inject]
			public IList<int> test;

			[Inject]
			public IList<int> test2;

			[Inject]
			public IList<int> test3 { get; private set; }

			[Inject]
			public IList<int> test4 { get; }

			[Inject]
			public readonly IList<int> test5;
		}
	}
}
