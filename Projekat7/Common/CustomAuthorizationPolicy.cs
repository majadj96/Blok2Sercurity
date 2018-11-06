﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using System.Security.Principal;
using Common;

namespace SecurityManager
{
	public class CustomAuthorizationPolicy : IAuthorizationPolicy
	{
		private string id;
		private object locker = new object();

		public CustomAuthorizationPolicy()
		{
			this.id = Guid.NewGuid().ToString();
		}

		public string Id
		{
			get { return this.id; }
		}

		public ClaimSet Issuer
		{
			get { return ClaimSet.System; }
		}

		public bool Evaluate(EvaluationContext evaluationContext, ref object state)
		{
			object list;

			if (!evaluationContext.Properties.TryGetValue("Identities", out list))
			{
				return false;
			}

			IList<IIdentity> identities = list as IList<IIdentity>;
			if (list == null || identities.Count <= 0)
			{
				return false;
			}

			evaluationContext.Properties["Principal"] = GetPrincipal(identities[0]);
			return true;
		}

		protected virtual IPrincipal GetPrincipal(IIdentity identity)
		{
			lock (locker)
			{
				IPrincipal principal = null;
				WindowsIdentity windowsIdentity = identity as WindowsIdentity;

				if (windowsIdentity != null)
				{
					//Audit.AuthenticationSuccess(windowsIdentity.Name);
					principal = new CustomPrincipal(windowsIdentity);	
				}

				return principal;
			}
		}

		
	}
}
