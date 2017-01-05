using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Common.Tests
{
	[TestFixture]
	public static class ContextualDependencyResolverDecoratorTests
	{
		[Test]
		public static void Test_Ctor_Doesnt_Throw()
		{
			//assert
			Assert.DoesNotThrow(() => new ContextualDependencyResolverDecorator(Mock.Of<IResolver>(), Mock.Of<IDictionary<Type, object>>()));
		}

		[Test]
		[TestCase(" ")]
		[TestCase("Hello")]
		[TestCase("\t")]
		public static void Test_Resolver_Resolves_Contextual_Instance_String(string sValue)
		{
			//arrange
			ContextualDependencyResolverDecorator resolver = new ContextualDependencyResolverDecorator(Mock.Of<IResolver>(), new Dictionary<Type, object>() { { typeof(string), sValue } });

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
			ContextualDependencyResolverDecorator resolver = new ContextualDependencyResolverDecorator(Mock.Of<IResolver>(), new Dictionary<Type, object>() { { typeof(string), sValue }, { typeof(int), 5 } });

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
			Mock<IResolver> resolverDecorated = new Mock<IResolver>();
			TestService service = new TestService();

			//setup the resolver's return for long type
			//Must set this up first before Moq creates it in the decorator
			resolverDecorated.Setup(r => r.Resolve<ITestService>())
				.Returns(service);

			resolverDecorated.Setup(r => r.Resolve(typeof(ITestService)))
				.Returns(service);

			ContextualDependencyResolverDecorator resolver = new ContextualDependencyResolverDecorator(resolverDecorated.Object, new Dictionary<Type, object>() { { typeof(string), sValue }, { typeof(int), iValue } });

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
