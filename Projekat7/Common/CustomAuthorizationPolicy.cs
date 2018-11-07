using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using System.Security.Principal;
using Common;

namespace Common
{
	public class CustomAuthorizationPolicy : IAuthorizationPolicy
	{
		private string id;
		private object locker = new object();
        public static bool ConfigurationUpdate = false;

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

        private static CustomPrincipal customPrincipalInstance;
        private static WindowsIdentity windowsIdentity;

        public static CustomPrincipal CustomPrincipalInstance
        {
            get
            {
                if (customPrincipalInstance == null || ConfigurationUpdate)
                {
                    customPrincipalInstance = new CustomPrincipal(windowsIdentity);
                    ConfigurationUpdate = false;
                }
                return customPrincipalInstance;
            }
        }

        protected virtual IPrincipal GetPrincipal(IIdentity identity)
		{
			lock (locker)
			{
				IPrincipal principal = null;
                windowsIdentity = identity as WindowsIdentity;

				if (windowsIdentity != null)
				{
                    if (InMemoryCash.PrincipalDict.ContainsKey(windowsIdentity.User))
                        principal = InMemoryCash.PrincipalDict[windowsIdentity.User];
                    else
                        principal = new CustomPrincipal(windowsIdentity);
                    //Audit.AuthenticationSuccess(windowsIdentity.Name);


                }

				return principal;
			}
		}

		
	}
}
