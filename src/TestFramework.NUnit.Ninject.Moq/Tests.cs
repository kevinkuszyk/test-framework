﻿using System.Collections.Generic;
using Moq;
using Ninject;
using Ninject.MockingKernel.Moq;
using NUnit.Framework;
using TestFramework.NUnit.Ninject.Moq.Syntax;

namespace TestFramework.NUnit.Ninject.Moq
{
    /// <summary>
    /// A base type for units tests.  Provides an auto mocking container. 
    /// </summary>
    /// <typeparam name="T">The class to test.</typeparam>
    public abstract class Tests<T> where T : class
    {
        private MoqMockingKernel kernel;

        /// <summary>
        /// Sets up a fresh auto mocking container before each test is run.
        /// </summary>
        [SetUp]
        public void SetUpKernel()
        {
            var settings = new NinjectSettings
            {
                AllowNullInjection = true
            };

            kernel = new MoqMockingKernel(settings);

            kernel.Settings.SetMockBehavior(MockBehavior.Strict);
        }

        /// <summary>
        /// Gets the service / class to test.
        /// </summary>
        /// <returns></returns>
        protected T GetService()
        {
            return kernel.Get<T>();
        }

        /// <summary>
        /// Sets the specified type to a specific instance in the auto mocking container.
        /// </summary>
        /// <typeparam name="TMock">The type to set.</typeparam>
        /// <returns></returns>
        protected SetMockBuilder<TMock> SetMock<TMock>()
        {
            return new SetMockBuilder<TMock>(kernel);
        }

        /// <summary>
        /// Gets the mock generated by the auto mocking container. 
        /// </summary>
        /// <typeparam name="TMock">The type to get.</typeparam>
        /// <returns></returns>
        protected Mock<TMock> GetMock<TMock>() where TMock : class
        {
            return kernel.GetMock<TMock>();
        }

        /// <summary>
        /// Resets the auto mocking container.
        /// </summary>
        protected void ResetAutoMocker()
        {
            kernel.Reset();
        }

        /// <summary>
        /// Gets all mocks of the specified type registered in the auto mocking container.
        /// </summary>
        /// <typeparam name="TMock">The type to get.</typeparam>
        /// <returns></returns>
        protected IEnumerable<TMock> GetAllMocks<TMock>()
        {
            return kernel.GetAll<TMock>();
        }

        /// <summary>
        /// Adds an instance of the specified type to the auto mocking container.
        /// </summary>
        /// <typeparam name="TMock">The type of the instance to add.</typeparam>
        /// <param name="instance">The instance to add.</param>
        protected void AddInstance<TMock>(TMock instance)
        {
            kernel.Bind<TMock>().ToMethod(x => instance);
        }
    }
}
