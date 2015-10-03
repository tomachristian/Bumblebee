using System;

using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using Bumblebee.Setup;

using OpenQA.Selenium;

namespace Bumblebee.IntegrationTests.Shared.Pages
{
	public class SlowWebPageWithExplicitWait : WebPage
	{
		public SlowWebPageWithExplicitWait(Session session) 
			: base(session, TimeSpan.FromSeconds(10))
		{
		}

		public ITextField FirstName
		{
			get {  return Wait.Until(x => new TextField(this, By.Id("firstName")));}
		}
	}
}