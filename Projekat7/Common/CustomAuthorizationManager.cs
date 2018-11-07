using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Security.Principal;
using System.Threading;
using Common;

namespace Common
{
	public class CustomAuthorizationManager : ServiceAuthorizationManager
	{
		protected override bool CheckAccessCore(OperationContext operationContext)
		{
			bool authorized = false;

			IPrincipal principal = operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] as CustomPrincipal;
			
			string group = string.Format("{0}\\Viewer", Environment.MachineName);


			if (principal.IsInRole("Access"))
			{
				return true;
			}
			

			return authorized;
		}
		
	}
}
