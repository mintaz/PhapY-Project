using Abp.Web.Mvc.Views;

namespace PhapY.Web.Views
{
    public abstract class PhapYWebViewPageBase : PhapYWebViewPageBase<dynamic>
    {

    }

    public abstract class PhapYWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected PhapYWebViewPageBase()
        {
            LocalizationSourceName = PhapYConsts.LocalizationSourceName;
        }
    }
}