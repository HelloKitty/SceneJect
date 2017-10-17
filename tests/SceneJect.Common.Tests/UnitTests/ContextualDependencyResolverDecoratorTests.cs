using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Core;

namespace SceneJect.Common.Tests
{
	[TestFixture]
	public static class ContextualDependencyResolverDecoratorTests
	{
		[Test]
		public static void Test_Ctor_Doesnt_Throw()
		{
			//assert
			Assert.DoesNotThrow(() => new ContextualDependencyResolverDecorator(Mock.Of<IComponentContext>(), Mock.Of<IDictionary<Type, Func<IComponentContext, object>>>()));
		}

		[Test]
		[TestCase(" ")]
		[TestCase("Hello")]
		[TestCase("\t")]
		public static void Test_Resolver_Resolves_Contextual_Instance_String(string sValue)
		{
			//arrange
			ContextualDependencyResolverDecorator resolver = new ContextualDependencyResolverDecorator(Mock.Of<IComponentContext>(), new Dictionary<Type, Func<IComponentContext, object>>() { { typeof(string), c => sValue } });

			//act
			string value = resolver.Resolve<string>();

			//assert
			Assert.NotNull(value);
			Assert.IsNotEmpty(value);

			Assert.AreEqual(sValue, value);
		}

		[Test]
		[TestCase(" ")]
		[TestCase("Hello")]
		[TestCase("\t")]
		public static void Test_Resolver_Resolves_Contextual_Instance_String_With_Multiple_Services(string sValue)
		{
			//arrange
			ContextualDependencyResolverDecorator resolver = new ContextualDependencyResolverDecorator(Mock.Of<IComponentContext>(), new Dictionary<Type, Func<IComponentContext, object>>() { { typeof(string), c => sValue }, { typeof(int), c => 5 } });

			//act
			string value = resolver.Resolve<string>();

			//assert
			Assert.NotNull(value);
			Assert.IsNotEmpty(value);

			Assert.AreEqual(sValue, value);
		}

		[Test]
		[TestCase(" ", 5)]
		[TestCase("Hello", 99)]
		[TestCase("\t", 56)]
		public static void Test_Resolver_Resolves_Contextual_TestService_From_Resolver_With_Contextual_Dictionary_Initialized(string sValue, int iValue)
		{
			//arrange
			ContainerBuilder builder = new ContainerBuilder();;
			TestService service = new TestService();
			builder.RegisterInstance(service)
				.As<ITestService>()
				.SingleInstance();

			IComponentContext resolveComponent = builder.Build();

			ContextualDependencyResolverDecorator resolver = new ContextualDependencyResolverDecorator(resolveComponent, new Dictionary<Type, Func<IComponentContext, object>>()
			{
				{ typeof(string), Service<string>.As(c => sValue).ServiceInstance },
				{ typeof(object), Service<object>.As(c => iValue).ServiceInstance }
			});

			//act
			string stringValue = resolver.Resolve<string>();
			ITestService resolvedService = resolver.Resolve<ITestService>();

			//assert
			Assert.NotNull(stringValue);
			Assert.IsNotEmpty(stringValue);

			//now check resolving something not in the dictionary
			Assert.NotNull(resolvedService);
			Assert.AreEqual(service, resolvedService);
		}

		public interface ITestService
		{

		}

		public class TestService : ITestService
		{

		}

	}
}
