﻿using System.Net;
using FluentAssertions;
using Protacon.NetCore.WebApi.TestUtil.Tests.Dummy;
using Xunit;

namespace Protacon.NetCore.WebApi.TestUtil.Tests.Tests
{
    public class BasicFlowTests
    {
        [Fact]
        public void WhenGetIsCalled_ThenAssertingItWorks()
        {
            TestHost.Run<TestStartup>().Get("/returnthree/")
                .ExpectStatusCode(HttpStatusCode.OK)
                .WithContentOf<int>()
                .Passing(
                    x => x.Should().Be(3));

            TestHost.Run<TestStartup>().Get("/returnthree/")
                .Invoking(x => x.ExpectStatusCode(HttpStatusCode.NoContent))
                .ShouldThrow<ExpectedStatusCodeException>();
        }

        [Fact]
        public void WhenDeleteIsCalled_ThenAssertingItWorks()
        {
            TestHost.Run<TestStartup>().Delete("/something/abc")
                .ExpectStatusCode(HttpStatusCode.NoContent);

            TestHost.Run<TestStartup>().Delete("/something/abc")
                .Invoking(x => x.ExpectStatusCode(HttpStatusCode.NotFound))
                .ShouldThrow<ExpectedStatusCodeException>();
        }

        [Fact]
        public void WhenPutIsCalled_ThenAssertingItWorks()
        {
            TestHost.Run<TestStartup>().Put("/returnsame/", new DummyRequest { Value = "3" })
                .ExpectStatusCode(HttpStatusCode.OK)
                .WithContentOf<DummyRequest>()
                .Passing(x => x.Value.Should().Be("3"));

            TestHost.Run<TestStartup>().Put("/returnsame/", new { value = 3 })
                .Invoking(x => x.ExpectStatusCode(HttpStatusCode.NotFound))
                .ShouldThrow<ExpectedStatusCodeException>();
        }

        [Fact]
        public void WhenPostIsCalled_ThenAssertingItWorks()
        {
            TestHost.Run<TestStartup>().Post("/returnsame/", new DummyRequest { Value = "3" })
                .ExpectStatusCode(HttpStatusCode.OK)
                .WithContentOf<DummyRequest>()
                .Passing(x => x.Value.Should().Be("3"));

            TestHost.Run<TestStartup>().Post("/returnsame/", new { value = 3 })
                .Invoking(x => x.ExpectStatusCode(HttpStatusCode.NotFound))
                .ShouldThrow<ExpectedStatusCodeException>();
        }
    }
}