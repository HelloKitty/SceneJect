using Autofac;
using Autofac.Core.Registration;
using NUnit.Framework;
using SceneJect.Common;
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

		[Test]
		public static void Test_AutoFacRegisterationStrat_Can_Register_Type()
		{
			//arrange
			AutofacRegisterationStrat register = new AutofacRegisterationStrat(new ContainerBuilder());

			//act
			register.Register<List<int>>(RegistrationType.Default);
			IContainer container = register.Build();

			//assert
			Assert.NotNull(container.Resolve<List<int>>());
		}

		[Test]
		public static void Test_AutoFacRegisterationStrat_Can_Register_Type_As_Interface()
		{
			//arrange
			AutofacRegisterationStrat register = new AutofacRegisterationStrat(new ContainerBuilder());

			//act
			register.Register<List<int>>(RegistrationType.Default, typeof(IList<int>));
			IContainer container = register.Build();

			//assert
			Assert.NotNull(container.Resolve<IList<int>>());
			Assert.Throws<ComponentNotRegisteredException>(() => container.Resolve<List<int>>());
		}

		[Test]
		public static void Test_AutoFacRegisterationStrat_Can_Register_Type_As_SingleInstance()
		{
			//arrange
			AutofacRegisterationStrat register = new AutofacRegisterationStrat(new ContainerBuilder());

			//act
			register.Register<List<int>>(RegistrationType.SingleInstance, typeof(IList<int>));
			IContainer container = register.Build();

			//assert
			Assert.NotNull(container.Resolve<IList<int>>());

			Assert.AreSame(container.Resolve<IList<int>>(), container.Resolve<IList<int>>());
		}

		[Test]
		public static void Test_AutoFacRegisterationStrat_Can_Register_Type_As_Instance_Per_Dependency()
		{
			//arrange
			AutofacRegisterationStrat register = new AutofacRegisterationStrat(new ContainerBuilder());

			//act
			register.Register<List<int>>(RegistrationType.InstancePerDependency, typeof(IList<int>));
			IContainer container = register.Build();

			//assert
			Assert.NotNull(container.Resolve<IList<int>>());

			Assert.AreNotSame(container.Resolve<IList<int>>(), container.Resolve<IList<int>>());
		}

		[Test]
		public static void Test_AutoFacRegisterationStrat_Can_Register_Type_As_Self()
		{
			//arrange
			AutofacRegisterationStrat register = new AutofacRegisterationStrat(new ContainerBuilder());

			//act
			register.Register<List<int>>(RegistrationType.AsSelf);
			IContainer container = register.Build();

			//assert
			Assert.NotNull(container.Resolve<List<int>>());
		}

		[Test]
		public static void Test_AutoFacRegisterationStrat_Can_Register_Type_As_Implemented_Interface()
		{
			//arrange
			AutofacRegisterationStrat register = new AutofacRegisterationStrat(new ContainerBuilder());

			//act
			register.Register<TestClass>(RegistrationType.AsImplementedInterface);
			IContainer container = register.Build();

			//assert
			Assert.NotNull(container.Resolve<TestInterface>());
			Assert.Throws<ComponentNotRegisteredException>(() => container.Resolve<TestClass>());
		}

		[Test]
		public static void Test_AutoFacRegisterationStrat_Can_Register_Type_As_Implemented_Interface_And_AsSelf()
		{
			//arrange
			AutofacRegisterationStrat register = new AutofacRegisterationStrat(new ContainerBuilder());

			//act
			register.Register<TestClass>(RegistrationType.AsImplementedInterface | RegistrationType.AsSelf);
			IContainer container = register.Build();

			//assert
			Assert.NotNull(container.Resolve<TestInterface>());
			Assert.NotNull(container.Resolve<TestClass>());
		}

		[Test]
		public static void Test_AutoFacRegisterationStrat_isLocked_After_Build()
		{
			//arrange
			AutofacRegisterationStrat register = new AutofacRegisterationStrat(new ContainerBuilder());

			//act
			IContainer container = register.Build();

			//assert
			Assert.IsTrue(register.isLocked);
		}

		[Test]
		public static void Test_AutoFacRegisterationStrat_Throws_When_Locked()
		{
			//arrange
			AutofacRegisterationStrat register = new AutofacRegisterationStrat(new ContainerBuilder());

			//act
			register.Lock();

			//assert
			Assert.Throws<InvalidOperationException>(() => register.Register<IList<int>>(RegistrationType.Default));
		}

		public interface TestInterface
		{

		}

		public class TestClass : TestInterface
		{

		}
	}
}
