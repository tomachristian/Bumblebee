﻿using System;
using System.Threading;

using Bumblebee.Extensions;
using Bumblebee.Implementation;
using Bumblebee.IntegrationTests.Shared.Hosting;
using Bumblebee.Setup;
using Bumblebee.Setup.DriverEnvironments;

using FluentAssertions;

using NUnit.Framework;

using OpenQA.Selenium;

namespace Bumblebee.IntegrationTests.Implementation
{
    [TestFixture]
    public class given_session_when_constructing : HostTestFixture
    {
		private readonly ThreadLocal<Session> _session = new ThreadLocal<Session>(() => new Session(new PhantomJS())); 

		[TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
	        Session
				.NavigateTo<TestablePage>(GetUrl("Dialogs.html"));
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
			Session
                .End();
        }

        [Test]
        public void should_load_body_as_tag()
        {
			Session
                .CurrentBlock<TestablePage>()
                .Tag
                .VerifyThat(t => t.TagName.Should().Be("body"));
        }

        [Test]
        public void should_leave_parent_null()
        {
			Session
                .CurrentBlock<TestablePage>()
                .VerifyThat(p => p.Parent.Should().BeNull());
        }

	    [Test]
	    public void should_return_same_session()
	    {
			Session
			    .CurrentBlock<TestablePage>()
				.VerifyThat(p => p.Session.Should().Be(Session));
	    }
		
	    public Session Session
	    {
		    get { return _session.Value; }
	    }
    }

    [TestFixture]
    public class PageTests
    {
        [Test]
        public void given_null_session_when_constructing_page_should_throw()
        {
            Action action = () => new TestablePage(null);
            action.ShouldThrow<ArgumentNullException>()
                .WithMessage("Value cannot be null.\r\nParameter name: session");
        }
    }

    public class TestablePage : Page
    {
        public TestablePage(Session session) : base(session)
        {
        }
    }
}
