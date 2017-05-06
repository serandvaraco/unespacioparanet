using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;



namespace WindowsIdentityFoundation
{

    public partial class Identityfrm : Form
    {
        public Identityfrm()
        {
            InitializeComponent();
            //ValidateIdentity();
        }
        private void ValidateIdentity()
        {
            IIdentity identity = new GenericIdentity("pepe");
            var profiles = new string[] { "Developer", "ALM", "IT" };

            IPrincipal _profilePrincipal = new GenericPrincipal(identity, profiles);
            System.Threading.Thread.CurrentPrincipal = _profilePrincipal;

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {

                IPrincipal profile = Thread.CurrentPrincipal;

                if (!profile.Identity.IsAuthenticated)
                    throw new Exception("Usuario no autenticado");

                if (!profile.IsInRole("Guest"))
                    throw new Exception("Perfil invalido para esta acción");


                lstResult.Items.Add(txtValue.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        //[PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role ="IT")]
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = "Guest")]

        private void SendAction()
        {

            lstResult.Items.Add(txtValue.Text);

        }

        private void btnSendAction_Click(object sender, EventArgs e)
        {
            try
            {
                SendAction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClaimTypes_Click(object sender, EventArgs e)
        {
            System.Security.Claims.Claim c1 = new Claim("http://blueyonder.cpm/claims/displayname",
                "Display Name is", txtValue.Text);

            Claim c2 = new Claim("age",
                "Age is", "30");

            IIdentity identity = new GenericIdentity("pepe");
            var profiles = new string[] { "Developer", "ALM", "IT" };

            IPrincipal _profilePrincipal = new GenericPrincipal(identity, profiles);
            System.Threading.Thread.CurrentPrincipal = _profilePrincipal;

            ClaimsIdentity identityClaim = new ClaimsIdentity(identity, new List<Claim> { c1, c2 });
            ClaimsPrincipal principalClaim = new ClaimsPrincipal(identityClaim);



            if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
                throw new Exception("No autenticado");

            var _claim = principalClaim.FindFirst("age");

            var message = string.Format("{0}. {1}", _claim.Value, _claim.ValueType);

            lstResult.Items.Add(message);

        }
    }
}
