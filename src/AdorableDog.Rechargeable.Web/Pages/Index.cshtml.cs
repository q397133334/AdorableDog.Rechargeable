namespace AdorableDog.Rechargeable.Pages
{
    public class IndexModel : RechargeablePageModelBase
    {
        public void OnGet()
        {
            if (CurrentUser.IsAuthenticated)
                Response.Redirect("/machines/index");
        }
    }
}